using NUnit.Framework;
using SeleniumTestProject.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniumTestProject.Drivers;

namespace SeleniumTestProject.Base
{
    public abstract class AbstractTest
    {
        protected IWebDriver Driver;
        private WebDriverWait _wait;

        [SetUp]
        public void BaseSetUp()
        {
            Driver = DriverFactory.GetDriver();
            _wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(15));

            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            TestContext.WriteLine("🟢 Test setup completed: Driver initialized.");
        }

        [TearDown]
        public void BaseTearDown()
        {
            var context = TestContext.CurrentContext;

            try
            {
                if (context.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
                {
                    if (Driver != null)
                    {
                        try
                        {
                            ScreenshotHelper.CaptureScreenshot(Driver, context.Test.Name);
                            TestContext.WriteLine($"❌ Test Failed: Screenshot captured for {context.Test.Name}");
                        }
                        catch (Exception ex)
                        {
                            TestContext.WriteLine($"⚠️ Error capturing screenshot: {ex.Message}");
                        }
                    }
                    else
                    {
                        TestContext.WriteLine("⚠️ WebDriver is null, skipping screenshot capture.");
                    }
                }

                LogTestResult(context);
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"⚠️ Unexpected error in TearDown: {ex.Message}");
            }
            finally
            {
                try
                {
                    Driver?.Quit();
                    TestContext.WriteLine("🧹 Test teardown completed: Driver quit.");
                }
                catch (Exception ex)
                {
                    TestContext.WriteLine($"⚠️ Error while quitting driver: {ex.Message}");
                }
            }
        }

        protected IWebElement WaitForElement(By locator)
        {
            try
            {
                return _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
            }
            catch (WebDriverTimeoutException)
            {
                TestContext.WriteLine($"⏱️ Timeout waiting for element: {locator}");
                throw;
            }
        }

        protected IWebElement WaitForElementToBeClickable(By locator)
        {
            try
            {
                return _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
            }
            catch (WebDriverTimeoutException)
            {
                TestContext.WriteLine($"⏱️ Timeout waiting for clickable element: {locator}");
                throw;
            }
        }

        private void LogTestResult(TestContext context)
        {
            if (context.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Passed)
            {
                TestContext.WriteLine($"✅ Test Passed: {context.Test.Name}");
            }
            else
            {
                TestContext.WriteLine($"❌ Test Failed: {context.Test.Name}");
            }
        }

        protected void WaitForCondition(Func<IWebDriver, bool> condition)
        {
            try
            {
                _wait.Until(condition);
            }
            catch (WebDriverTimeoutException)
            {
                TestContext.WriteLine("⏱️ Timeout waiting for custom condition.");
                throw;
            }
        }
    }
}
