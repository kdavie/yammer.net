using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Reflection;
using System.Collections.Specialized;
namespace Yammer
{
    public class Utility
    {
        /// <summary>
        /// Retrieves application directory collection
        /// </summary>
        /// <returns>application directory collection</returns>
        public static Dictionary<string, DirectoryInfo> GetAppData()
        {
            Dictionary<string, DirectoryInfo> appData = new Dictionary<string, DirectoryInfo>();
           
            string path = System.Environment.CurrentDirectory;

            if (!Directory.Exists(path + "\\" + "Yammer\\Data"))
                Directory.CreateDirectory(path + "\\" + "Yammer\\Data");

            if (!Directory.Exists(path + "\\" + "Yammer\\Images"))
                Directory.CreateDirectory(path + "\\" + "Yammer\\Images");

            appData.Add("data", new DirectoryInfo(path + "\\" + "Yammer\\Data"));
            appData.Add("images", new DirectoryInfo(path + "\\" + "Yammer\\Images"));

            return appData;

        }

        /// <summary>
        /// Retrieves application variables
        /// </summary>
        /// <returns>application<see cref="Variables"/>variables</returns>
        public static Variables GetApplicationData()
        {
            Dictionary<string, DirectoryInfo> appData = Utility.GetAppData();
            if (System.IO.File.Exists(appData["data"] + "\\application.yam"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Variables));
                TextReader reader = new StreamReader(appData["data"] + "\\application.yam");
                Variables variables = (Variables)serializer.Deserialize(reader);
                reader.Close();
                return variables;
            }
            return null;
        }

        /// <summary>
        /// Writes application variables to client computer
        /// </summary>
        /// <param name="lastMessageId"></param>
        public static void SetApplicationData(string lastMessageId)
        {
            Dictionary<string, DirectoryInfo> appData = Utility.GetAppData();
            Variables variables = new Variables();
            variables.LastMessageId = lastMessageId;
            TextWriter writer = new StreamWriter(appData["data"] + "\\application.yam");
            XmlSerializer serializer = new XmlSerializer(typeof(Variables));
            serializer.Serialize(writer, variables);
            writer.Close();
        }


        public static string GetErrorLog()
        {
            Dictionary<string, DirectoryInfo> appData = Utility.GetAppData();
            if (System.IO.File.Exists(appData["data"] + "\\errorlog.yam"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Variables));
                TextReader reader = new StreamReader(appData["data"] + "\\errorlog.yam");
                string errorLog = reader.ReadToEnd();
                reader.Close();
                return errorLog;
            }
            return null;
        }

        public static void SetErrorLog(string error)
        {
            Dictionary<string, DirectoryInfo> appData = Utility.GetAppData();
            string errorLog = GetErrorLog();
            StringBuilder sb = new StringBuilder();
            if (errorLog != null)
                sb.AppendLine(errorLog);

            sb.AppendLine(error);
            sb.AppendLine("**************************************************************************");
            sb.AppendLine("");

            TextWriter writer = new StreamWriter(appData["data"] + "\\errorlog.yam");
            writer.Write(sb.ToString());
            writer.Close();

        }


        public static object Deserialize(Type type, string xml)
        {

            XmlSerializer serializer = new XmlSerializer(type);
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            byte[] bytes = encoding.GetBytes(xml);
            System.IO.MemoryStream stream = new MemoryStream(bytes);
            object obj = (object)serializer.Deserialize(stream);

            return obj;

        }

        public static void AddMembershipParams(NameValueCollection parameters, MembershipParameters groupParams)
        {
            PropertyInfo[] pic = groupParams.GetType().GetProperties();
            foreach (PropertyInfo pi in pic)
            {
                object value = pi.GetValue(groupParams, null);
                bool include = false;
                if (value != null)
                {
                    string typeName = value.GetType().Name;
                    switch (typeName)
                    {
                        case "Int32":
                            if ((int)value > 0)
                                include = true;
                            break;
                        case "SortBy":
                            if ((SortBy)value != SortBy.NONE)
                                include = true;
                            break;
                        default:
                            include = true;
                            break;
                    }


                    if (include)
                    {
                        MembershipParameterAttribute name = (MembershipParameterAttribute)MembershipParameterAttribute.GetCustomAttribute(pi, typeof(MembershipParameterAttribute));
                        parameters.Add(name.Name, pi.GetValue(groupParams, null).ToString());
                    }
                }
            }


        }
    }
}
