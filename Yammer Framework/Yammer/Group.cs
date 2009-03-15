using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Yammer
{
    [DataContract(Name = "group")]
    [XmlRoot(ElementName = "group")]
    public class Group
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
        /// Name given to this group.
        /// </summary>
        [DataMember(Name = "full-name")]
        [XmlElement(ElementName = "full-name")]
        public string FullName { get; set; }

        /// <summary>
        /// Shortened name of this group. Used in references (@salesteam), addressing (to:salesteam), 
        /// the email address for group updates (salesteam@yammer.com) and the web URL (www.yammer.com/groups/salesteam). 
        /// </summary>
        [DataMember(Name = "name")]
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Either public or private, to indicate whether updates are visible to non-members 
        /// (public) and whether joining requires a group admin's approval (private). 
        /// </summary>
        [DataMember(Name = "privacy")]
        [XmlElement(ElementName = "privacy")]
        public string Privacy { get; set; }

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
        /// Group stats
        /// </summary>
        [DataMember(Name = "stats")]
        [XmlElement(ElementName = "stats")]
        public GroupStats Stats { get; set; }

    }

    public class GroupStats
    {
        [DataMember(Name = "members")]
        [XmlElement(ElementName = "members")]
        public string Members { get; set; }

        [DataMember(Name = "updates")]
        [XmlElement(ElementName = "updates")]
        public string Updates { get; set; }
    }
}
