using System;
using System.Linq.Expressions;

namespace OnBoardingTracker.Application.Infrastructure.Expressions.Extensions
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> expression, Expression<Func<T, bool>> other)
        {
            ParameterExpression param = expression.Parameters[0];
            if (ReferenceEquals(param, other.Parameters[0]))
            {
                expression = Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expression.Body, other.Body), param);
            }
            else
            {
                expression = Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expression.Body, Expression.Invoke(other, param)), param);
            }

            return expression;
        }
    }
}
