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
        EnemyTableRepository _enemyRepo;
        public SpyDuhMemberController()
        {
            _repo = new SpyDuhMemberRepository();
            _friendsRepo = new FriendTableRepository();
            _enemyRepo = new EnemyTableRepository();
        }

        [HttpGet]
        public IActionResult GetAllSpyDuhMembers()
        {
            return Ok(_repo.GetAll());
        }

        [HttpGet("{spyName}")]
        public IActionResult GetSpy(string spyName)
        {
            var spy = _repo.GetSingleSpyBySpyName(spyName);

            if (string.IsNullOrWhiteSpace(spyName)) return BadRequest("SpyName is a required field");

            if (spy == null) return NotFound("No spy with this name exists");

            return Ok(spy);
        }
        // Here we can use the spy nick name which should be unique to fetch all their friends in the URL IE. api/Jrob/friends
        [HttpGet("{spyName}/friends")]
        public IActionResult GetSingleSpyFriends(string spyName)
        {
            if (string.IsNullOrWhiteSpace(spyName)) return BadRequest("Spy name must be a string");
            // First we need to get the single instance of a spy via their spy name
            var singleSpy = _repo.GetSingleSpyBySpyName(spyName);
            if (singleSpy == null) return NotFound("Spy with this name does not exist");

            // Next we search the intermediary table for matching Guids and get the friend ID's and add them to List
            List<Guid> friendIdList = _friendsRepo.GetFriends(singleSpy.Id).ToList();

            // This list will hold the object references of the friends to send back to the httpget request
            List<Spy> friendsList = new List<Spy>();

            // Iterating over the list of friends IDs to populate the friends list with object references instead of GUIDs
            friendIdList.ForEach(friend => friendsList.Add(_repo.GetSingleSpyById(friend)));

            if (friendsList == null || friendsList.Count < 1) return Ok("No friends :*-( ");
            return Ok(friendsList);
        }

        [HttpGet("{spyName}/enemies")]
        public IActionResult GetSingleSpyEnemies(string spyName)
        {
            if (string.IsNullOrWhiteSpace(spyName)) return BadRequest("Spy name must be a string");

            var singleSpy = _repo.GetSingleSpyBySpyName(spyName);

            if (singleSpy == null) return NotFound("Spy with this name does not exist");

            List<Guid> enemyIdList = _enemyRepo.GetEnemies(singleSpy.Id).ToList();
            List<Spy> enemyList = new List<Spy>();
            enemyIdList.ForEach(enemy => enemyList.Add(_repo.GetSingleSpyById(enemy)));

            if (enemyList == null || enemyList.Count < 1) return Ok("No enemies! :-D ");

            return Ok(enemyList);
        }

        [HttpPost]
        public IActionResult AddSpyDuhMember(Spy newSpy)
        {
            if (string.IsNullOrWhiteSpace(newSpy.Name) || string.IsNullOrWhiteSpace(newSpy.SpyName)) return BadRequest("Name and Spyname are required fields");
            if (newSpy.Age <= 12) return BadRequest("Spy must be 13 or older to participate in the SpyDuh Membership Program\u2122");
            _repo.AddSpyDuh(newSpy);
            return Created($"api/{newSpy.Id}", newSpy);
        }

        [HttpPost("{userSpyName}/add-friend/{friendSpyName}")]
        public IActionResult AddFriend(string userSpyName, string friendSpyName)
        {
            var relationship = new FriendRelationshipTable
            {
                Id = Guid.NewGuid(),
                UserId = _repo.GetSingleSpyBySpyName(userSpyName).Id,
                FriendId = _repo.GetSingleSpyBySpyName(friendSpyName).Id,
            };
            var friendDoesNotExist = _friendsRepo.CheckUniqueFriendTable(relationship.UserId, relationship.FriendId);
            var enemyDoesNotExist = _enemyRepo.CheckUniqueEnemyTable(relationship.UserId, relationship.FriendId);
            if (friendDoesNotExist && enemyDoesNotExist)
            { 
                _friendsRepo.Add(relationship);
                return Created("api/[controller]", relationship);
            } else if (!enemyDoesNotExist)
            {
                _enemyRepo.Remove(relationship.UserId, relationship.FriendId);
                _friendsRepo.Add(relationship);
                return Created("api/[controller]", relationship);
            }
            return BadRequest("This person is already a friend of user");
        }

        [HttpPost("{userSpyName}/add-enemy/{enemySpyName}")]
        public IActionResult AddEnemy(string userSpyName, string enemySpyName)
        {
            var relationship = new EnemyRelationshipTable
            {
                Id = Guid.NewGuid(),
                UserId = _repo.GetSingleSpyBySpyName(userSpyName).Id,
                EnemyId = _repo.GetSingleSpyBySpyName(enemySpyName).Id,
            };
            var friendDoesNotExist = _friendsRepo.CheckUniqueFriendTable(relationship.UserId, relationship.EnemyId);
            var enemyDoesNotExist = _enemyRepo.CheckUniqueEnemyTable(relationship.UserId, relationship.EnemyId);
            if (friendDoesNotExist && enemyDoesNotExist)
            {
                _enemyRepo.Add(relationship);
                return Created("api/[controller]", relationship);
            } else if (!friendDoesNotExist)
            {
                _friendsRepo.Remove(relationship.UserId, relationship.EnemyId);
                _enemyRepo.Add(relationship);
                return Created("api/[controller]", relationship);
            }
            return BadRequest("This person is already an enemy of user");
        }

        [HttpGet("/allSpyDuhMemberSkills")]
        public IActionResult GetAllSkills()
        {
            return Ok(_repo.GetAllSpySkills());
        }


        [HttpGet("{id}/skillsBySpyDuhMemberId")]
        public IActionResult FindSpy(Guid id)
        {
            var spy = _repo.GetSingleSpyById(id);

            if (spy == null)
            {
                return BadRequest("No matching ID found");
            }
            
            return Ok(_repo.GetSkillById(id));
        }
    }
}
