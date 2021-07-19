using EPAM.AWARDS.Entities;
using System;
using System.Collections.Generic;

namespace EPAM.AWARDS.DAL.Interfaces
{
    public interface IAwardsDAO
    {
        Award CreateAward(string title);
        bool DeleteAward(int id);
        bool DeleteAward(Award Award);
        Award GetAward(int id);

        IEnumerable<Award> GetAwards(bool orderedById);

        

        User CreateUser(string name, DateTime dateOfBirth,string passHash);
        User UpdateUser(User user);
        bool DeleteUser(int id);
        bool DeleteUser(User user);
        User GetUser(int id);

        IEnumerable<User> GetUsers(bool orderedById);


    }
}
