using SpyDuh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpyDuh.DataAccess
{
    public class EnemyTableRepository
    {
        static List<EnemyRelationshipTable> _enemyTable = new List<EnemyRelationshipTable>();

        internal void Add(EnemyRelationshipTable relationshipTable)
        {
            _enemyTable.Add(relationshipTable);
        }
        internal List<EnemyRelationshipTable> Remove(Guid userId, Guid enemyId)
        {
            var enemyToRemove = _enemyTable.FirstOrDefault(table => table.UserId == userId && table.EnemyId == enemyId);
            _enemyTable.Remove(enemyToRemove);
            return _enemyTable;
        }
        internal IEnumerable<Guid> GetEnemies(Guid userId)
        {
            var tables = _enemyTable.Where(tables => tables.UserId == userId).ToList();
            List<Guid> enemyList = new List<Guid>();
            tables.ForEach(id => enemyList.Add(id.EnemyId));
            return enemyList;
        }
        internal bool CheckUniqueEnemyTable(Guid userId, Guid enemyId)
        {
            var uniqueId = _enemyTable.FirstOrDefault(table => table.EnemyId == enemyId && table.UserId == userId);
            if (uniqueId != null)
            {
                return false;
            }
            return true;
        }
    }
}
