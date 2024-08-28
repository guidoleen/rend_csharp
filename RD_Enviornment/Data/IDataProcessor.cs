

using System.Linq.Expressions;

namespace RD_Enviornment.Data
{
	public interface IDataProcessor<T> where T : class
	{
		public Task<T[]> Find(string field, object value);
		public Task<T[]> Find(Expression<Func<T, bool>> expression);
		public Task InsertOne(T obj);
		public Task<bool> UpdateOne(T obj);
		public Task<bool> Delete(T obj);
	}
}
