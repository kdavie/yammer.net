using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using OAuth;
using System.Net;

namespace Yammer
{
    [XmlRoot]
    public class Settings
    {
        public Settings()
        {
            this.Proxy = new ProxySettings();
            this.OAuth = new OAuthSettings();
        }
        [XmlElement]
        public ProxySettings Proxy { get; set; }
        [XmlElement]
        public OAuthSettings OAuth { get; set; }
        [XmlElement]
        public string UserId { get; set; }

        /// <summary>
        /// Checks if the persisted <see cref="Settings">settings</see> file exists on the client
        /// </summary>
        /// <returns></returns>
        public static Settings CheckConfiguration()
        {
            Dictionary<string, DirectoryInfo> appData = Utility.GetAppDirectory();
            if (System.IO.File.Exists(appData["data"] + "\\settings.yam"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                TextReader reader = new StreamReader(appData["data"] + "\\settings.yam");
                Settings settings = (Settings)serializer.Deserialize(reader);
                reader.Close();
                if (settings.OAuth.TokenKey != null && settings.OAuth.TokenSecret != null)
                    return settings;
                else
                    return null;

            }
            return null;
        }
        /// <summary>
        /// Saves the <see cref="Settings">settings</see> file to the client
        /// </summary>
        /// <param name="tokenKey"></param>
        /// <param name="tokenSecret"></param>
        public static void SaveConfiguration(string tokenKey, string tokenSecret, OAuthKey key, WebProxy proxy)
        {
            Dictionary<string, DirectoryInfo> appData = Utility.GetAppDirectory();
            Settings settings = new Settings();
            if (proxy != null)
            {
                settings.Proxy.Address = proxy.Address.ToString();
                NetworkCredential creds = (NetworkCredential)proxy.Credentials;
                settings.Proxy.Id = creds.UserName;
                settings.Proxy.Password = creds.Password;
                settings.Proxy.Enable = true;
            }
            else
                settings.Proxy.Enable = false;
            key.TokenKey = tokenKey;
            key.TokenSecret = tokenSecret;
            settings.OAuth.TokenKey = tokenKey;
            settings.OAuth.TokenSecret = tokenSecret;
            TextWriter writer = new StreamWriter(appData["data"] + "\\settings.yam");
            XmlSerializer serializer = new XmlSerializer(typeof(Settings));
            serializer.Serialize(writer, settings);
            writer.Close();

        }

    }

    public class ProxySettings
    {
        [XmlAttribute]
        public bool Enable { get; set; }
        [XmlElement]
        public string Address { get; set; }
        [XmlElement]
        public string Port { get; set; }
        [XmlElement]
        public string Id { get; set; }
        [XmlElement]
        public string Password { get; set; }

    }

    public class OAuthSettings
    {
        [XmlElement]
        public string TokenKey { get; set; }
        [XmlElement]
        public string TokenSecret { get; set; }
    }
}
