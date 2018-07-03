using EasyReproTest.Extensions;
using log4net;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Dynamics365.UIAutomation.Api;
using Microsoft.Dynamics365.UIAutomation.Browser;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace EasyReproTest
{
    public class ContactTestsAdvanced
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string _instrumentationKey;

        public ContactTestsAdvanced()
        {
            _instrumentationKey = Environment.GetEnvironmentVariable("APP_INSIGHTS_INST_KEY");
            TelemetryConfiguration.Active.InstrumentationKey = _instrumentationKey;
        }

        [Fact]
        public void CreateContact()
        {
            var executionId = Guid.NewGuid().ToString();

            _log.Info("1. Create instance of the browser");
            using (var xrmBrowser = new Browser(new BrowserOptions() { BrowserType=BrowserType.Chrome }))
            {
                // Try to get these values from environment variables. If they're not available, then predefined values will be used
                var urlStr = Environment.GetEnvironmentVariable("D365_URL") ?? "http://acme.crm.dynamics.com";
                var userNameStr = Environment.GetEnvironmentVariable("D365_USER") ?? "admin@acme.onmicrosoft.com";
                var pwdStr = Environment.GetEnvironmentVariable("D365_PWD") ?? "Password@12345";

                xrmBrowser.LogAndScreenShot($"Loging to {urlStr}...");

                var url = new Uri(urlStr);
                var userName = userNameStr.ToSecureString();
                var pwd = pwdStr.ToSecureString();

                xrmBrowser.LogAndScreenShot("2. Log-in to Dynamics 365");
                xrmBrowser.LoginPage.Login(url, userName, pwd);

                xrmBrowser.ThinkTime(1000);

                var perf = xrmBrowser.PerformanceCenter;
                if (!perf.IsEnabled)
                    perf.IsEnabled = true;

                xrmBrowser.LogAndScreenShot("3. Go to Sales/Contacts using the Sitemap");
                xrmBrowser.Navigation.OpenSubArea("Sales", "Contacts");

                xrmBrowser.LogAndScreenShot("4. Change the active view");
                xrmBrowser.Grid.SwitchView("Active Contacts");

                _log.Info("5. Click on the 'New' button");
                xrmBrowser.CommandBar.ClickCommand("New");

                var fields = new List<Field>
                {
                    new Field() {Id = "firstname", Value = "Test"},
                    new Field() {Id = "lastname", Value = "Contact"}
                };

                xrmBrowser.LogAndScreenShot("6. Set the attribute values in the form");
                xrmBrowser.Entity.SetValue(new CompositeControl() { Id = "fullname", Fields = fields });
                xrmBrowser.Entity.SetValue("emailaddress1", "test@contoso.com");
                xrmBrowser.Entity.SetValue("mobilephone", "555-555-5555");
                xrmBrowser.Entity.SetValue("birthdate", DateTime.Parse("11/1/1980"));
                xrmBrowser.Entity.SetValue(new OptionSet { Name = "preferredcontactmethodcode", Value = "Email" });

                xrmBrowser.LogAndScreenShot("7. Save the new record");
                xrmBrowser.CommandBar.ClickCommand("Save");

                var perfResults = perf.GetMarkers().Value;

                new Telemetry().AzureKey(_instrumentationKey)
                    .ExecutionId(executionId)
                    .RequestId(perf.GetRequestId())
                    .TrackEvents(perfResults.Select(x => x.Value).ToList());
            }
        }
    }


}
