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
    public class PlayersController : ControllerBase
    {

        private IRepositoryWrapper _repoWrapper;
        
         public PlayersController(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }

       [HttpGet("team/{id}")]
        public IActionResult GetByTeam(int id)
        {
            var players =_repoWrapper.Player.FindByCondition(e=>e.TeamId==id).ToList();

            if (players == null)
                return NotFound(new {data="", message = "Team Not Found" });

            return Ok(new {data=players, message = "PLayers Found" });
        }

        [HttpGet]
        public IActionResult GetPlayers()
        {
            try
            {
                var teams=_repoWrapper.Team.SelectAll().ToList();
               return Ok(new { data= _repoWrapper.Player.IncludeRels(e=>new Player{
                   PlayerId=e.PlayerId,
                   Name=e.Name,
                   TeamId=e.TeamId,
                   BirthDate=e.BirthDate,
                   Nationality=e.Nationality,
                   Team=teams.SingleOrDefault(r=>r.TeamId==e.TeamId)
                   }).ToList(), message="Players Found Successfully"});
            //    return Ok(new { data= _repoWrapper.Player.SelectAll().ToList(), message="Players Found Successfully"});
            }
            catch (Exception exp)
            {
                 return BadRequest(new { message = exp.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var obj =  _repoWrapper.Player.FindOne(x => x.PlayerId==id);

                if (obj == null) return NotFound();

               return Ok(new { data= obj, message="Player Found Successfully"});
            }
            catch (Exception exp)
            {
                 return BadRequest(new { message = exp.Message });
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Player obj)
        {
            try
            {
                _repoWrapper.Player.Create(obj);
                _repoWrapper.Save();

                var _obj=_repoWrapper.Player.FindOne(e=>e.Name==obj.Name);
               return Ok(new { data= _obj, message="Player Created Successfully"});
               
            }
            catch (Exception exp)
            {
                 return BadRequest(new { message = exp.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Player obj)
        {
            try
            {
                _repoWrapper.Player.Update(obj);
                _repoWrapper.Save();

               return Ok(new { data= obj, message="Player Updated Successfully"});
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
                var _obj =  _repoWrapper.Player.FindOne(x => x.PlayerId==id);

                if (_obj == null) return NotFound();

                _repoWrapper.Player.Delete(_obj);
                _repoWrapper.Save();

               return Ok(new { data= _obj, message="Player Deleted Successfully"});

            }
             catch (Exception exp)
            {
                 return BadRequest(new { message = exp.Message });
            }
        }
    }
}
