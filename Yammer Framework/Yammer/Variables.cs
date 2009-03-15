using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Yammer
{
    [XmlRoot]
    public class Variables
    {
        [XmlElement]
        public string LastMessageId { get; set; }
    }
}
