using CommonClass.ViewModel;
using CRUDTestWithAPI.CRUDTestWithAPI.DAL;
using CRUDTestWithAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;



namespace CRUDTestWithAPI.Controllers
{
    public class APITestController : ApiController
    {
        UserManager _usermanager = new UserManager();
        

        [HttpPost]
        public Object GetToken(UserAccount userAccounts)
        {
            UserAccount userAccount = _usermanager.getUserData(userAccounts.Email);

            if (userAccount != null)
            {
                string key = "my_secret_key_12345";
                var issuer = "http://mysite.com";
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var permClaims = new List<Claim>();
                permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                permClaims.Add(new Claim("valid", "1"));
                permClaims.Add(new Claim("Email", userAccount.Email));
                permClaims.Add(new Claim("Password", userAccount.Password));

                var token = new JwtSecurityToken(issuer,
                                issuer,
                                permClaims,
                                expires: DateTime.Now.AddDays(1),
                                signingCredentials: credentials);
                var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
                return new { data = jwt_token };
            }
            else
            {
                return BadRequest();
            }
        }

        
        public IHttpActionResult Save(UserAccount userAccounts)
        {
            long uId = _usermanager.Save(userAccounts);
            if (uId != 0)
            {
                return Ok();
            }
            return BadRequest("User data not saved! This email already exist in the system");
        }

        
        [AllowAnonymous]
        [HttpPut]
        public IHttpActionResult Update(UserAccount userAccounts)
        {
            long uId = _usermanager.Update(userAccounts);
            if (uId != 0)
            {
                return Ok("User data  updated.");
            }
            return BadRequest("User data didn't update.");
        }

        
        [AllowAnonymous]
        [HttpDelete]
        public IHttpActionResult Delete(long userId)
        {
            if (_usermanager.Delete(userId))
            {
                return Ok("This account is deleted");
            }
            
            return BadRequest("User data didn't delete.");
        }

        
        public IHttpActionResult GetList()
        {
            List<UserAccount> userAccounts = _usermanager.userDataList();
            
            if (userAccounts.Count!=0)
            {
                return Ok(userAccounts);
            }

            return BadRequest("No data found");
        }
        
        
        public IHttpActionResult Login(LoginData loginUser)
        {
            int userId;
            if (new UserManager().Login(loginUser, out userId))
            {
               
                FormsAuthentication.RedirectFromLoginPage(userId.ToString(), loginUser.RememberMe);
                return Ok();

            }
            else
            {
                return BadRequest("Username or password was incorrect");
            }
        }
        
        public IHttpActionResult GetUser(long userId)
        {
            UserAccount userAccounts = _usermanager.userData(userId);

            if (userAccounts != null)
            {
                return Ok(userAccounts);
            }

            return BadRequest("No data found");
        }
    }
}