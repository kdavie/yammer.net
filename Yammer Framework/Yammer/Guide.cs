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
    public class Guide
    {
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
        /// The name of this guide
        /// </summary>
        [DataMember(Name = "name")]
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// The guide's full name.
        /// </summary>
        [DataMember(Name = "full-name")]
        [XmlElement(ElementName = "full-name")]
        public string FullName { get; set; }

        /// <summary>
        /// The URL of this guide's picture.
        /// </summary>
        [DataMember(Name = "mugshot-url")]
        [XmlElement(ElementName = "mugshot-url")]
        public string MugshotUrl { get; set; }

        /// <summary>
        /// The URL for viewing this object on the main Yammer website.
        /// </summary>
        [DataMember(Name = "web-url")]
        [XmlElement(ElementName = "web-url")]
        public string WebUrl { get; set; }

    }
}
