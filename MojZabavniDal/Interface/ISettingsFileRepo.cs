using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojZabavniDal.Interface
{
    public interface ISettingsFileRepo
    {
        void WriteSettingsFile(List<string> Content);
    }
}
