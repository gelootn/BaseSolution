using System;
using System.Globalization;
using System.Web.Mvc;

namespace BaselineSolution.WebApp.Components.ModelBinders
{
    public class DateTimeModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var displayFormat = bindingContext.ModelMetadata.DisplayFormatString;
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (string.IsNullOrEmpty(displayFormat) || value == null)
                return base.BindModel(controllerContext, bindingContext);

            DateTime date;
            displayFormat = displayFormat
                .Replace("{0:", string.Empty)
                .Replace("}", string.Empty);
            // use the format specified in the DisplayFormat attribute to parse the date
            return DateTime.TryParseExact(value.AttemptedValue, displayFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out date)
                ? date
                : base.BindModel(controllerContext, bindingContext);
        }
    }
}
