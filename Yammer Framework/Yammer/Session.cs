using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using OAuth;
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
        }
    }
}
