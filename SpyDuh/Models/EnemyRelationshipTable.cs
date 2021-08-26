using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpyDuh.Models
{
    public class EnemyRelationshipTable
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid EnemyId { get; set; }
    }
}
