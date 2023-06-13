
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;


namespace Task15_Selenium.Pages
{
    public class BasePage
    {
        [FindsBy(How = How.Id, Using = "ui-id-6")]
        private IWebElement _gearCategoryButton;

        [FindsBy(How = How.XPath, Using = "//*[@id='narrow-by-list2']//a[contains(.,'Bags')]")]
        private IWebElement _bagsButton;

        protected readonly IWebDriver _driver;

        public BasePage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public BagsPage OpenBagsPage()
        {
            _gearCategoryButton.Click();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));

            wait.Until((driver) => driver.Title.StartsWith("Gear"));

            _bagsButton.Click();
            
            wait.Until((driver) => driver.Title.StartsWith("Bags"));

            
            return new BagsPage(_driver);
        }

    }
}
