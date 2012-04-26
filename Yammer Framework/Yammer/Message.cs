using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Specialized;
using System.Xml;
using System.IO;

namespace Yammer
{
    [DataContract(Name="message")]
    [XmlRoot(ElementName="message")]
    public class Message 
    {
        public Message()
        {
        }

        #region All Messages

        /// <summary>
        /// All messages in this network. Corresponds to the "All" tab on the website.
        /// </summary>
        /// <returns></returns>
        public static List<Message> GetAllMessages()
        {
            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_ALL));
        }

        /// <summary>
        /// All messages in this network. Corresponds to the "All" tab on the website.
        /// </summary>
        /// <param name="threaded">Return only the first message in each thread.</param>
        /// <returns></returns>
        public static List<Message> GetAllMessages(bool threaded)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("threaded", threaded.ToString());
            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_ALL, parameters));
        }

        /// <summary>
        /// Corresponds to the "All" tab on the website. 
        /// </summary>
        /// <param name="newer_than"></param>
        /// <returns></returns>
        public static List<Message> GetAllMessages(PageFlag flag, int id)
        {
            NameValueCollection parameters = new NameValueCollection();
            AddPageFlagParam(flag, parameters, id);

            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_ALL, parameters));
        }

        /// <summary>
        /// Corresponds to the "All" tab on the website. 
        /// </summary>
        /// <param name="newer_than"></param>
        /// <param name="threaded">Return only the first message in each thread.</param>
        /// <returns></returns>
        public static List<Message> GetAllMessages(PageFlag flag, int id, bool threaded)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("threaded", threaded.ToString());
            AddPageFlagParam(flag, parameters, id);

            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_ALL, parameters));

        }

        #endregion

        #region Sent Messages

        /// <summary>
        /// Corresponds to the "Sent" tab on the website.
        /// </summary>
        /// <returns></returns>
        public static List<Message> GetSentMessages()
        {
            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_SENT));
        }

        /// <summary>
        /// Corresponds to the "Sent" tab on the website.
        /// </summary>
        /// <param name="threaded">Return only the first message in each thread.</param>
        /// <returns></returns>
        public static List<Message> GetSentMessages(bool threaded)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("threaded", threaded.ToString());
            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_SENT, parameters));
        }

        /// <summary>
        /// Corresponds to the "Sent" tab on the website.
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="threaded"></param>
        /// <returns></returns>
        public static List<Message> GetSentMessages(PageFlag flag, int id)
        {
            NameValueCollection parameters = new NameValueCollection();
            AddPageFlagParam(flag, parameters, id);

            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_SENT, parameters));
            
        }

        /// <summary>
        /// Corresponds to the "Sent" tab on the website.
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="threaded"></param>
        /// <returns></returns>
        public static List<Message> GetSentMessages(PageFlag flag, int id, bool threaded)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("threaded", threaded.ToString());
            AddPageFlagParam(flag, parameters, id);

            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_SENT, parameters));
          
        }

        #endregion

        #region Received Messages

        /// <summary>
        /// Messages received by the logged-in user. Corresponds to the "Received" tab on the website.
        /// </summary>
        /// <returns></returns>
        public static List<Message> GetReceivedMessages()
        {
            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_RECEIVED));
        }

        /// <summary>
        /// Messages received by the logged-in user. Corresponds to the "Received" tab on the website.
        /// </summary>
        /// <returns></returns>
        public static List<Message> GetReceivedMessages(bool threaded)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("threaded", threaded.ToString());
            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_RECEIVED, parameters));
        }

        /// <summary>
        /// Messages received by the logged-in user. Corresponds to the "Received" tab on the website.
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="thread"></param>
        /// <returns></returns>
        public static List<Message> GetReceivedMessages(PageFlag flag, int id)
        {
            NameValueCollection parameters = new NameValueCollection();
            AddPageFlagParam(flag, parameters, id);

            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_RECEIVED, parameters));

        }

        /// <summary>
        /// Messages received by the logged-in user. Corresponds to the "Received" tab on the website.
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="thread"></param>
        /// <returns></returns>
        public static List<Message> GetReceivedMessages(PageFlag flag, int id, bool threaded)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("threaded", threaded.ToString());
            AddPageFlagParam(flag, parameters, id);

            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_RECEIVED,parameters));

        }

        #endregion

        #region Following Messages

        /// <summary>
        /// Messages followed by the logged-in user. Corresponds to the "Following" tab on the website.
        /// </summary>
        /// <returns></returns>
        public static List<Message> GetFollowingMessages()
        {
            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_FOLLOWING)); ;
        }

        /// <summary>
        /// Messages followed by the logged-in user. Corresponds to the "Following" tab on the website.
        /// </summary>
        /// <returns></returns>
        public static List<Message> GetFollowingMessages(bool threaded)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("threaded", threaded.ToString());

            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_FOLLOWING, parameters)); ;
        }

        /// <summary>
        /// Messages followed by the logged-in user. Corresponds to the "Following" tab on the website.
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static List<Message> GetFollowingMessages(PageFlag flag, int id)
        {
            NameValueCollection parameters = new NameValueCollection();
            AddPageFlagParam(flag, parameters, id);

            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_FOLLOWING, parameters));

        }

        /// <summary>
        /// Messages followed by the logged-in user. Corresponds to the "Following" tab on the website.
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static List<Message> GetFollowingMessages(PageFlag flag, int id, bool threaded)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("threaded", threaded.ToString());
            AddPageFlagParam(flag, parameters, id);

            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_FOLLOWING, parameters));

        }

        #endregion

        #region Messages Sent By

        /// <summary>
        /// Messages sent by the user with the given ID. Corresponds to the messages on a user profile page on the website.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Message> GetMessagesSentBy(int id)
        {
            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_SENT_BY + id.ToString() + ".xml"));
        }

        /// <summary>
        /// Messages sent by the user with the given ID. Corresponds to the messages on a user profile page on the website.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Message> GetMessagesSentBy(int id, bool threaded)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("threaded", threaded.ToString());
            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_SENT_BY + id.ToString() + ".xml", parameters));
        }

        /// <summary>
        /// Messages sent by the user with the given ID. Corresponds to the messages on a user profile page on the website.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Message> GetMessagesSentBy(PageFlag flag, int id, int pageId)
        {
            NameValueCollection parameters = new NameValueCollection();
            AddPageFlagParam(flag, parameters, pageId);

            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_SENT_BY + id.ToString() + ".xml", parameters));
        }

        /// <summary>
        /// Messages sent by the user with the given ID. Corresponds to the messages on a user profile page on the website.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Message> GetMessagesSentBy(PageFlag flag, int id, int pageId, bool threaded)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("threaded", threaded.ToString());
            AddPageFlagParam(flag, parameters, pageId);

            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_SENT_BY + id.ToString() + ".xml", parameters));
        }

        #endregion

        #region Messages Tagged With

        /// <summary>
        /// Messages including the tag with given ID. Corresponds to the messages on a tag's page on the website.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Message> GetMessagesTaggedWith(int id)
        {
            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_TAGGED_WITH + id.ToString() + ".xml"));
        }

        /// <summary>
        /// Messages including the tag with given ID. Corresponds to the messages on a tag's page on the website.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Message> GetMessagesTaggedWith(int id, bool threaded)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("threaded", threaded.ToString());

            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_TAGGED_WITH + id.ToString() + ".xml", parameters));
        }

        /// <summary>
        /// Messages including the tag with given ID. Corresponds to the messages on a tag's page on the website.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Message> GetMessagesTaggedWith(PageFlag flag, int id, int pageId)
        {
            NameValueCollection parameters = new NameValueCollection();
            AddPageFlagParam(flag, parameters, pageId);
            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_TAGGED_WITH + id.ToString() + ".xml", parameters));
        }

        /// <summary>
        /// Messages including the tag with given ID. Corresponds to the messages on a tag's page on the website.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Message> GetMessagesTaggedWith(PageFlag flag, int id, int pageId, bool threaded)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("threaded", threaded.ToString());
            AddPageFlagParam(flag, parameters, pageId);
            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_TAGGED_WITH + id.ToString() + ".xml", parameters));
        }


        #endregion

        #region Messages About Topic

        /// <summary>
        /// Messages including the topic with given ID. Corresponds to the messages on a topic's page on the website.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Message> GetMessagesAboutTopic(int id)
        {
            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_ABOUT_TOPIC + id.ToString() + ".xml"));
        }

        /// <summary>
        /// Messages including the topic with given ID. Corresponds to the messages on a topic's page on the website.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Message> GetMessagesAboutTopic(int id, bool threaded)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("threaded", threaded.ToString());

            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_ABOUT_TOPIC + id.ToString() + ".xml", parameters));
        }

        /// <summary>
        /// Messages including the topic with given ID. Corresponds to the messages on a topic's page on the website.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Message> GetMessagesAboutTopic(PageFlag flag, int id, int pageId)
        {
            NameValueCollection parameters = new NameValueCollection();
            AddPageFlagParam(flag, parameters, pageId);
            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_ABOUT_TOPIC + id.ToString() + ".xml", parameters));
        }

        /// <summary>
        /// Messages including the topic with given ID. Corresponds to the messages on a topic's page on the website.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Message> GetMessagesAboutTopic(PageFlag flag, int id, int pageId, bool threaded)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("threaded", threaded.ToString());
            AddPageFlagParam(flag, parameters, pageId);
            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_ABOUT_TOPIC + id.ToString() + ".xml", parameters));
        }

        #endregion

        #region Messages in Group

        /// <summary>
        /// Messages in the group with the given ID. Corresponds to the messages you'd see on a group's profile page.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Message> GetMessagesInGroup(int id)
        {
            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_IN_GROUP + id.ToString() + ".xml"));
        }

        /// <summary>
        /// Messages in the group with the given ID. Corresponds to the messages you'd see on a group's profile page.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Message> GetMessagesInGroup(int id, bool threaded)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("threaded", threaded.ToString());

            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_IN_GROUP + id.ToString() + ".xml", parameters));
        }

        /// <summary>
        /// Messages in the group with the given ID. Corresponds to the messages you'd see on a group's profile page.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Message> GetMessagesInGroup(PageFlag flag, int id, int pageId)
        {
            NameValueCollection parameters = new NameValueCollection();
            AddPageFlagParam(flag, parameters, pageId);

            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_IN_GROUP + id.ToString() + ".xml", parameters));
        }


        /// <summary>
        /// Messages in the group with the given ID. Corresponds to the messages you'd see on a group's profile page.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Message> GetMessagesInGroup(PageFlag flag, int id, int pageId, bool threaded)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("threaded", threaded.ToString());
            AddPageFlagParam(flag, parameters, pageId);

            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_IN_GROUP + id.ToString() + ".xml", parameters));
        }


        #endregion

        #region Messages Favorites Of

        /// <summary>
        /// Favorite messages of the given user ID. Can pass 'current' in place of user_id for currently logged in user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Message> GetMessagesFavoritesOf(int id)
        {
            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_FAVORITES_OF + id.ToString() + ".xml"));
        }

        /// <summary>
        /// Favorite messages of the given user ID. Can pass 'current' in place of user_id for currently logged in user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Message> GetMessagesFavoritesOf(int id, bool threaded)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("threaded", threaded.ToString());

            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_FAVORITES_OF + id.ToString() + ".xml", parameters));
        }

        /// <summary>
        /// Favorite messages of the given user ID. Can pass 'current' in place of user_id for currently logged in user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Message> GetMessagesFavoritesOf(PageFlag flag, int id, int pageId)
        {
            NameValueCollection parameters = new NameValueCollection();
            AddPageFlagParam(flag, parameters, pageId);

            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_FAVORITES_OF + id.ToString() + ".xml", parameters));
        }

        /// <summary>
        /// Favorite messages of the given user ID. Can pass 'current' in place of user_id for currently logged in user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Message> GetMessagesFavoritesOf(PageFlag flag, int id, int pageId, bool threaded)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("threaded", threaded.ToString());
            AddPageFlagParam(flag, parameters, pageId);

            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_FAVORITES_OF + id.ToString() + ".xml", parameters));
        }


        #endregion

        #region Messages In Thread

        /// <summary>
        /// Messages in the thread with the given ID. Corresponds to the page you'd see when clicking on "in reply to" on the website.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Message> GetMessagesInThread(int id)
        {
            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_IN_THREAD + id.ToString() + ".xml"));
        }

        /// <summary>
        /// Messages in the thread with the given ID. Corresponds to the page you'd see when clicking on "in reply to" on the website.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Message> GetMessagesInThread(PageFlag flag, int id, int pageId)
        {
            NameValueCollection parameters = new NameValueCollection();
            AddPageFlagParam(flag, parameters, pageId);
            return RetrieveMessages(Yammer.HttpUtility.Get(Resources.YAMMER_MESSAGES_IN_THREAD + id.ToString() + ".xml", parameters));
        }

        #endregion

        #region Post Message

        /// <summary>
        /// Create a new message.
        /// </summary>
        /// <param name="body"></param>
        public static string PostMessage(string body, List<string> attachments)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("body", body);
            string response = string.Empty;
            if (attachments.Count > 0)
                response = Yammer.HttpUtility.Upload(Resources.YAMMER_MESSAGES_CREATE, parameters, attachments);
            else
                response = Yammer.HttpUtility.Post(Resources.YAMMER_MESSAGES_CREATE, parameters);

            return response;
        }

        /// <summary>
        /// Sends message in reply to message of given id
        /// </summary>
        /// <param name="body"></param>
        /// <param name="reply"></param>
        public static string PostMessage(string body, int id, List<string> attachments)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("body", body);
            parameters.Add("replied_to_id", id.ToString());
            string response = string.Empty;
            if (attachments.Count > 0)
                response = Yammer.HttpUtility.Upload(Resources.YAMMER_MESSAGES_CREATE, parameters, attachments);
            else
                response = Yammer.HttpUtility.Post(Resources.YAMMER_MESSAGES_CREATE, parameters);

            return response;
        }


        /// <summary>
        /// Sends a private message directly to the user indicated.
        /// </summary>
        /// <param name="directToId"></param>
        /// <param name="body"></param>
        /// <param name="attachments"></param>
        /// <returns></returns>
        public static string PostMessage(int directToId, string body, List<string> attachments)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("body", body);
            parameters.Add("direct_to_id", directToId.ToString());
            string response = string.Empty;
            if (attachments.Count > 0)
                response = Yammer.HttpUtility.Upload(Resources.YAMMER_MESSAGES_CREATE, parameters, attachments);
            else
                response = Yammer.HttpUtility.Post(Resources.YAMMER_MESSAGES_CREATE, parameters);

            return response;
        }

        


        #endregion

        #region Delete Message

        /// <summary>
        /// Delete a message owned by the current user.
        /// </summary>
        /// <param name="id"></param>
        public static void DeleteMessage(int id)
        {
            Yammer.HttpUtility.Delete(Resources.YAMMER_MESSAGES_DELETE + id.ToString());
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

        private static void AddPageFlagParam(PageFlag pf, NameValueCollection parameters, int id)
        {
            if (pf == PageFlag.NEWER_THAN)
                parameters.Add("newer_than", id.ToString());
            else
                parameters.Add("older_than", id.ToString());
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

        #region Yammer Properties

        /// <summary>
        /// The ID number for this object. Note that IDs are not unique across all object types: 
        /// there may be a user and tag with the same numerical ID.
        /// </summary>
        [DataMember(Name="id")]
        [XmlElement(ElementName="id")]
        public string Id { get; set; }

        /// <summary>
        /// (Optional) When a message is posted into a group, that group's ID will appear here 
        /// and the group will be available in the references section.
        /// </summary>
        [DataMember(Name="group-id")]
        [XmlElement(ElementName="group-id")]
        public string GroupId { get; set; }

        /// <summary>
        /// (Optional) When a message is a private 1-to-1 (or "direct") message, this will 
        /// indicate the intended recipient.
        /// </summary>
        [DataMember(Name="direct-to-id")]
        [XmlElement(ElementName="direct-to-id")]
        public string DirectToId { get; set; }

        /// <summary>
        /// The API resource for fetching this object.
        /// </summary>
        [DataMember(Name="url")]
        [XmlElement(ElementName="url")]
        public string Url { get; set; }

        /// <summary>
        /// The URL for viewing this object on the main Yammer website.
        /// </summary>
        [DataMember(Name="web-url")]
        [XmlElement(ElementName="web-url")]
        public string WebUrl { get; set; }

        /// <summary>
        /// The ID of the message this message is in reply to, if applicable.
        /// </summary>
        [DataMember(Name="replied-to-id")]
        [XmlElement(ElementName="replied-to-id")]
        public string RepliedToId { get; set; }

        /// <summary>
        /// The thread in which this message appears.
        /// </summary>
        [DataMember(Name="thread-id")]
        [XmlElement(ElementName="thread-id")]
        public string ThreadId { get; set; }

        /// <summary>
        /// Message body
        /// </summary>
        [DataMember(Name="body")]
        [XmlElement(ElementName="body")]
        public Body Body { get; set; }

        /// <summary>
        /// A list of attachments for this message.
        /// </summary>
        [DataMember(Name="attachments")]         
        [System.Xml.Serialization.XmlArray("attachments")]
        [System.Xml.Serialization.XmlArrayItem("attachment", typeof(Attachment))]
        public List<Attachment> Attachments { get; set; }
        
        /// <summary>
        /// This will be system or update. A system message is automatically generated by the 
        /// system and describes an action, such as "Kris Gale has joined the Geni network." 
        /// An update message is a regular message posted by a user such as "Kris Gale: Hi everyone." 
        /// Put simply, this indicates whether a colon will separate the body of the message 
        /// from the sender's name in the web interface.
        /// </summary>
        [DataMember(Name="message-type")]
        [XmlElement(ElementName="message-type")]
        public string MessageType { get; set; }


        [DataMember(Name="client-type")]
        [XmlElement(ElementName="client-type")]
        public string ClientType { get; set; }
        /// <summary>
        /// The ID of the message's sender.
        /// </summary>
        [DataMember(Name="sender-id")]
        [XmlElement(ElementName="sender-id")]
        public int SenderId { get; set; }

        /// <summary>
        /// The sender's object type: user or guide. The guide is virtual user that exists 
        /// in the system to send messages such as the tips and initial welcome message.
        /// </summary>
        [DataMember(Name="sender-type")]
        [XmlElement(ElementName="sender-type")]
        public string SenderType { get; set; }

        /// <summary>
        /// The time and date this resource was created. This would indicate when a 
        /// user joined the network or when a message was posted.
        /// </summary>
        [DataMember(Name="created-at")]
        [XmlElement(ElementName="created-at")]
        public string CreatedAt { get; set; }

        [XmlIgnore]
        public string Participants { get; set; }



        #endregion

        #region Client Properties

        public Reference References { get; set; }

        public User Sender
        {
            get
            {
                return Yammer.User.GetUserById(this.References.Users, this.SenderId);
            }
        }

        public Yammer.User Recipient
        {
            get
            {
                if (this.DirectToId != null)
                    return Yammer.User.GetUserById(this.References.Users, int.Parse(this.DirectToId));
                else
                    return null;
            }
        }

        public Guide Guide
        {
            get
            {
                return this.References.Guide;
            }
        }


        #endregion
    }
}


