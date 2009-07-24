using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using OAuth;
using System.Threading;
using System.Windows.Forms;
using threading = System.Threading;
namespace Yammer
{
    public class Session
    {
        public WebProxy Proxy { get; set; }
        public OAuthKey AuthKey { get; set; }

        
        public Session(OAuthKey key, WebProxy proxy)
        {
            this.AuthKey = key;
            this.Proxy = proxy;
            Session.Assets = new Assets(this);
        }

        public static Assets Assets { get; set; }

        //New Code************

        static Session()
        {
            
        }

        static void Auth_AuthorizationComplete(object sender, EventArgs e)
        {
            OnAuthorizationComplete();
        }
        public static  event EventHandler AuthorizationComplete;
        public static void OnAuthorizationComplete()
        {
            if (Session.AuthorizationComplete != null)
                Session.AuthorizationComplete(null, new EventArgs());
        }

        public static void Start()
        {
            Start(false, null, null, null, null);
        }

        public static void Start(bool useProxy, string address, string port, string username, string password)
        {
            Login(useProxy, address, port, username, password);
        }

        public static Yammer.Settings Settings { get; set; }

        private static Yammer.Auth auth;
        public static Yammer.Auth Auth 
        {
            get
            {
                if (auth == null)
                    CheckAuth();

                return auth;
            }
            set
            {
                auth = value;
                auth.AuthorizationComplete += new EventHandler(Auth_AuthorizationComplete);
            }
        }

        public static WebProxy WebProxy { get; set; }

        private static void Login(bool useProxy, string address, string port, string username, string password)
        {
            
            Session.WebProxy = null;
            bool validProxy;

            if (useProxy)
                validProxy = ValidateProxySettings(address, port, username, password);
            else
                validProxy = true;


            if (validProxy)
            {
                try
                {
                    if (useProxy)
                    {
                        Session.WebProxy = new System.Net.WebProxy();
                        Session.WebProxy.Address = new Uri(address + ":" + port);
                        Session.WebProxy.Credentials = new NetworkCredential(username, password);
                    }

                    threading.Thread thread = new threading.Thread(new ThreadStart(GetRequestToken));
                    thread.Start();
                }
                catch (Exception ex)
                {
                    Yammer.Utility.SetErrorLog(ex.Message);
                }
            }
            else
                MessageBox.Show("If you are using a proxy-server please add values for each setting.");
        }

        private static bool ValidateProxySettings(string address, string port, string username, string password)
        {
            if (address == null || port == null || username == null || password == null)
                return false;

            if (address == string.Empty || port == string.Empty || username == string.Empty || password == string.Empty)
                return false;

            return true;
        }

        private static void GetRequestToken()
        {
            string ck = System.Configuration.ConfigurationSettings.AppSettings["CONSUMER_KEY"];
            string cs = System.Configuration.ConfigurationSettings.AppSettings["CONSUMER_SECRET"];
            Session.Auth = Yammer.Auth.GetRequestToken(Session.WebProxy, ck, cs);
            //TODO: Allow callback URL
            if (Session.Auth != null)
                Session.Auth.Authorize(null);

            Session.OnReceiveRequestToken();
        }

        public static event EventHandler ReceiveRequestToken;

        static void OnReceiveRequestToken()
        {
            if (Session.ReceiveRequestToken != null)
                Session.ReceiveRequestToken(null, new EventArgs()); 
        }

        private static void CheckAuth()
        {
            string ck = System.Configuration.ConfigurationSettings.AppSettings["CONSUMER_KEY"];
            string cs = System.Configuration.ConfigurationSettings.AppSettings["CONSUMER_SECRET"];
            Yammer.Settings settings = Yammer.Settings.CheckConfiguration();
            if (settings != null)
            {
                Yammer.Session.Auth = new Auth();                
                Yammer.Session.Auth.Key = new OAuthKey(ck, cs, settings.OAuth.TokenKey, settings.OAuth.TokenSecret);
            }

        }
    }
}
