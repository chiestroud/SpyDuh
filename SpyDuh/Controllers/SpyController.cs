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

        [HttpGet]
        public IEnumerable<Spy> GetAllSpies()
        {
            return _repo.GetAll();
        }
    }
}
