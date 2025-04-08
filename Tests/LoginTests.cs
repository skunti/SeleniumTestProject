using NUnit.Framework;
using SeleniumTestProject.Pages;
using SeleniumTestProject.Utils;
using Allure.Commons;
using Allure.NUnit.Attributes;
using NUnit.Allure.Core;
using SeleniumTestProject.Base;

namespace SeleniumTestProject.Tests
{
    [TestFixture]
    [Allure.NUnit.AllureNUnitAttribute]
    [AllureSuite("Login Suite")]
    public class LoginTests : AbstractTest
    {
        private LoginPage loginPage;

        [SetUp]
        public void TestSetUp()
        {
            loginPage = new LoginPage();
            loginPage.GoTo();
        }

        [Test]
        [AllureTag("Smoke")]
        [AllureSeverity(Allure.Net.Commons.SeverityLevel.critical)]
        [AllureOwner("QA Team")]
        [AllureSubSuite("Valid Login")]
        public void Login_WithValidCredentials_ShouldNavigateToDashboard()
        {
            loginPage
                .EnterEmail(FakeDataGenerator.GetFakeEmail())
                .EnterPassword(FakeDataGenerator.GetFakePassword())
                .ClickLogin();

            //Assert.That(Driver.Url, Does.Contain("dashboard"));
        }

        // [Test]
        // [AllureTag("Regression")]
        // [AllureSeverity(Allure.Net.Commons.SeverityLevel.normal)]
        // [AllureOwner("QA Team")]
        // [AllureSubSuite("Invalid Login")]
        // public void Login_WithInvalidCredentials_ShouldShowError()
        // {
        //     loginPage
        //         .EnterEmail("wrong@example.com")
        //         .EnterPassword("wrongpass")
        //         .ClickLogin();

        //     string error = loginPage.GetErrorMessage();
        //     Assert.That(error, Is.Not.Empty.And.Contains("Invalid"));
        // }
    }
}
