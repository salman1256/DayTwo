using Microsoft.AspNetCore.Mvc;
using WebIPLDapperServices.Dtos;
using WebIPLDapperServices.Repos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebIPLDapperServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamRepo _teamRepo;

        public TeamsController(ITeamRepo teamRepo)
        {
            _teamRepo=teamRepo;
        }
        // GET: api/<TeamsController>
        [HttpGet]
        public  async Task<IActionResult>GetTeams()
        {
            try {
                var teams = await _teamRepo.GetTeams();
                return Ok(teams);
               }
            catch (Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{id}", Name ="TeamById")]
        public async Task<IActionResult> GetTeam(int id)
        {
            try
            {
                var team = await _teamRepo.GetTeamById(id);
                if (team == null)
                { 
                    return NotFound(); 
                }
                return Ok(team);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        //POST api/<TeamsController>
        [HttpPost]
        public async Task<IActionResult> CreateTeam(TeamForCreationDto team)
        {
            try
            {
                var createdTeam = await _teamRepo.CreateTeam(team);
                return CreatedAtRoute("TeamById", new { id = createdTeam.Id }, createdTeam);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        // PUT api/<TeamsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeam(int id,TeamForUpdateDto team)
        {
            try
            {
                var teamToUpdate = await _teamRepo.GetTeamById(id);
               if(teamToUpdate == null)
                {
                    return NotFound();
                }
                await _teamRepo.UpdateTeam(id, team);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //DELETE api/<TeamsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            try
            {
                var teamToDel = await _teamRepo.GetTeamById(id);
                if (teamToDel == null)
                {
                    return NotFound();
                }
                await _teamRepo.DeleteTeam(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("ByPlayerId/{id}")]
        public async Task<IActionResult> GetTeamForPlayer(int id)
        {
            try
            {
                var team = await _teamRepo.GetTeamByPlayerId(id);
                if (team == null)
                {
                    return NotFound();
                }
                return Ok(team);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
