using SpyDuh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpyDuh.DataAccess
{
    public class SpyDuhMemberRepository
    {
        static List<Spy> _spyDuhMembers = new List<Spy>
        {
            new SpyDuhMember
            {
                Id = Guid.NewGuid(),
                Name = "Jin",
                SpyName = "Raptor-Johnny",
                Age = 54,
                Skills = new List<Skills>{ Skills.Spying, Skills.Lockpicking, Skills.Hacking },
                AssignmentDaysLeft = 3
            },
            new SpyDuhMember
            {
                Id = Guid.NewGuid(),
                Name = "Sonny",
                SpyName = "The-Morning-Star",
                Age = 15,
                Skills = new List<Skills>{ Skills.Spying, Skills.Lying },
                AssignmentDaysLeft = 56
            },
            new SpyDuhMember
            {
                Id = Guid.NewGuid(),
                Name = "Kenji",
                SpyName = "Okonomiyaki",
                Age = 54,
                Skills = new List<Skills>{ Skills.Spying, Skills.Sneaking },
                AssignmentDaysLeft = 1
            },
            new SpyDuhMember
            {
                Id = Guid.NewGuid(),
                Name = "Demetri",
                SpyName = "Slav-Squat",
                Age = 54,
                Skills = new List<Skills>{ Skills.Spying, Skills.Hacking, Skills.Lockpicking },
                AssignmentDaysLeft = 100
            },
            new SpyDuhMember
            {
                Id = Guid.NewGuid(),
                Name = "Sara",
                SpyName = "Black-Widow",
                Age = 54,
                Skills = new List<Skills>{ Skills.Spying },
                AssignmentDaysLeft = 0
            },
            new SpyDuhMember
            {
                Id = Guid.NewGuid(),
                Name = "Theocles",
                SpyName = "Zoo-Zeus",
                Age = 91,
                Skills = new List<Skills>{ Skills.Spying, Skills.Lying },
                AssignmentDaysLeft = 7
            },
        };

        internal void AddSpyDuh(Spy newSpy)
        {
            _spyDuhMembers.Add(newSpy);
            SpyRepository.RemoveSpy(newSpy);
        }

        internal IEnumerable<Spy> GetAll()
        {
            return _spyDuhMembers;
        }

        internal Spy GetSingleSpyById(Guid id)
        {
            return _spyDuhMembers.FirstOrDefault(spy => spy.Id == id);
        }

        internal Spy GetSingleSpyBySpyName(string spyName)
        {
            return _spyDuhMembers.FirstOrDefault(spy => spy.SpyName == spyName);
        }

        // Lists all the spyduh member skills
        internal List<string> GetAllSpySkills()
        {
            List<string> skills = new List<string>();
            foreach(var spy in _spyDuhMembers)
            {
                foreach (var skill in spy.Skills)
                {
                    skills.Add(skill.ToString());
                }
            }
            return skills;
        }

        // Gets skills from spyduh member id
        internal object GetSkillById(Guid id)
        {
            var skills = _spyDuhMembers.FirstOrDefault(x => x.Id == id).Skills;
            List<string> spySkill = new List<string>();
            foreach(var skill in skills)
            {
                spySkill.Add(skill.ToString());
            }
            return spySkill;
        }

        internal IEnumerable<Spy> GetSpiesBySkill(Skills skill)
        {
            var filteredSpyList = _spyDuhMembers.Where(spy => spy.Skills.Contains(skill));
            return filteredSpyList;
        }

        internal object AssignmentDaysLeftById(Guid id)
        {
            var daysLeft = _spyDuhMembers.FirstOrDefault(x => x.Id == id).AssignmentDaysLeft;
            return daysLeft;
        }
    }
}
