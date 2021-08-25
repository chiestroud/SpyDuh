using SpyDuh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpyDuh.DataAccess
{
    public class SpyDuhMemberRepository
    {
        //static List<Spy> _spyDuhMembers = new List<Spy>();

        static List<Spy> _spyDuhMembers = new List<Spy>
        {
            new SpyDuhMember
            {
                Id = Guid.NewGuid(),
                Name = "Jin",
                SpyName = "Raptor-Johnny",
                Age = 54,
                Skills = new List<Skills>{ Skills.Spying }
            },
            new SpyDuhMember
            {
                Id = Guid.NewGuid(),
                Name = "Sonny",
                SpyName = "The-Morning-Star",
                Age = 15,
                Skills = new List<Skills>{ Skills.Spying }
            },
            new SpyDuhMember
            {
                Id = Guid.NewGuid(),
                Name = "Kenji",
                SpyName = "Okonomiyaki",
                Age = 54,
                Skills = new List<Skills>{ Skills.Spying }
            },
            new SpyDuhMember
            {
                Id = Guid.NewGuid(),
                Name = "Demetri",
                SpyName = "Slav-Squat",
                Age = 54,
                Skills = new List<Skills>{ Skills.Spying }
            },
            new SpyDuhMember
            {
                Id = Guid.NewGuid(),
                Name = "Sara",
                SpyName = "Black-Widow",
                Age = 54,

                Skills = new List<Skills>{ Skills.Spying }
            },
            new SpyDuhMember
            {
                Id = Guid.NewGuid(),
                Name = "Theocles",
                SpyName = "Zoo-Zeus",
                Age = 91,
                Skills = new List<Skills>{ Skills.Spying }
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

        // internal IEnumerable<SpyDuhMember> GetSkills(Skills skills)
        //{
        //return (IEnumerable<SpyDuhMember>)_spyDuhMembers.Where(x => x.Skills.Contains(skills));
        //}

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
    }

}
