using MojZabavniDal.Interface;
using MojZabavniDal.Repo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojZabavniDal.Factory
{
    public static class RepoFactory
    {
        public static IDataRepo GetDataRepo()
        {

            if (PreferencesRepo.IsFirstLineApi("Settings.csv"))
            {
                return new DataFileRepo();
            }
            else
            {
                return new DataRepo();
            }
        }

       
    }
}
