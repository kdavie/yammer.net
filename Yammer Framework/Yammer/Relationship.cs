using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace Yammer
{
    public class Relationship
    {


        /// <summary>
        /// Retrieves org chart relationships.
        /// </summary>
        public static void GetAllRelationships()
        {
            string response = Yammer.HttpUtility.Get(Resources.YAMMER_RELATIONSHIPS_ALL);
        }

        /// <summary>
        /// Creates a new org chart relationship
        /// </summary>
        /// <param name="type"></param>
        /// <param name="email"></param>
        public static void CreateRelationship(RelationshipType type, string email)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add(type.ToString().ToLower(), email);
            Yammer.HttpUtility.Post(Resources.YAMMER_RELATIONSHIPS_CREATE, parameters);
        }

        /// <summary>
        /// Deletes org chart relationship
        /// </summary>
        public static void DeleteRelationship(int id, RelationshipType type)
        {
            Yammer.HttpUtility.Delete(Resources.YAMMER_RELATIONSHIPS_DELETE + id.ToString().ToLower() + ".xml?type=" + type.ToString().ToLower());
        }

  
    }
}
