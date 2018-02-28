using System.Web.Mvc;

namespace BaselineSolution.WebApp.Components.Extensions
{
    public static class ExtensionsForModelBindingContext
    {
        /// <summary>
        ///     Gets a value from the ModelBindingContext by its key.
        /// </summary>
        /// <param name="bindingContext">
        ///     The binding context.
        /// </param>
        /// <param name="key">
        ///     The key.
        /// </param>
        /// <typeparam name="T">
        ///     The type of the value
        /// </typeparam>
        /// <returns>
        ///     The <see cref="T" />.
        /// </returns>
        public static T GetValue<T>(this ModelBindingContext bindingContext, string key)
        {
            bool hasPrefix = bindingContext.ValueProvider.ContainsPrefix(bindingContext.ModelName);
            string modelPrefix = hasPrefix ? bindingContext.ModelName + "." : string.Empty;
            ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(modelPrefix + key);
            if (valueResult == null)
            {
                return default(T);
            }

            return (T) valueResult.ConvertTo(typeof (T));
        }
    }
}
