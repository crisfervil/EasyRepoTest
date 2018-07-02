﻿using Microsoft.Dynamics365.UIAutomation.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyReproTest.Extensions
{
    public static class BrowserExtensions
    {
        private static int screenshotsCount = 0;
        public static void TakeScreenShot(this Browser browser)
        {
            var screenshotsDir = "screenshots";
            System.IO.Directory.CreateDirectory(screenshotsDir);
            var path = $"{screenshotsDir}/screen{++screenshotsCount}.png";
            Console.WriteLine($"Screenshot: {path}");
            browser.TakeWindowScreenShot(path, OpenQA.Selenium.ScreenshotImageFormat.Png);
        }

    }
}
