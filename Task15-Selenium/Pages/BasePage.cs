
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace Task15_Selenium.Pages
{
    public class BasePage
    {
        [FindsBy(How = How.Id, Using = "ui-id-6")]
        private IWebElement _gearCategoryButton;       

        [FindsBy(How = How.XPath, Using = "//*[@id='narrow-by-list2']//a[contains(.,'Bags')]")]
        private IWebElement _bagsButton;

        [FindsBy(How = How.CssSelector, Using = ".counter-number")]
        private IWebElement _cartNumProducts;

        [FindsBy(How = How.Id, Using = "ui-id-27")]
        private IWebElement _watchesItemButton;

        [FindsBy(How = How.ClassName, Using = "minicart-wrapper")]
        private IWebElement _miniCartButton;

        [FindsBy(How = How.CssSelector, Using = ".action.primary.checkout")]
        private IWebElement _checkOutButton;




        protected readonly IWebDriver _driver;

        public BasePage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public ProductListingPage OpenBagsPage()
        {
            _gearCategoryButton.Click();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));

            wait.Until((driver) => driver.Title.StartsWith("Gear"));

            _bagsButton.Click();
            
            wait.Until((driver) => driver.Title.StartsWith("Bags"));

            
            return new ProductListingPage(_driver);
        }

        public IWebElement GetCounter() {

            return _cartNumProducts;
        }

        public ProductListingPage GoToWatchesPageButton()
        {

            Hover(_gearCategoryButton);            

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));

            _watchesItemButton.Click();

            wait.Until((driver) => driver.Title.StartsWith("Watches"));

            return new ProductListingPage(_driver); 
        }

        public CheckOutPage ProceedToCheckOut() {

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));
            wait.Until(ExpectedConditions.ElementToBeClickable(_miniCartButton));
            _miniCartButton.Click();

            wait.Until((_) => ExpectedConditions.ElementToBeClickable(_checkOutButton));
            _checkOutButton.Click();


            wait.Until((driver) => driver.Title.StartsWith("Checkout"));


            return new CheckOutPage(_driver);
        }


        public void Hover(IWebElement target)
        {
            Actions actions = new Actions(_driver);
            actions.MoveToElement(target);
            actions.Perform();


        }

    }
}
