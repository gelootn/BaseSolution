using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using BaselineSolution.WebApp.Components.Extensions;
using HtmlBuilders;

namespace BaselineSolution.WebApp.Components.HtmlHelpers.Forms
{
    public static class FormElementForHelper
    {
        public static MvcHtmlString FormElement<TModel>(
            this HtmlHelper<TModel> html,
            IHtmlString editorHtml,
            IHtmlString labelHtml,
            IHtmlString validationHtml)
        {
            var controlGroup = new TagBuilder("div").Class("form-group");

            var controls = new TagBuilder("div")
                .Class("controls col-sm-8");

            var helpInline = new TagBuilder("span")
                .Class("help-inline")
                .Html(validationHtml);

            controls.Html(editorHtml)
                .AppendHtml(helpInline);

            controlGroup.Html(labelHtml)
                .AppendHtml(controls);

            return controlGroup.ToHtml();
        }

        public static MvcHtmlString FormElement<TModel>(
            this HtmlHelper<TModel> html,
            IHtmlString editorHtml,
            IHtmlString labelHtml)
        {
            return FormElement(html, editorHtml, labelHtml, MvcHtmlString.Create(string.Empty));
        }

        public static MvcHtmlString FormElement<TModel>(
            this HtmlHelper<TModel> html,
            IHtmlString editorHtml)
        {
            return FormElement(html, editorHtml, MvcHtmlString.Create(string.Empty), MvcHtmlString.Create(string.Empty));
        }

        /// <summary>
        ///     Returns a completely formatted form element for the given expression, including label, input and validation messages
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="html"></param>
        /// <param name="propertyExpression"></param>
        /// <param name="editorHtml"></param>
        /// <param name="labelHtml"></param>
        /// <param name="validationHtml"></param>
        /// <param name="prefixclass"></param>
        /// <returns></returns>
        public static MvcHtmlString FormElementFor<TModel, TProperty>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> propertyExpression,
            IHtmlString editorHtml,
            IHtmlString labelHtml,
            IHtmlString validationHtml,
            string prefixclass)
        {

            var controlGroup = HtmlTags.Div.Class("form-group");
            // var controlGroup = new TagBuilder("div").Class("form-group");
            if (!html.ViewData.ModelState.IsValidField(html.NameFor(propertyExpression).ToString()))
                controlGroup.Class("has-error");

            var controls = HtmlTags.Div.Class("col-sm-8");



            var helpInline = HtmlTags.Div.Class("help-block").Append(validationHtml.ToHtmlString());

            var parsed = HtmlTag.Parse(editorHtml.ToHtmlString());
            parsed.Class("form-control");

            if (!string.IsNullOrWhiteSpace(prefixclass))
            {
                var inputGroup =
                    HtmlTags.Div.Class("input-group")
                        .Append(
                            HtmlTags.Span.Class("input-group-addon")
                                .Append(HtmlTags.I.Class(prefixclass))
                        ).Append(parsed);

                controls.Append(inputGroup)
               .Append(helpInline);
            }
            else
            {
                controls.Append(parsed.ToString())
                .Append(helpInline);
            }

            



            controlGroup.Append(labelHtml.ToHtmlString())
                .Append(controls);

            return (MvcHtmlString)controlGroup.ToHtml();
        }

