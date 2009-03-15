using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OAuth;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections; 

namespace Yammer
{
    public static class HttpUtility
    {
        /// <summary>
        /// Creates http get web request and returns response
        /// </summary>
        /// <param name="url">The URL for the web request</param>
        /// <param name="session">The Yammer<see cref="Session">session</see> object</param>
        /// <returns>http response</returns>
        public static string Get(string url, Session session)
        {
            string nonce, timestamp;
            string signature = GetSignature(WebMethod.GET, session, url, out timestamp, out nonce);
            HttpWebRequest request = CreateWebRequest(url, WebMethod.GET, nonce, timestamp, signature, session);
            return GetWebResponse(request);
        }

        /// <summary>
        /// Creates http post web request and returns response
        /// </summary>
        /// <param name="url">The URL for the web request</param>
        /// <param name="parameters">the query string parameters</param>
        /// <param name="session">The Yammer<see cref="Session">session</see> object</param>
        /// <returns>http response</returns>
        public static string Post(string url, NameValueCollection parameters, Session session)
        {
            string nonce, timestamp;
            string fullUrl = EncodeUrl(url, parameters);
            string signature = GetSignature(WebMethod.POST, session, fullUrl, out timestamp, out nonce);
            HttpWebRequest request = CreateWebRequest(url, WebMethod.POST, nonce, timestamp, signature, session);
            WritePostData(parameters, request);
            return GetWebResponse(request);
        }

        /// <summary>
        /// Creates http multipart post web request and returns response
        /// </summary>
        /// <param name="url">The URL for the web request</param>
        /// <param name="parameters">the query string parameters</param>
        /// <param name="session">The Yammer<see cref="Session">session</see> object</param>
        /// <param name="files">The file path collection to upload</param>
        /// <remarks>Used to upload attachments</remarks>
        /// <returns>http response</returns>
        public static string Upload(string url, NameValueCollection parameters, Session session, List<string> files)
        {
                     
            //MultipartForm form;
            //form = new MultipartForm(url);
            //form.FileContentType = "image/gif";
            //form.setField("body", parameters["body"]);
            //if (files.Count == 1)
            //    form.sendFile(files[0], session);
            //else
            //    if (files.Count > 1)
            //        form.sendFiles(files, session);
            //return "";

            return UploadAttachments(url, parameters, files, session);
        }

        /// <summary>
        /// Creates http delete web request and returns response
        /// </summary>
        /// <param name="url">The URL for the web request</param>
        /// <param name="session">The Yammer<see cref="Session">session</see> object</param>
        /// <returns>http response</returns>
        public static string Delete(string url, Session session)
        {
            string nonce, timestamp;
            string signature = GetSignature(WebMethod.DELETE, session, url, out timestamp, out nonce);
            HttpWebRequest request = CreateWebRequest(url, WebMethod.DELETE, nonce, timestamp, signature, session);
            return GetWebResponse(request);

        }

        /// <summary>
        /// Creates the OAuth header for the http request
        /// </summary>
        /// <param name="method">The <see cref="WebMethod"/> http web method</param>
        /// <param name="nonce">The OAuth nonce string</param>
        /// <param name="timeStamp">The generated OAuth timestamp</param>
        /// <param name="sig">The generatred OAuth signature</param>
        /// <param name="session">The Yammer<see cref="Session">session</see> object</param>
        /// <returns></returns>
        private static string CreateAuthHeader(WebMethod method, string nonce, string timeStamp, string sig, Session session)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("OAuth ");
            if (method == WebMethod.POST)
                sb.Append("realm=\"" + Resources.YAMMER_MESSAGES_CREATE + "\",");

            string authHeader = "oauth_consumer_key=\"" + session.AuthKey.ConsumerKey + "\"," +
                                "oauth_token=\"" + session.AuthKey.TokenKey + "\"," +
                                "oauth_nonce=\"" + nonce + "\"," +
                                "oauth_timestamp=\"" + timeStamp + "\"," +
                                "oauth_signature_method=\"" + "HMAC-SHA1" + "\"," +
                                "oauth_version=\"" + "1.0" + "\"," +
                                "oauth_signature=\"" + sig + "\"";

            sb.Append(authHeader);
            return sb.ToString();
        }

        /// <summary>
        /// Creates intstance of the HttpRequest object
        /// </summary>
        /// <param name="method">The <see cref="WebMethod"/> http web method</param>
        /// <param name="proxy"> The client <see cref="WebProxy"/> proxy</param>
        /// <param name="requestUrl">The URL for the web request</param>
        /// <param name="preAuth">Send authentication header with request</param>
        /// <returns>HttpRequest object</returns>
        public static HttpWebRequest CreateWebRequest(WebMethod method, WebProxy proxy, string requestUrl, bool preAuth)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(requestUrl);
            request.Method = method.ToString();
            request.PreAuthenticate = preAuth;
            request.Proxy = proxy;

