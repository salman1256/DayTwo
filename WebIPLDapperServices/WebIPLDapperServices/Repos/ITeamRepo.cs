using WebIPLDapperServices.Dtos;
using WebIPLDapperServices.Entities;

namespace WebIPLDapperServices.Repos
{
    public interface ITeamRepo
    {
        public Task<IEnumerable<Team>> GetTeams();
        public Task<Team> GetTeamById(int id);
        public Task<Team> CreateTeam(TeamForCreationDto team);
        public Task UpdateTeam(int id,TeamForUpdateDto team);
        public Task DeleteTeam(int id);
        // For Stored Procedure
        public Task<Team> GetTeamByPlayerId(int id);
    }
}
