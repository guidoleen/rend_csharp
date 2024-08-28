using System;
using System.Linq.Expressions;

namespace RD_Enviornment.Test
{
	internal class ExpressionTest<T> where T : class
	{
		internal object ExpressionValidate(T? obj, Expression<Func<T, object>> expression)
		{
			if (obj == null) return null;

            Func<T, object> compliled = expression.Compile();

			var result = compliled(obj!);

			return result;
        }
	}
}