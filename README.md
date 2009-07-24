<h2><span style="color:#4183C4">Usage</span></h2>

<p>Yammer uses <a href="http://oauth.net/" target="_blank">OAuth</a> to authenticate clients. This allows applications to securely access data on a user's behalf without storing or needing the user's password. 
Before you can use Yammer.Net, you must first obtain a client key and secret from Yammer.  To do this, log into Yammer and navigate to <a href="https://www.yammer.com/client_applications/new" target="_blank">https://www.yammer.com/client_applications/new</a>.  This form will allow you to register your application.  Once you complete and submit the form you will be provided with your client key and secret.  Make a note of these; you'll need them to make use of the library.</p>

<p>Now that you have your client key and secret you are ready to being using the framework.  Create a new project and add references to the OAuth and Yammer assemblies.  The oAuth dance has been simplified from previous versions. To get started using the wrapper, now you just need to handle a few events and call Session.Start():</p>

<pre><code>static void Menu_Connect(object sender, EventArgs e)
{
     Console.WriteLine("You will need to authorize this appliction to continue.\r\nWe'll open a browser window where you can login to\r\nYammer to complete the authorization.\r\nPress any key to continue");
     Console.Read();
     Yammer.Session.ReceiveRequestToken += new EventHandler(Session_ReceiveRequestToken);
     Yammer.Session.AuthorizationComplete += new EventHandler(Session_AuthorizationComplete);
     Yammer.Session.Start();
     Console.ReadLine();
}
</code></pre>

<p>Now you'll need to set up the even handlers:</p>

<pre><code>static void Session_AuthorizationComplete(object sender, EventArgs e)
{
     if (Yammer.Session.Auth.Success)
     {
          Console.WriteLine("Connected, Please enter a command:");
          Menu.Display(false);
     }
}

static void Session_ReceiveRequestToken(object sender, EventArgs e)
{
     string message = "Once you've logged in and authorized this application via your browser, please \r\nenter the provided code and press enter to start using Yammer.";
     Console.WriteLine(message);
     string code = Console.ReadLine();
     if (Yammer.Session.Auth != null)
          Yammer.Session.Auth.GetAccessToken(code);
}
</code></pre>

<p>That's about it for authorization, now you can start making calls to the Yammer API. The following sample source illustrates various calls to the API. The complete code is available in the project source:</p>

