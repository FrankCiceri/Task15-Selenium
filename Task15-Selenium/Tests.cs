using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V112.Network;
using Task15_Selenium.Pages;

namespace Task15_Selenium
{

    public class Tests
    {

        [ThreadStatic]
        private static IWebDriver _driver;

        [ThreadStatic]
        private static MainPage _mainPage;


        [SetUp]
        public void Setup()
        {
            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArgument("headless");

            _driver = new ChromeDriver(chromeOptions);

            _driver.Manage().Window.Maximize();

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

            _driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com");

            _mainPage = new MainPage(_driver);


        }



        [Test]
        public void Scenario1() 
        {
            var customerLoginPage = _mainPage.ClickLoginButton();
            customerLoginPage.Login("abc@email.com", "Abc123456");

            var watchesPage =  _mainPage.GoToWatchesPageButton();

            string expectedItem = "Dash Digital Watch";
            watchesPage.LookForItem(expectedItem);

            var checkoutPage = watchesPage.ProceedToCheckOut();
            checkoutPage.FillInformationAndContinue();

            var expectedPrices = checkoutPage.GetPrices();

            var successPage = checkoutPage.PlaceOrder();          

            string orderNumber = successPage.GetOrderNumber();
            successPage.ContinueShopping();



            var customerPage = _mainPage.GoToAccount();

            customerPage.GoToOrders();

            customerPage.ClickOrderNumber(orderNumber);

            var actualItem = customerPage.GetProductName();
            var actualPrices = customerPage.GetPrices();

            Assert.Multiple(() =>
            {
                CollectionAssert.AreEqual(expectedPrices, actualPrices);
                Assert.AreEqual(expectedItem, actualItem);

            });       

        }




        [Test]
        public void Scenario2()
        { 
           var customerCreateAccPage = _mainPage.ClickCreateAccButton();
            customerCreateAccPage.CreateAccount("f", "c", "Abc123456");
            Assert.AreEqual(customerCreateAccPage.GetEmailError(), "This is a required field.");
        
        }


        [Test]
        public void Scenario3()
        {
            var customerLoginPage= _mainPage.ClickLoginButton();
            customerLoginPage.Login("fciceri@gmail.com", "Abc123456");
            
            var bagsPage = _mainPage.OpenBagsPage();

            bagsPage.AddTwoFirstItemsToCart();

            var productPage = bagsPage.HoverGoToThirdElement();
            productPage.AddProduct();            

            var numCartProducts = productPage.GetCartNumProducts();            

            Assert.AreEqual("3", numCartProducts);



        }


        [TearDown]
        public void close_Browser()
        {

            _driver.Close();

        }


    }
}