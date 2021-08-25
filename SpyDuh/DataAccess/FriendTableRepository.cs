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
            _friendTable.Add(relationshipTable);
        }
        internal IEnumerable<Guid> GetFriends(Guid userId)
        {
            var tables = _friendTable.Where(table => table.UserId == userId).ToList();
            List<Guid> friendsList = new List<Guid>();
            tables.ForEach(id => friendsList.Add(id.FriendId));
            return friendsList;
        }
        internal bool CheckUniqueTable(Guid userId, Guid friendId)
        {
            var uniqueId = _friendTable.FirstOrDefault(table => table.FriendId == friendId && table.UserId == userId);
            if (uniqueId != null)
            {
                return false;
            }
            return true;
        }
    }
}
