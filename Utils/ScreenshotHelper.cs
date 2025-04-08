using OpenQA.Selenium;
using System;
using System.IO;
using Allure.Commons;
using NUnit.Framework;
using Allure.Net.Commons;

namespace SeleniumTestProject.Utils
{
    public static class ScreenshotHelper
    {
        public static void CaptureScreenshot(IWebDriver driver, string testName)
        {
            try
            {
                string screenshotsDir = Path.Combine(AppContext.BaseDirectory, "Screenshots");
                Directory.CreateDirectory(screenshotsDir);

                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                string filePath = Path.Combine(screenshotsDir, $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");

                // Save screenshot without specifying the image format (default is PNG)
                screenshot.SaveAsFile(filePath);

                try
                {
                    // Attach the screenshot file to Allure report (using file path string)
                    TestContext.AddTestAttachment(filePath);
                    AllureApi.AddAttachment(testName, "image/png", filePath);
                    //AllureLifecycle.Instance.AddAttachment(testName, "image/png", filePath);
                }
                catch (Exception ex)
                {
                    TestContext.WriteLine($"[Allure Warning] Failed to attach screenshot: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"[Screenshot Error] Failed to capture screenshot: {ex.Message}");
            }
        }
    }
}
