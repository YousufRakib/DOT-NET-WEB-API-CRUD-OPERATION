using CommonClass.ViewModel;
using CRUDTestWithAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUDTestWithAPI.CRUDTestWithAPI.DAL
{
    public class UserManager
    {
        UserRepository userRepository = new UserRepository();
        public long Save(UserAccount userAccounts)
        {
            return userRepository.Save(userAccounts);
        }
        public long Update(UserAccount userAccounts)
        {
            return userRepository.Update(userAccounts);
        }
        public bool Delete(long userId)
        {
            return userRepository.Delete(userId);
        }
        public List<UserAccount> userDataList()
        {
            return userRepository.userDataList();
        }

        public UserAccount userData(long userId)
        {
            return userRepository.userData(userId);
        }

        public UserAccount getUserData(string Email)
        {
            return userRepository.getUserData(Email);
        }

        public bool Login(LoginData loginUser, out int userId)
        {
            return userRepository.Login(loginUser, out userId);
        }
        
    }
}