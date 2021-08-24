using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpyDuh.Models
{
    public class SpyDuhMember : Spy
    {
        public List<Guid> FriendList { get; set; }
        public List<Guid> EnemyList { get; set; }
    }

    
}
