using System.Dynamic;
using System.Linq;
using System.Reflection;
using Allure.Commons;
using log4net;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using NUnitAutomationFramework.Framework.ReflectionHandler;

namespace NUnitAutomationFramework.DevTests
{
    [TestFixture]
    [AllureNUnit]
    [AllureDisplayIgnored]
    [Parallelizable]
    public class TestReflection
    {
        [Test(Description = "Can Create a dynamic object")]
        [AllureTag("Framework Implementation")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureIssue("ISSUE-Reflection-1")]
        [AllureTms("TMS-Reflection-1")]
        [AllureOwner("Behrang Bina")]
        [AllureSuite("Framework")]
        [AllureSubSuite("Reflection")]
        public void TestCreatingAnObject()
        {
            //Setup
            dynamic dynamicCustomer = new ExpandoObject();

            var propertyName1 = "CustomerName";
            var propertyValue1 = "BehrangBina";
            var propertyName2 = "CustomerEmail";
            var propertyValue2 = "BehrangBina@Hotmail.com";
            var propertyName3 = "CustomerAge";
            var propertyValue3 = 39;

            ObjectManipulator.AddProperty(dynamicCustomer,propertyName1,propertyValue1);
            ObjectManipulator.AddProperty(dynamicCustomer, propertyName2, propertyValue2);
            ObjectManipulator.AddProperty(dynamicCustomer, propertyName3, propertyValue3);

            var firstPropertyName = ObjectManipulator.GetExpandoObjectPropertyName(dynamicCustomer,1);
            var firstPropertyValue = ObjectManipulator.GetExpandoObjectPropertyValue(dynamicCustomer, 1);
            Assert.True(firstPropertyName.Equals(propertyName1) , "First Property Name Should be: "+ propertyName1);
            Assert.True(firstPropertyValue.Equals(propertyValue1), "First Property Value Should be: " + propertyValue1);

            var secondPropertyName = ObjectManipulator.GetExpandoObjectPropertyName(dynamicCustomer, 2);
            var secondPropertyValue = ObjectManipulator.GetExpandoObjectPropertyValue(dynamicCustomer, 2);
            Assert.True(secondPropertyName.Equals(propertyName2), "Second Property Name Should be: " + propertyName2);
            Assert.True(secondPropertyValue.Equals(propertyValue2), "Second Property Value Should be: " + propertyValue2);

            var thirdPropertyName = ObjectManipulator.GetExpandoObjectPropertyName(dynamicCustomer, 3);
            var thirdPropertyValue = ObjectManipulator.GetExpandoObjectPropertyValue(dynamicCustomer, 3);
            Assert.True(thirdPropertyName.Equals(propertyName3), "Third Property Name Should be: " + propertyName3);
            Assert.True(thirdPropertyValue.Equals(propertyValue3), "Third Property Value Should be: " + propertyValue3);
        }
    }
}
