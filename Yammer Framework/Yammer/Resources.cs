using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yammer
{
    public static class Resources
    {

        private static string APP_PATH = System.Configuration.ConfigurationSettings.AppSettings["APP_PATH"];
        
        #region oAuth

        public static string OAUTH_REQUEST_TOKEN = APP_PATH + "/oauth/request_token";
        public static string OAUTH_AUTHORIZE = APP_PATH + "/oauth/authorize";
        public static string OAUTH_ACCESS_TOKEN = APP_PATH + "/oauth/access_token";

        #endregion

        #region Messages

        /// <summary>
        /// All messages in this network. Corresponds to the "All" tab on the website
        /// </summary>
        public static string YAMMER_MESSAGES_ALL = APP_PATH + "/api/v1/messages.xml";
        /// <summary>
        /// Alias for /api/v1/messages/from_user/logged-in_user_id.format Corresponds to the "Sent" tab on the website.
        /// </summary>
        public static string YAMMER_MESSAGES_SENT = APP_PATH + "/api/v1/messages/sent.xml";
        /// <summary>
        /// Messages received by the logged-in user. Corresponds to the "Received" tab on the website.
        /// </summary>
        public static string YAMMER_MESSAGES_RECEIVED = APP_PATH + "/api/v1/messages/received.xml";
        /// <summary>
        /// Messages sent by the user with the given ID. Corresponds to the messages on a user profile page on the website.
        /// </summary>
        public static string YAMMER_MESSAGES_FOLLOWING = APP_PATH + "/api/v1/messages/following.xml";
        /// <summary>
        /// Messages sent by the user with the given ID. Corresponds to the messages on a user profile page on the website.
        /// </summary>
        public static string YAMMER_MESSAGES_SENT_BY = APP_PATH + "/api/v1/messages/from_user/";
        /// <summary>
        /// Messages including the tag with given ID. Corresponds to the messages on a tag's page on the website.
        /// </summary>
        public static string YAMMER_MESSAGES_TAGGED_WITH = APP_PATH + "/api/v1/messages/tagged_with/";
        /// <summary>
        /// Messages including the topic with given ID. Corresponds to the messages on a topic's page on the website.
        /// </summary>
        public static string YAMMER_MESSAGES_ABOUT_TOPIC = APP_PATH + "/api/v1/messages/about_topic/";
        /// <summary>
        /// Messages in the thread with the given ID. Corresponds to the page you'd see when clicking on "in reply to" on the website.
        /// </summary>
        public static string YAMMER_MESSAGES_IN_THREAD = APP_PATH + "/api/v1/messages/in_thread/";
        /// <summary>
        /// Messages in the group with the given ID. Corresponds to the messages you'd see on a group's profile page.
        /// </summary>
        public static string YAMMER_MESSAGES_IN_GROUP = APP_PATH + "/api/v1/messages/in_group/";
        /// <summary>
        /// Favorite messages of the given user ID. Can pass 'current' in place of user_id for currently logged in user
        /// </summary>
        public static string YAMMER_MESSAGES_FAVORITES_OF = APP_PATH + "/api/v1/messages/favorites_of/";
        /// <summary>
        /// Create a new message.
        /// </summary>
        public static string YAMMER_MESSAGES_CREATE = APP_PATH + "/api/v1/messages/";
        /// <summary>
        /// Delete a message owned by the current user.
        /// </summary>
        public static string YAMMER_MESSAGES_DELETE = APP_PATH + "/api/v1/messages/";
        /// <summary>
        /// Add a message to user_id's favorite messages.
       /// </summary>
        public static string YAMMER_MESSAGES_ADD_FAVORITE = APP_PATH + "/api/v1/favorites_of/";
        /// <summary>
        /// Remove a favorite.
        /// </summary>
        public static string YAMMER_MESSAGES_DELETE_FAVORITE = APP_PATH + "/api/v1/favorites_of/";

        #endregion

        #region Group

        /// <summary>
        /// A list of groups. 
        /// </summary>
        public static string YAMMER_GROUP_LIST = APP_PATH + "/api/v1/groups.xml";

        public static string YAMMER_GROUP_DATA = APP_PATH + "/api/v1/groups/";

        #endregion

        #region Tags

        /// <summary>
        /// Tags in this network. NOT YET IMPLEMENTED
        /// </summary>
        public static string YAMMER_TAGS = APP_PATH + "/api/v1/tags.xml";
        /// <summary>
        /// View data about one tag.
        /// </summary>
        public static string YAMMER_TAGS_DATA = APP_PATH + "/api/v1/tags/";

        #endregion

        #region Users

        /// <summary>
        /// Users in this network.
        /// </summary>
        public static string YAMMER_USERS_ALL = APP_PATH + "/api/v1/users.xml";
        /// <summary>
        /// View data about one user. 	
        /// </summary>
        public static string YAMMER_USERS_SINGLE = APP_PATH + "/api/v1/users/";
        /// <summary>
        /// Alias to /api/v1/users/current user's id.format.
        /// </summary>
        public static string YAMMER_USERS_CURRENT = APP_PATH + "/api/v1/users/current.xml";

        public static string YAMMER_USERS_CREATE = APP_PATH + "/api/v1/users.xml";

        public static string YAMMER_USERS_MODIFY = APP_PATH + "/api/v1/users/";

        public static string YAMMER_USERS_DELETE = APP_PATH + "/api/v1/users/";

        #endregion

        #region Group Membership

        /// <summary>
        /// Join a group.
        /// </summary>
        public static string YAMMER_GROUP_JOIN = APP_PATH + "/api/v1/group_memberships/";
        /// <summary>
        /// Leave a group.
        /// </summary>
        public static string YAMMER_GROUP_RESIGN = APP_PATH + "/api/v1/group_memberships/";
         
        #endregion

        #region Relationships

        /// <summary>
        /// Show org chart relationships.
        /// </summary>
        public static string YAMMER_RELATIONSHIPS_ALL = APP_PATH + "/api/v1/relationships.xml";
        /// <summary>
        /// Add an org chart relationship.
        /// </summary>
        public static string YAMMER_RELATIONSHIPS_CREATE = APP_PATH + "/api/v1/relationships.xml";
        /// <summary>
        /// Remove a relationship.
        /// </summary>
        public static string YAMMER_RELATIONSHIPS_DELETE = APP_PATH + "/api/v1/relationships/";
       
        #endregion

        #region Suggestions

        /// <summary>
        /// Show suggested groups and users. 
        /// </summary>
        public static string YAMMER_SUGGESTIONS_SHOW_ALL = APP_PATH + "/api/v1/suggestions.xml";
        /// <summary>
        /// Show only suggested users. 
        /// </summary>
        public static string YAMMER_SUGGESTIONS_SHOW_USERS = APP_PATH + "/api/v1/suggestions/users.xml";
        /// <summary>	                                      
        /// Show only suggested groups. 	
        /// </summary>
        public static string YAMMER_SUGGESTIONS_SHOW_GROUPS = APP_PATH + "/api/v1/suggestions/groups.xml";
        /// <summary>
        /// Decline a suggestion.
        /// </summary>
        public static string YAMMER_SUGGESTIONS_DECLINE = APP_PATH + "/api/v1/suggestions/";

        #endregion

        #region Subscriptions
        /// <summary>
        /// Check to see if you are subscribed to the user of the given id.
        /// </summary>
        public static string YAMMER_SUBSCRIPTIONS_TO_USER = APP_PATH + "/api/v1/subscriptions/to_user/";
        /// <summary>
        /// Check to see if you are subscribed to the tag of the given id. 
        /// </summary>
        public static string YAMMER_SUBSCRIPTIONS_TO_TAG = APP_PATH + "/api/v1/subscriptions/to_tag/";
        /// <summary>
        /// Subscribe to a user or tag.
        /// </summary>
        public static string YAMMER_SUBSCRIPTIONS_SUBSCRIBE = APP_PATH + "/api/v1/subscriptions/";
        /// <summary>
        /// Unsubscribe to a user or tag.
        /// </summary>
        public static string YAMMER_SUBSCRIPTIONS_UNSUBSCRIBE = APP_PATH + "/api/v1/subscriptions/ ";

        #endregion

        #region AutoComplete

        /// <summary>
        /// Return typeahead suggestions for the prefix passed
        /// </summary>
        public static string YAMMER_AUTOCOMPLETE = APP_PATH + "/api/v1/autocomplete.xml";

        #endregion

       


    }
}
