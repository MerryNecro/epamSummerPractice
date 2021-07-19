using EPAM.AWARDS.BLL;
using EPAM.AWARDS.BLL.Interfaces;
using EPAM.AWARDS.DAL;
using EPAM.AWARDS.DAL.Interfaces;
using System;

namespace EPAM.AWARDS.Dependencies
{
    public class DependencyResolver
    {
        #region SINGLETONE

        private static DependencyResolver _instance;
        public static DependencyResolver Instance
        {
            get
            {
                if (_instance == null) 
                    return _instance = new DependencyResolver();
                else
                return _instance;
            }
        }

        #endregion

        public IAwardsDAO DAO => new SQLDAO();

        public IAwardsBll Logic => new AwardsLogic(DAO);
        public IAutchModel AutchModel => new BLL.AutchModel(Logic, DAO);

    }
}
