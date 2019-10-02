using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace HiTours.Api.Common.Data.Azure
{
    public class CustomTableEntity : TableEntity
    {
        private const string DecimalPrefix = "D_";

        public override IDictionary<string, EntityProperty> WriteEntity(OperationContext operationContext)
        {
            var entityProperties = base.WriteEntity(operationContext);
            var objectProperties = this.GetType().GetProperties();

            foreach (var item in objectProperties.Where(f => f.PropertyType == typeof(decimal)))
            {
                entityProperties.Add(
                    DecimalPrefix + item.Name,
                    new EntityProperty(item.GetValue(this, null).ToString()));
            }

            return entityProperties;
        }

        public override void ReadEntity(
            IDictionary<string, EntityProperty> properties,
            OperationContext operationContext)
        {
            base.ReadEntity(properties, operationContext);

            foreach (var property in properties.Where(p => p.Key.StartsWith(DecimalPrefix)))
            {
                var realPropertyName = property.Key.Substring(DecimalPrefix.Length);
                var propertyInfo = this.GetType().GetProperty(realPropertyName);
                var format = new NumberFormatInfo();
                format.NumberDecimalSeparator = ",";
                var value = property.Value.StringValue?.Replace(".", ",") ?? "0";

                propertyInfo.SetValue(
                    this,
                    decimal.Parse(value, NumberStyles.AllowDecimalPoint, format),
                    null);
            }
        }
    }
}
