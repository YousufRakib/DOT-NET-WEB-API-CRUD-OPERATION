using CommonClass.ViewModel;
using CRUDTestWithAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace CRUDTestWithAPI.CRUDTestWithAPI.DAL
{
    public class UserRepository:Controller
    {
        APITestDatabaseEntities _apiTestDatabaseEntities = new APITestDatabaseEntities();
        internal long Save(UserAccount userAccounts)
        {
           
            UserAccount user = new UserAccount();

            if (userAccounts.UserId == 0)
            {
                var existUser = _apiTestDatabaseEntities.UserAccounts.Where(x => x.Email == userAccounts.Email).FirstOrDefault();

                if (existUser == null)
                {
                  
                    user.Name = userAccounts.Name;
                    user.Email = userAccounts.Email;
                    user.Password = userAccounts.Password;


                    _apiTestDatabaseEntities.UserAccounts.Add(user);
                    _apiTestDatabaseEntities.SaveChanges();

                    
                    //BuildEmailTemplate(user.UserId);

                    return user.UserId;
                }
               return  0;
            }
            else
            {
                var existUser = _apiTestDatabaseEntities.UserAccounts.Where(x => x.UserId == userAccounts.UserId).FirstOrDefault();

                existUser.Name = userAccounts.Name;
                existUser.Email = userAccounts.Email;
                existUser.Password = userAccounts.Password;

                _apiTestDatabaseEntities.SaveChanges();

                return existUser.UserId;
            }
        }

        internal long Update(UserAccount userAccounts)
        {
            
            if (userAccounts.UserId != 0)
            {
                var existUser = _apiTestDatabaseEntities.UserAccounts.Where(x => x.UserId == userAccounts.UserId).FirstOrDefault();

                existUser.Name = userAccounts.Name;
                existUser.Email = userAccounts.Email;
                existUser.Password = userAccounts.Password;

                _apiTestDatabaseEntities.SaveChanges();

                return existUser.UserId;
            }
            return 0;
        }

        internal bool Delete(long userId)
        {

            try
            {
                var userData = _apiTestDatabaseEntities.UserAccounts.Where(x => x.UserId == userId).FirstOrDefault();

                _apiTestDatabaseEntities.UserAccounts.Remove(userData);
                _apiTestDatabaseEntities.SaveChanges();
                return true;
            }
            catch { return false; }
        }
        internal List<UserAccount> userDataList()
        {
            List<UserAccount> userData = _apiTestDatabaseEntities.UserAccounts.ToList();
            return userData;
        }

        internal UserAccount userData(long userId)
        {
            UserAccount userData = _apiTestDatabaseEntities.UserAccounts.Where(x=>x.UserId==userId).FirstOrDefault();
            return userData;
        }

        internal UserAccount getUserData(string Email)
        {
            UserAccount userData = _apiTestDatabaseEntities.UserAccounts.Where(x => x.Email == Email).FirstOrDefault();
            return userData;
        }

        internal bool Login(LoginData loginData, out int userId)
        {
            userId = 0;
            var currentUser = _apiTestDatabaseEntities.UserAccounts.Where(x => x.Email == loginData.Email && x.Password == loginData.Password).FirstOrDefault();
            if (currentUser != null)
            {
                return true;
            }
            return false;
        }
    }
}