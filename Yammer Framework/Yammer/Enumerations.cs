using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yammer
{
    public enum AttachmentType
    {
        NONE,
        IMAGE,
        FILE
    }

    public enum SenderType
    {
        USER,
        SYSTEM
    }

    public enum MessageType
    {
        SYSTEM,
        UPDATE
    }

    public enum ObjectType
    {
        NONE,
        MESSAGE,
        USER,
        TAG,
        THREAD,
        GROUP,
        GUIDE,
        TOPIC
    }

    public enum RelationshipType
    {
        SUBORDINATE,
        SUPERIOR, 
        COLLEAGUE
    }

    public enum PageFlag
    {
        OLDER_THAN,
        NEWER_THAN
    }

    public enum WebMethod
    {
        GET,
        POST,
        DELETE,
        PUT
    }

    public enum SortBy
    {
        NONE,
        MESSAGES,
        MEMBERS,
        PRIVACY,
        CREATED_AT,
        CREATOR,
        FOLLOWERS
    }

    public enum SuggestionType
    {
        USER,
        GROUP
    }
}
