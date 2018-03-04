using System;
using System.Web.Mvc;
using BaselineSolution.WebApp.Components.Datatables.Remote.Request;
using BaselineSolution.WebApp.Components.ModelBinders;
using BaselineSolution.WebApp.Components.Models.Links;

namespace BaselineSolution.WebApp
{
    public static class ModelBinderConfig
    {
        public static void RegisterModelBinders(ModelBinderDictionary modelBinders)
        {
            modelBinders.Add(typeof(DatatableRequest), new DatatableRequestModelBinder());
            modelBinders.Add(typeof(decimal), new DecimalModelBinder(allowNull: false));
            modelBinders.Add(typeof(decimal?), new DecimalModelBinder(allowNull: true));
            modelBinders.Add(typeof(DateTime?), new DateTimeModelBinder());
            modelBinders.Add(typeof(DateTime), new DateTimeModelBinder());
            modelBinders.Add(typeof(ILinkBuilder), new LinkBuilderModelBinder());
        }
    }
}