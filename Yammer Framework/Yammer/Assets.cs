using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Web.Caching;
using System.Net;

namespace Yammer
{
    public class Assets
    {
        public Assets()
        {

        }

        public Assets(Session session)
        {
            this.Session = session;
        }

        private Session Session { get; set; }
        private System.Web.Caching.Cache Cache
        {
            get
            {
                EnsureHttpRuntime();
                return HttpRuntime.Cache;
            }
        }

        private HttpRuntime httpRuntime;
        private void EnsureHttpRuntime()
        {
            if (null == httpRuntime)
            {
                try
                {
                    Monitor.Enter(typeof(Yammer.Assets));
                    if (null == httpRuntime)
                        httpRuntime = new HttpRuntime();
                }
                finally
                {
                    Monitor.Exit(typeof(Yammer.Assets));
                }
            }
        }

        public ImageList Avatars
        {
            get
            {
                return null;
            }
            set
            {

            }
        }

       
        public List<User> Users
        {
            get
            {
                if (this.Cache["users"] == null)
                    GetUsers();

                return (List<User>)this.Cache["users"];
            }

        }

        public List<Group> Groups
        {
            get
            {
                return null;
            }

            set
            {

            }
        }

        public void UpdateUsers()
        {
            Dictionary<string, DirectoryInfo> appData = Yammer.Utility.GetAppData();
            string path = appData["data"].FullName + "\\users.yam";
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            this.Cache.Remove("users");
            GetUsers();
        }

        private void GetUsers()
        {
            Dictionary<string, DirectoryInfo> appData = Yammer.Utility.GetAppData();
            List<Yammer.User> users = null;
            string path = appData["data"].FullName + "\\users.yam";

            if (System.IO.File.Exists(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Yammer.User>));
                TextReader reader = new StreamReader(path);
                users = (List<Yammer.User>)serializer.Deserialize(reader);
                reader.Close();
            }
            else
            {
                MembershipParameters mp1 = new MembershipParameters();
                mp1.PageId = 1;
                users = Yammer.User.GetAllUsers(mp1);
                if (users.Count > 49)
                {
                    bool cont = true;
                    int i = 2;
                    while (cont)
                    {
                        MembershipParameters mp = new MembershipParameters();
                        mp.PageId = i;
                        List<User> userPage = Yammer.User.GetAllUsers(mp);
                        if (userPage.Count > 0)
                        {
                            users.AddRange(userPage.ToArray());
                            i++;
                            cont = true;
                        }
                        else
                            cont = false;
                    }
                    
                    
                }
                TextWriter writer = new StreamWriter(path);
                XmlSerializer serializer = new XmlSerializer(typeof(List<Yammer.User>));
                serializer.Serialize(writer, users);
                writer.Close();
            }

            this.Cache.Add("users", users, null, DateTime.Now.AddHours(6), System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
        }

        private void GetAvatars()
        {
            Dictionary<string, DirectoryInfo> appData = Yammer.Utility.GetAppData();
            ImageList imageList = new ImageList();
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            imageList.ImageSize = new System.Drawing.Size(48, 48);
            
            foreach (Yammer.User user in Users)
                if (!imageList.Images.ContainsKey(user.MugshotUrl))
                    GetImage(appData, imageList, user.MugshotUrl);
        }

        private void GetImage(Dictionary<string, DirectoryInfo> appData, ImageList imageList, string url)
        {
            string fileName = Yammer.Security.GetMd5Sum(url);
            System.Drawing.Image image;
            if (!System.IO.File.Exists(appData["images"] + "\\" + fileName + ".jpg"))
            {
                HttpWebRequest request = Yammer.HttpUtility.CreateWebRequest(Yammer.WebMethod.GET, Yammer.Session.WebProxy, url, false);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                image = System.Drawing.Image.FromStream(stream);
                image.Save(appData["images"] + "\\" + fileName + ".jpg");
                if (stream != null)
                    stream.Close();
            }
            else
                image = System.Drawing.Image.FromFile(appData["images"] + "\\" + fileName + ".jpg");

            imageList.Images.Add(fileName, image);
        }


    }
}
