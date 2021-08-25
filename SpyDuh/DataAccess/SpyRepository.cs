using SpyDuh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpyDuh.DataAccess
{
    public class SpyRepository
    {
        static List<Spy> _spies = new List<Spy>
        {
            new Spy
            {
                Name = "Jesse",
                Age = 27,
                SpyName = "Messy Jesse",
                Skills = new List<Skills> {Skills.Lying, Skills.Sneaking},
                Id = Guid.NewGuid()
            },
            new Spy
            {
                Name = "Lindsey",
                Age = 30,
                SpyName = "Lockpicking Lindsey",
                Skills = new List<Skills> {Skills.Lockpicking, Skills.Hacking},
                Id = Guid.NewGuid()
            },
            new Spy
            {
                Name = "Chie",
                Age = 34,
                SpyName = "Pikachie",
                Skills = new List<Skills> {Skills.Sneaking, Skills.Spying},
                Id = Guid.NewGuid()
            },

        };


        internal object RemoveSkillById(Guid id, Skills skill)
        {
            var skills = _spies.FirstOrDefault(x => x.Id == id).Skills;
            skills.Remove(skill);
            return _spies;
        }

        internal object AddSkillById(Guid id, Skills skill)
        {
            var skills = _spies.FirstOrDefault(x => x.Id == id).Skills;
            skills.Add(skill);
            return _spies;
        }

        internal void Add(Spy newSpy)
        {
            _spies.Add(newSpy);
        }

        internal IEnumerable<Spy> GetAll()
        {
            return _spies;
        }

        static internal List<Spy> RemoveSpy(Spy spy)
        {

            var spyToRemove = _spies.FirstOrDefault(x => x.SpyName == spy.SpyName);
            _spies.Remove(spyToRemove);
            return _spies;
        }
    }
}
