using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Yammer
{
    public class Body
    {
        /// <summary>
        /// A plaintext version of the message body.
        /// </summary>
        [DataMember(Name = "plain")]
        [XmlElement(ElementName = "plain")]
        public string Plain { get; set; }

        /// <summary>
        /// A version of the message body with #tags and @users replaced by [[object:id]]. 
        /// This is not present in the reference version of a message.
        /// </summary>
        [DataMember(Name = "parsed")]
        [XmlElement(ElementName = "parsed")]
        public string Parsed { get; set; }
    }
}
