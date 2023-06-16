using NUnit.Framework.Internal;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Internal;

namespace Task15_Selenium.Pages
{
    public class CheckOutPage
    {

        private By _newAddressButton = By.CssSelector(".action.action-show-popup");

        private By _saveAddressButton = By.CssSelector(".action.primary.action-save-address");

        private By _form = By.Id("opc-new-shipping-address");

        [FindsBy(How = How.CssSelector, Using = ".action.primary.checkout")]
        private IWebElement _placeOrderButton;

        [FindsBy(How = How.Name, Using = "street[0]")]
        private IWebElement _streetField;

        [FindsBy(How = How.Name, Using = "city")]
        private IWebElement _cityField;

        [FindsBy(How = How.Name, Using = "postcode")]
        private IWebElement _postCodeField;  

        [FindsBy(How = How.Name, Using = "country_id")]
        private IWebElement _countryField;            

        [FindsBy(How = How.Name, Using = "telephone")]
        private IWebElement _phoneField;

        [FindsBy(How = How.ClassName, Using = "radio")]
        private IWebElement _radioButton;

        [FindsBy(How = How.XPath, Using = "//*[@data-th=\"Cart Subtotal\"]")]
        private IWebElement _cartSubtotal;

        [FindsBy(How = How.XPath, Using = "//*[@data-th=\"Shipping\"]")]
        private IWebElement _shippingPrice;

        [FindsBy(How = How.XPath, Using = "//*[@data-th=\"Order Total\"]")]
        private IWebElement _orderTotal;

        [FindsBy(How = How.CssSelector, Using = ".button.action.continue.primary")]
        private IWebElement _continueButton;




        private IWebElement _newAddressButtonWE;


        readonly IWebDriver _driver;
        public CheckOutPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }


        public bool AlreadyHasAddress()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));

            try {

                wait.Until((driver) => driver.FindElement(_newAddressButton).Displayed);
                _newAddressButtonWE = _driver.FindElement(_newAddressButton);
                return true;

            }catch (Exception ex) { 
            
                return false;                
            
            }        

        }

       
        public void FillInformationForm() {

            Random rnd = new Random();
            var phoneNumber = rnd.Next(10000000, 100000000);

            _streetField.SendKeys("Spur Road");
            _cityField.SendKeys("London");
            _postCodeField.SendKeys("SW1A 1AA");

            var countryDropDown = new SelectElement(_countryField);
            countryDropDown.SelectByText("United Kingdom");

            _phoneField.SendKeys(phoneNumber.ToString());
                      

        }


        public void FillInformationAndContinue() {

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));

            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id("checkout-loader")));

            if (AlreadyHasAddress())
            {
                
                wait.Until(ExpectedConditions.ElementToBeClickable(_newAddressButtonWE));
                _newAddressButtonWE.Click();
                wait.Until(ExpectedConditions.ElementIsVisible(_form));
                IWebElement form = _driver.FindElement(_form);


                Actions action = new Actions(_driver);
                action.MoveToElement(form);
                action.Perform();


                FillInformationForm();

                wait.Until(ExpectedConditions.ElementToBeClickable(_saveAddressButton));

                IWebElement saveAddressButton = _driver.FindElement(_saveAddressButton);
                saveAddressButton.Click();

                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(_form));

            }
            else 
            {
                FillInformationForm();

            }

            wait.Until(ExpectedConditions.ElementToBeClickable(_radioButton));

            _radioButton.Click();

            wait.Until(ExpectedConditions.ElementToBeSelected(_radioButton));
        
            _continueButton.Click();

            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.ClassName("loading-mask")));


        }

        public SuccessPage PlaceOrder() 
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));
            
            _placeOrderButton.Click();


            wait.Until((driver) => driver.Title.StartsWith("Success"));

            return new SuccessPage(_driver);

        }

        public List<double> GetPrices()         
        { 
           List<double> result = new List<double>();
            result.Append(GetSubtotal());
            result.Append(GetShippingPrice());
            result.Append(GetOrderTotal());

            return result;
        }


        public double GetSubtotal()         
        {
            var carSubtotal = _cartSubtotal.Text.Replace("$","");
            return double.Parse(carSubtotal);
        }

        public double GetShippingPrice()
        {
            var shipping= _shippingPrice.Text.Replace("$", "");
            return double.Parse(shipping);
        }

        public double GetOrderTotal()
        {
            var orderTotal = _orderTotal.Text.Replace("$", "");
            return double.Parse(orderTotal);
        }

    }
}