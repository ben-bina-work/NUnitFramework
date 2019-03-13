using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using log4net;

namespace NUnitAutomationFramework.Framework.ReflectionHandler
{
    /// <summary>
    /// create dynamic object and dynamic properties
    /// </summary>
    public class ObjectManipulator
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ObjectManipulator));
        public static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            // ExpandoObject supports IDictionary so we can extend it like this
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
            {
                expandoDict[propertyName] = propertyValue;
            }
            else
            {
                expandoDict.Add(propertyName, propertyValue);
            }
            Log.Info($"Adding The property of {propertyName} with value of {propertyValue} to expando object of {expando}");
        }
        /// <summary>
        /// get the property name of an object in order of appearance
        /// </summary>
        /// <param name="className">Class Name</param>
        /// <returns></returns>
        public static List<string> GetObjectPropertyNames(object className)
        {
            var fieldProperties = className.GetType()
                .GetProperties()
                .Select(field => field.Name)
                .ToList();
            Log.Info("Field properties added to a list");
            return fieldProperties;
        }
        /// <summary>
        /// get the value of a property in order of appearance
        /// </summary>
        /// <param name="className">Class name</param>
        /// <returns></returns>
        public static List<object> GetObjectPropertyValues(object className)
        {
            var fieldValues = className.GetType()
                .GetProperties()
                .Select(field => field.GetValue(className))
                .ToList();
            Log.Info("Field values added to a list");
            return fieldValues;
        }
        /// <summary>
        /// Get Property Name Of Expando Object By property Index starting from 1
        /// </summary>
        /// <param name="expandoObj">Expando Object Instance</param>
        /// <param name="propertyNumber">Property Number i.e 1 for the first property</param>
        /// <returns>Property Name</returns>
        public static string GetExpandoObjectPropertyName(object expandoObj,int propertyNumber)
        {
            var propertyName = ((ExpandoObject) expandoObj).ElementAt(propertyNumber-1);
            Log.Info($"Trying to get property Name[{propertyNumber}] from object {expandoObj}=> {propertyName.Key}");
            return propertyName.Key;
        }
        /// <summary>
        /// Get Property Value Of Expando Object By property Index starting from 1
        /// </summary>
        /// <param name="expandoObj">Expando Object Instance</param>
        /// <param name="propertyNumber">Property Number i.e 1 for the first property</param>
        /// <returns></returns>
        public static object GetExpandoObjectPropertyValue(object expandoObj,int propertyNumber)
        {
            var propertyName = ((ExpandoObject)expandoObj).ElementAt(propertyNumber-1);
            Log.Info($"Trying to get property Value[{propertyNumber}] from object {expandoObj}=> {propertyName.Value}");
            return propertyName.Value;
        }
    }
}
