using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

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

        [FindsBy(How = How.CssSelector, Using = ".action.switch")]
        private IWebElement _customerMenu;

        [FindsBy(How = How.XPath, Using = "//*[contains(text(),\"My Account\")]")]
        private IWebElement _accountOption;


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


        public CustomerPage GoToAccount() 
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));
            _customerMenu.Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(_accountOption));

            _accountOption.Click();

            return new CustomerPage(_driver);
        
        }



    }
}
