using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
namespace Yammer
{
    public class Contact
    {
        [XmlArray("email-addresses")]
        [XmlArrayItem("email-address", typeof(EmailAddress))]
        public List<EmailAddress> EmailAddresses{ get; set; }

        [XmlArray("phone-numbers")]
        [XmlArrayItem("phone-number", typeof(PhoneNumber))]
        public List<PhoneNumber> PhoneNumbers{ get; set; }

        [DataMember(Name = "im")]
        [XmlElement(ElementName = "im")]
        public InstantMessager InstantMessenger{ get; set; }


    }

    public class EmailAddress
    {
        [DataMember(Name = "type")]
        [XmlElement(ElementName = "type")]
        public string Type{ get; set; }

        [DataMember(Name = "address")]
        [XmlElement(ElementName = "address")]
        public string Address{ get; set; }
    }

    public class PhoneNumber
    {
        [DataMember(Name = "type")]
        [XmlElement(ElementName = "type")]
        public string Type{ get; set; }

        [DataMember(Name = "number")]
        [XmlElement(ElementName = "number")]
        public string Number{ get; set; }
    }

    public class InstantMessager
    {
        [DataMember(Name = "username")]
        [XmlElement(ElementName = "username")]
        public string UserName{ get; set; }

        [DataMember(Name = "provider")]
        [XmlElement(ElementName = "provider")]
        public string Provider{ get; set; }
    }
}


