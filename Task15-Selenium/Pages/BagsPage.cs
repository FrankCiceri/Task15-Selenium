using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task15_Selenium.Pages
{
    public class BagsPage :BasePage
    {
        [FindsBy(How = How.CssSelector, Using = ".item.product.product-item")]
        private IList<IWebElement> _products;

        private By _toCartButton = By.CssSelector(".action.tocart.primary");

        private By _goToProduct = By.CssSelector(".product.photo.product-item-photo");

       

        public BagsPage(IWebDriver driver)  : base(driver)
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

            Hover(targetProduct);

            IWebElement productAddToCartButton = targetProduct.FindElement(_toCartButton);

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));


            productAddToCartButton.Click();      

        }

        public void Hover(IWebElement targetProduct) 
        {
            Actions actions = new Actions(_driver);
            actions.MoveToElement(targetProduct);
            actions.Perform();


        }



    }
}
