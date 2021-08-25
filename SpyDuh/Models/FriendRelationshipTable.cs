using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpyDuh.Models
{
    public class FriendRelationshipTable
    {
        public Guid Id { get; set; }
        public Guid FriendId { get; set; }
        public Guid FriendedId { get; set; }
    }
}
