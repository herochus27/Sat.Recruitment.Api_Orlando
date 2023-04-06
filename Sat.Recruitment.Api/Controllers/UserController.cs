using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sat.Recruitment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private List<User> _users;

        public UserController()
        {
            _userService = new UserService();
            _users = _userService.GetList().ToList();
        }


        // GET: api/user
        [HttpGet]
        public Result<User> Get()
        {
            try
            {
                var users = _userService.GetList();
                return new Result<User>
                {
                    Data = users,
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                var response = new Result<User>
                {
                    Data = new List<User>(),
                    IsSuccess = false,
                    Errors = ex.Message
                };
                return response;
            }
        }

        [HttpPost]
        public Result<User> Create([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (_users.Any(e => (e.Email.Trim().ToLower() == user.Email.Trim().ToLower()
                    || e.Phone == user.Phone) || e.Name.Trim().ToLower() == user.Name.Trim().ToLower()
                    && e.Address.Trim().ToLower() == user.Address.Trim().ToLower()))
                    {
                        return new Result<User>()
                        {
                            IsSuccess = false,
                            Errors = "The user is duplicated"
                        };
                    }
                    else
                    {
                        _userService.Create(user);
                        return new Result<User>()
                        {
                            IsSuccess = true,
                            Errors = "User Created"
                        };
                    }
                }
                catch (Exception ex)
                {

                    var response = new Result<User>
                    {
                        Data = new List<User>(),
                        IsSuccess = false,
                        Errors = ex.Message
                    };
                    return response;
                }

            }
            else
            {
                return new Result<User>()
                {
                    IsSuccess = false,
                    Errors = "Please check the user's fields"
                };
            }
        }
    }

}
