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
    public class SpyController : ControllerBase
    {
        SpyRepository _repo;
        public SpyController()
        {
            _repo = new SpyRepository();
        }

        // Get all Spies
        [HttpGet]
        public IEnumerable<Spy> GetAllSpies()
        {
            return _repo.GetAll();
        }

        // Get skills from Spy ID
        [HttpGet("{id}/skills")]
        public IActionResult FindSpy(Guid id)
        {
            if (_repo.FindSpyWithId(id) is null)
            {
                return BadRequest("No matching ID found");
            }
            return Ok(_repo.FindSkillWithId(id));
        }

        // Add a new spy
        [HttpPost]

        public IActionResult AddSpy(Spy newSpy)
        {
            if (string.IsNullOrEmpty(newSpy.Name) || string.IsNullOrEmpty(newSpy.SpyName))
            {
                return BadRequest("Name and SpyName are required fields");
            }
            _repo.Add(newSpy);
            return Created("/spy/1", newSpy);
        }

      

        // Add a new skill to a spy
        [HttpPut("{id}/skills")]
        public IActionResult UpdateSkill(Guid id, Skills skill)
        {
            if (_repo.FindSpyWithId(id) is null)
            {
                return BadRequest("We don't have a spy with the ID");
            }
            else
            {
                return Ok(_repo.AddSkillById(id, skill));
            }
        }

        // Delete sklls from a spy
        [HttpDelete("{id}/skills")]
        public IActionResult RemoveSkill(Guid id, Skills skill)
        {
            if (_repo.AddSkillById(id, skill) is null)
            {
                return BadRequest("The spy doesn't have the skill. Try again");
            }
            else if (_repo.FindSpyWithId(id) is null)
            {
                return BadRequest("We don't have a spy with the ID");
            }
            {
                return Ok(_repo.RemoveSkillById(id, skill));
            }
            
        }

        
    }
}
