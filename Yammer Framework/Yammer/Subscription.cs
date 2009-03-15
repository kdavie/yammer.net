using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

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


    }
}



