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
    public class RolesController : ControllerBase
    {

        private IRepositoryWrapper _repoWrapper;
        
         public RolesController(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
       
       
        
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
               return Ok(new { data= _repoWrapper.Role.SelectAll().ToList(), message="Roles Found Successfully"});
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
                var _obj =  _repoWrapper.Role.FindOne(x => x.Id==id);

                if (_obj == null)return NotFound();

               return Ok(new { data= _obj, message="Role Found Successfully"});
            }
            catch (Exception exp)
            {
                 return BadRequest(new { message = exp.Message });
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Role obj)
        {
            try
            {
                _repoWrapper.Role.Create(obj);
                _repoWrapper.Save();

                var _obj=_repoWrapper.Role.FindOne(e=>e.Name==obj.Name);
               return Ok(new { data= _obj, message="Role Created Successfully"});
               
            }
            catch (Exception exp)
            {
                 return BadRequest(new { message = exp.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Role obj)
        {
            try
            {
                _repoWrapper.Role.Update(obj);
                _repoWrapper.Save();

               return Ok(new { data= obj, message="Role Updated Successfully"});
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
                var _obj =  _repoWrapper.Role.FindOne(x => x.Id==id);

                if (_obj == null) return NotFound();

                _repoWrapper.Role.Delete(_obj);
                _repoWrapper.Save();
                
               return Ok(new { data= _obj, message="Role Deleted Successfully"});

            }
             catch (Exception exp)
            {
                 return BadRequest(new { message = exp.Message });
            }
        }
    }
}
