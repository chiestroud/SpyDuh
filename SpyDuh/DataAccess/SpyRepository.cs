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
                Id = Guid.NewGuid(),
                AssignmentDaysLeft = 5
            },
            new Spy
            {
                Name = "Lindsey",
                Age = 30,
                SpyName = "Lockpicking Lindsey",
                Skills = new List<Skills> {Skills.Lockpicking, Skills.Hacking},
                Id = Guid.NewGuid(),
                AssignmentDaysLeft = 100
            },
            new Spy
            {
                Name = "Chie",
                Age = 34,
                SpyName = "Pikachie",
                Skills = new List<Skills> {Skills.Sneaking, Skills.Spying},
                Id = Guid.NewGuid(),
                AssignmentDaysLeft = 3
            },
            new Spy
            {
                Name = "Rob",
                Age = 28,
                SpyName = "The Invisible",
                Skills = new List<Skills> {Skills.Spying, Skills.Hacking},
                Id = Guid.NewGuid(),
                AssignmentDaysLeft = 0
            }

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
            if (skills.Contains(skill))
            {
                return _spies;
            }
            skills.Add(skill);
            return _spies;
        }

        internal object FindSkillWithId(Guid id)
        {
            var skills = _spies.FirstOrDefault(x => x.Id == id).Skills;
            List<string> spySkill = new List<string>();
            foreach(var skill in skills)
            {
                spySkill.Add(skill.ToString());
            }
            return spySkill;
        }

        internal object FindSpyWithId(Guid id)
        {
            var spy = _spies.FirstOrDefault(x => x.Id == id);
            return spy;
        }

        internal object FindSkillFromSpy(Guid id, Skills skill)
        {
            var spy = _spies.FirstOrDefault(x => x.Id == id).Skills;
            if (spy.Contains(skill))
            {
                return spy;
            }
            else
            {
                return null;
            }
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
