using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumTestProject.Drivers;
using SeleniumTestProject.Utils;

namespace SeleniumTestProject.Pages
{
    public class LoginPage
    {
        private IWebDriver driver;

        public LoginPage()
        {
            this.driver = DriverFactory.GetDriver();
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "[data-auto='username']")]
        private IWebElement emailInput;

        [FindsBy(How = How.CssSelector, Using = "[data-auto='password']")]
        private IWebElement passwordInput;

        [FindsBy(How = How.CssSelector, Using = "[data-auto='kklogin']")]
        private IWebElement loginButton;

        [FindsBy(How = How.ClassName, Using = "error-message")]
        private IWebElement errorMessage;

        public void GoTo()
        {
            driver.Navigate().GoToUrl(ConfigManager.Get("BaseUrl"));
        }

        public LoginPage EnterEmail(string email)
        {
            emailInput.Clear();
            emailInput.SendKeys(email);
            return this;
        }

        public LoginPage EnterPassword(string password)
        {
            passwordInput.Clear();
            passwordInput.SendKeys(password);
            return this;
        }

        public LoginPage ClickLogin()
        {
            loginButton.Click();
            return this;
        }

        public string GetErrorMessage()
        {
            return errorMessage.Text;
        }
    }
}