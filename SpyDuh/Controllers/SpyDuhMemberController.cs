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
        FriendTableRepository _friendsRepo;
        public SpyDuhMemberController()
        {
            _repo = new SpyDuhMemberRepository();
            _friendsRepo = new FriendTableRepository();
        }

        [HttpGet]
        public IEnumerable<Spy> GetAllSpyDuhMembers()
        {
            return _repo.GetAll();
        }
        [HttpGet("{spyName}")]
        public Spy GetSpy(string spyName)
        {
           return _repo.GetSingleSpyBySpyName(spyName);
        }
        [HttpGet("{spyName}/friends")]
        public List<Spy> GetSingleSpyFriends(string spyName)
        {
            var spy = _repo.GetSingleSpyBySpyName(spyName);
            var friendIdList = _friendsRepo.GetFriends(spy.Id);
            List<Spy> friendsList = new List<Spy>();
            foreach (var friend in friendIdList)
            {
                friendsList.Add(_repo.GetSingleSpyById(friend));
            }
            return friendsList;
        }
        [HttpPost]

        public void AddSpyDuhMember(Spy newSpy)
        {
            _repo.AddSpyDuh(newSpy);
        }
        [HttpPost("{friend}/add/{friended}")]
        public void AddFriend(string friend, string friended)
        {
            var relationship = new FriendRelationshipTable
            {
                FriendId = _repo.GetSingleSpyBySpyName(friend).Id,
                FriendedId = _repo.GetSingleSpyBySpyName(friended).Id,
            };

            _friendsRepo.Add(relationship);
        }
    }
}
