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


        //UpdateUser endpoint
        [HttpPut]
        [Route("UpdateUser")]

        public bool UpdateUser(UserModel userToUpdate)
        {
            return _data.UpdateUser(userToUpdate);
        }

        [HttpPut]
        [Route("UpdateUser/{id}/{username}")]

        public bool UpdateUser(int id, string username)
        {
            return _data.UpdateUsername(id, username);
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