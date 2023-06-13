using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace Task15_Selenium.Pages
{
    public class MainPage : BasePage
    {
        [FindsBy(How = How.XPath, Using = "//*[@class=\"page-header\"]//*[@class=\"header links\"]/li[last()]")]
        private IWebElement _createAccountButton;


        [FindsBy(How = How.ClassName, Using = "authorization-link")]
        private IWebElement _loginButton;


        [FindsBy(How = How.ClassName, Using = "logged-in")]
        private IWebElement _welcomeMessage;

        public MainPage(IWebDriver driver) : base(driver) 
        {
        }

        public CustomerRegisterPage ClickCreateAccButton()
        {
            _createAccountButton.Click();

            return new CustomerRegisterPage(_driver);
        }


        public CustomerLoginPage ClickLoginButton()
        {
            _loginButton.Click();

            return new CustomerLoginPage(_driver);
        }

        public string GetCreateAccButtonText()
        {
            return _createAccountButton.Text;
        }

        public string GetLoginText()
        {
            return _loginButton.Text;
        }

        public string GetWelcomeMessage()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));

            wait.Until((_) => _welcomeMessage.Text.StartsWith("Welcome, "));

            return _welcomeMessage.Text;
        }
    }
}
