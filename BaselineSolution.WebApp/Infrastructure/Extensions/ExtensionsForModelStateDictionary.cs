using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using BaselineSolution.Bo.Internal;
using BaselineSolution.Framework.Extensions;

namespace BaselineSolution.WebApp.Infrastructure.Extensions
{
    public static class ExtensionsForModelStateDictionary
    {
        public static void AddModelErrorFor<TModel, TProperty>(this ModelStateDictionary modelState,
            Expression<Func<TModel, TProperty>> property,
            string errorMessage)
        {
            modelState.AddModelError(ExpressionHelper.GetExpressionText(property), errorMessage);
        }

        public static void AddResponseValidationError(this ModelStateDictionary modelState, ValidationMessage message)
        {
            if (message.FieldName.IsNullOrEmpty())
            {
                modelState.AddModelError("", message.Message);
            }
            else
            {
                var state = modelState.FirstOrDefault(x => x.Key.EndsWith(message.FieldName));
                modelState.AddModelError(state.Key, message.Message);
            }
        }

        public static List<string> GetErrorMessages(this ModelStateDictionary modelState)
        {
            var stringlist = new List<string>();

            foreach (var item in modelState)
            {
                if (item.Value.Errors.Any())
                {
                    var message = string.Format("{0} Contains errors {1}", item.Key, string.Join(", ", item.Value.Errors));
                    stringlist.Add(message);
                }
            }

            return stringlist;

        }
    }
}