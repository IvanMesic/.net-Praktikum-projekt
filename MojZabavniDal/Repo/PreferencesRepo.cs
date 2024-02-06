using MojZabavniDal.Model;
using System;
using System.IO;

namespace MojZabavniDal.Repo
{
    public static class PreferencesRepo
    {
        public static bool IsSecondLineMen(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    using (StreamReader reader = new StreamReader(fileName))
                    {
                        string firstLine = reader.ReadLine();

                        string secondLine = reader.ReadLine();
                        if (secondLine != null)
                        {
                            return secondLine.Trim().Equals("men", StringComparison.OrdinalIgnoreCase);
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"File not found: {fileName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return false;
        }



        public static bool IsFirstLineApi(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    using (StreamReader reader = new StreamReader(fileName))
                    {
                        string firstLine = reader.ReadLine();
                        return (firstLine != null && firstLine.Trim().ToLower() == "api");
                    }
                }
                else
                {
                    Console.WriteLine($"File not found: {fileName}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
            
        public static void WriteTeamToFile(List<Player> players)
        {
            string fileName = "Team.txt";
            try
            {
                if (!File.Exists(fileName))
                {
                    File.Create(fileName).Close();
                    foreach (var player in players)
                    {
                        File.AppendAllText(fileName, player.Name + "," + player.ShirtNumber + "," + player.Position + "," + player.Captain + Environment.NewLine);
                    }
                }
                else
                {
                    foreach (var player in players)
                    {
                        File.AppendAllText(fileName, player.Name + "," + player.ShirtNumber + "," + player.Position + "," + player.Captain + Environment.NewLine);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public static List<String> ReadTeamCodeAndCountryFromFile()
        {
            string fileName = "TeamCode.txt";
            try
            {
                if (File.Exists(fileName))
                {
                    using (StreamReader reader = new StreamReader(fileName))
                    {
                        string firstLine = reader.ReadLine();
                        string secondLine = reader.ReadLine();

                        return new List<string>() { firstLine, secondLine }; 
                    }
                }
                else
                {
                    Console.WriteLine($"File not found: {fileName}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public static async void WriteTeamCodeToFile(List<string> text)
        {
            string fileName = "TeamCode.txt";
            try
            {
                if (!File.Exists(fileName))
                {
                    File.Create(fileName).Close();
                }

                await File.WriteAllLinesAsync(fileName, text);
                

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public static void WriteImageAssetNameToFile(string imageName)
        {
            string fileName = "images.txt"; 
            if (!File.Exists("fileName"))
            {
                File.Create(fileName).Close();
            }

            File.AppendAllText(fileName, imageName.ToLower() + Environment.NewLine); ;

        }


        public static IEnumerable<string> ReadAllImagesNamesForPlayers()
        {
            string filename = "images.txt";
            

            try
            {
               var names = File.ReadAllLines(filename);
               return names;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public static void WriteResolutionToFile(string line)
        {
            string file = "resolution.txt";
            if (!File.Exists(file))
            {
                File.Create(file).Close();
            }

            File.WriteAllText(file, line);
        }


        public static string ReadResolutionFromFile()
        {
            string file = "resolution.txt";

            try
            {
                var line = File.ReadLines(file).First();
                return line;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }

}
