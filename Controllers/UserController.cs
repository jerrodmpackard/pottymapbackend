using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pottymapbackend.Models;
using pottymapbackend.Models.DTO;
using pottymapbackend.Services;

namespace pottymapbackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _data;

        public UserController(UserService data)
        {
            // we want to pass data into _data so we can use it outside of our constructor
            _data = data;
        }


        // Login endpoint
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginDTO User)
        {
            return _data.Login(User);
        }


        //AddUser endpoint
        //if user already exists (check this)
        //if user does not exist, create new account
        //else return false
        //we will set this up as a bool

        [HttpPost]
        [Route("AddUser")]
        // UserToAdd is a variable we created
        public bool AddUser(CreateAccountDTO UserToAdd)
        {
            return _data.AddUser(UserToAdd);
        }


        //Update User endpoint
        [HttpPut]
        [Route("UpdateUser")]
        public bool UpdateUser(UserModel userToUpdate)
        {
            return _data.UpdateUser(userToUpdate);
        }


        // Get User By Username endpoint
        [HttpGet]
        [Route("GetUserByUsername/{username}")]
        public UserIdDTO GetUserByUsername(string username)
        {
            return _data.GetUserIdDTOByUsername(username);
        }


        // Update Username endpoint
        [HttpPut]
        [Route("UpdateUsername/{id}/{username}")]
        public bool UpdateUser(int id, string username)
        {
            return _data.UpdateUsername(id, username);
        }


        // Forgot Password endpoint
        [HttpPut]
        [Route("ForgotPassword/{username}/{password}")]
        public bool ForgotPassword(string username, string password)
        {
            return _data.ForgotPassword(username, password);
        }


        //DeleteUser endpoint
        [HttpDelete]
        [Route("DeleteUser/{userToDelete}")]
        public bool DeleteUser(string userToDelete)
        {
            return _data.DeleteUser(userToDelete);
        }

    }
}