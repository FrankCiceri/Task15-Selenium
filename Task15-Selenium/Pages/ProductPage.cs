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
    public class ProductPage : BasePage
    {
        [FindsBy(How = How.Id, Using = "product-addtocart-button")]
        private IWebElement _addToCartButton;

        [FindsBy(How = How.CssSelector, Using = ".counter-number")]        
        private IWebElement _cartNumProducts;

        public ProductPage(IWebDriver driver) : base(driver) 
        { 
        }



        public void AddProduct() {


            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));
            
            wait.Until((_) => _cartNumProducts.Text != string.Empty);
            var currentText = _cartNumProducts.Text;

            _addToCartButton.Click();

            wait.Until((_) => _cartNumProducts.Text != currentText);


        }

        public string GetCartNumProducts()
        {
            
            var texto = _cartNumProducts.Text;
            return texto;

        }


        public string EmptyCart()
        {

            var texto = _cartNumProducts.Text;
            return texto;

        }

    }
}
