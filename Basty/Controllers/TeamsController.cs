using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Basty.Models;
using Basty.Interfaces;
using Basty.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;


namespace Basty.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {

        private IRepositoryWrapper _repoWrapper;
        
         public TeamsController(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
       
       
        
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
               return Ok(new { data= _repoWrapper.Team.SelectAll().ToList(), message="Teams Found Successfully"});
            }
            catch (Exception exp)
            {
                 return BadRequest(new { message = exp.Message });
            }
        }

        // GET api/teams/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var _obj =  _repoWrapper.Team.FindOne(x => x.TeamId==id);

                if (_obj == null)return NotFound();

               return Ok(new { data= _obj, message="Team Found Successfully"});
            }
            catch (Exception exp)
            {
                 return BadRequest(new { message = exp.Message });
            }
        }

        // POST api/teams
        [HttpPost]
        public IActionResult Post([FromBody] Team obj)
        {
            try
            {
                _repoWrapper.Team.Create(obj);
                _repoWrapper.Save();

                var _obj=_repoWrapper.Team.FindOne(e=>e.Name==obj.Name);
               return Ok(new { data= _obj, message="Team Created Successfully"});
               
            }
            catch (Exception exp)
            {
                 return BadRequest(new { message = exp.Message });
            }
        }

        // PUT api/teams/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Team obj)
        {
            try
            {
                _repoWrapper.Team.Update(obj);
                _repoWrapper.Save();

               return Ok(new { data= obj, message="Team Updated Successfully"});
            }
             catch (Exception exp)
            {
                 return BadRequest(new { message = exp.Message });
            }
            
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _repoWrapper.Player.RemoveAllRange(e=>e.TeamId==id);
                var _obj =  _repoWrapper.Team.FindOne(x => x.TeamId==id);

                if (_obj == null) return NotFound();

                _repoWrapper.Team.Delete(_obj);

                _repoWrapper.Save();
                
               return Ok(new { data= _obj, message="Team Deleted Successfully"});

            }
             catch (Exception exp)
            {
                 return BadRequest(new { message = exp.Message });
            }
        }
    }
}
