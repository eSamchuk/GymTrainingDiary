using GymTrainingDiary.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymTrainingDiary.Web.Models
{
    public class CreateUserModel
    {
        public CreateUserModel()
        {
            this.NewUser = new User();
        }

        public User NewUser { get; set; }

        public SelectList UserAccountTypes { get; set; }


    }
}