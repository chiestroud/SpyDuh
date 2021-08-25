using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpyDuh.DataAccess;
using SpyDuh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpyDuh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpyDuhMemberController : ControllerBase
    {
        SpyDuhMemberRepository _repo;
        public SpyDuhMemberController()
        {
            _repo = new SpyDuhMemberRepository();
        }

        [HttpGet]
        public IEnumerable<Spy> GetAllSpyDuhMembers()
        {
            return _repo.GetAll();
        }
        [HttpPost]

        public void AddSpyDuhMember(Spy newSpy)
        {
            _repo.AddSpyDuh(newSpy);
        }

        [HttpGet]
        public IEnumerable<SpyDuhMember> GetAllSpyDuhMembersSkills(Skills skills)
        {
            return _repo.GetSkills(skills);
        }

    }
}
