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
    public class User
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
        /// The username of this user.
        /// </summary>
        [DataMember(Name = "name")]
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// The user's full name.
        /// </summary>
        [DataMember(Name = "full-name")]
        [XmlElement(ElementName = "full-name")]
        public string FullName { get; set; }

        /// <summary>
        /// The URL of this user's picture.
        /// </summary>
        [DataMember(Name = "mugshot-url")]
        [XmlElement(ElementName = "mugshot-url")]
        public string MugshotUrl { get; set; }

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
        /// User's job title
        /// </summary>
        [DataMember(Name = "job-title")]
        [XmlElement(ElementName = "job-title")]
        public string JobTitle { get; set; }

        /// <summary>
        /// User's contact information
        /// </summary>
        [DataMember(Name = "contact")]
        [XmlElement(ElementName = "contact")]
        public Contact Contact { get; set; }

        /// <summary>
        /// The network id of the user
        /// </summary>
        [DataMember(Name = "network-id")]
        [XmlElement(ElementName = "network-id")]
        public string NetworkId { get; set; }

        // User's birthdate
        [DataMember(Name = "birth-date")]
        [XmlElement(ElementName = "birth-date")]
        public string BirthDate { get; set; }

        /// <summary>
        /// User's date of hire
        /// </summary>
        [DataMember(Name = "hire-date", IsRequired=false)]
        [XmlElement(ElementName = "hire-date", IsNullable=true)]
        public string HireDate { get; set; }

        /// <summary>
        /// The network name of the user
        /// </summary>
        [DataMember(Name = "network-name")]
        [XmlElement(ElementName = "network-name")]
        public string NetworkName { get; set; }

        /// <summary>
        /// User's stats
        /// </summary>
        [DataMember(Name = "stats")]
        [XmlElement(ElementName = "stats")]
        public UserStats Stats { get; set; }

        /// <summary>
        /// User location
        /// </summary>
        [DataMember(Name = "location")]
        [XmlElement(ElementName = "location")]
        public string Location { get; set; }



    }

    public class UserStats
    {

        [DataMember(Name = "updates")]
        [XmlElement(ElementName = "updates")]
        public string Updates { get; set; }


        [DataMember(Name = "followers")]
        [XmlElement(ElementName = "followers")]
        public string Followers { get; set; }

        [DataMember(Name = "following")]
        [XmlElement(ElementName = "following")]
        public string Following { get; set; }
    }

}

