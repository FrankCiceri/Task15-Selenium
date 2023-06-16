using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Task15_Selenium.Pages
{
    public class CustomerPage : BasePage
    {
        [FindsBy(How = How.XPath, Using = "//a[text()=\"My Orders\"]")]
        private IWebElement _ordersButton;
        
        [FindsBy(How = How.ClassName, Using = "page-title")]
        private IWebElement _pageTitle;

        [FindsBy(How = How.CssSelector, Using = ".product.name.product-item-name")]
        private IWebElement _productName;

        [FindsBy(How = How.XPath, Using = "//*[@data-th=\"Subtotal\" and @class=\"amount\"]")]
        private IWebElement _subtotal;

        [FindsBy(How = How.XPath, Using = "//*[@data-th=\"Shipping & Handling\" and @class=\"amount\"]")]
        private IWebElement _shipping;

        [FindsBy(How = How.XPath, Using = "//*[@data-th=\"Grand Total\" and @class=\"amount\"]")]
        private IWebElement _total;


        public CustomerPage(IWebDriver driver) : base(driver)
        {
            
        }

        public void GoToOrders() 
        { 
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));

            _ordersButton.Click();

            wait.Until((_) => _pageTitle.Text.Equals("My Orders"));
        
            
        }

        public void ClickOrderNumber(string orderNumber) 
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));
            By pathOrderButton = By.XPath($"//*[@class=\"col id\" and text()={orderNumber}]/..//*[@class=\"action view\"]");

            var orderButton = _driver.FindElement(pathOrderButton);

            orderButton.Click();

            wait.Until((_) => _pageTitle.Text.Equals($"Order # {orderNumber}"));

        }

        public List<double> GetPrices()
        {
            List<double> result = new List<double>();
            result.Append(GetSubtotal());
            result.Append(GetShippingPrice());
            result.Append(GetOrderTotal());

            return result;
        }

        public string GetProductName() {
        
            return _productName.Text;
        }

        public double GetSubtotal()
        {
            var carSubtotal = _subtotal.Text.Replace("$", "");
            return double.Parse(carSubtotal);
        }

        public double GetShippingPrice()
        {
            var shipping = _shipping.Text.Replace("$", "");
            return double.Parse(shipping);
        }

        public double GetOrderTotal()
        {
            var orderTotal = _total.Text.Replace("$", "");
            return double.Parse(orderTotal);
        }


    }
}
