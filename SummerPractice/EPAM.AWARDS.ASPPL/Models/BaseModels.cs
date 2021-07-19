using EPAM.AWARDS.BLL.Interfaces;
using EPAM.AWARDS.Dependencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPAM.AWARDS.ASPPL.Models
{
    static public class BaseModels
    {
       static public IAwardsBll bll = DependencyResolver.Instance.Logic;

       static public string GetUsers()
        {
            return string.Join(Environment.NewLine,bll.GetUsers());
        }
    }
}