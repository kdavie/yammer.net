using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Yammer
{
    [DataContract(Name = "suggested")]
    [XmlRoot(ElementName = "suggested")]
    public class Suggested
    {
        #region ctor

        public Suggested() { }

        #endregion

        #region Yammer Properties

        [DataMember(Name = "stats")]
        [XmlElement(ElementName = "stats")]
        public Stats Stats { get; set; }


        [DataMember(Name = "type")]
        [XmlElement(ElementName = "type")]
        public string Type { get; set; }

        [DataMember(Name = "web-url")]
        [XmlElement(ElementName = "web-url")]
        public string WebUrl { get; set; }

        [DataMember(Name = "url")]
        [XmlElement(ElementName = "url")]
        public string Url { get; set; }

        [DataMember(Name = "mugshot-url")]
        [XmlElement(ElementName = "mugshot-url")]
        public string MugshotUrl { get; set; }

        [DataMember(Name = "full-name")]
        [XmlElement(ElementName = "full-name")]
        public string FullName { get; set; }

        [DataMember(Name = "name")]
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [DataMember(Name = "state")]
        [XmlElement(ElementName = "state")]
        public string State { get; set; }

        [DataMember(Name = "id")]
        [XmlElement(ElementName = "id")]
        public int Id { get; set; }

        [DataMember(Name = "job-title")]
        [XmlElement(ElementName = "job-title")]
        public string JobTitle { get; set; }

        #endregion

    }
}
