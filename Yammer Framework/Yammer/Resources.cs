using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yammer
{
    public static class Resources
    {

        #region oAuth

        public static string OAUTH_REQUEST_TOKEN = "https://www.yammer.com/oauth/request_token";
        public static string OAUTH_AUTHORIZE = "https://www.yammer.com/oauth/authorize";
        public static string OAUTH_ACCESS_TOKEN = "https://www.yammer.com/oauth/access_token";

        #endregion

        #region Messages

        /// <summary>
        /// All messages in this network. Corresponds to the "All" tab on the website
        /// </summary>
        public static string YAMMER_MESSAGES_ALL = "https://www.yammer.com/api/v1/messages.xml";
        /// <summary>
        /// Alias for /api/v1/messages/from_user/logged-in_user_id.format Corresponds to the "Sent" tab on the website.
        /// </summary>
        public static string YAMMER_MESSAGES_SENT = "https://www.yammer.com/api/v1/messages/sent.xml";
        /// <summary>
        /// Messages received by the logged-in user. Corresponds to the "Received" tab on the website.
        /// </summary>
        public static string YAMMER_MESSAGES_RECEIVED = "https://www.yammer.com/api/v1/messages/received.xml";
        /// <summary>
        /// Messages sent by the user with the given ID. Corresponds to the messages on a user profile page on the website.
        /// </summary>
        public static string YAMMER_MESSAGES_FOLLOWING = "https://www.yammer.com/api/v1/messages/following.xml";
        /// <summary>
        /// Messages sent by the user with the given ID. Corresponds to the messages on a user profile page on the website.
        /// </summary>
        public static string YAMMER_MESSAGES_SENT_BY = "https://www.yammer.com/api/v1/messages/from_user/";
        /// <summary>
        /// Messages including the tag with given ID. Corresponds to the messages on a tag's page on the website.
        /// </summary>
        public static string YAMMER_MESSAGES_TAGGED_WITH = "https://www.yammer.com/api/v1/messages/tagged_with/";
        /// <summary>
        /// Messages in the thread with the given ID. Corresponds to the page you'd see when clicking on "in reply to" on the website.
        /// </summary>
        public static string YAMMER_MESSAGES_IN_THREAD = "https://www.yammer.com/api/v1/messages/in_thread/";
        /// <summary>
        /// Messages in the group with the given ID. Corresponds to the messages you'd see on a group's profile page.
        /// </summary>
        public static string YAMMER_MESSAGES_IN_GROUP = "https://www.yammer.com/api/v1/messages/in_group/";
        /// <summary>
        /// Favorite messages of the given user ID. Can pass 'current' in place of user_id for currently logged in user
        /// </summary>
        public static string YAMMER_MESSAGES_FAVORITES_OF = "https://www.yammer.com/api/v1/messages/favorites_of/";
        /// <summary>
        /// Create a new message.
        /// </summary>
        public static string YAMMER_MESSAGES_CREATE = "https://www.yammer.com/api/v1/messages/";
        /// <summary>
        /// Delete a message owned by the current user.
        /// </summary>
        public static string YAMMER_MESSAGES_DELETE = "https://www.yammer.com/api/v1/messages/";
        /// <summary>
        /// Add a message to user_id's favorite messages.
       /// </summary>
        public static string YAMMER_MESSAGES_ADD_FAVORITE = "https://www.yammer.com/api/v1/favorites_of/";
        /// <summary>
        /// Remove a favorite.
        /// </summary>
        public static string YAMMER_MESSAGES_DELETE_FAVORITE = "https://www.yammer.com/api/v1/favorites_of/";

        #endregion

        #region Group

        /// <summary>
        /// A list of groups. 
        /// </summary>
        public static string YAMMER_GROUP_LIST = "https://www.yammer.com/api/v1/groups.xml";
        /// <summary>
        /// https://www.yammer.com/api/v1/groups/id.format
        /// </summary>
        public static string YAMMER_GROUP_DATA = "https://www.yammer.com/api/v1/groups/";

        #endregion

        #region Tags

        /// <summary>
        /// Tags in this network. NOT YET IMPLEMENTED
        /// </summary>
        public static string YAMMER_TAGS = "https://www.yammer.com/api/v1/tags.xml";
        /// <summary>
        /// View data about one tag.
        /// </summary>
        public static string YAMMER_TAGS_DATA = "https://www.yammer.com/api/v1/tags/";

        #endregion

        #region Users

        /// <summary>
        /// Users in this network.
        /// </summary>
        public static string YAMMER_USERS_ALL = "https://www.yammer.com/api/v1/users.xml";
        /// <summary>
        /// View data about one user. 	
        /// </summary>
        public static string YAMMER_USERS_SINGLE = "https://www.yammer.com/api/v1/users/";
        /// <summary>
        /// Alias to /api/v1/users/current user's id.format.
        /// </summary>
        public static string YAMMER_USERS_CURRENT = "https://www.yammer.com/api/v1/users/current.xml";

        #endregion

        #region Group Membership

        /// <summary>
        /// Join a group.
        /// </summary>
        public static string YAMMER_GROUP_JOIN = "https://www.yammer.com/api/v1/group_memberships/";
        /// <summary>
        /// Leave a group.
        /// </summary>
        public static string YAMMER_GROUP_RESIGN = "https://www.yammer.com/api/v1/group_memberships/";
         
        #endregion

        #region Relationships

        /// <summary>
        /// Show org chart relationships.
        /// </summary>
        public static string YAMMER_RELATIONSHIPS_ALL = "https://www.yammer.com/api/v1/relationships.xml";
        /// <summary>
        /// Add an org chart relationship.
        /// </summary>
        public static string YAMMER_RELATIONSHIPS_CREATE = "https://www.yammer.com/api/v1/relationships.xml";
        /// <summary>
        /// Remove a relationship.
        /// </summary>
        public static string YAMMER_RELATIONSHIPS_DELETE = "https://www.yammer.com/api/v1/relationships/";

        #endregion

        #region Suggestions

        /// <summary>
        /// Show suggested groups and users. 
        /// </summary>
        public static string YAMMER_SUGGESTIONS_SHOW_ALL = "https://www.yammer.com/api/v1/suggestions.xml";
        /// <summary>
        /// Show only suggested users. 
        /// </summary>
        public static string YAMMER_SUGGESTIONS_SHOW_USERS = "https://www.yammer.com/api/v1/suggestions/users.xml";
        /// <summary>
        /// Show only suggested groups. 	
        /// </summary>
        public static string YAMMER_SUGGESTIONS_SHOW_GROUPS = "https://www.yammer.com/api/v1/suggestions/groups.xml";
        /// <summary>
        /// Decline a suggestion.
        /// </summary>
        public static string YAMMER_SUGGESTIONS_DECLINE = "https://www.yammer.com/api/v1/suggestions/";

        #endregion

        #region Subscriptions
        /// <summary>
        /// Check to see if you are subscribed to the user of the given id.
        /// </summary>
        public static string YAMMER_SUBSCRIPTIONS_TO_USER = "https://www.yammer.com/api/v1/subscriptions/to_user/";
        /// <summary>
        /// Check to see if you are subscribed to the tag of the given id. 
        /// </summary>
        public static string YAMMER_SUBSCRIPTIONS_TO_TAG = "https://www.yammer.com/api/v1/subscriptions/to_tag/";
        /// <summary>
        /// Subscribe to a user or tag.
        /// </summary>
        public static string YAMMER_SUBSCRIPTIONS_SUBSCRIBE = "https://www.yammer.com/api/v1/subscriptions/";
        /// <summary>
        /// Unsubscribe to a user or tag.
        /// </summary>
        public static string YAMMER_SUBSCRIPTIONS_UNSUBSCRIBE = "https://www.yammer.com/api/v1/subscriptions/ ";

        #endregion

        #region AutoComplete

        /// <summary>
        /// Return typeahead suggestions for the prefix passed
        /// </summary>
        public static string YAMMER_AUTOCOMPLETE = "https://www.yammer.com/api/v1/autocomplete.xml";

        #endregion

       


    }
}
