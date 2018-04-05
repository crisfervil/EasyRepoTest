using OpenQA.Selenium.Chrome;
using Xunit;

namespace EasyRepoTest
{
    public class SeleniumBasicExample
    {
        [Fact]
        public void GoogleSearch()
        {
            // 1. Initialize the Driver
            using (var driver = new ChromeDriver())
            {
                // 2. Go to the "Google" homepage
                driver.Navigate().GoToUrl("http://www.google.com");

                // 3. Find the search box on the page
                var searchBox = driver.FindElementById("lst-ib");

                // 4. Enter the text to search for
                searchBox.SendKeys("Selenium Test");

                // 5. Find the search button
                var searchButton = driver.FindElementByName("btnK");

                // 6. Click on it to start the search
                searchButton.Submit();

                // 7. Find the "Id" of the "Div" containing results stats,
                // located just above the results table.
                var searchResults = driver.FindElementById("resultStats");

                // 8. TODO: Perform any operations to make sure the behavior is correct 
                Assert.Contains("seconds", searchResults.Text);
            }
        }
    }
}
