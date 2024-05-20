using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using pottymapbackend.Models;
using pottymapbackend.Models.DTO;
using pottymapbackend.Services.Context;

namespace pottymapbackend.Services
{
    public class UserService : ControllerBase
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public bool DoesUserExist(string Username)
        {
            // check if username exists
            // if 1 item matches, then return the item
            // if no item matches, then return null

            return _context.UserInfo.SingleOrDefault(user => user.Username == Username) != null;
        }

        public bool AddUser(CreateAccountDTO UserToAdd)
        {
            // if user already exists
            bool result = false;

            // if user does not exist, add user based on model
            if (!DoesUserExist(UserToAdd.Username))
            {

                // creating a blank template
                UserModel newUser = new UserModel();

                var hashPassword = HashPassword(UserToAdd.Password);

                newUser.ID = UserToAdd.ID;
                newUser.Username = UserToAdd.Username;
                newUser.Salt = hashPassword.Salt;
                newUser.Hash = hashPassword.Hash;

                // Adds newUser to the database
                _context.Add(newUser);

                // We need to save into database, return of number of entries written into database. Where all of this would equal to one entry

                result = _context.SaveChanges() != 0;

            }

            return result;
        }

        public PasswordDTO HashPassword(string password)
        {

            // We are creating a new instance of our PasswordDTO
            PasswordDTO newHashPassword = new PasswordDTO();

            // Setting up a byte array using 64 bytes of salt. we chose 64 bit because it's a common choice for salt
            byte[] SaltByte = new byte[64];

            // We need to randomize our hashed password so we're gonna create a randomizer
            // This is our randomizer
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();

            // We now need to take our salt byte and make sure there are no zeroes in it. That makes it even more secure
            // Take our SaltByte and make sure it contains no zeroes. Making it more secure
            // There is a method we can apply to it to do this
            provider.GetNonZeroBytes(SaltByte);

            // Converting our salt into a string
            string salt = Convert.ToBase64String(SaltByte);

            // Encode our password with salt into our hash. 
            // We pass in password, our SaltByte array, and we will iterate through 10,000 times until we get our hash, where 10,000 is a common starting point for hashing
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, SaltByte, 10000);

            // We will take this resulting byte array and convert it into a string of 256 bytes, and then save it into a hash. Remember that 1-2 characters equals 1 byte. This will get saved into our database
            string hash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            // We now have our salt and our hash
            // We can now save our hash and salt into our PasswordDTO
            // We do .Salt and .Hash because that is what we defined in our PasswordDTO
            // We save these values into newHashPassword, our new instance of our PasswordDTO
            newHashPassword.Salt = salt;
            newHashPassword.Hash = hash;

            return newHashPassword;

        }

        // We will now create a new helper function to verify our password. It will verify through our salt
        // Verify users password
        public bool VerifyUsersPassword(string? password, string? storedHash, string? storedSalt)
        {

            // We want to encode our salt back into the original byte array
            byte[] SaltBytes = Convert.FromBase64String(storedSalt);

            // Repeat process of taking password entered and hashing it again
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, SaltBytes, 10000);

            // We will take our Rfc2898 object, retrieve the 256 bytes of hash, convert those into a string, and then assign the result to the newHash
            string newHash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            return newHash == storedHash;

        }

        public IActionResult Login(LoginDTO User)
        {
            IActionResult Result = Unauthorized();

            // Check if user exists
            if (DoesUserExist(User.Username))
            {
                // If user exists, this will evaluate to true. If true, continue with authentication
                // If true, we need to store our user object
                // To do this, we need to create another helper function

                UserModel foundUser = GetUserByUsername(User.Username);

                // This will check if our password is correct
                if (VerifyUsersPassword(User.Password, foundUser.Hash, foundUser.Salt))
                {

                    // anyone with this secretKey can access the login
                    // Think of this as a Costco membership. You can only get into Costco if you have a membership. This is the same thing
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));

                    // Sign in credentials
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    // Generates a new token and signs a user out after 30 minutes
                    // issuer and audience is a local port for our jwt token
                    // Once you deploy, you will remove that port and add in your Azure front end URL!
                    var tokenOptions = new JwtSecurityToken(
                        issuer: "http://localhost:5000",
                        audience: "http://localhost:5000",
                        claims: new List<Claim>(), // Claims can be added here if needed
                        expires: DateTime.Now.AddMinutes(30), // Set token expiration time (e.g., 30 minutes)
                        signingCredentials: signinCredentials // Set signing credentials
                    );

                    // Generate JWT token as a string
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                    // return JWT token through http response with status code 200
                    Result = Ok(new { Token = tokenString });
                }

            }

            return Result;
        }


        public UserModel GetUserByUsername(string username)
        {
            return _context.UserInfo.SingleOrDefault(user => user.Username == username);
        }


        public bool UpdateUser(UserModel userToUpdate)
        {
            // This updates the UserModel for the user that we want to update
            _context.Update<UserModel>(userToUpdate);

            // Whenever we make a change, we return a number (probably 1). Otherwise, we return a 0
            // 
            return _context.SaveChanges() != 0;
        }


        public bool UpdateUsername(int id, string username)
        {
            // Sending over just the id and username
            // We have to get the object to be updated
            // To do this, we need yet another helper function

            UserModel foundUser = GetUserById(id);

            bool result = false;

            if (foundUser != null)
            {
                // If we found a user, execute this code
                // Update founderUser object
                foundUser.Username = username;
                _context.Update<UserModel>(foundUser);
                result = _context.SaveChanges() != 0;
            }

            return result;
        }

        public bool ForgotPassword(string username, string password)
        {
            UserModel foundUser = GetUserByUsername(username);

            var newPassword = HashPassword(password);

            bool result = false;

            if (foundUser != null)
            {
                foundUser.Salt = newPassword.Salt;
                foundUser.Hash = newPassword.Hash;
                _context.Update<UserModel>(foundUser);
                result = _context.SaveChanges() != 0;
            }

            return result;
        }

        public UserModel GetUserById(int id)
        {
            return _context.UserInfo.SingleOrDefault(user => user.ID == id);
        }


        public bool DeleteUser(string userToDelete)
        {
            // We are only sending over the username
            // If username found, delete user

            UserModel foundUser = GetUserByUsername(userToDelete);

            bool result = false;

            if (foundUser != null)
            {
                // If we have found a user

                _context.Remove<UserModel>(foundUser);
                result = _context.SaveChanges() != 0;
            }

            return result;
        }

        public UserIdDTO GetUserIdDTOByUsername(string username)
        {
            UserIdDTO UserInfo = new UserIdDTO();

            // Now we need to query through our database to find the user based on the name inside the database
            UserModel foundUser = _context.UserInfo.SingleOrDefault(user => user.Username == username);

            UserInfo.UserId = foundUser.ID;

            // Assign the 
            UserInfo.PublisherName = foundUser.Username;

            return UserInfo;
        }

    }
}