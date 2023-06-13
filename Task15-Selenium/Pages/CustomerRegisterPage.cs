using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace Task15_Selenium.Pages
{
    public class CustomerRegisterPage : BasePage
    {
        [FindsBy(How = How.Name, Using = "firstname")]
        private IWebElement _firstNameInput;

        [FindsBy(How = How.Name, Using = "lastname")]
        private IWebElement _lastNameInput;

        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement _passwordInput;

        [FindsBy(How = How.Id, Using = "email_address-error")]
        private IWebElement _emailError;

        [FindsBy(How = How.ClassName, Using = "primary")]
        private IWebElement _formCreateAccButton;



        public CustomerRegisterPage(IWebDriver driver) : base(driver) 
       { 
       }

        public void CreateAccount(string firstName, string lastName, string password)
        {
            EnterFirstName(firstName);
            EnterLastName(lastName);
            EnterPassword(password);
            ClickCreateAccButton();

        
        }

        public void EnterFirstName(string value) {
            _firstNameInput.SendKeys(value);
        
        }

        public void EnterLastName(string value)
        {
            _lastNameInput.SendKeys(value);

        }

        public void EnterPassword(string value)
        {
            _passwordInput.SendKeys(value);

        }

        public string GetEmailError() {

            return _emailError.Text;
        }


        public void ClickCreateAccButton() {

            _formCreateAccButton.Click();            
        }


    }
}
