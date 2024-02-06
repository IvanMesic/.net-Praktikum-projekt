using MojZabavniDal.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MojZabavniDal.Repo
{
    public class SettingRepo : ISettingsFileRepo
    {
        public string FileName = "Settings.csv";

        public void WriteSettingsFile(List<string> content)
        {
            try
            {

                if (!File.Exists(FileName))
                {
                    File.Create(FileName).Close();
                }
                    File.WriteAllLines(FileName , content);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
