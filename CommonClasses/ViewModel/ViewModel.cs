//using CRUDTestWithAPI.Models;
using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
//using System.Web.Mvc;

namespace CommonClass.ViewModel
{
    public class ViewModel
    {
        public List<RegistrationClass> UserAccountList { get; set; }

        public ViewModel()
        {
            UserAccountList = new List<RegistrationClass>();
            
        }

    }
}