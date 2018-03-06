using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using BaselineSolution.Framework.Exporting;

namespace BaselineSolution.Framework.Extensions
{
    /// <summary>
    ///     Class that provides extensions for IEnumerable
    /// </summary>
    public static class ExtensionsForIEnumerable
    {
        /// <summary>
        ///     Function that constructs a select list in a type safe way
        /// </summary>
        /// <typeparam name="TModel">Type of the model</typeparam>
        /// <param name="items">List of items to use for the selectlist</param>
        /// <param name="valueFunction">Function that extracts a unique value out of the model (usually Id)</param>
        /// <param name="textFunction">Function that extracts text to show in the view out of the model (usually description, name, ...)</param>
        /// <param name="selectedValue">(Optional) value that needs to be selected by default</param>
        /// <returns>A selectlist of the items</returns>
        public static SelectList ToSelectList<TModel>(this IEnumerable<TModel> items, Func<TModel, string> valueFunction,
                                                      Func<TModel, string> textFunction,
                                                      TModel selectedValue = default(TModel))
        {
            IEnumerable<SelectListItem> selectListItems = items.Select(model => new SelectListItem
            {
                Value = valueFunction(model),
                Text = textFunction(model)
            });
            return new SelectList(selectListItems, "Value", "Text", !Equals(default(TModel), selectedValue) ? valueFunction(selectedValue) : null);
        }

        /// <summary>
        ///     Function that constructs a select list in a type safe way
        /// </summary>
        /// <typeparam name="TModel">Type of the model</typeparam>
        /// <param name="items">List of items to use for the selectlist</param>
        /// <param name="valueFunction">Function that extracts a unique value out of the model (usually Id)</param>
        /// <param name="textFunction">Function that extracts text to show in the view out of the model (usually description, name, ...)</param>
        /// <param name="selectedValue">(Optional) value that needs to be selected by default</param>
        /// <returns>A selectlist of the items</returns>
        public static SelectList ToSelectList<TModel>(this IEnumerable<TModel> items,
                                                      Func<TModel, int, string> valueFunction,
                                                      Func<TModel, int, string> textFunction,
                                                      TModel selectedValue = default(TModel))
        {
            var itemsList = items.ToList();
            IEnumerable<SelectListItem> selectListItems = itemsList.Select((model, index) => new SelectListItem
            {
                Value =
                    valueFunction(
                        model, index),
                Text =
                    textFunction(
                        model, index)
            });
            return new SelectList(selectListItems, "Value", "Text", !Equals(default(TModel), selectedValue) ? valueFunction(selectedValue, itemsList.IndexOf(selectedValue)) : null);
        }

        /// <summary>
        ///     Function that constructs a select list in a type safe way
        /// </summary>
        /// <typeparam name="TModel">Type of the model</typeparam>
        /// <param name="items">List of items to use for the selectlist</param>
        /// <param name="valueFunction">Function that extracts a unique value out of the model (usually Id)</param>
        /// <param name="textFunction">Function that extracts text to show in the view out of the model (usually description, name, ...)</param>
        /// <param name="selectedValue">(Optional) value that needs to be selected by default</param>
        /// <returns>A selectlist of the items</returns>
        public static SelectList ToSelectList<TModel>(this IEnumerable<TModel> items, Func<TModel, int> valueFunction,
                                                      Func<TModel, string> textFunction,
                                                      TModel selectedValue = default(TModel))
        {
            return ToSelectList(items, i => valueFunction(i).ToString(CultureInfo.InvariantCulture), textFunction,
                                selectedValue);
        }

        /// <summary>
        ///     Function that constructs a select list in a type safe way
        /// </summary>
        /// <typeparam name="TModel">Type of the model</typeparam>
        /// <param name="items">List of items to use for the selectlist</param>
        /// <param name="valueFunction">Function that extracts a unique value out of the model (usually Id)</param>
        /// <param name="textFunction">Function that extracts text to show in the view out of the model (usually description, name, ...)</param>
        /// <param name="selectedValue">(Optional) value that needs to be selected by default</param>
        /// <returns>A selectlist of the items</returns>
        public static SelectList ToSelectList<TModel>(this IEnumerable<TModel> items,
                                                      Func<TModel, int, int> valueFunction,
                                                      Func<TModel, int, string> textFunction,
                                                      TModel selectedValue = default(TModel))
        {
            return ToSelectList(items,
                                (model, index) => valueFunction(model, index).ToString(CultureInfo.InvariantCulture),
                                textFunction, selectedValue);
        }

        /// <summary>
        ///     Function that constructs a multi select list in a type safe way
        /// </summary>
        /// <typeparam name="TModel">Type of the model</typeparam>
        /// <param name="items">List of items to use for the selectlist</param>
        /// <param name="valueFunction">Function that extracts a unique value out of the model (usually Id)</param>
        /// <param name="textFunction">Function that extracts text to show in the view out of the model (usually description, name, ...)</param>
        /// <param name="selectedValues">(Optional) values that needs to be selected by default</param>
        /// <returns>A multiselectlist of the items</returns>
        public static MultiSelectList ToMultiSelectList<TModel>(this IEnumerable<TModel> items,
                                                                Func<TModel, string> valueFunction,
                                                                Func<TModel, string> textFunction,
                                                                IEnumerable<TModel> selectedValues = null)
        {
            IEnumerable<SelectListItem> selectListItems = items.Select(i => new SelectListItem
            {
                Value = valueFunction(i),
                Text = textFunction(i)
            });
            return new MultiSelectList(selectListItems, "Value", "Text", selectedValues != null ? selectedValues.Select(v => valueFunction(v)) : null);
        }

        /// <summary>
        ///     Function that constructs a multi select list in a type safe way
        /// </summary>
        /// <typeparam name="TModel">Type of the model</typeparam>
        /// <param name="items">List of items to use for the selectlist</param>
        /// <param name="valueFunction">Function that extracts a unique value out of the model (usually Id)</param>
        /// <param name="textFunction">Function that extracts text to show in the view out of the model (usually description, name, ...)</param>
        /// <param name="selectedValues">(Optional) values that needs to be selected by default</param>
        /// <returns>A multiselectlist of the items</returns>
        public static MultiSelectList ToMultiSelectList<TModel>(this IEnumerable<TModel> items,
                                                                Func<TModel, int> valueFunction,
                                                                Func<TModel, string> textFunction,
                                                                IEnumerable<TModel> selectedValues = null)
        {
            return ToMultiSelectList(items, i => valueFunction(i).ToString(CultureInfo.InvariantCulture), textFunction,
                                     selectedValues);
        }

        public static ExportBuilder<TModel> Export<TModel>(this IEnumerable<TModel> models) where TModel : class
        {
            return ExportBuilder<TModel>.Create(models);
        }
    }
}
