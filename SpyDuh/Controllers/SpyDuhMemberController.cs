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
        // Here we can use the spy nick name which should be unique to fetch all their friends in the URL IE. api/Jrob/friends
        [HttpGet("{spyName}/friends")]
        public List<Spy> GetSingleSpyFriends(string spyName)
        {
            // First we need to get the single instance of a spy via their spy name
            var singleSpy = _repo.GetSingleSpyBySpyName(spyName);

            // Next we search the intermediary table for matching Guids and get the friend ID's and add them to List
            List<Guid> friendIdList = _friendsRepo.GetFriends(singleSpy.Id).ToList();

            // This list will hold the object references of the friends to send back to the httpget request
            List<Spy> friendsList = new List<Spy>();

            // Iterating over the list of friends IDs to populate the friends list with object references instead of GUIDs
            friendIdList.ForEach(friend => friendsList.Add(_repo.GetSingleSpyById(friend)));

            return friendsList;
        }

        [HttpPost]
        public void AddSpyDuhMember(Spy newSpy)
        {
            _repo.AddSpyDuh(newSpy);
        }
        [HttpPost("{user}/add/{friend}")]
        public IActionResult AddFriend(string user, string friend)
        {
            var relationship = new FriendRelationshipTable
            {
                Id = Guid.NewGuid(),
                UserId = _repo.GetSingleSpyBySpyName(user).Id,
                FriendId = _repo.GetSingleSpyBySpyName(friend).Id,
            };

            if (_friendsRepo.CheckUniqueTable(relationship.UserId, relationship.FriendId))
            { 
                _friendsRepo.Add(relationship);
                return Created("api/[controller]", relationship);
            }
            return BadRequest("This person is already a friend of user");
        }

        [HttpGet("/allSpyDuhMemberSkills")]
        public List<string> GetAllSkills()
        {
            return _repo.GetAllSpySkills();
        }

        [HttpGet("{id}/skillsBySpyDuhMemberId")]
        public object FindSpy(Guid id)
        {
            return _repo.GetSkillById(id);
        }

    }
}
