using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Yammer
{
    [DataContract(Name = "reference")]
    [XmlRoot(ElementName = "reference")]
    public class Thread
    {
        /// <summary>
        /// The object type, such as user, tag, etc.
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
        /// Thread stats
        /// </summary>
        [DataMember(Name = "stats")]
        [XmlElement(ElementName = "stats")]
        public ThreadStats Stats { get; set; }
    }

    public class ThreadStats
    {
        [DataMember(Name = "updates")]
        [XmlElement(ElementName = "updates")]
        public string Updates { get; set; }

        [DataMember(Name = "latest-reply-at")]
        [XmlElement(ElementName = "latest-reply-at")]
        public string LastestReplyAt { get; set; }
    }
}