<pre><code>using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace TestConsole
{
    /// <summary>
    /// Yammer.Net example
    /// </summary>
    /// <example>
    /// yam myfeed
    /// yam sent
    /// yam post -m "this is a sample post"
    /// yam post -m "this is a sample post with an attachment" -a "c:\myImage.gif"
    /// yam post -m "this is a sample post with multiple attachments" -a "c:\myImage.gif;c:\myText.txt"
    /// yam viewuser -uid "kevin"
    /// yam viewuser -all
    /// yam currentuser
    /// yam updateuser -uid "kevin" -params "MobilePhone=602-555-5555"
    /// yam updateuser -uid "kevin" -params "MobilePhone=602-555-1111;Location=Phoenix"
    /// yam deleteuser -uid "elisabeth-poo-waller" 
    /// </example>
    class Program
    {
        static void Main(string[] args)
        {
            Menu.Connect += new EventHandler(Menu_Connect);
            Menu.RetrieveFeed += new EventHandler(Menu_RetrieveFeed);
            Menu.PostMessage += new InputEventHandler(Menu_PostMessage);
            Menu.ViewUser += new InputEventHandler(Menu_ViewUser);
            Menu.CurrentUser += new InputEventHandler(Menu_CurrentUser);
            Menu.UpdateUser += new InputEventHandler(Menu_UpdateUser);
            Menu.DeleteUser += new InputEventHandler(Menu_DeleteUser);
            Menu.Display(true);
        }

        static void Menu_Connect(object sender, EventArgs e)
        {
            Console.WriteLine("You will need to authorize this appliction to continue.\r\nWe'll open a browser window where you can login to\r\nYammer to complete the authorization.\r\nPress any key to continue");
            Console.Read();
            Yammer.Session.ReceiveRequestToken += new EventHandler(Session_ReceiveRequestToken);
            Yammer.Session.AuthorizationComplete += new EventHandler(Session_AuthorizationComplete);
            Yammer.Session.Start();
            Console.ReadLine();
        }

        static void Menu_RetrieveFeed(object sender, EventArgs e)
        {
            foreach (Yammer.Message msg in Menu.Messages)
            {
                object[] args = null;

                if (msg.Sender != null)
                    args = new object[] { msg.Sender.Name, msg.Body.Plain, msg.CreatedAt };
                else
                {
                    if (msg.Guide != null)
                        args = new object[] { msg.Guide.Name, msg.Body.Plain, msg.CreatedAt };
                }

                Console.WriteLine(string.Format("{0}\r\n{1}\r\n{2}\r\n", args));
            }
            Console.ReadLine();
        }

        static void Menu_PostMessage(InputEventArgs e)
        {
            MatchPattern[] patterns = new MatchPattern[] { new MatchPattern("-m", true), new MatchPattern("-a", true) };

            Dictionary<string, string> parameters = Menu.ParseInput(e.Input, patterns);

            string body = string.Empty;
            if (parameters.ContainsKey("-m"))
                body = parameters["-m"];

            List<string> attachmentList = new List<string>();
            if (parameters.ContainsKey("-a"))
                attachmentList.AddRange(parameters["-a"].Split(';'));

            //post message
            Yammer.Message.PostMessage(body, attachmentList);
        }

        static void Menu_ViewUser(InputEventArgs e)
        {
            MatchPattern[] patterns = new MatchPattern[] { new MatchPattern("-uid", true), new MatchPattern("-all", false) };
            Dictionary<string, string> parameters = Menu.ParseInput(e.Input, patterns);
            string uid = string.Empty;
            if (parameters.ContainsKey("-uid"))
            {
                uid = parameters["-uid"];
                Yammer.User user = Yammer.User.GetUserByUserName(uid);
                if (user != null)
                {
                    object[] args = new object[] { user.Name, user.JobTitle, user.MugshotUrl, user.WebUrl, user.Contact.PhoneNumbers[0].Number, user.Location };
                    Console.WriteLine(string.Format("UserName:{0}\r\nTitle:{1}\r\nAvatar:{2}\r\nUrl:{3}\r\nMobilePhone:{4}\r\nLocation:{5}", args));
                }
                else
                    Console.WriteLine("User not found");
            }
            else if (parameters.ContainsKey("-all"))
            {
                List<Yammer.User> users = Yammer.User.GetAllUsers();
                foreach (Yammer.User u in users)
                {
                    object[] args = new object[] { u.Name, u.JobTitle, u.MugshotUrl, u.WebUrl, u.Contact.PhoneNumbers.Count > 0 ? u.Contact.PhoneNumbers[0].Number : string.Empty, u.Location };
                    Console.WriteLine(string.Format("UserName:{0}\r\nTitle:{1}\r\nAvatar:{2}\r\nUrl:{3}\r\nMobilePhone:{4}\r\n\r\nLocation:{5}", args));
                }
            }

        }

        static void Menu_CurrentUser(InputEventArgs e)
        {
            string input = e.Input;
            Yammer.User user = Yammer.User.GetCurrentUser();
            object[] args = new object[] { user.Name, user.JobTitle, user.MugshotUrl, user.WebUrl, user.Contact.PhoneNumbers[0].Number, user.Location };
            Console.WriteLine(string.Format("UserName:{0}\r\nTitle:{1}\r\nAvatar:{2}\r\nUrl:{3}\r\nMobilePhone:{4}\r\nLocation:{5}", args));
        }

        static void Menu_UpdateUser(InputEventArgs e)
        {
            MatchPattern[] patterns = new MatchPattern[] { new MatchPattern("-uid", true), new MatchPattern("-params", true) };
            Dictionary<string, string> parameters = Menu.ParseInput(e.Input, patterns);

            string uid = string.Empty;
            if (parameters.ContainsKey("-uid"))
                uid = parameters["-uid"];

            Yammer.User user = null;
            if (uid != null && uid != string.Empty)
                user = Yammer.User.GetUserByUserName(uid);

            string attributes = string.Empty;
            Yammer.UserParameters userParams = null;
            List<PropertyInfo> properties = null;
            string[] props = null;

            if (parameters.ContainsKey("-params"))
            {
                attributes = parameters["-params"];
                props = attributes.Split(';');
                userParams = new Yammer.UserParameters();
                properties = new List<PropertyInfo>(userParams.GetType().GetProperties());
            }

            foreach (string prop in props)
            {
                string[] hash = prop.Split('=');

                PropertyInfo property = properties.Find(delegate(PropertyInfo p) { return p.Name == hash[0]; });
                if (property != null)
                    property.SetValue(userParams, hash[1], null);
            }

            if(user != null)
                user.Save(userParams);

        }

        static void Menu_DeleteUser(InputEventArgs e)
        {
            MatchPattern[] patterns = new MatchPattern[] { new MatchPattern("-uid", true) };
            Dictionary<string, string> parameters = Menu.ParseInput(e.Input, patterns);
            string uid = string.Empty;
            if (parameters.ContainsKey("-uid"))
                uid = parameters["-uid"];

            if(uid != null && uid != string.Empty)
                Yammer.User.GetUserByUserName(uid).Delete();
        }

        static void Session_AuthorizationComplete(object sender, EventArgs e)
        {
            if (Yammer.Session.Auth.Success)
            {
                Console.WriteLine("Connected, Please enter a command:");
                Menu.Display(false);
            }
        }

        static void Session_ReceiveRequestToken(object sender, EventArgs e)
        {
            string message = "Once you've logged in and authorized this application via your browser, please \r\nenter the provided code and press enter to start using Yammer.";
            Console.WriteLine(message);
            string code = Console.ReadLine();
            if (Yammer.Session.Auth != null)
                Yammer.Session.Auth.GetAccessToken(code);
        }


    }
}

</code></pre>



<h2><span style="color:#4183C4">License</span></h2>

<p><b>Copyright (c) 2009 Kevin Davie</b></p>

<p style="font-size:10pt;">
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:   The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.   
</p>

<p style="font-size:10pt;">
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
</p>
