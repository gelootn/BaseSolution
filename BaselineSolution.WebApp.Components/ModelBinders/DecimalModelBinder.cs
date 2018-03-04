using System;
using System.Globalization;
using System.Web.Mvc;

namespace BaselineSolution.WebApp.Components.ModelBinders
{
    public class DecimalModelBinder : DefaultModelBinder
    {
        private readonly bool _allowNull;

        public DecimalModelBinder(bool allowNull)
        {
            _allowNull = allowNull;
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (string.IsNullOrWhiteSpace(valueResult.AttemptedValue))
            {
                return _allowNull ? (decimal?) null : 0m;
            }
            var modelState = new ModelState { Value = valueResult };
            object actualValue = null;
            try
            {
                actualValue = Convert.ToDecimal(
                    valueResult.AttemptedValue.Replace(",", "."),
                    CultureInfo.InvariantCulture
                );
            }
            catch (FormatException e)
            {
                modelState.Errors.Add(e);
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
            return actualValue;
        }
    }
}
