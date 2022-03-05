using MyTestedAspNetCoreApp;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

using Xunit;

namespace MyTestedApp.Test
{
    public class SeleniumTests
    {
        [Fact]
        public void H1ElementIsRemovedFromPrivacyPage()
        {
            var serverFactory = new SeleniumServerFactory<Startup>();

            var options = new ChromeOptions();
            options.AddArguments("--headless");
            options.AcceptInsecureCertificates = true;

            var webDriver = new ChromeDriver(options);

            webDriver.Navigate().GoToUrl(serverFactory.RootUri + "/Home/Privacy");

            Assert.Throws<NoSuchElementException>(() => webDriver.FindElement(By.TagName("p")));
        }
    }
}