        /// <summary>
        ///     Method to build form input with label, editor and validation message.
        /// </summary>
        /// <example>
        ///     <code>
        ///         Html.FormElementFor(model => model.MyProperty)
        ///     </code>
        /// </example>
        /// <typeparam name="TModel">Model</typeparam>
        /// <typeparam name="TProperty">Property of the model</typeparam>
        /// <param name="html">This Html Helper</param>
        /// <param name="propertyExpression">Expression to get property of the model</param>
        /// <param name="prefixClass"></param>
        /// <returns></returns>
        public static MvcHtmlString FormElementFor<TModel, TProperty>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> propertyExpression,
            string prefixClass = "")
        {
            return html.FormElementFor(propertyExpression,
                                       html.EditorFor(propertyExpression),
                                       html.LabelFor(propertyExpression, new { @class = "control-label col-sm-4" }),
                                       html.ValidationMessageFor(propertyExpression), prefixClass);
        }

        /// <summary>
        ///     Method to build form input with label, editor and validation message.
        /// </summary>
        /// <example>
        ///     <code>
        ///         Html.FormElementFor(model => model.MyProperty, Html.DropDownListFor(model => model.MyProperty, MySelectList))
        ///     </code>
        /// </example>
        /// <typeparam name="TModel">Model</typeparam>
        /// <typeparam name="TProperty">Property of the model</typeparam>
        /// <param name="html">This Html Helper</param>
        /// <param name="propertyExpression">Expression to get property of the model</param>
        /// <param name="editorHtml">Some custom MvcHtmlString to use as the editor field</param>
        /// <param name="prefixClass"></param>
        /// <returns></returns>
        public static MvcHtmlString FormElementFor<TModel, TProperty>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> propertyExpression,
            IHtmlString editorHtml,
            string prefixClass = "")
        {
            return html.FormElementFor(propertyExpression,
                                       editorHtml,
                                       html.LabelFor(propertyExpression, new { @class = "control-label col-sm-4" }),
                                       html.ValidationMessageFor(propertyExpression), prefixClass);
        }

        /// <summary>
        ///     Method to build form input with label, editor and validation message.
        /// </summary>
        /// <example>
        ///     <code>
        ///         Html.FormElementFor(model => model.MyProperty, Html.CheckBoxFor(model => model.MyBoolean), Html.DisplayNameFor(model => model.SomeOtherProperty))
        ///     </code>
        /// </example>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="html"></param>
        /// <param name="propertyExpression"></param>
        /// <param name="editorHtml"></param>
        /// <param name="labelHtml"></param>
        /// <param name="prefixClass"></param>
        /// <returns></returns>
        public static MvcHtmlString FormElementFor<TModel, TProperty>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> propertyExpression,
            IHtmlString editorHtml,
            IHtmlString labelHtml,
            string prefixClass = ""
            )
        {
            return html.FormElementFor(propertyExpression,
                                       editorHtml,
                                       labelHtml,
                                       html.ValidationMessageFor(propertyExpression), prefixClass);
        }

        /*** EXTRAS ***/

        /// <summary>
        ///     Method to build form input with label, editor and validation message.
        /// </summary>
        /// <example>
        ///     <code>
        ///         Html.FormElementFor(model => model.MyProperty, Html.TextAreaFor)
        ///     </code>
        /// </example>
        /// <typeparam name="TModel">Model</typeparam>
        /// <typeparam name="TProperty">Property of the model</typeparam>
        /// <param name="html">This Html Helper</param>
        /// <param name="expression">Expression to get property of the model</param>
        /// <param name="editor">Editor expression that takes the previous expression parameter and converts it into a MvcHtmlString</param>
        /// <returns></returns>
        public static MvcHtmlString FormElementFor<TModel, TProperty>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression,
            Func<Expression<Func<TModel, TProperty>>, MvcHtmlString> editor)
        {
            return FormElementFor(html, expression, editor(expression));
        }

        /// <summary>
        ///     Method to build form input with label, editor and validation message.
        /// </summary>
        /// <example>
        ///     <code>
        ///         Html.FormElementFor(model => model.MyProperty, Html.TextAreaFor, Html.MyDisplayHelper(model => model.MyProperty))
        ///     </code>
        /// </example>
        /// <typeparam name="TModel">Model</typeparam>
        /// <typeparam name="TProperty">Property of the model</typeparam>
        /// <param name="html">This Html Helper</param>
        /// <param name="expression">Expression to get property of the model</param>
        /// <param name="editor">Editor expression that takes the previous expression parameter and converts it into a MvcHtmlString</param>
        /// <param name="labelHtml">Html to use as the label</param>
        /// <returns></returns>
        public static MvcHtmlString FormElementFor<TModel, TProperty>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression,
            Func<Expression<Func<TModel, TProperty>>, MvcHtmlString> editor,
            IHtmlString labelHtml)
        {
            return FormElementFor(html, expression, editor(expression), labelHtml);
        }

        /// <summary>
        ///     Method to build form input with label, editor and validation message.
        /// </summary>
        /// <example>
        ///     <code>
        ///         Html.FormElementFor(model => model.MyProperty, Html.TextAreaFor, Html.DisplayFor)
        ///     </code>
        /// </example>
        /// <typeparam name="TModel">Model</typeparam>
        /// <typeparam name="TProperty">Property of the model</typeparam>
        /// <param name="html">This Html Helper</param>
        /// <param name="expression">Expression to get property of the model</param>
        /// <param name="editor">Editor expression that takes the previous expression parameter and converts it into a IHtmlString</param>
        /// <param name="label">Label expression that takes the previous expression parameter and converts it into a IHtmlString</param>
        /// <returns></returns>
        public static MvcHtmlString FormElementFor<TModel, TProperty>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression,
            Func<Expression<Func<TModel, TProperty>>, MvcHtmlString> editor,
            Func<Expression<Func<TModel, TProperty>>, MvcHtmlString> label)
        {
            return FormElementFor(html, expression, editor(expression), label(expression));
        }
    }
}
