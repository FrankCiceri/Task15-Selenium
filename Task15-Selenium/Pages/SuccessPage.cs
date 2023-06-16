using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace Task15_Selenium.Pages
{
    public class SuccessPage : BasePage
    {


        [FindsBy(How = How.ClassName, Using = "order-number")]
        private IWebElement _orderNumber;

        [FindsBy(How = How.CssSelector, Using = ".action.primary.continue")]
        private IWebElement _continueShoppingButton;

        public SuccessPage(IWebDriver driver) : base(driver) 
        {
            
        }

        public string GetOrderNumber() 
        {
            return _orderNumber.Text;  
            
        }

        public void ContinueShopping()         
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));

            _continueShoppingButton.Click();

            wait.Until((driver) => driver.Title.StartsWith("Home Page"));
            
        
        }


    }
}