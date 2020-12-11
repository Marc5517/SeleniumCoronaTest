using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumCoronaTest
{
    [TestClass]
    public class UnitTest1
    {
        private static readonly string DriverDirectory = "C:\\Users\\Marc\\Downloads\\selenium";
        private static IWebDriver _driver;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _driver = new ChromeDriver(DriverDirectory); // fast
            // if your Chrome browser was updated, you must update the driver as well ...
            //    https://chromedriver.chromium.org/downloads
            //_driver = new FirefoxDriver(DriverDirectory);  // slow
        }

        [ClassCleanup]
        public static void TearDown()
        {
            _driver.Dispose();
        }

        [TestMethod]
        public void TestMethod1GetAll()
        {
            _driver.Navigate().GoToUrl("http://localhost:3000/");
            string title = _driver.Title;
            Assert.AreEqual("Temperature", title);

            IWebElement buttonElement = _driver.FindElement(By.Id("getAllButton"));
            buttonElement.Click();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)); // decorator pattern?
            IWebElement coronaTestList = wait.Until(d => d.FindElement(By.Id("coronaTestList")));
            Assert.IsTrue(coronaTestList.Text.Contains("Maskine 3"));
        }

        [TestMethod]
        public void TestMethodGetAllWithHighTemperature()
        {
            _driver.Navigate().GoToUrl("http://localhost:3000/");

            IWebElement buttonElement = _driver.FindElement(By.Id("getWithHighTemperatureButton"));
            buttonElement.Click();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)); // decorator pattern?
            IWebElement coronaTestList = wait.Until(d => d.FindElement(By.Id("coronaTestList")));
            Assert.IsTrue(coronaTestList.Text.Contains("Maskine 4"));
        }

        [TestMethod]
        public void TestMethodGetById()
        {
            _driver.Navigate().GoToUrl("http://localhost:3002/");
            IWebElement inputElement = _driver.FindElement(By.Id("putTestId"));
            inputElement.Clear();
            inputElement.SendKeys("7");

            IWebElement buttonElement = _driver.FindElement(By.Id("getById"));
            buttonElement.Click();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement idToGetBy = wait.Until(d => d.FindElement(By.Id("testTable")));
            Assert.IsTrue(idToGetBy.Text.Contains("Maskine 3"));
        }
    }
}
