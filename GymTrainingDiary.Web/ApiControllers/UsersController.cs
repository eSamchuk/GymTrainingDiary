using GymTrainingDiary.Data;
using GymTrainingDiary.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GymTrainingDiary.Web.ApiControllers
{
    public class UsersController : ApiController
    {


        public IEnumerable<User> Get()
        {
            var list = DataAccessMethods.GetUsers();
            return list;
        }
    }
}
