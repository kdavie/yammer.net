<p>Yammer uses OAuth to authenticate clients. This allows applications to securely access data on a user's behalf without storing or needing the user's password. 
Before you can use Yammer.Net, you must first obtain a client key and secret from Yammer.  To do this, log into Yammer and navigate to https://www.yammer.com/client&#95;applications/new.  This form will allow you to register your application.  Once you complete and submit the form you will be provided with your client key and secret.  Make a note of these; you'll need them to make use of the library.</p>

<p>Now that you have your client key and secret you are ready to being using the framework.  Create a new project and add references to the OAuth and Yammer assemblies.  Yammer.Net uses a settings file to persist authorization information between your application sessions.  This file is stored in the user's Application Data folder under Yammer\Data.  The first thing you should do when your application loads is check if this file exists:</p>

<pre><code>using System;    
using System.Collections.Generic;    
using System.Linq;    
using System.Text;    
namespace YammerNetExample    
{
    class Program
    {
        const string CONSUMER&amp;#95;KEY = "myConsumerKey";
        const string CONSUMER&amp;#95;SECRET = "myConsumerSecret";
        static void Main(string[] args)
        {
            Yammer.Settings settings = Yammer.Settings.CheckConfiguration();
            if (settings == null)
            {
                //Need to configure client
            }
            else
            {
                //Client already configured, use persisted settings
            }
        }
    }
}
</code></pre>

<p>Configuring your client for first time use is a three step process.  First, you must obtain a request token from Yammer using your client key and secret.   The Yammer.Auth.GetRequestToken member accepts three arguments: proxy, consumerKey and consumerSecret .  If your client won't be behind a proxy, you can pass in a null for this argument.  After obtaining the request token you can allow the user to authorize your application.  If you would like the user to be redirected to your website after authorizing with Yammer, you can provide a value for the callbackUrl argument of the Yammer.Auth.Authorize member.</p>

<pre><code>private static Yammer.Auth GetRequestToken()
{
    Yammer.Auth auth = Yammer.Auth.GetRequestToken(null, CONSUMER&amp;#95;KEY, CONSUMER&amp;#95;SECRET);
    if (auth != null)
    {
        auth.Authorize(null);
        Console.WriteLine("We've opened up a browser so you can authenticate this application with Yammer.");
        Console.WriteLine("Once you've authenticated, press any key to continue.");
        Console.ReadLine();
    }
    return auth;
}
</code></pre>

<p>The framework will open a browser and allow the user to authorize your application.  Once the user authorizes through the Yammer website, your application can request its permanent access key and secret:</p>

<pre><code>private static void GetAccessToken(Yammer.Auth auth)
{
    if (auth != null)
        auth.GetAccessToken();

    if (auth.Success)
    {
        Console.WriteLine("Welcome to my Yammer application!");
        Console.ReadLine();
    }
}
</code></pre>

<p>After performing these three steps (GetRequestToken(), Authorize(), and GetAccessToken()), your application will be configured to communicate with Yammer.  The next step is to start making calls to the Yammer API.  To continue with my console example, I've added some code to handle a few menu options:</p>

<pre><code>public static void ReadMessages(List&lt;Yammer.Message&gt; messages)
{
    Console.WriteLine();
    foreach (Yammer.Message msg in messages)
    {
        string body = msg.Body.Plain;
        string timeStamp = msg.CreatedAt;

        Yammer.User sender = null;
        Yammer.Guide guide = null;
        Yammer.Message repliedToMessage = null;
        Yammer.User repliedToUser = null;

        //if message sent from user, store user information
        if (msg.SenderType.ToUpper() == Yammer.SenderType.USER.ToString())
            sender = msg.References.Users.Find(delegate(Yammer.User u) { return u.Id == msg.SenderId.ToString(); });

        //if message sent from guide, store guid information
        if (msg.SenderType.ToUpper() == Yammer.SenderType.SYSTEM.ToString()) 
            guide = msg.References.Guide;

        //if message is a reply, store replied-to-message
        if (msg.RepliedToId != null &amp;&amp; msg.RepliedToId != string.Empty)
            repliedToMessage = msg.References.Messages.Find(delegate(Yammer.Message m) { return m.Id == msg.RepliedToId; });

        //if reply-to-message exists, store replied-to-user
        if(repliedToMessage != null)
            repliedToUser = msg.References.Users.Find(delegate(Yammer.User u) { return int.Parse(u.Id) == repliedToMessage.SenderId; });

        StringBuilder sb = new StringBuilder();
        //Write sender name
        if(sender != null)
            sb.Append(sender.FullName);
        //Write guid name
        if (guide != null)
            sb.Append(guide.FullName);
        //Write replied-to-user name
        if (repliedToUser != null)
            sb.Append(" in-reply-to: " + repliedToUser.FullName);
        sb.AppendLine();

        //Write message body
        sb.AppendLine(body);

        //Write attachments
        if (msg.Attachments.Count &gt; 0)
            foreach (Yammer.Attachment attachment in msg.Attachments)
                sb.AppendLine(attachment.Name);

        //Write timestamp
        sb.AppendLine(timeStamp);

        Console.WriteLine(sb.ToString());
    }
}


public static void PostMessage(string input, Yammer.Session session)
{
    //parse message
    string pattern = "-m\\s\"+.+?\"";
    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern, System.Text.RegularExpressions.RegexOptions.Singleline);
    System.Text.RegularExpressions.Match message = regex.Match(input);
    string body = string.Empty;
    if (message != null)
        body = message.Value.Replace("-m", "").Trim().Replace("\"","");

    //parse attachments
    pattern = "-a\\s\"+.+?\"";
    regex = new System.Text.RegularExpressions.Regex(pattern, System.Text.RegularExpressions.RegexOptions.Singleline);
    List&lt;string&gt; attachmentList = new List&lt;string&gt;();
    if (regex.IsMatch(input))
    {
        message = regex.Match(input);
        attachmentList.AddRange(message.Value.Replace("-a", "").Trim().Replace("\"", "").Split(';'));
    }

    //post message
    Yammer.ApiWrapper.PostMessage(body, session, attachmentList);
}
</code></pre>

