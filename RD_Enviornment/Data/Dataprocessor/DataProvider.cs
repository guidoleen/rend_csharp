using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver.Linq;
using RD_Enviornment.Types;
using System;
using System.Linq.Expressions;
using ZstdSharp.Unsafe;

namespace RD_Enviornment.Data.Dataprocessor
{
	internal class DataProvider<T> : IDataProcessor<T> where T : class
	{
		private IMongoClient? _client;
		private string _collectionName = String.Empty;
		private string _databaseName = String.Empty;

		internal DataProvider<T> InitDataProvider(string connectionString, string databaseName)
		{
			this._client = new MongoClient(connectionString);
			this._collectionName = typeof(T).Name.ToLower();
			this._databaseName = databaseName;

			return this;
		}

		private IMongoCollection<T> GetCollection() => this._client!.GetDatabase(this._databaseName).GetCollection<T>(this._collectionName);
		private IMongoCollection<BsonDocument> GetDefaultCollection() => this._client!.GetDatabase(this._databaseName).GetCollection<BsonDocument>(this._collectionName);

		public async Task<bool> Delete(T obj)
		{
			var bsonDocument = obj.ToBsonDocument();
			var result = await this.GetDefaultCollection().DeleteOneAsync(ob => ob["_id"] == bsonDocument["_id"]);
			return result.DeletedCount != 0;
		}

		public async Task<T[]> Find(string field, object value)
		{
			var filter = Builders<T>.Filter.Eq(field, value);
			var result = await this.GetCollection().FindAsync<T>(filter);
			return result.ToList().ToArray();
		}

		public async Task<T[]> Find(Expression<Func<T, bool>> expression)
		{
			var result = await this.GetCollection().FindAsync<T>(expression);
			return result.ToList().ToArray();
		}

		public async Task InsertOne(T obj)
		{
			await this.GetCollection().InsertOneAsync(obj);
		}

		public async Task<bool> UpdateOne(T obj)
		{
			var bsonObj = obj.ToBsonDocument();
			var result = await this.GetDefaultCollection().ReplaceOneAsync(ob => ob["_id"] == bsonObj["_id"], bsonObj, new ReplaceOptions { IsUpsert = true });
			return result.ModifiedCount != 0;
		}
	}
}
