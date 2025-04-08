using NUnit.Framework;
using NUnit.Framework.Interfaces;  // Required for ITestListener and ITest
using OpenQA.Selenium;
using SeleniumTestProject.Utils;

namespace SeleniumTestProject.Utils
{
    public class TestListener : ITestListener
    {
        private IWebDriver driver;

        // This is required by ITestListener. You can leave it empty or log the start.
        public void TestStarted(ITest test)
        {
            // Initialize WebDriver or perform any other setup before the test starts
            if (driver == null)
            {
                //driver = new ChromeDriver(); // Initialize the WebDriver
            }
        }

        // This is required by ITestListener. We handle the test results here.
        public void TestFinished(ITestResult result)
        {
            // Capture a screenshot if the test failed
            if (result.ResultState == ResultState.Failure)
            {
                ScreenshotHelper.CaptureScreenshot(driver, result.Name);  // Capture screenshot on failure
            }
        }

        // This is required by ITestListener. You can log or capture the test output.
        public void TestOutput(TestOutput output)
        {
            // You can log the output or leave it empty for now
            // Example: Console.WriteLine(output.Text);
        }

        // This is required by ITestListener. You can implement sending messages if necessary.
        public void SendMessage(TestMessage message)
        {
            // You can log messages here or implement functionality if needed
        }

        // Optional: Add a cleanup method if necessary after each test
        public void OnTestStarted(ITest test) { }
        public void OnTestFinished(ITest test) { }
        public void OnTestFailed(ITest test, System.Exception failure) { }
        public void OnTestSkipped(ITest test) { }
        public void OnTestInvoked(ITest test) { }
    }
}
