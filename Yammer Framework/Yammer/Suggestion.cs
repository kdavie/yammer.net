using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Xml;

namespace Yammer
{
    [DataContract(Name = "suggestion")]
    [XmlRoot(ElementName = "suggestion")]
    public class Suggestion
    {
        public Suggestion() { }

        [DataMember(Name = "suggested")]
        [XmlElement(ElementName = "suggested")]
        public Suggested Suggested { get; set; }

        [DataMember(Name = "weight")]
        [XmlElement(ElementName = "weight")]
        public double Weight { get; set; }

        [DataMember(Name = "id")]
        [XmlElement(ElementName = "id")]
        public int Id { get; set; }

        internal static List<Suggestion> GetSuggestions(string data)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(data);
            XmlNodeList nodes = xdoc.SelectNodes("/suggestions/suggestion");
            List<Suggestion> suggestions = new List<Suggestion>();
            foreach (XmlNode node in nodes)
            {
                Suggestion suggestion = (Suggestion)Utility.Deserialize(typeof(Suggestion), node.OuterXml);
                suggestions.Add(suggestion);
            }
            return suggestions;
        }

    

        /// <summary>
        /// Returns list of all suggested groups and users
        /// </summary>
        /// <param name="session"></param>
        public static List<Suggestion> GetAllSuggestions()
        {
            string response = Yammer.HttpUtility.Get(Resources.YAMMER_SUGGESTIONS_SHOW_ALL);
            return Suggestion.GetSuggestions(response);
        }
        /// <summary>
        /// Returns list of all suggested users
        /// NOT YET IMPLEMENTED
        /// </summary>
        /// <param name="session"></param>
        public static List<Suggestion> GetSuggestedUsers()
        {
            throw new System.NotImplementedException();

            string response = Yammer.HttpUtility.Get(Resources.YAMMER_SUGGESTIONS_SHOW_USERS);
            return Suggestion.GetSuggestions(response);
        }
        /// <summary>
        /// Returns list of all suggested groups
        /// NOT YET IMPLEMENTED
        /// </summary>
        /// <param name="session"></param>
        public static List<Suggestion> GetSuggestedGroups()
        {
            throw new System.NotImplementedException();

            string response = Yammer.HttpUtility.Get(Resources.YAMMER_SUGGESTIONS_SHOW_GROUPS);
            return Suggestion.GetSuggestions(response);
        }

        public static void DeclineSuggestion(int id)
        {
            Yammer.HttpUtility.Delete(Resources.YAMMER_SUGGESTIONS_DECLINE + id.ToString() + ".xml");
        }
     

    }
}
