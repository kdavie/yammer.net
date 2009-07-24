using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Xml;
using System.Collections.Specialized;
using System.Reflection;
namespace Yammer
{
    [DataContract(Name = "reference")]
    [XmlRoot(ElementName = "reference")]
    public class User
    {
        #region Yammer Properties

        /// <summary>
        /// The object type.
        /// </summary>
        [DataMember(Name = "type")]
        [XmlElement(ElementName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// The ID number for this object. Note that IDs are not unique across all object types: 
        /// there may be a user and tag with the same numerical ID.
        /// </summary>
        [DataMember(Name = "id")]
        [XmlElement(ElementName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// The username of this user.
        /// </summary>
        [DataMember(Name = "name")]
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// The user's full name.
        /// </summary>
        [DataMember(Name = "full-name")]
        [XmlElement(ElementName = "full-name")]
        public string FullName { get; set; }

        /// <summary>
        /// The URL of this user's picture.
        /// </summary>
        [DataMember(Name = "mugshot-url")]
        [XmlElement(ElementName = "mugshot-url")]
        public string MugshotUrl { get; set; }

        /// <summary>
        /// The API resource for fetching this object.
        /// </summary>
        [DataMember(Name = "url")]
        [XmlElement(ElementName = "url")]
        public string Url { get; set; }

        /// <summary>
        /// The URL for viewing this object on the main Yammer website.
        /// </summary>
        [DataMember(Name = "web-url")]
        [XmlElement(ElementName = "web-url")]
        public string WebUrl { get; set; }

        /// <summary>
        /// User's job title
        /// </summary>
        [DataMember(Name = "job-title")]
        [XmlElement(ElementName = "job-title")]
        public string JobTitle { get; set; }

        /// <summary>
        /// User's contact information
        /// </summary>
        [DataMember(Name = "contact")]
        [XmlElement(ElementName = "contact")]
        public Contact Contact { get; set; }

        /// <summary>
        /// The network id of the user
        /// </summary>
        [DataMember(Name = "network-id")]
        [XmlElement(ElementName = "network-id")]
        public string NetworkId { get; set; }

        // User's birthdate
        [DataMember(Name = "birth-date")]
        [XmlElement(ElementName = "birth-date")]
        public string BirthDate { get; set; }

        /// <summary>
        /// User's date of hire
        /// </summary>
        [DataMember(Name = "hire-date", IsRequired=false)]
        [XmlElement(ElementName = "hire-date", IsNullable=true)]
        public string HireDate { get; set; }

        /// <summary>
        /// The network name of the user
        /// </summary>
        [DataMember(Name = "network-name")]
        [XmlElement(ElementName = "network-name")]
        public string NetworkName { get; set; }

        /// <summary>
        /// User's stats
        /// </summary>
        [DataMember(Name = "stats")]
        [XmlElement(ElementName = "stats")]
        public Stats Stats { get; set; }

        /// <summary>
        /// User location
        /// </summary>
        [DataMember(Name = "location")]
        [XmlElement(ElementName = "location")]
        public string Location { get; set; }


        #endregion

        internal static User GetUser(string data)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(data);
            XmlNode node = xdoc.SelectSingleNode("/response");
            User user = (User)Utility.Deserialize(typeof(User), "<reference>" + node.InnerXml + "</reference>");
            return user;
        }

        internal static List<User> GetAllUsers(string data)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(data);
            
            XmlNodeList nodes = xdoc.SelectNodes("/response/response");
            List<User> users = new List<User>();
            foreach (XmlNode node in nodes)
            {
                User user = (User)Utility.Deserialize(typeof(User), "<reference>" + node.InnerXml + "</reference>");
                users.Add(user);
            }
            return users;
        }

        /// <summary>
        /// Retreives data about current user
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static User GetCurrentUser()
        {
            string response = Yammer.HttpUtility.Get(Resources.YAMMER_USERS_CURRENT);
            return User.GetUser(response);
        }

        /// <summary>
        /// Retrieves list of users in network
        /// </summary>
        /// <returns></returns>
        public static List<User> GetAllUsers()
        {
           
            //string response = Yammer.HttpUtility.Get(Resources.YAMMER_USERS_ALL);
            //return User.GetAllUsers(response);
            if (Session.Assets == null)
                Session.Assets = new Assets();

            return Session.Assets.Users;
        }

        /// <summary>
        /// Retrieves list of users in network 
        /// </summary>
        /// <param name="userParams"></param>
        /// <returns></returns>
        public static List<User> GetAllUsers(MembershipParameters userParams)
        {
            NameValueCollection parameters = new NameValueCollection();
            Yammer.Utility.AddMembershipParams(parameters, userParams);
            string response = Yammer.HttpUtility.Get(Resources.YAMMER_USERS_ALL, parameters);
            return User.GetAllUsers(response);
        }

        public static User GetUserById(int id)
        {
            if (Session.Assets == null)
                Session.Assets = new Assets();

            return Session.Assets.Users.Find(delegate(User u) { return int.Parse(u.Id) == id; });
        }

        public static User GetUserById(List<User> users, int id)
        {
            return users.Find(delegate(User u) { return int.Parse(u.Id) == id; });
        }

        public static User GetUserByUserName(string name)
        {
            return Yammer.User.GetAllUsers().Find(delegate(Yammer.User u) { return u.Name.ToLower() == name.ToLower(); });
        }

        public static void Update(int id, UserParameters userParams)
        {
            NameValueCollection parameters = new NameValueCollection();
            AddUserParam(parameters, userParams);
            string response = Yammer.HttpUtility.Put(Resources.YAMMER_USERS_MODIFY + id.ToString() + ".xml", parameters);
            Session.Assets.UpdateUsers();
        }

        public static void Create(UserParameters userParams)
        {
            NameValueCollection parameters = new NameValueCollection();
            AddUserParam(parameters, userParams);
            string response = Yammer.HttpUtility.Post(Resources.YAMMER_USERS_CREATE, parameters);
        }

        public static void Delete(int id)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("delete", "true");
            string response = Yammer.HttpUtility.Delete(Resources.YAMMER_USERS_DELETE + id.ToString() + ".xml", parameters);
            Session.Assets.UpdateUsers();
        }

        public void Delete()
        {
            User.Delete(int.Parse(this.Id));
           
        }

        private static void AddUserParam(NameValueCollection parameters, UserParameters userParams)
        {
            PropertyInfo[] pic = userParams.GetType().GetProperties();
            UserAttribute name;
            foreach (PropertyInfo pi in pic)
            {
                object value = pi.GetValue(userParams, null);
                bool include = false;
                if (value != null)
                {
                    string typeName = value.GetType().Name;
                    switch (typeName)
                    {
                        case "String":
                            name = (UserAttribute)UserAttribute.GetCustomAttribute(pi, typeof(UserAttribute));
                            parameters.Add(name.Name, pi.GetValue(userParams, null).ToString());
                            break;
                        case "List`1":
                            name = (UserAttribute)UserAttribute.GetCustomAttribute(pi, typeof(UserAttribute));
                            if (name.Name == "education[]")
                            {
                                List<UserEducation> edl = (List<UserEducation>)pi.GetValue(userParams, null);
                                foreach (UserEducation pc in edl)
                                    parameters.Add(name.Name, pc.School + "," + pc.Degree + "," + pc.Description + "," + pc.StartYear + "," + pc.EndYear);

                            }
                            else if (name.Name == "previous_companies[]")
                            {
                                List<PreviousCompany> pcl = (List<PreviousCompany>)pi.GetValue(userParams, null);
                                foreach (PreviousCompany pc in pcl)
                                    parameters.Add(name.Name, pc.Company + "," + pc.Position + "," + pc.Description + "," + pc.StartYear + "," + pc.EndYear);
                            }
                            break;
                        default:
                            include = false;
                            break;
                    }
                }
            }
        }

        public void Save(UserParameters up)
        {
            User.Update(int.Parse(this.Id), up);
            
        }

    }

    public class Stats
    {
        [DataMember(Name = "updates")]
        [XmlElement(ElementName = "updates")]
        public string Updates { get; set; }

        [DataMember(Name = "followers")]
        [XmlElement(ElementName = "followers")]
        public string Followers { get; set; }

        [DataMember(Name = "following")]
        [XmlElement(ElementName = "following")]
        public string Following { get; set; }
    }

}

