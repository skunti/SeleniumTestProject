using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using System;

namespace SeleniumTestProject.Drivers
{
    public static class __DriverFactory
    {
        // Method to get WebDriver based on browser choice
        public static IWebDriver GetDriver(string browser = "chrome")
        {
            IWebDriver driver;

            // Switch based on the browser type
            switch (browser.ToLower())
            {
                case "chrome":
                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("start-maximized");  // Start maximized
                    driver = new ChromeDriver(chromeOptions);
                    break;

                case "firefox":
                    FirefoxOptions firefoxOptions = new FirefoxOptions();
                    driver = new FirefoxDriver(firefoxOptions);
                    break;

                case "edge":
                    EdgeOptions edgeOptions = new EdgeOptions();
                    driver = new EdgeDriver(edgeOptions);
                    break;

                default:
                    throw new NotImplementedException($"Browser '{browser}' is not supported.");
            }

            return driver;
        }

        // Method to quit the WebDriver
        public static void QuitDriver(IWebDriver driver)
        {
            driver.Quit();
        }
    }
}
