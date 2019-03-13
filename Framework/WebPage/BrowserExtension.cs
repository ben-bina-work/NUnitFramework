

using System;
using System.Collections.ObjectModel;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;

namespace NUnitAutomationFramework.Framework.WebPage
{
 
        /// <summary>
        /// Driver Extension Methods helps working with browser object
        /// </summary>
        public static class BrowserExtension
        {
            private static readonly ILog Log = LogManager.GetLogger(typeof(BrowserExtension));
            /// <summary>
            /// Waits for the page to contain an element 
            /// </summary>
            /// <param name="driver">driver instance</param>
            /// <param name="locator">object locator</param>
            /// <param name="timeSpan">time span wait for the element to be presented</param>
            public static void PageContainsElement(this IWebDriver driver, By locator, TimeSpan timeSpan)
            {
                try
                {
                    Log.Info($"Searching if element by locator |{locator}| exist in the page in {timeSpan}");
                    WebDriverWait wait = new WebDriverWait(driver, timeSpan);
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));

                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                }
            }
            /// <summary>
            /// Get Value of an element 
            /// </summary>
            /// <param name="driver">driver instance</param>
            /// <param name="locator">locator instance</param>
            /// <returns></returns>
            public static string GetValueOfElement(this IWebDriver driver, By locator)
            {
                Log.Info("Trying to get Value attribute of an element " + locator);
                return driver.FindElement(locator).GetAttribute("value");
            }
            /// <summary>
            /// Get The Selected Text of the Dropdown
            /// </summary>
            /// <param name="driver"></param>
            /// <param name="locator"></param>
            /// <returns></returns>
            public static string GetSelectedTextFromDropdown(this IWebDriver driver, By locator)
            {
                Log.Info($"Trying to get the selected text of the dropdown with locator {locator}");
                var element = driver.FindElement(locator);
                var selectedOption = new SelectElement(element).SelectedOption;
                return selectedOption.Text;
            }
            /// <summary>
            /// Select the dropdown by value
            /// </summary>
            /// <param name="driver"></param>
            /// <param name="locator"></param>
            /// <param name="value"></param>
            public static void SelectDropdownByValue(this IWebDriver driver, By locator, string value)
            {
                Log.Info($"Trying to select the  the dropdown with locator {locator} and value {value}");
                var element = driver.FindElement(locator);
                new SelectElement(element).SelectByValue(value);
            }
            /// <summary>
            /// Select a dropdown by text
            /// </summary>
            /// <param name="driver">driver instance</param>
            /// <param name="locator">locator</param>
            /// <param name="dropdownText">dropdown text</param>
            public static void SelectDropdownByText(this IWebDriver driver, By locator, string dropdownText)
            {
                Log.Info($"Trying to select the  the dropdown with locator {locator} and text {dropdownText}");
                var element = driver.FindElement(locator);
                new SelectElement(element).SelectByText(dropdownText);
            }
            /// <summary>
            /// Send a value to an element
            /// </summary>
            /// <param name="driver">driver instance</param>
            /// <param name="locator">locator  selector</param>
            /// <param name="textValue">vaue to be sent to that element</param>
            /// <param name="clickOnElement"> Bool: click on element</param>
            /// <param name="clearFirst">Bool: need to clear?</param>
            /// Example: myElement.SendKeys("populate field", clearFirst: true);
            public static void WriteStringToElement(this IWebDriver driver, By locator, string textValue, bool clickOnElement = false, bool clearFirst = false)
            {
                try
                {
                    Log.Info($"Trying To Send text to an element {locator} | text: {textValue} | click on element {clickOnElement} | Clear Existing element text {clearFirst}");
                    var element = driver.FindElement(locator);
                    if (clickOnElement) element.Click();
                    if (clearFirst) element.Clear();
                    element.SendKeys(textValue);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                }
            }
            /// <summary>
            /// Wait for a page to load completly
            /// </summary>
            /// <param name="driver"></param>
            /// <param name="timeSpan">time spa</param>
            public static void WaitForPageToLoadByJavascript(this IWebDriver driver, TimeSpan timeSpan)
            {
                var wait = new WebDriverWait(driver, timeSpan);
                var javascript = driver as IJavaScriptExecutor;
                Log.Info($"Waiting for page to load in {timeSpan} ");
                var message = $"Driver must support javascript execution";
                if (javascript == null)
                {
                    Log.Error(message);
                    throw new ArgumentException("driver", $"Driver must support javascript execution");
                }
                wait.Until((d) =>
                {
                    try
                    {
                        string readyState = javascript.ExecuteScript(
                            "if (document.readyState) return document.readyState;").ToString();
                        return readyState.ToLower() == "complete";
                    }
                    catch (InvalidOperationException e)
                    {
                        //Window is no longer available
                        message = "unable to get browser\r\n" + e.StackTrace;
                        Log.Error(message);
                        return e.Message.ToLower().Contains(message);
                    }
                    catch (WebDriverException e)
                    {
                        //Browser is no longer available
                        message = "unable to connect\r\n" + e.StackTrace;
                        return e.Message.ToLower().Contains(message);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                });
            }
            /// <summary>
            /// Set Attribute of an element
            /// </summary>
            /// <param name="driver">driver instance</param>
            /// <param name="locator">element locator</param>
            /// <param name="attributeName">attribute name</param>
            /// <param name="value">attribute text</param>
            public static void SetAttribute(this IWebDriver driver, By locator, string attributeName, string value)
            {
                var element = driver.FindElement(locator);
                IWrapsDriver wrappedElement = element as IWrapsDriver;
                string message;
                if (wrappedElement == null)
                {
                    message = $"Element must wrap a web driver";
                    Log.Error(message);
                    throw new ArgumentException("element", $"Element must wrap a web driver");
                }
                driver = wrappedElement.WrappedDriver;
                IJavaScriptExecutor javascript = driver as IJavaScriptExecutor;
                if (javascript == null)
                {
                    message = $"Element must wrap a web driver that supports javascript execution";
                    Log.Error(message);
                    throw new ArgumentException("element", message);
                }
                javascript.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2])",
                    element, attributeName, value);

            }
            /// <summary>
            ///  put a border around an element and simulates the flashing 
            /// </summary>
            /// <param name="driver">driver instance</param>
            /// <param name="element">element instance</param>
            public static void FlashElement(this IWebDriver driver, IWebElement element)
            {
                var js = (IJavaScriptExecutor)driver;
                for (var i = 0; i < 3; i++)
                {
                    js.ExecuteScript("arguments[0].style.border='3px solid red'", element);
                    js.ExecuteScript("arguments[0].style.border=''", element);
                }
            }
            /// <summary>
            /// scrolls down to a particular location
            /// </summary>
            /// <param name="driver">driver instance</param>
            /// <param name="w">horizontal location</param>
            /// <param name="h">vertical location</param>
            public static void ScrollDownTo(this IWebDriver driver, int h, int w = 0)
            {
                var js = (IJavaScriptExecutor)driver;
                js.ExecuteScript($"window.scrollBy({w},{h})");
            }
            /// <summary>
            /// Scrolls too top of the paee
            /// </summary>
            /// <param name="driver">driver instance</param>
            public static void ScrollToTop(this IWebDriver driver)
            {
                var js = (IJavaScriptExecutor)driver;
                js.ExecuteScript($"window.scrollTo({250},{0})");
            }
            /// <summary>
            /// Scrolls to the buttom of the page
            /// </summary>
            /// <param name="driver">driver instance</param>
            public static void FullScrollDown(this IWebDriver driver)
            {
                var js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("window.scrollTo(0,document.body.scrollHeight)");

            }
            /// <summary>
            /// Get Current window width
            /// </summary>
            /// <param name="driver">web driver instance</param>
            /// <returns>int width</returns>
            public static int GetViewPortPageWidth(this IWebDriver driver)
            {
                long viewportWidth1 = (long)((IJavaScriptExecutor)driver)
                    .ExecuteScript("return document.body.clientWidth");//documentElement.scrollWidth");
                int viewportWidth = (int)viewportWidth1;
                return viewportWidth;
            }
            /// <summary>
            /// Get Current window Height
            /// </summary>
            /// <param name="driver">web driver instance</param>
            /// <returns>int Height</returns>
            public static int GetViewPortPageHeight(this IWebDriver driver)
            {
                var viewportHeight1 = (long)((IJavaScriptExecutor)driver)
                    .ExecuteScript("return window.innerHeight");//documentElement.scrollWidth");
                int viewportHeight = (int)viewportHeight1;
                return viewportHeight;
            }
            /// <summary>
            /// Get total window width
            /// </summary>
            /// <param name="driver">web driver instance</param>
            /// <returns>int width</returns>
            public static int GetFullPageWidth(this IWebDriver driver)
            {
                var totalwidth1 = (long)((IJavaScriptExecutor)driver).ExecuteScript("return document.body.offsetWidth");//documentElement.scrollWidth");
                int totalwidth = (int)totalwidth1;
                return totalwidth;
            }
            /// <summary>
            /// Close a tab
            /// </summary>
            /// <param name="driver"></param>
            /// <param name="windowHandlers">collection of window handlers</param>
            public static void CloseTab(this IWebDriver driver, ReadOnlyCollection<string> windowHandlers)
            {
                if (windowHandlers.Count > 1)
                {
                    driver.SwitchTo().Window(windowHandlers[1]);

                    driver.Close();

                    driver.SwitchTo().Window(windowHandlers[0]);
                }
            }
            /// <summary>
            /// Waits and searches to find an element on a page, for X seconds
            /// </summary>
            /// <param name="driver">Driver Instance</param>
            /// <param name="by">Condition of search</param>
            /// <param name="timeoutInSeconds">time out in sec</param>
            /// <returns>driver instance</returns>
            public static IWebElement FindElementWithTimeout(this IWebDriver driver, By by, int timeoutInSeconds)
            {
                if (timeoutInSeconds <= 0) return driver.FindElement(by);
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }
        }
    }

