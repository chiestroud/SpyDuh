using SpyDuh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpyDuh.DataAccess
{
    public class SpyDuhMemberRepository
    {
        static List<Spy> _spyDuhMembers = new List<Spy>();

        internal void AddSpyDuh(Spy newSpy)
        {
            _spyDuhMembers.Add(newSpy);
            SpyRepository.RemoveSpy(newSpy);
        }

        internal IEnumerable<Spy> GetAll()
        {
            return _spyDuhMembers;
        }

        internal IEnumerable<SpyDuhMember> GetSkills(Skills skills)
        {
            return _spyDuhMembers.Where(x => x.Skills.Any(skills));
        }
    }

}
