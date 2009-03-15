using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Yammer
{
    public class File
    {
        [DataMember(Name = "size")]
        [XmlElement(ElementName = "size")]
        public string Size { get; set; }
        [DataMember(Name = "url")]
        [XmlElement(ElementName = "url")]
        public string Url { get; set; }
    }
}
