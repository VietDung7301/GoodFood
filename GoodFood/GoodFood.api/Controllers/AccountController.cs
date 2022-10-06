using Dapper;
using GoodFood.api.Controllers;
using GoodFood.api.Helpers;
using GoodFood.api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System;

namespace WebApplication.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtSettings jwtSettings;
        public AccountController(JwtSettings jwtSettings)
        {
            this.jwtSettings = jwtSettings;
        }



        [HttpPost]
        public IActionResult Login(UserLogins userLogins)
        {
            try
            {
                string connectionString = "Server=localhost;Port=3306;Database=goodfood;Uid=root;Pwd=yasuo1234gg";
                var mySQLconnection = new MySqlConnection(connectionString);
                string getAllUser = "Select * from salers";
                IEnumerable<Users> logins = mySQLconnection.Query<Users>(getAllUser);

                var Token = new UserTokens();
                var Valid = logins.Any(x =>
                    x.user_name.Equals(userLogins.UserName, StringComparison.Ordinal)
                   && x.password.Equals(userLogins.Password, StringComparison.Ordinal)
                );
                if (Valid)
                {
                    var user = logins.FirstOrDefault(x => x.user_name.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));
                    Token = JwtHelpers.GenTokenkey(new UserTokens()
                    {
                        Id = user.saler_Id,
                        UserName = user.user_name,
                        SalerName = user.Name,

                    }, jwtSettings);
                }
                else
                {
                    return BadRequest("wrong password");
                }
                return Ok(Token);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Get List of UserAccounts
        /// </summary>
        /// <returns>List Of UserAccounts</returns>
      
    }
    
}