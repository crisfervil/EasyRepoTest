using System;
using System.Collections.Generic;
using Microsoft.Dynamics365.UIAutomation.Api;
using Microsoft.Dynamics365.UIAutomation.Browser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyReproTest.MsTests
{
    [TestClass]
    public class ContactTests : TestBase
    {
        [TestMethod]
        public void CreateContact()
        {
            // 1. Create instance of the browser
            using (var xrmBrowser = new Browser(new BrowserOptions() { BrowserType = BrowserType.Chrome }))
            {
                // to run this from command line:
                // ..\packages\xunit.runner.console.2.3.1\tools\net452\xunit.console.exe bin\Debug\EasyReproTest.dll

                // Try to get these values from environment variables. If they're not available, then predefined values will be used
                var urlStr = Environment.GetEnvironmentVariable("D365_URL") ?? "http://acme.crm.dynamics.com";
                var userNameStr = Environment.GetEnvironmentVariable("D365_USER") ?? "admin@acme.onmicrosoft.com";
                var pwdStr = Environment.GetEnvironmentVariable("D365_PWD") ?? "Password@12345";
                Console.WriteLine($"Loging to {urlStr}...");

                var url = new Uri(urlStr);
                var userName = userNameStr.ToSecureString();
                var pwd = pwdStr.ToSecureString();

                // 2. Log-in to Dynamics 365
                xrmBrowser.LoginPage.Login(url, userName, pwd);
                TakeScreenShot(xrmBrowser);

                xrmBrowser.ThinkTime(1000);

                //Console.WriteLine($"GuidedHelp:{xrmBrowser.GuidedHelp.IsEnabled}");
                //xrmBrowser.GuidedHelp.CloseGuidedHelp();
                //xrmBrowser.GuidedHelp.CloseGuidedHelp();

                //xrmBrowser.ThinkTime(1000);

                // 3. Go to Sales/Accounts using the Sitemap
                xrmBrowser.Navigation.OpenSubArea("Sales", "Contacts");
                TakeScreenShot(xrmBrowser);

                //xrmBrowser.ThinkTime(2000);

                // 4. Change the active view
                xrmBrowser.Grid.SwitchView("Active Contacts");
                TakeScreenShot(xrmBrowser);

                //xrmBrowser.ThinkTime(500);
                //5. Click on the "New" button
                xrmBrowser.CommandBar.ClickCommand("New");

                //xrmBrowser.ThinkTime(2000);

                var fields = new List<Field>
                {
                    new Field() {Id = "firstname", Value = "Test"},
                    new Field() {Id = "lastname", Value = "Contact"}
                };
                //6. Set the attribute values in the form
                xrmBrowser.Entity.SetValue(new CompositeControl() { Id = "fullname", Fields = fields });
                xrmBrowser.Entity.SetValue("emailaddress1", "test@contoso.com");
                xrmBrowser.Entity.SetValue("mobilephone", "555-555-5555");
                xrmBrowser.Entity.SetValue("birthdate", DateTime.Parse("11/1/1980"));
                xrmBrowser.Entity.SetValue(new OptionSet { Name = "preferredcontactmethodcode", Value = "Email" });
                TakeScreenShot(xrmBrowser);

                //7. Save the new record
                xrmBrowser.CommandBar.ClickCommand("Save");
                TakeScreenShot(xrmBrowser);
            }
        }
    }
}
