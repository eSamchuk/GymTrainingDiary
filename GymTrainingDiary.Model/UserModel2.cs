﻿namespace GymTrainingDiary.Model
{
    public class UserModel2
    {
        public UserModel2(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
