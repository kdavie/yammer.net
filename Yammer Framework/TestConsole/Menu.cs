using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsole
{
    public class Menu
    {
        #region Consts

        const string RETRIEVE_MY_FEED = "yam myfeed";
        const string RETRIEVE_SENT = "yam sent";
        const string POST_MESSAGE = "yam post";
        const string VIEW_USER = "yam viewuser";
        const string CURRENT_USER = "yam currentuser";
        const string UPDATE_USER = "yam updateuser";
        const string DELETE_USER = "yam deleteuser";
        const string CREATE_USER = "yam createuser";

        #endregion

        #region Properties

        public static List<Yammer.Message> Messages { get; set; }

        #endregion

        #region ctor

        static Menu()
        {
        }

        #endregion

        #region Public Functions

        public static void Display(bool welcome)
        {
            if (welcome)
                Console.WriteLine("Welcome to my Yammer application!");

            welcome = false;

            if (Yammer.Session.Auth == null)
                OnConnect();
            else
            {
                string input = Console.ReadLine();
                string option = ResolveInput(input);

                switch (option)
                {
                    case RETRIEVE_MY_FEED:
                        Messages = Yammer.Message.GetFollowingMessages();
                        OnRetrieveFeed();
                        break;
                    case RETRIEVE_SENT:
                        Messages =  Yammer.Message.GetSentMessages();
                        OnRetrieveFeed();
                        break;
                    case POST_MESSAGE:
                        OnPostMessage(new InputEventArgs(input));
                        break;
                    case VIEW_USER:
                        OnViewUser(new InputEventArgs(input));
                        break;
                    case CURRENT_USER:
                        OnCurrentUser(new InputEventArgs(input));
                        break;
                    case UPDATE_USER:
                        OnUpdateUser(new InputEventArgs(input));
                        break;
                    case DELETE_USER:
                        OnDeleteUser(new InputEventArgs(input));

                        break;
                }
                Menu.Display(welcome);
            }
        }

        private static string ResolveInput(string input)
        {
            if (input.ToLower().Contains(RETRIEVE_SENT))
                return RETRIEVE_SENT;

            if (input.ToLower().Contains(RETRIEVE_MY_FEED))
                return RETRIEVE_MY_FEED;

            if (input.ToLower().Contains(POST_MESSAGE))
                return POST_MESSAGE;

            if (input.ToLower().Contains(VIEW_USER))
                return VIEW_USER;

            if (input.ToLower().Contains(CURRENT_USER))
                return CURRENT_USER;

            if (input.ToLower().Contains(UPDATE_USER))
                return UPDATE_USER;

            if (input.ToLower().Contains(DELETE_USER))
                return DELETE_USER;

            return null;
        }

        public static Dictionary<string, string> ParseInput(string input, MatchPattern[] switches)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            foreach (MatchPattern mp in switches)
            {
                string pattern = mp.HasValue ? mp.Key + mp.Value : mp.Key;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern, System.Text.RegularExpressions.RegexOptions.Singleline);
                System.Text.RegularExpressions.Match match = regex.Match(input);
                string output = string.Empty;
                if (match.Success)
                    parameters.Add(mp.Key, match.Value.Replace(mp.Key, "").Trim().Replace("\"", ""));
            }
            return parameters;
        }

        #endregion

        #region Events

        public static event EventHandler Connect;
        public static void OnConnect()
        {
            if (Connect != null)
                Connect(null, new EventArgs());
        }

        public static event EventHandler RetrieveFeed;
        public static void OnRetrieveFeed()
        {
            if (RetrieveFeed != null)
                RetrieveFeed(null, new EventArgs());
        }

        public static event InputEventHandler PostMessage;
        public static void OnPostMessage(InputEventArgs e)
        {
            if (PostMessage != null)
                PostMessage(e);
        }

        public static event InputEventHandler ViewUser;
        public static void OnViewUser(InputEventArgs e)
        {
            if (ViewUser != null)
                ViewUser( e);
        }

        public static event InputEventHandler CurrentUser;
        public static void OnCurrentUser(InputEventArgs e)
        {
            if (CurrentUser != null)
                CurrentUser(e);
        }

        public static event InputEventHandler UpdateUser;
        public static void OnUpdateUser(InputEventArgs e)
        {
            if (UpdateUser != null)
                UpdateUser(e);
        }

        public static event InputEventHandler DeleteUser;
        public static void OnDeleteUser(InputEventArgs e)
        {
            if (DeleteUser != null)
                DeleteUser(e);
        }

        #endregion

    }

    public delegate void InputEventHandler(InputEventArgs e);
    public class InputEventArgs : EventArgs
    {
        public string Input { get; set; }

        public InputEventArgs(string input)
        {
            this.Input = input;
        }
    }

    public class MatchPattern
    {
        public MatchPattern(string key, bool hasValue)
        {
            this.Key = key;
            this.HasValue = hasValue;
        }
        public string Key { get; set; }
        public string Value { get { return "\\s\"+.+?\""; } }
        public bool HasValue { get; set; }
    }

        
}
