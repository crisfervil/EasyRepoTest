using Microsoft.Dynamics365.UIAutomation.Api;
using Microsoft.Dynamics365.UIAutomation.Browser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EasyReproTest
{
    public class GlobalSearchTest
    {
        [Fact]
        public void WEBTestGlobalSearch()
        {
            using (var xrmBrowser = new Browser(BrowserType.Chrome))
            {
                var urlStr = Environment.GetEnvironmentVariable("D365_URL") ?? "http://acme.crm.dynamics.com";
                var userNameStr = Environment.GetEnvironmentVariable("D365_USER") ?? "admin@acme.onmicrosoft.com";
                var pwdStr = Environment.GetEnvironmentVariable("D365_PWD") ?? "Password@12345";

                var url = new Uri(urlStr);
                var userName = userNameStr.ToSecureString();
                var pwd = pwdStr.ToSecureString();

                xrmBrowser.LoginPage.Login(url, userName, pwd);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                xrmBrowser.ThinkTime(500);

                xrmBrowser.Navigation.GlobalSearch("contoso");

                xrmBrowser.GlobalSearch.Search("Contoso");
                xrmBrowser.ThinkTime(4000);
            }
        }
    }
}
