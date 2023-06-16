using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task15_Selenium.Pages
{
    public class ProductListingPage :BasePage
    {
        [FindsBy(How = How.CssSelector, Using = ".item.product.product-item")]
        private IList<IWebElement> _products;

        private By _toCartButton = By.CssSelector(".action.tocart.primary");

        private By _goToProduct = By.CssSelector(".product.photo.product-item-photo");

       

        public ProductListingPage(IWebDriver driver)  : base(driver)
        {
            

        }


        public void AddTwoFirstItemsToCart() {

            _products.Take(2).ToList().ForEach( product => HoverAddElement(product));

        }



        public ProductPage HoverGoToThirdElement()
        {
            var targetProduct = _products.ToList()[2];

            Hover(targetProduct);

            IWebElement goToProductButton = targetProduct.FindElement(_goToProduct);

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));


            goToProductButton.Click();


            wait.Until((driver) => !driver.Title.StartsWith("Bags"));

            return new ProductPage(_driver);

        }


        public void HoverAddElement(IWebElement targetProduct) {

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));

            Hover(targetProduct);

            wait.Until(ExpectedConditions.ElementIsVisible(_toCartButton));

            IWebElement productAddToCartButton = targetProduct.FindElement(_toCartButton);
           
            var currentText = GetCounter().Text;

            wait.Until((_) => productAddToCartButton.Displayed);
            productAddToCartButton.Click();

            wait.Until((_) => GetCounter().Text != currentText);


        }

        public void LookForItem(string itemName)         
        {
          var item =  _products.ToList().Select((item) => item).Where((item) => item.Text.StartsWith(itemName)).First();
          HoverAddElement(item);   
        
        }   

    }
}
