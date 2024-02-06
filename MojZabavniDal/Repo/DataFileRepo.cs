using MojZabavniDal.Interface;
using MojZabavniDal.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojZabavniDal.Repo
{
    internal class DataFileRepo : IDataRepo
    {
        string path = "";

        public DataFileRepo()
        {
            if(PreferencesRepo.IsSecondLineMen("Settings.csv"))
            {
                path = "men";
            }
            else
            {
                path = "women";
            }
        }

        public async Task<IEnumerable<Match>> GetMatchesAsync()
        {
            string filePath = path + "/matches.json";
            string json = File.ReadAllText(filePath);
            IEnumerable<Match> matches = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Match>>(json);

            return matches;
        }

        public async Task<IEnumerable<Match>> GetMatchesForStatistics()
        {
            string country = PreferencesRepo.ReadTeamCodeAndCountryFromFile()[1];
            List<Match>? matches = await GetMatchesAsync() as List<Match>;
            matches = matches.FindAll(match => match.HomeTeamStatistics.Country == country|| match.AwayTeamStatistics.Country == country);
            return matches;
        }

        public Task<IEnumerable<Match>> GetMatchesForStatistics(string fifaCode)
        {
            throw new NotImplementedException();
        }

        public async Task<Match> GetMatchForTeams(string fifaCode1, string fifaCode2)
        {
            Match match = new Match();
            List<Match>? matches = await GetMatchesAsync() as List<Match>;
            match = matches.FirstOrDefault(x => (x.HomeTeam.FifaCode == fifaCode1 && x.AwayTeam.FifaCode == fifaCode2)
            || (x.HomeTeam.FifaCode == fifaCode2 && x.AwayTeam.FifaCode == fifaCode1));
            return match;
        }

        public async Task<IEnumerable<Team>> GetOpponentTeams(string fifaCode)
        {
            List<Team> teams = new List<Team>();
            List<Match> matches = GetMatchesAsync().Result.ToList();
            List<Match> otherMatches = matches.Where(x => x.HomeTeam.FifaCode == fifaCode || x.AwayTeam.FifaCode == fifaCode).ToList();
            
            foreach (var match in otherMatches)
            {
                if (match.HomeTeam.FifaCode == fifaCode)
                {
                    teams.Add(match.AwayTeam);
                }
                else
                {
                    teams.Add(match.HomeTeam);
                }
            }

            return teams;

        }

        public async Task<IEnumerable<Player>> GetPlayersAsync(string fifaCode)
        {
            List<Player> players = new List<Player>();
            string filePath = path + "/matches.json";
            string json = File.ReadAllText(filePath);
            IEnumerable<Match> matches = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Match>>(json);
          

            var match = matches.FirstOrDefault(x => x.HomeTeam.FifaCode == fifaCode || x.AwayTeam.FifaCode == fifaCode);

            if (match.HomeTeam.FifaCode == fifaCode)
            {
                players.AddRange(match.HomeTeamStatistics.StartingEleven.ToList());
            }
            else
            {
                players.AddRange(match.AwayTeamStatistics.StartingEleven.ToList());
            }
            if (match.HomeTeam.FifaCode == fifaCode)
            {
                players.AddRange(match.HomeTeamStatistics.Substitutes.ToList());
            }
            else
            {
                players.AddRange(match.AwayTeamStatistics.Substitutes.ToList());
            }

            return players;

        }

     
        public async Task<IEnumerable<Match>> GetSpecificMatchesAsync(string country)
        {
            string filePath = path + "/matches.json";
            string json = File.ReadAllText(filePath);
            IEnumerable<Match> matches = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Match>>(json);

            matches = matches.ToList().FindAll(match => match.HomeTeamStatistics.Country == country || match.AwayTeamStatistics.Country == country);

            return matches;
        }

        public async Task<IEnumerable<Player>> GetStartingElevenAsync(string fifaCode)
        {
            List<Player> players = new List<Player>();
            string filePath = path + "/matches.json";
            string json = File.ReadAllText(filePath);
            IEnumerable<Match> matches = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Match>>(json);


            var match = matches.FirstOrDefault(x => x.HomeTeam.FifaCode == fifaCode || x.AwayTeam.FifaCode == fifaCode);

            if (match.HomeTeam.FifaCode == fifaCode)
            {
                players.AddRange(match.HomeTeamStatistics.StartingEleven.ToList());
            }
            else
            {
                players.AddRange(match.AwayTeamStatistics.StartingEleven.ToList());
            }

            return players;
        }

        public async Task<Team> GetTeamResults(string fifaCode)
        {
            string filePath = path + "/results.json";
            string json = File.ReadAllText(filePath);
            List<Team> teams = JsonConvert.DeserializeObject<List<Team>>(json);

            Team team = teams.ToList().FirstOrDefault(x => x.FifaCode == fifaCode);

            return team;

        }

        public Task<IEnumerable<Team>> GetTeamsAsync()
        {
            string filePath = path + "/teams.json";
            string json = File.ReadAllText(filePath);
            IEnumerable<Team> teams = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Team>>(json);
            return Task.FromResult(teams);
        }
    }
}
