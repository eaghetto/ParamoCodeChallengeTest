using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Core.Contracts;
using Sat.Recruitment.Domain.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Errors { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("create-user")]
        public async Task<Result> CreateUser(string name, string email, string address, string phone, string userType, string money)
        {
            try
            {
                string errors = string.Empty;
                _userService.ValidateErrors(name, email, address, phone, ref errors);

                if (errors != null && errors != "")
                    return new Result()
                    {
                        IsSuccess = false,
                        Errors = errors
                    };

                if (money == null)
                    return new Result()
                    {
                        IsSuccess = false,
                        Errors = "Money can not be null."
                    };

                User newUser = new User
                {
                    Name = name,
                    Email = email,
                    Address = address,
                    Phone = phone,
                    UserType = userType,
                    Money = decimal.Parse(money)
                };

                bool isDuplicated = await _userService.AddUser(newUser);

                if (!isDuplicated)
                    return new Result()
                    {
                        IsSuccess = true,
                        Errors = "User Created"
                    };
                else
                {
                    Debug.WriteLine("The user is duplicated");

                    return new Result()
                    {
                        IsSuccess = false,
                        Errors = "The user is duplicated"
                    };
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new Result()
                {
                    IsSuccess = false,
                    Errors = ex.Message
                };
            }
        }
    }
}
