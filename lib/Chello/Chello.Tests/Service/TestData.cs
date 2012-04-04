using System;
using System.Collections.Generic;
using System.Linq;
using Chello.Core;

namespace Chello.Tests
{
	public class TestData
	{
		public string AuthKey { get; private set; }

		// Temp strings for merging
		public static string UserName = "TESTMEMB1";
		public static string BoardName = "TESTBOARD1";
		public static string CardName = "TESTCARD1";
		public static string OrganizationName = "TESTORG1";
		public static string List1Name = "TESTLIST1";
		public static string List2Name = "TESTLIST2";

		protected Dictionary<TrelloTestDataKey, ITrelloEntity> entities = new Dictionary<TrelloTestDataKey, ITrelloEntity>();
		protected List<KeyValuePair<TrelloTestDataKey, TrelloTestDataKey>> relationships = new List<KeyValuePair<TrelloTestDataKey, TrelloTestDataKey>>();

		public TestData(string authKey)
		{
			this.AuthKey = authKey;
		}

		/// <summary>
		/// Adds a new entity to the "data store".
		/// </summary>
		/// <typeparam name="T">The type of entity to store (must be an ITrelloEntity).</typeparam>
		/// <param name="entity">The entity to store.</param>
		public void Add<T>(T entity)
			where T : ITrelloEntity
		{
			// Construct a key for this entity using the type and ID.
			var key = new TrelloTestDataKey { EntityType = typeof(T), EntityId = entity.Id };

			// Don't allow us to store two entities with the same ID (per entity).
			if (entities.ContainsKey(key))
				throw new ArgumentException("Item already exists in the \"database\"", "entity");

			// Add it to the "database".
			entities.Add(key, entity);
		}

		/// <summary>
		/// Updates an entity in the "data store".
		/// </summary>
		/// <typeparam name="T">The type of entity to update (must be an ITrelloEntity).</typeparam>
		/// <param name="entity">The entity to update.</param>
		public void Update<T>(T entity)
			where T : ITrelloEntity
		{
			// Construct a key for this entity using the type and ID.
			var key = new TrelloTestDataKey { EntityType = typeof(T), EntityId = entity.Id };

			// Update the value stored.
			entities[key] = entity;
		}

		/// <summary>
		/// Gets an entity from the "data store".
		/// </summary>
		/// <typeparam name="T">The type of entity to retrieve.</typeparam>
		/// <param name="id">The ID of the entity to retrieve.</param>
		public T Get<T>(string id)
			where T : class, ITrelloEntity
		{
			// Construct a key for this entity using the type and ID.
			var key = new TrelloTestDataKey { EntityType = typeof(T), EntityId = id };

			return entities[key] as T;
		}

		/// <summary>
		/// Adds a relationship between two entities.
		/// </summary>
		/// <typeparam name="T1">Type of first entity</typeparam>
		/// <typeparam name="T2">Type of second entity</typeparam>
		/// <param name="entity1ID">ID of first entity</param>
		/// <param name="entity2ID">ID of second entity</param>
		public void AddRelationship<T1, T2>(string entity1ID, string entity2ID)
			where T1 : ITrelloEntity
			where T2 : ITrelloEntity
		{
			// Construct the keys for the entities
			var key1 = new TrelloTestDataKey { EntityType = typeof(T1), EntityId = entity1ID };
			var key2 = new TrelloTestDataKey { EntityType = typeof(T2), EntityId = entity2ID };

			// Add to the "data store"
			this.relationships.Add(new KeyValuePair<TrelloTestDataKey, TrelloTestDataKey>(key1, key2));
		}

		/// <summary>
		/// Removes a relationship between two entities.
		/// </summary>
		/// <typeparam name="T1">Type of first entity</typeparam>
		/// <typeparam name="T2">Type of second entity</typeparam>
		/// <param name="entity1ID">ID of first entity</param>
		/// <param name="entity2ID">ID of second entity</param>
		public void RemoveRelationship<T1, T2>(string entity1ID, string entity2ID)
			where T1 : ITrelloEntity
			where T2 : ITrelloEntity
		{
			// Construct the keys for the entities
			var key1 = new TrelloTestDataKey { EntityType = typeof(T1), EntityId = entity1ID };
			var key2 = new TrelloTestDataKey { EntityType = typeof(T2), EntityId = entity2ID };

			// Add to the "data store"
			this.relationships.Remove(new KeyValuePair<TrelloTestDataKey, TrelloTestDataKey>(key1, key2));
		}

		public IEnumerable<T2> GetRelated<T1, T2>(string id)
			where T1 : ITrelloEntity
			where T2 : ITrelloEntity
		{
			var key = new TrelloTestDataKey { EntityType = typeof(T1), EntityId = id };

			// Find any relationships that are related to this entity.
			var rels = relationships
				.Where(kvp => kvp.Key == key || kvp.Value == key) // Where either side are for our object
				.Select(kvp => kvp.Key == key ? kvp.Value : kvp.Key) // Grab the side that isn't our object
				.Where(k => k.EntityType == typeof(T2))
				.ToList();

			// Return any entities that have one of these keys.
			return entities
				.Where(kvp => rels.Contains(kvp.Key))
				.Select(kvp => kvp.Value)
				.Cast<T2>();
		}
	}
}
