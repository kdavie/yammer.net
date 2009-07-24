using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Yammer
{
    public class Image
    {
        [DataMember(Name = "size")]
        [XmlElement(ElementName = "size")]
        public string Size { get; set; }
        [DataMember(Name = "url")]
        [XmlElement(ElementName = "url")]
        public string Url { get; set; }
        [DataMember(Name = "thumbnail-url")]
        [XmlElement(ElementName = "thumbnail-url")]
        public string ThumbnailUrl { get; set; }

       
    }
}
