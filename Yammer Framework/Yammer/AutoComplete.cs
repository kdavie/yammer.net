using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
namespace Yammer
{
    /// <summary>
    /// NOT YET IMPLEMENTED
    /// </summary>
    [DataContract(Name = "response")]
    [XmlRoot(ElementName = "response")]
    public class AutoComplete
    {
        [DataMember(Name = "tags")]
        [System.Xml.Serialization.XmlArray("tags")]
        [System.Xml.Serialization.XmlArrayItem("tag", typeof(AutoCompleteData))]
        public List<AutoCompleteData> Tags { get; set; }

        [DataMember(Name = "groups")]
        [System.Xml.Serialization.XmlArray("groups")]
        [System.Xml.Serialization.XmlArrayItem("group", typeof(AutoCompleteData))]
        public List<AutoCompleteData> Groups { get; set; }

        [DataMember(Name = "users")]
        [System.Xml.Serialization.XmlArray("users")]
        [System.Xml.Serialization.XmlArrayItem("user", typeof(AutoCompleteData))]
        public List<AutoCompleteData> Users { get; set; }

    }

    public class AutoCompleteData
    {
        [DataMember(Name = "name")]
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [DataMember(Name = "id")]
        [XmlElement(ElementName = "id")]
        public string Id { get; set; }

        [DataMember(Name = "messages")]
        [XmlElement(ElementName = "messages")]
        public string Messages { get; set; }

        [DataMember(Name = "followers")]
        [XmlElement(ElementName = "followers")]
        public string Followers { get; set; }

        [DataMember(Name = "members")]
        [XmlElement(ElementName = "members")]
        public string Members { get; set; }
    }




}
