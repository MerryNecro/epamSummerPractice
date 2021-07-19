using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.AWARDS.BLL.Interfaces
{
    public interface IAutchModel
    {
        bool CanLogin(string login, string pass);
        string HashPassword(string pass);

    }
}
