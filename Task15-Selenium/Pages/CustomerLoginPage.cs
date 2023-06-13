using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task15_Selenium.Pages
{
    public class CustomerLoginPage : BasePage
    {

        [FindsBy(How = How.Name, Using = "login[username]")]
        private IWebElement _emailInput;

        [FindsBy(How = How.Name, Using = "login[password]")]
        private IWebElement _passwordInput;

        [FindsBy(How = How.Id, Using = "send2")]
        private IWebElement _signInFormButton;

        public CustomerLoginPage(IWebDriver driver) : base(driver)
        {
        }

        public void Login(string email, string password)
        {
            EnterEmail(email);
            EnterPassword(password);
            ClickSignInButton();
        }

        public void EnterEmail(string value)
        {
            _emailInput.SendKeys(value);
        }

        public void EnterPassword(string value)
        {
            _passwordInput.SendKeys(value);
        }

        public void ClickSignInButton()
        {
            _signInFormButton.Click();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));

            wait.Until((driver) => !driver.Title.StartsWith("Customer Login "));
        }
    }
}
