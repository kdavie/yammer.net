using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Specialized;
using System.Net;
namespace Yammer
{
    public static class ApiWrapper
    {

        #region Messages

        /// <summary>
        /// All messages in this network. Corresponds to the "All" tab on the website.
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static List<Message> GetAllMessages(Session session)
        {
            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_ALL, session));
        }

        /// <summary>
        /// Corresponds to the "All" tab on the website. 
        /// </summary>
        /// <param name="newer_than"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public static List<Message> GetAllMessages(PageFlag flag, int id, Session session)
        {
            if (flag == PageFlag.NEWER_THAN)
                return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_ALL + "?newer_than=" + id.ToString(), session));
            else
                return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_ALL + "?older_than=" + id.ToString(), session));
        }


        /// <summary>
        /// Corresponds to the "Sent" tab on the website.
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static List<Message> GetSentMessages(Session session)
        {
            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_SENT, session));
        }

        /// <summary>
        /// Corresponds to the "Sent" tab on the website.
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="thread"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public static List<Message> GetSentMessages(PageFlag flag, int id, Session session)
        {
            if (flag == PageFlag.NEWER_THAN)
                return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_SENT + "?newer_than=" + id.ToString(), session));
            else
                return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_SENT + "?older_than=" + id.ToString(), session));
        }

        /// <summary>
        /// Messages received by the logged-in user. Corresponds to the "Received" tab on the website.
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static List<Message> GetReceivedMessages(Session session)
        {
            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_RECEIVED, session));
        }

        /// <summary>
        /// Messages received by the logged-in user. Corresponds to the "Received" tab on the website.
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="thread"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public static List<Message> GetReceivedMessages(PageFlag flag, int id, Session session)
        {
            if (flag == PageFlag.NEWER_THAN)
                return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_RECEIVED + "?newer_than=" + id.ToString(), session));
            else
                return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_RECEIVED + "?older_than=" + id.ToString(), session));
        }
        /// <summary>
        /// Messages followed by the logged-in user. Corresponds to the "Following" tab on the website.
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static List<Message> GetFollowingMessages(Session session)
        {
            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_FOLLOWING, session)); ;
        }

        /// <summary>
        /// Messages followed by the logged-in user. Corresponds to the "Following" tab on the website.
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="date"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public static List<Message> GetFollowingMessages(PageFlag flag, int id, Session session)
        {
            if(flag == PageFlag.NEWER_THAN)
                return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_FOLLOWING + "?newer_than=" + id.ToString(), session));
            else
                return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_FOLLOWING + "?older_than=" + id.ToString(), session));
        }

        /// <summary>
        /// Messages sent by the user with the given ID. Corresponds to the messages on a user profile page on the website.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public static List<Message> GetMessagesSentBy(int id, Session session)
        {
            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_SENT_BY + id.ToString() + ".xml" , session)); 
        }

        /// <summary>
        /// Messages including the tag with given ID. Corresponds to the messages on a tag's page on the website.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public static List<Message> GetMessagesTaggedWith(int id, Session session)
        {
            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_TAGGED_WITH + id.ToString() + ".xml", session)); 
        }

        /// <summary>
        /// Messages in the thread with the given ID. Corresponds to the page you'd see when clicking on "in reply to" on the website.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public static List<Message> GetMessagesInThread(int id, Session session)
        {
            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_IN_THREAD + id.ToString() + ".xml", session)); 
        }

        /// <summary>
        /// Messages in the group with the given ID. Corresponds to the messages you'd see on a group's profile page.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public static List<Message> GetMessagesInGroup(int id, Session session)
        {
            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_IN_GROUP + id.ToString() + ".xml", session)); 
        }

        /// <summary>
        /// Favorite messages of the given user ID. Can pass 'current' in place of user_id for currently logged in user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public static List<Message> GetMessagesFavoritesOf(int id, Session session)
        {
            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_FAVORITES_OF + id.ToString() + ".xml", session));
        }

        /// <summary>
        /// Create a new message.
        /// </summary>
        /// <param name="body"></param>
        /// <param name="session"></param>
        public static void PostMessage(string body, Session session, List<string> attachments)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("body", body);
            string response;
            if(attachments.Count > 0)
                Yammer.HttpUtility.Upload(Resources.YAMMER_MESSAGES_CREATE, parameters, session, attachments);
            else
                response = Yammer.HttpUtility.Post(Resources.YAMMER_MESSAGES_CREATE, parameters, session);
                
        }

        /// <summary>
        /// Create a new message in reply to message of given id
        /// </summary>
        /// <param name="body"></param>
        /// <param name="reply"></param>
        /// <param name="session"></param>
        public static void PostMessage(string body, int id, Session session, List<string> attachments)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("body", body);
            parameters.Add("replied_to_id", id.ToString());
            if (attachments.Count > 0)
                Yammer.HttpUtility.Upload(Resources.YAMMER_MESSAGES_CREATE, parameters, session, attachments);
            else
                Yammer.HttpUtility.Post(Resources.YAMMER_MESSAGES_CREATE, parameters, session);
        }


        /// <summary>
        /// Delete a message owned by the current user.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="session"></param>
        public static void DeleteMessage(int id, Session session)
        {
            Yammer.HttpUtility.Delete(Resources.YAMMER_MESSAGES_DELETE + id.ToString(), session);

        }

       

        #endregion

        #region Groups

        /// <summary>
        /// Retrieves a list of all groups
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static List<Group> GetAllGroups(Session session)
        {
            string response = Yammer.HttpUtility.Get(Resources.YAMMER_GROUP_LIST, session);
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(response);

            XmlNodeList nodes = xdoc.SelectNodes("/response/response");
            List<Group> groups = new List<Group>();
            foreach (XmlNode node in nodes)
            {
                Group group = (Group)Deserialize(typeof(Group), "<group>" + node.InnerXml + "</group>");
                groups.Add(group);
            }
            return groups;

        }

        /// <summary>
        /// Retrieves data about group of given id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public static Group GetGroupById(int id, Session session)
        {
            string response = Yammer.HttpUtility.Get(Resources.YAMMER_GROUP_DATA + id.ToString() + ".xml", session);
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(response);
            XmlNode node = xdoc.SelectSingleNode("/response");
            Group group = (Group)Deserialize(typeof(Group), "<group>" + node.InnerXml + "</group>");
            return group;
        }

        /// <summary>
        /// Join a group.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="session"></param>
        public static void JoinGroup(int id, Session session)
        {
            GroupMembership(id, session);
        }

        /// <summary>
        /// Leave a group.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="session"></param>
        public static void LeaveGroup(int id, Session session)
        {
            GroupMembership(id, session);
            
        }

        private static void GroupMembership(int id, Session session)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("group_id", id.ToString());
            Yammer.HttpUtility.Post(Resources.YAMMER_GROUP_JOIN + id.ToString() + ".xml", parameters, session);
        }


        #endregion

        #region Users

        /// <summary>
        /// Retreives data about current user
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static User GetCurrentUser(Session session)
        {
            string response = Yammer.HttpUtility.Get(Resources.YAMMER_USERS_CURRENT, session);
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(response);
            XmlNode node = xdoc.SelectSingleNode("/response");
            User user = (User)Deserialize(typeof(User), "<reference>" + node.InnerXml + "</reference>");
            return user;
        }

        /// <summary>
        /// Retrieves data about user of given id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public static User GetUserById(int id, Session session)
        {
            string response = Yammer.HttpUtility.Get(Resources.YAMMER_USERS_SINGLE + id.ToString() + ".xml", session);
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(response);
            XmlNode node = xdoc.SelectSingleNode("/response");
            User user = (User)Deserialize(typeof(User), "<reference>" + node.InnerXml + "</reference>");
            return user;
        }

        /// <summary>
        /// Retrieves list of users in network
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static List<User> GetAllUsers(Session session)
        {
            string response = Yammer.HttpUtility.Get(Resources.YAMMER_USERS_ALL, session);
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(response);
            XmlNodeList nodes = xdoc.SelectNodes("/response/response");
            List<User> users = new List<User>();
            foreach (XmlNode node in nodes)
            {
                User user = (User)Deserialize(typeof(User), "<reference>" + node.InnerXml + "</reference>");
                users.Add(user);
            }
            return users;
        }
        #endregion

        #region Relationships

        /// <summary>
        /// Retrieves org chart relationships.
        /// </summary>
        /// <param name="session"></param>
        public static void GetAllRelationships(Session session)
        {
            string response = Yammer.HttpUtility.Get(Resources.YAMMER_RELATIONSHIPS_ALL, session);
        }

        /// <summary>
        /// Creates a new org chart relationship
        /// </summary>
        /// <param name="type"></param>
        /// <param name="email"></param>
        /// <param name="session"></param>
        public static void CreateRelationship(RelationshipType type, string email, Session session)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add(type.ToString().ToLower(), email);
            Yammer.HttpUtility.Post(Resources.YAMMER_RELATIONSHIPS_CREATE, parameters, session);
        }

        /// <summary>
        /// Deletes org chart relationship
        /// NOT YET IMPLEMENTED
        /// </summary>
        public static void DeleteRelationship()
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Suggestions

        /// <summary>
        /// Returns list of all suggested groups and users
        /// NOT YET IMPLEMENTED
        /// </summary>
        /// <param name="session"></param>
        public static void GetAllSuggestions(Session session)
        {
            string response = Yammer.HttpUtility.Get(Resources.YAMMER_SUGGESTIONS_SHOW_ALL, session);
        }

        #endregion

        #region Subscriptions

        /// <summary>
        /// Subscribe to user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="session"></param>
        public static void SubscribeToUser(int id, Session session)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("target_type", "user");
            parameters.Add("target_id", id.ToString());
            string data = Yammer.HttpUtility.Post(Resources.YAMMER_SUBSCRIPTIONS_SUBSCRIBE, parameters, session);
        }

        /// <summary>
        /// Subscribe to tag
        /// </summary>
        /// <param name="id"></param>
        /// <param name="session"></param>
        public static void SubscribeToTag(int id, Session session)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("target_type", "tag");
            parameters.Add("target_id", id.ToString());
            string data = Yammer.HttpUtility.Post(Resources.YAMMER_SUBSCRIPTIONS_SUBSCRIBE, parameters, session);
        }

        /// <summary>
        /// Unsubscribe to user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="session"></param>
        public static void DeleteSubscriptionToUser(int id, Session session)
        {
            string data = Yammer.HttpUtility.Delete(Resources.YAMMER_RELATIONSHIPS_DELETE + "?target_type=user" + "&target_id=" + id.ToString(), session);
        }

        /// <summary>
        /// Unsubscribe to tag
        /// </summary>
        /// <param name="id"></param>
        /// <param name="session"></param>
        public static void DeleteSubscriptionToTag(int id, Session session)
        {
            string data = Yammer.HttpUtility.Delete(Resources.YAMMER_RELATIONSHIPS_DELETE + "?target_type=tag" + "&target_id=" + id.ToString(), session);
        }

        #endregion

        #region Helper Methods

        private static object Deserialize(Type type, string xml)
        {

            XmlSerializer serializer = new XmlSerializer(type);
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            byte[] bytes = encoding.GetBytes(xml);
            System.IO.MemoryStream stream = new MemoryStream(bytes);
            object obj = (object)serializer.Deserialize(stream);

            return obj;

        }

        private static void SetMessageReference(Message message, Message reference)
        {
            if (message != null)
                message.References.Messages.Add(reference);
            
        }

        private static void SetMessageReference(Message message, Guide reference)
        {
            if (message != null)
                message.References.Guide = reference;
        }

        private static void AddMessageReferences(List<Message> messages, object reference)
        {
            if (messages != null)
            {
                User user;
                Tag tag;
                Thread thread;
                foreach (Message msg in messages)
                {
                    switch (ConvertReferenceType(reference, out user, out tag, out thread))
                    {
                        case ObjectType.USER:
                            msg.References.Users.Add(user);
                            break;
                        case ObjectType.THREAD:
                            msg.References.Thread = thread;
                            break;
                        case ObjectType.TAG:
                            msg.References.Tags.Add(tag);
                            break;
                    }
                }
            }

        }

        private static ObjectType ConvertReferenceType(object reference, out User user, out Tag tag, out Thread thread)
        {
            user = null;
            tag = null;
            thread = null;
            user = reference as User;
            ObjectType type = ObjectType.NONE;
            bool converted = false;
            if (user != null) { converted = true; type = ObjectType.USER; }
            if (!converted)
            {
                thread = reference as Thread;
                if (thread != null) { converted = true; type = ObjectType.THREAD; }
                if (!converted)
                {
                    tag = reference as Tag;
                    if (tag != null) { converted = true; type = ObjectType.TAG; }
                }
            }

            return type;
        }

        private static List<Message> RetrieveMessages(string data)
        {
            List<Message> messages = null;
            try
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(data);
                messages = new List<Message>();
                ReadMessages(messages, xdoc);
                ReadReferences(messages, xdoc);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Deserialization Error: " + ex.Message.ToString());
            }

            return messages;
        }

        private static void ReadReferences(List<Message> messages, XmlDocument xdoc)
        {
            foreach (XmlNode node in xdoc.SelectNodes("/response/references/reference"))
            {
                string xml = node.OuterXml;
                ObjectType objectType = (ObjectType)Enum.Parse(Type.GetType("Yammer.ObjectType"), node.SelectSingleNode("type").InnerText.ToUpper());
                switch (objectType)
                {
                    case ObjectType.MESSAGE:
                        Message message = (Message)Deserialize(typeof(Message), "<message>" + node.InnerXml + "</message>");
                        SetMessageReference(messages.Find(delegate(Message m) { return m.RepliedToId == message.Id; }), message);
                        break;
                    case ObjectType.USER:
                        User user = (User)Deserialize(typeof(User), node.OuterXml);
                        AddMessageReferences(messages.FindAll(delegate(Message m) { return m.SenderId.ToString() == user.Id || m.RepliedToId == user.Id; }), user);
                        break;
                    case ObjectType.TAG:
                        Tag tag = (Tag)Deserialize(typeof(Tag), node.OuterXml);
                        string tagText = "[[tag:" + tag.Id + "]] tag";
                        AddMessageReferences(messages.FindAll(delegate(Message m) { return m.Body.Parsed.Contains(tagText); }), tag);
                        break;
                    case ObjectType.THREAD:
                        Thread thread = (Thread)Deserialize(typeof(Thread), node.OuterXml);
                        AddMessageReferences(messages.FindAll(delegate(Message m) { return m.ThreadId == thread.Id; }), thread);
                        break;
                    case ObjectType.GUIDE:
                        Guide guide = (Guide)Deserialize(typeof(Guide), node.OuterXml);
                        SetMessageReference(messages.Find(delegate(Message m) { return m.SenderId.ToString() == guide.Id; }), guide);
                        break;
                    default:
                        break;
                }
            }
        }

        private static void ReadMessages(List<Message> messages, XmlDocument xdoc)
        {
            foreach (XmlNode mnode in xdoc.SelectNodes("/response/messages/message"))
            {
                Message message = (Message)Deserialize(typeof(Message), mnode.OuterXml);
                message.References = new Reference();
                messages.Add(message);
            }
        }

        #endregion

    }
}
