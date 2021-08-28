using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpyDuh.Models
{
    public class Spy
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string SpyName { get; set; }

        public List<Skills> Skills { get; set; }
        public Guid Id { get; internal set; }

        public Spy()
        {
            Id = Guid.NewGuid();
        }
    }

    public enum Skills
    {
        Spying,
        Lockpicking,
        Hacking,
        Sneaking,
        Lying,
    }
}
