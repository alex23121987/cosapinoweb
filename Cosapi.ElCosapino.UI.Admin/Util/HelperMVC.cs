using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Cosapi.ElCosapino.UI.Admin.Util
{
    public static class HelperMVC
    {
        public static MvcHtmlString RawActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            //ajaxOptions.OnBegin = "LoadLoading()";
            //ajaxOptions.OnComplete = "UnloadLoading()";
            var repID = Guid.NewGuid().ToString();
            var lnk = ajaxHelper.ActionLink(repID, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes);
            return MvcHtmlString.Create(lnk.ToString().Replace(repID, linkText));
        }

        public static MvcHtmlString MyTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var attr = MergeAnonymous(new { @class = "form-control" }, htmlAttributes);
            return html.TextBoxFor<TModel, TProperty>(expression, attr);
        }

        public static MvcHtmlString MyTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var attr = MergeAnonymous(new { @class = "form-control" }, htmlAttributes);
            return html.TextAreaFor<TModel, TProperty>(expression, attr);
        }

        public static MvcHtmlString MyCkEditorFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, int rows, object htmlAttributes = null)
        {
            var attr = MergeAnonymous(new { @class = "ckeditor form-control", @rows = rows }, htmlAttributes);
            return html.TextAreaFor(expression, attr);
        }
        public static MvcHtmlString MyCkEditorFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var attr = MergeAnonymous(new { @class = "ckeditor form-control", @rows = "10" }, htmlAttributes);
            return html.TextAreaFor(expression, attr);
        }

        public static MvcHtmlString MyDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList)
        {
            return html.DropDownListFor(expression, selectList, new { @class = "form-control" });
        }

        private static string GetPropertyNameFromExpression<TModel, TProperty>(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            MemberExpression memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
                throw new InvalidOperationException("Not a memberExpression");

            if (!(memberExpression.Member is PropertyInfo))
                throw new InvalidOperationException("Not a property");

            return memberExpression.Member.Name;
        }

        private static IDictionary<string, object> MergeAnonymous(object obj1, object obj2)
        {
            var dict1 = new RouteValueDictionary(obj1);
            var dict2 = new RouteValueDictionary(obj2);
            IDictionary<string, object> result = new Dictionary<string, object>();

            foreach (var pair in dict1.Concat(dict2))
            {
                result.Add(pair);
            }

            return result;
        }

        public static string IsActive(this HtmlHelper html, int value)
        {
            object menuId = 0;
            html.ViewData.TryGetValue("menuActive", out menuId);
            if (menuId == null) return "";
            var result = value == (int)menuId ? "active" : "";
            return result;
        }
        public static string IsActive(this HtmlHelper html, int[] values)
        {
            object menuId = 0;
            html.ViewData.TryGetValue("menuActive", out menuId);
            if (menuId == null) return "";
            var id = (int)menuId;
            var result = values.Any(x => x == id) ? "active" : "";

            return result;
        }
    }
}