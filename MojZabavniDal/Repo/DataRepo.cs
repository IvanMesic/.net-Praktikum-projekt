using MojZabavniDal.Factory;
using MojZabavniDal.Interface;
using MojZabavniDal.Model;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojZabavniDal.Repo
{
    public class DataRepo : IDataRepo
    {
        private string APIUrl;
        ISettingsFileRepo settingsFileRepo = new SettingRepo();

        public DataRepo()
        {
            string fileName = "Settings.csv";
            if (PreferencesRepo.IsSecondLineMen(fileName))
            {
                APIUrl = "http://worldcup-vua.nullbit.hr/MEN";
            }
            else
            {
                APIUrl = "http://worldcup-vua.nullbit.hr/WOMEN";
            }
        }

        public async Task<IEnumerable<Match>> GetMatchesAsync()
        {

            string matchesAPIUrl = $"{APIUrl}/matches";

            var restClient = new RestClient(matchesAPIUrl);

            RestResponse<Match> response = restClient.Execute<Match>(new RestRequest());
            IEnumerable<Match> matches = JsonConvert.DeserializeObject<List<Match>>(response.Content);


            return matches;
        }

        public async Task<IEnumerable<Match>> GetMatchesForStatistics(string fifaCode)
        {
            string country = PreferencesRepo.ReadTeamCodeAndCountryFromFile()[1];
            List<Match>? matches = await GetMatchesAsync() as List<Match>;
            matches = matches.FindAll(match => match.HomeTeamStatistics.Country == country || match.AwayTeamStatistics.Country == country);
            return matches;
        }

        public async Task<Match> GetMatchForTeams(string fifaCode1, string fifaCode2)
        {
            Match   match = new Match();
            List<Match> matches = GetMatchesAsync().Result.ToList();
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

            string matchesAPIUrl = $"{APIUrl}/matches";

            var restClient = new RestClient(matchesAPIUrl);

            RestResponse<Match> response = restClient.Execute<Match>(new RestRequest());
            IEnumerable<Match> matches = JsonConvert.DeserializeObject<List<Match>>(response.Content);

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
            List<Match> matches = new List<Match>();
            matches = GetMatchesAsync().Result.ToList();
            matches = matches.ToList().FindAll(match => match.HomeTeamStatistics.Country == country || match.AwayTeamStatistics.Country == country);

            return matches;

        }

        public async Task<IEnumerable<Player>> GetStartingElevenAsync(string fifaCode)
        {
            List<Player> players = new List<Player>();

            string matchesAPIUrl = $"{APIUrl}/matches";

            var restClient = new RestClient(matchesAPIUrl);

            RestResponse<Match> response = restClient.Execute<Match>(new RestRequest());
            IEnumerable<Match> matches = JsonConvert.DeserializeObject<List<Match>>(response.Content);

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
            try
            {
                string matchesAPIUrl = $"{APIUrl}/teams/results";
                var restClient = new RestClient(matchesAPIUrl);

                var request = new RestRequest(); 

                var response = await restClient.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    List<Team> teams = JsonConvert.DeserializeObject<List<Team>>(response.Content);

                    Team team = teams.ToList().FirstOrDefault(x => x.FifaCode == fifaCode);

                    return team;
                }
                else
                {
                    throw new Exception($"Error getting team results. Status Code: {response.StatusCode}, Content: {response.Content}");
                }
            }
            catch (JsonSerializationException ex)
            {
                throw new Exception($"Error deserializing the response: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}");
            }
        }


        public async Task<IEnumerable<Team>> GetTeamsAsync()
        {
            string teamsAPIUrl = $"{APIUrl}/teams";

            var restClient = new RestClient(teamsAPIUrl);

            RestResponse<Team> response = await restClient.ExecuteAsync<Team>(new RestRequest());

            IEnumerable<Team> teams = JsonConvert.DeserializeObject<List<Team>>(response.Content);

            return teams;
        }
    }
}
