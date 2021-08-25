using SpyDuh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpyDuh.DataAccess
{
    public class FriendTableRepository
    {
        static List<FriendRelationshipTable> _friendTable = new List<FriendRelationshipTable>();
        internal void Add(FriendRelationshipTable relationshipTable)
        {
            relationshipTable.Id = Guid.NewGuid();
            _friendTable.Add(relationshipTable);
        }
        internal IEnumerable<Guid> GetFriends(Guid userId)
        {
            var tables = _friendTable.Where(table => table.FriendId == userId).ToList();
            List<Guid> friendsList = new List<Guid>();
            tables.ForEach(id => friendsList.Add(id.FriendedId));
            return friendsList;
        }
    }
}
