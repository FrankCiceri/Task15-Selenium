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

       

        public ProductPage(IWebDriver driver) : base(driver) 
        { 
        }



        public void AddProduct() {


            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));
            
            wait.Until((_) => GetCounter().Text != string.Empty);
            var currentText = GetCounter().Text;

            _addToCartButton.Click();

            wait.Until((_) => GetCounter().Text != currentText);


        }

        public string GetCartNumProducts()
        {
            
            var texto = GetCounter().Text;
            return texto;

        }


        public string EmptyCart()
        {

            var texto = GetCounter().Text;
            return texto;

        }

    }
}