            return request;
        }

        /// <summary>
        /// Creates intstance of the HttpRequest object
        /// </summary>
        /// <param name="fullUrl">The full URL with querystring for the web request</param>
        /// <param name="method">The <see cref="WebMethod"/> http web method</param>
        /// <param name="nonce">The OAuth nonce string</param>
        /// <param name="timeStamp">The generated OAuth timestamp</param>
        /// <param name="sig">The generatred OAuth signature</param>
        /// <param name="session">The Yammer<see cref="Session">session</see> object</param>
        /// <returns>HttpRequest object</returns>
        private static HttpWebRequest CreateWebRequest(string fullUrl, WebMethod method, string nonce, string timeStamp, string sig, Session session)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(fullUrl);
            request.Method = method.ToString();
            request.Proxy = session.Proxy;
            string authHeader = CreateAuthHeader(method, nonce, timeStamp, sig, session);
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers.Add("Authorization", authHeader);

            return request;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullUrl">The full URL with querystring for the web request</param>
        /// <param name="method">The <see cref="WebMethod"/> http web method</param>
        /// <param name="nonce">The OAuth nonce string</param>
        /// <param name="timeStamp">The generated OAuth timestamp</param>
        /// <param name="sig">The generatred OAuth signature</param>
        /// <param name="boundary">The boundary for the http multipart request</param>
        /// <param name="session">The Yammer<see cref="Session">session</see> object</param>
        /// <returns>HttpRequest object</returns>
        public static HttpWebRequest CreateWebRequest(string fullUrl, WebMethod method, string nonce, string timeStamp, string sig, string boundary, Session session)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(fullUrl);
            request.Method = method.ToString();
            request.Proxy = session.Proxy;
            string authHeader = CreateAuthHeader(method, nonce, timeStamp, sig, session);
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.Headers.Add("Authorization", authHeader);
            return request;
        }

        /// <summary>
        /// Encodes URL to RFC 3986
        /// </summary>
        /// <param name="url">The url to encode</param>
        /// <param name="parameters">The query string parameters</param>
        /// <returns></returns>
        private static string EncodeUrl(string url, NameValueCollection parameters)
        {
            string fullUrl = string.Empty;
            int count = 0;
            foreach (string key in parameters.Keys)
            {
                if (count == 0)
                    fullUrl = url + "?" + key + "=" + Rfc3986.Encode(parameters[key]);
                else
                    fullUrl += "&" + key + "=" + Rfc3986.Encode(parameters[key]);
                count++;
            }
            return fullUrl;
        }

        private static string GenerateRandomString(int intLenghtOfString)
        {
            StringBuilder randomString = new StringBuilder();
            Random randomNumber = new Random();
            Char appendedChar;
            for (int i = 0; i <= intLenghtOfString; ++i)
            {
                appendedChar = Convert.ToChar(Convert.ToInt32(26 * randomNumber.NextDouble()) + 65);
                randomString.Append(appendedChar);
            }
            return randomString.ToString();
        }

        /// <summary>
        /// Generates OAuth signature for http request
        /// </summary>
        /// <param name="method">The <see cref="WebMethod"/> http web method</param>
        /// <param name="session">he Yammer<see cref="Session">session</see> object</param>
        /// <param name="url">The URL for the web request</param>
        /// <param name="nonce">The OAuth nonce string</param>
        /// <param name="timeStamp">The generated OAuth timestamp</param>
        /// <returns>OAuth signature</returns>
        public static string GetSignature(WebMethod method, Session session, string url, out string timestamp, out string nonce)
        {
            OAuthBase oAuth = new OAuthBase();
            nonce = oAuth.GenerateNonce();
            timestamp = oAuth.GenerateTimeStamp();
            string nurl, nrp;

            Uri uri = new Uri(url);
            string sig = oAuth.GenerateSignature(
                uri,
                session.AuthKey.ConsumerKey,
                session.AuthKey.ConsumerSecret,
                session.AuthKey.TokenKey,
                session.AuthKey.TokenSecret,
                method.ToString(),
                timestamp,
                nonce,
                OAuthBase.SignatureTypes.HMACSHA1, out nurl, out nrp);

            return System.Web.HttpUtility.UrlEncode(sig);
        }

        /// <summary>
        /// Reads web response
        /// </summary>
        /// <param name="request"></param>
        /// <returns>http web response</returns>
        public static string GetWebResponse(HttpWebRequest request)
        {
            WebResponse response = null;
            string data = string.Empty;
            try
            {
                response = request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    data = reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error retrieving web response " + ex.Message);
                throw ex;
            }
            finally
            {
                if (response != null)
                    response.Close();
            }

            return data;


        }

        /// <summary>
        /// Writes post data to request stream
        /// </summary>
        /// <param name="parameters">The query string parameters</param>
        /// <param name="request">The http request to write to</param>
        private static void WritePostData(NameValueCollection parameters, HttpWebRequest request)
        {
            int count = 0;
            string queryString = string.Empty;
            foreach (string key in parameters.Keys)
            {
                if (count == 0)
                    queryString = key + "=" + Rfc3986.Encode(parameters[key]);
                else
                    queryString += "&" + key + "=" + Rfc3986.Encode(parameters[key]);
                count++;
            }

            byte[] postDataBytes = Encoding.ASCII.GetBytes(queryString);
            request.ContentLength = postDataBytes.Length;
            Stream reqStream = request.GetRequestStream();
            reqStream.Write(postDataBytes, 0, postDataBytes.Length);
            reqStream.Close();
        }
        /// <summary>
        /// Writes post data to request stream
        /// </summary>
        /// <param name="parameters">The query string parameters</param>
        /// <param name="request">The http request to write to</param>
        private static void WritePostData(string postData, Stream requestStream, bool close)
        {
            byte[] postDataBytes = System.Text.Encoding.ASCII.GetBytes(postData);
            requestStream.Write(postDataBytes, 0, postDataBytes.Length);
            if (close)
                requestStream.Close();
        }

        private static string UploadAttachments(string url, NameValueCollection parameters, List<string> fileNames, Session session)
        {
            string nonce, timestamp;
            string beginBoundary = GenerateRandomString(25);
            string contentBoundary = "--" + beginBoundary;
            string endBoundary = contentBoundary + "--";
            string contentTrailer = "\r\n" + endBoundary;
         
            string signature = HttpUtility.GetSignature(WebMethod.POST, session, url, out timestamp, out nonce);
            HttpWebRequest request = HttpUtility.CreateWebRequest(url, WebMethod.POST, nonce, timestamp, signature, beginBoundary, session);
            Version protocolVersion = HttpVersion.Version11;
            string method = WebMethod.POST.ToString();
            string contentType = "multipart/form-data; boundary=" + beginBoundary;
            string contentDisposition = "Content-Disposition: form-data; name=";
            request.Headers.Add("Cache-Control", "no-cache");
            request.KeepAlive = true;
            string postParams = GetPostParameters(parameters, contentBoundary, contentDisposition);

            FileInfo[] fi = new FileInfo[fileNames.Count];
            int i = 0;
            long postDataSize = 0;
            int headerLength = 0;
            List<string> fileHeaders = new List<string>();
            AddFileHeaders(fileNames, contentBoundary, contentDisposition, fi, ref i, ref postDataSize, ref headerLength, fileHeaders);
            request.ContentLength = postParams.Length + headerLength + contentTrailer.Length + postDataSize;
            System.IO.Stream io =  request.GetRequestStream();
            WritePostData(postParams, io, false);
            i = 0;
            foreach (string fileName in fileNames)
            {
                WritePostData(fileHeaders[i], io, false);
                WriteFile(io, fileName);
                i++;
            }
            WritePostData(contentTrailer,io, true);

            string response = GetWebResponse(request);
            io.Close();
            request = null;

            return response;
        }

        private static bool IsValidImage(Stream imageStream)
        {
            if (imageStream.Length > 0)
            {
                byte[] header = new byte[4]; // Change size if needed.
                string[] imageHeaders = new[]{
                "\xFF\xD8", // JPEG
                "BM",       // BMP
                "GIF",      // GIF
                Encoding.ASCII.GetString(new byte[]{137, 80, 78, 71})}; // PNG

                imageStream.Read(header, 0, header.Length);

                bool isImageHeader = imageHeaders.Count(str => Encoding.ASCII.GetString(header).StartsWith(str)) > 0;
                if (isImageHeader == true)
                {
                    try
                    {
                        System.Drawing.Image.FromStream(imageStream).Dispose();
                        imageStream.Close();
                        return true;
                    }

                    catch
                    {

                    }
                }
            }

            imageStream.Close();
            return false;
        }
        private static void AddFileHeaders(List<string> fileNames, string contentBoundary, string contentDisposition, FileInfo[] fi, ref int i, ref long postDataSize, ref int headerLength, List<string> fileHeaders)
        {
            foreach (string s in fileNames)
            {
                bool isImage = IsValidImage(System.IO.File.OpenRead(s));
                string contentType = isImage ? "image/gif" : "text/xml";
                string header = contentBoundary + "\r\n" + contentDisposition + "\"attachment" + (i + 1).ToString() +
                                    "\"; filename=\"" + Path.GetFileName(s) + "\"\r\n" + "Content-type: " + contentType + "\r\n\r\n";
                fi[i] = new FileInfo(s);
                postDataSize += fi[i].Length;
                headerLength += header.Length;
                fileHeaders.Add(header);
                i++;
            }
        }

        private static string GetPostParameters(NameValueCollection parameters, string contentBoundary, string contentDisposition)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < parameters.Count; i++)
                sb.Append(contentBoundary + "\r\n" + contentDisposition + '"' + parameters.GetKey(i) + "\"\r\n\r\n" + parameters[i].ToString() + "\r\n");


            return sb.ToString();
        }

        public static void WriteFile(System.IO.Stream io, string fileName )
        {
            int bufferSize = 10240;
            FileStream readIn = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            readIn.Seek(0, SeekOrigin.Begin); // move to the start of the file
            byte[] fileData = new byte[bufferSize];
            int bytes;
            while ((bytes = readIn.Read(fileData, 0, bufferSize)) > 0)
            {
                // read the file data and send a chunk at a time
                io.Write(fileData, 0, bytes);
            }
            readIn.Close();
        }
    

    }
 


} 