<p>The Yammer.ApiWrapper.PostMessage member accepts a list of attachments.  This list should be a string of paths to attach with the message.  The follow is the complete source for the example console app used to illustrate these steps:</p>

<pre><code>using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace YammerNetExample
{
    /// &lt;summary&gt;
    /// Yammer.Net example
    /// &lt;/summary&gt;
    /// &lt;example&gt;
    /// yam myfeed
    /// yam post -m "this is a sample post"
    /// yam post -m "this is a sample post with an attachment" -a "c:\myImage.gif"
    /// yam post -m "this is a sample post with multiple attachments" -a "c:\myImage.gif;c:\myText.txt"
    /// &lt;/example&gt;
    class Program
    {
        const string CONSUMER&amp;#95;KEY = "myConsumerKey";
        const string CONSUMER&amp;#95;SECRET = "myConsumerSecret";
        const string RETRIEVE&amp;#95;MY&amp;#95;FEED = "yam myfeed";
        const string POST&amp;#95;MESSAGE = "yam post";
        static void Main(string[] args)
        {
            Yammer.Settings settings = Yammer.Settings.CheckConfiguration();
            if (settings == null)
            {
                Yammer.Auth auth = GetRequestToken();
                GetAccessToken(auth);
            }
            else
                Menu(true, ConfigureClient(settings), null, null);
        }

        private static OAuth.OAuthKey ConfigureClient(Yammer.Settings settings)
        {
            OAuth.OAuthKey key = new OAuth.OAuthKey(CONSUMER&amp;#95;KEY, CONSUMER&amp;#95;SECRET, settings.OAuth.TokenKey, settings.OAuth.TokenSecret);
            WebProxy proxy = null;
            if (settings.Proxy.Enable)
            {
                proxy = new System.Net.WebProxy();
                proxy.Address = new Uri(settings.Proxy.Address + ":" + settings.Proxy.Port);
                proxy.Credentials = new NetworkCredential(settings.Proxy.Id, settings.Proxy.Password);
            }
            return key;
        }

        public static Yammer.Auth GetRequestToken()
        {
        Yammer.Auth auth = Yammer.Auth.GetRequestToken(null, CONSUMER&amp;#95;KEY, CONSUMER&amp;#95;SECRET);
        if (auth != null)
        {
            auth.Authorize(null);
            Console.WriteLine("We've opened up a browser so you can authenticate this application with Yammer.");
            Console.WriteLine("Once you've authenticated, press any key to continue.");
            Console.ReadLine();
        }
        return auth;
        }

        public static void GetAccessToken(Yammer.Auth auth)
        {
            if (auth != null)
                auth.GetAccessToken();

            if (auth.Success)
                Menu(true, auth.Key, null, null);

        }

        public static void Menu(bool welcome, OAuth.OAuthKey key, WebProxy proxy, Yammer.Session session)
        {
            if(session == null)
                session = new Yammer.Session(key, proxy);

            if (welcome)
            {
                Console.WriteLine("Welcome to my Yammer application!");
                welcome = false;
            }

            string input = Console.ReadLine();
            string option = ParseInput(input);

            switch (option)
            {
                case RETRIEVE&amp;#95;MY&amp;#95;FEED:
                    ReadMessages(Yammer.ApiWrapper.GetFollowingMessages(session));
                    break;
                case POST&amp;#95;MESSAGE:
                    PostMessage(input, session);
                    break;
            }

            Menu(welcome, key, proxy, session);

        }

        public static string ParseInput(string input)
        {
            if (input.ToLower().Contains(RETRIEVE&amp;#95;MY&amp;#95;FEED))
                return RETRIEVE&amp;#95;MY&amp;#95;FEED;

            if (input.ToLower().Contains(POST&amp;#95;MESSAGE))
                return POST&amp;#95;MESSAGE;

            return null;
        }

        public static void ReadMessages(List&lt;Yammer.Message&gt; messages)
        {
            Console.WriteLine();
            foreach (Yammer.Message msg in messages)
            {
                string body = msg.Body.Plain;
                string timeStamp = msg.CreatedAt;

                Yammer.User sender = null;
                Yammer.Guide guide = null;
                Yammer.Message repliedToMessage = null;
                Yammer.User repliedToUser = null;

                //if message sent from user store user information
                if (msg.SenderType.ToUpper() == Yammer.SenderType.USER.ToString())
                    sender = msg.References.Users.Find(delegate(Yammer.User u) { return u.Id == msg.SenderId.ToString(); });

                //if message sent from guide store guid information
                if (msg.SenderType.ToUpper() == Yammer.SenderType.SYSTEM.ToString()) 
                    guide = msg.References.Guide;

                //if message is a reply, store replied-to-message
                if (msg.RepliedToId != null &amp;&amp; msg.RepliedToId != string.Empty)
                    repliedToMessage = msg.References.Messages.Find(delegate(Yammer.Message m) { return m.Id == msg.RepliedToId; });

                //if reply-to-message exists, store replied-to-user
                if(repliedToMessage != null)
                    repliedToUser = msg.References.Users.Find(delegate(Yammer.User u) { return int.Parse(u.Id) == repliedToMessage.SenderId; });

                StringBuilder sb = new StringBuilder();
                //Write sender name
                if(sender != null)
                    sb.Append(sender.FullName);
                //Write guid name
                if (guide != null)
                    sb.Append(guide.FullName);
                //Write replied-to-user name
                if (repliedToUser != null)
                    sb.Append(" in-reply-to: " + repliedToUser.FullName);
                sb.AppendLine();

                //Write message body
                sb.AppendLine(body);

                //Write attachments
                if (msg.Attachments.Count &gt; 0)
                    foreach (Yammer.Attachment attachment in msg.Attachments)
                        sb.AppendLine(attachment.Name);

                //Write timestamp
                sb.AppendLine(timeStamp);

                Console.WriteLine(sb.ToString());
            }
        }


        public static void PostMessage(string input, Yammer.Session session)
        {
            //parse message
            string pattern = "-m\\s\"+.+?\"";
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern, System.Text.RegularExpressions.RegexOptions.Singleline);
            System.Text.RegularExpressions.Match message = regex.Match(input);
            string body = string.Empty;
            if (message != null)
                body = message.Value.Replace("-m", "").Trim().Replace("\"","");

            //parse attachments
            pattern = "-a\\s\"+.+?\"";
            regex = new System.Text.RegularExpressions.Regex(pattern, System.Text.RegularExpressions.RegexOptions.Singleline);
            List&lt;string&gt; attachmentList = new List&lt;string&gt;();
            if (regex.IsMatch(input))
            {
                message = regex.Match(input);
                attachmentList.AddRange(message.Value.Replace("-a", "").Trim().Replace("\"", "").Split(';'));
            }

            //post message
            Yammer.ApiWrapper.PostMessage(body, session, attachmentList);
        }
    }
}
</code></pre>