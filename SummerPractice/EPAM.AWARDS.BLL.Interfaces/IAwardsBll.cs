
using EPAM.AWARDS.Entities;
using System;
using System.Collections.Generic;

namespace EPAM.AWARDS.BLL.Interfaces
{


    public interface IAwardsBll
    {
        User AddAwardToUser(int idUser, int idAward);
        User AddAwardToUser(int idUser, string awardTitle);
     

        Award CreateAward(Award Award);
        Award CreateAward(string awardTitle);
        Award GetAward(int id);
        IEnumerable<Award> GetAwards();
        bool DeleteAward(int idAward);

        User CreateUser(string name,DateTime date,string hasPass);
        User GetUser(int id);
        User UpdateUser(User user);
        User FindUser(string name);
        IEnumerable<User> GetUsers();
        IEnumerable<Award> GetAwardsOfUser(int id);

        bool DeleteUser(int id);
        bool DeleteUser(User user);
        





    }
}
