using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BaselineSolution.Framework.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ExtensionsForLambdaExpression
    {
        public static string GetPropertyName<TProperty>(this Expression<Func<TProperty>> propertyExpression)
        {
            MemberExpression memberExpression; if (!TryFindMemberExpression(propertyExpression.Body, out memberExpression))
                return string.Empty;
            return GetPropertyName(memberExpression);
        }

        public static string GetPropertyName<TModel, TProperty>(
            this Expression<Func<TModel, TProperty>> propertyExpression)
        {
            MemberExpression memberExpression;
            if (!TryFindMemberExpression(propertyExpression.Body, out memberExpression))
                return string.Empty;

            return GetPropertyName(memberExpression);
        }

        private static string GetPropertyName(MemberExpression memberExpression)
        {
            var memberNames = new Stack<string>();
            do
            {
                memberNames.Push(memberExpression.Member.Name);
            }
            while (TryFindMemberExpression(memberExpression.Expression, out memberExpression));

            return string.Join(".", memberNames.Except(new[] { "Model" }, StringComparer.OrdinalIgnoreCase));
        }

        private static bool TryFindMemberExpression(Expression expression, out MemberExpression memberExpression)
        {
            memberExpression = expression as MemberExpression;
            if (memberExpression != null)
            {
                return true;
            }

            if (IsConversion(expression) && expression is UnaryExpression)
            {
                memberExpression = ((UnaryExpression)expression).Operand as MemberExpression;
                if (memberExpression != null)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsConversion(Expression exp)
        {
            return (
                exp.NodeType == ExpressionType.Convert ||
                exp.NodeType == ExpressionType.ConvertChecked
            );
        }

        /// <summary>
        ///     Method to downcast lambda expressions in a typesafe way
        /// </summary>
        /// <param name="expression"></param>
        /// <typeparam name="TOriginal"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TCast"></typeparam>
        /// <returns></returns>
        public static Expression<Func<TCast, TResult>> Cast<TOriginal, TResult, TCast>(this Expression<Func<TOriginal, TResult>> expression) where TCast : TOriginal
        {
            Expression<Func<TOriginal, TCast>> conversion = entity => (TCast)entity;
            var lambdaParameter = Expression.Parameter(typeof(TCast), "cast");
            var lambdaBody = Expression.Invoke(expression, Expression.Invoke(conversion, lambdaParameter));
            return Expression.Lambda<Func<TCast, TResult>>(lambdaBody, lambdaParameter);
        }
    }
}
