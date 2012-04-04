using System;

namespace Chello.Tests
{
	/// <summary>
	/// Classes to represent an entities type and ID, to allow for generic storing in an un-typed (erk!) bag.
	/// </summary>
	public struct TrelloTestDataKey
	{
		public Type EntityType { get; set; }
		public string EntityId { get; set; }

		public override bool Equals(Object obj)
		{
			return obj is TrelloTestDataKey && this == (TrelloTestDataKey)obj;
		}
		public override int GetHashCode()
		{
			return this.EntityType.GetHashCode() ^ this.EntityId.GetHashCode();
		}
		public static bool operator ==(TrelloTestDataKey x, TrelloTestDataKey y)
		{
			return x.EntityType == y.EntityType && x.EntityId == y.EntityId;
		}
		public static bool operator !=(TrelloTestDataKey x, TrelloTestDataKey y)
		{
			return !(x == y);
		}
	}
}
