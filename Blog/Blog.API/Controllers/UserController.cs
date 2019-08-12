using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Blog.API.Models;
using Blog.API.Services.IServices;
using Blog.Core.Enums;
using AutoMapper;
using Blog.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private UserManager<User> _userManager;
        public UserController(IUserService userService, UserManager<User> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetUser(int id, string includes = null)
        {
            try
            {
                var user = _userService.GetUsers(x => x.Id == id && x.Status == Status.Active, includes).FirstOrDefault();
                if (user != null)
                {
                    return Ok(ModelFactory.CreateModel(user));
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetUsers(string includes = null)
        {
            try
            {
                var user = _userService.GetUsers(x => x.Status == Status.Active, includes);
                IEnumerable<UserModel> results = user.Select(x => ModelFactory.CreateModel(x));
                return Ok(user);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserCreateModel userCreateModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var user = Mapper.Map<UserCreateModel, User>(userCreateModel);
                user.Status = Status.Active;
                user.CreateDate = DateTime.UtcNow;
                var result = await _userManager.CreateAsync(user, userCreateModel.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                    return Ok(ModelFactory.CreateModel(user));
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = _userService.GetUsers(x => x.Id == id && x.Status == Status.Active).FirstOrDefault();
                if (user != null)
                {
                    await _userService.DeleteUser(user);
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}