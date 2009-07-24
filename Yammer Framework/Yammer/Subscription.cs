using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Collections.Specialized;

namespace Yammer
{
    [DataContract(Name = "response")]
    [XmlRoot(ElementName = "response")]
    public class Subscription
    {
        /// <summary>
        /// The API resource for fetching this object.
        /// </summary>
        [DataMember(Name = "url")]
        [XmlElement(ElementName = "url")]
        public string Url { get; set; }

        /// <summary>
        /// The object type, such as user, tag, etc.
        /// </summary>
        [DataMember(Name = "type")]
        [XmlElement(ElementName = "type")]
        public string Type { get; set; }


        [DataMember(Name = "target-web-url")]
        [XmlElement(ElementName = "target-web-url")]
        public string TargetWebUrl { get; set; }

        [DataMember(Name = "target-type")]
        [XmlElement(ElementName = "target-type")]
        public string TargetType { get; set; }

        [DataMember(Name = "target-url")]
        [XmlElement(ElementName = "target-url")]
        public string TargetUrl { get; set; }

       
        /// <summary>
        /// Subscribe to user
        /// </summary>
        /// <param name="id"></param>
        public static void SubscribeToUser(int id)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("target_type", "user");
            parameters.Add("target_id", id.ToString());
            string data = Yammer.HttpUtility.Post(Resources.YAMMER_SUBSCRIPTIONS_SUBSCRIBE, parameters);
        }

        /// <summary>
        /// Subscribe to tag
        /// </summary>
        /// <param name="id"></param>
        public static void SubscribeToTag(int id)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("target_type", "tag");
            parameters.Add("target_id", id.ToString());
            string data = Yammer.HttpUtility.Post(Resources.YAMMER_SUBSCRIPTIONS_SUBSCRIBE, parameters);
        }

        /// <summary>
        /// Unsubscribe to user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="session"></param>
        public static void DeleteSubscriptionToUser(int id)
        {
            string data = Yammer.HttpUtility.Delete(Resources.YAMMER_RELATIONSHIPS_DELETE + "?target_type=user" + "&target_id=" + id.ToString());
        }

        /// <summary>
        /// Unsubscribe to tag
        /// </summary>
        /// <param name="id"></param>
        /// <param name="session"></param>
        public static void DeleteSubscriptionToTag(int id, Session session)
        {
            string data = Yammer.HttpUtility.Delete(Resources.YAMMER_RELATIONSHIPS_DELETE + "?target_type=tag" + "&target_id=" + id.ToString());
        }
 

    }
}



