using Dapper;
using System.Data;
using WebIPLDapperServices.Context;
using WebIPLDapperServices.Dtos;
using WebIPLDapperServices.Entities;

namespace WebIPLDapperServices.Repos
{
    public class TeamRepo : ITeamRepo
    {
        private readonly DapperContext _context;

        public TeamRepo(DapperContext context)
        {
            _context = context;
        }

        public async Task<Team> CreateTeam(TeamForCreationDto team)
        {
            var query = "insert into Teams (Name,Slogan,City) values (@name,@slogan,@city)"
                + "select cast(SCOPE_IDENTITY() as int)";
            var parameters = new DynamicParameters();
            parameters.Add("@name", team.Name, DbType.String);
            parameters.Add("@slogan", team.Slogan, DbType.String);
            parameters.Add("@city", team.City, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);
                var createdTeam = new Team
                {
                    Id = id,
                    Name = team.Name,
                    City = team.City,
                    Slogan = team.Slogan,

                };
                return createdTeam;
            }
        }

       

        public async Task<Team> GetTeamById(int id)
        {
            var query = "select * from Teams where Id=@id";
            using (var connection = _context.CreateConnection())
            {
                var team = await connection.QuerySingleOrDefaultAsync<Team>(query, new { id });
                return team;
            }
        }

        public async Task<IEnumerable<Team>> GetTeams()
        {
            var query = "select * from Teams";
            using (var connection = _context.CreateConnection())
            {
                var teams = await connection.QueryAsync<Team>(query);
                return teams.ToList();
            }
        }

        public async Task UpdateTeam(int id, TeamForUpdateDto team)
        {
            var query = "update Teams set Name=@name,Slogan=@slogan,City=@city where Id=@id";

            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int32);
            parameters.Add("@name", team.Name, DbType.String);
            parameters.Add("@slogan", team.Slogan, DbType.String);
            parameters.Add("@city", team.City, DbType.String);

            using (var connection = _context.CreateConnection())
            {
              await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task DeleteTeam(int id)
        {
            var query = "delete from teams where Id=@id";

          

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id});
            }

        }

        public async Task<Team> GetTeamByPlayerId(int id)
        {
            var procName = "usp_ShowTeamsofPlayer";
            var parameters=new DynamicParameters();
            parameters.Add("@id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var team=await connection.QueryFirstOrDefaultAsync<Team>(procName,parameters,commandType:CommandType.StoredProcedure);
                return team;
            }
        }
    }
}
