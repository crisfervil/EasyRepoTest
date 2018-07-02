using Microsoft.Dynamics365.UIAutomation.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyReproTest.MsTests
{
    public class TestBase
    {

        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        private int screenshotsCount = 0;
        public void TakeScreenShot(Browser browser)
        {
            var screenshotsDir = "screenshots";
            System.IO.Directory.CreateDirectory(screenshotsDir);
            var path = $"{screenshotsDir}/screen{++screenshotsCount}.png";
            Console.WriteLine($"Screenshot: {path}");

            browser.TakeWindowScreenShot(path, OpenQA.Selenium.ScreenshotImageFormat.Png);
            TestContext.AddResultFile(path);
        }

    }
}
