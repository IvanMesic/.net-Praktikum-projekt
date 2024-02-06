using MojZabavniDal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojZabavniDal.Interface
{
    public interface IDataRepo
    {
        Task<IEnumerable<Team>> GetTeamsAsync();
        Task<IEnumerable<Player>> GetPlayersAsync(string fifaCode);
        Task<IEnumerable<Match>> GetMatchesAsync();
        Task<IEnumerable<Match>> GetSpecificMatchesAsync(string fifaCode);
        Task<IEnumerable<Team>> GetOpponentTeams(string fifaCode);
        Task<IEnumerable<Player>> GetStartingElevenAsync(string fifaCode);
        Task<Match> GetMatchForTeams(string fifaCode1, string fifaCode2);
        Task<IEnumerable<Match>> GetMatchesForStatistics(string fifaCode);
        Task<Team> GetTeamResults(string fifaCode);

    }
}
