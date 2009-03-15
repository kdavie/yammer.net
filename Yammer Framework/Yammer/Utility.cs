using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
namespace Yammer
{
    public class Utility
    {
        /// <summary>
        /// Retrieves application directory collection
        /// </summary>
        /// <returns>application directory collection</returns>
        public static Dictionary<string, DirectoryInfo> GetAppDirectory()
        {
            Dictionary<string, DirectoryInfo> appData = new Dictionary<string, DirectoryInfo>();
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);

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
            Dictionary<string, DirectoryInfo> appData = Utility.GetAppDirectory();
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
            Dictionary<string, DirectoryInfo> appData = Utility.GetAppDirectory();
            Variables variables = new Variables();
            variables.LastMessageId = lastMessageId;
            TextWriter writer = new StreamWriter(appData["data"] + "\\application.yam");
            XmlSerializer serializer = new XmlSerializer(typeof(Variables));
            serializer.Serialize(writer, variables);
            writer.Close();
        }
    }
}
