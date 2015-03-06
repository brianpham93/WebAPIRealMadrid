using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPIRealMadrid.Models;

namespace WebAPIRealMadrid.Controllers
{
    public class PlayerAchievementsController : ApiController
    {

        protected IPlayerAchievementRepository _repository;

        public PlayerAchievementsController()
            : this(new PlayerAchievementRepository())
        {
        }

        public PlayerAchievementsController(IPlayerAchievementRepository repository)
        {
            _repository = repository;
        }

        // GET: api/PlayerAchievements/01
        [HttpGet]
        [Route("api/playerachievements/{playerID}")]
        public IEnumerable<PlayerAchievement> GetPlayerAchievements(String playerID)
        {
            return _repository.GetPlayerAchievementsByPlayerID(playerID);
        }
        
        // GET: api/PlayerAchievements/01/European Cup
        [HttpGet]
        [ResponseType(typeof(PlayerAchievement))]        
        [Route("api/playerachievements/{playerID}/{achievementName}")]
        public IHttpActionResult GetPlayerAchievement(string playerID, string achievementName)
        {
            PlayerAchievement playerAchievement = _repository.GetPlayerAchievement(playerID, achievementName);
            if (playerAchievement == null)
            {
                return NotFound();
            }

            return Ok(playerAchievement);
        }

        // PUT: api/PlayerAchievements/5/foot
        [ResponseType(typeof(void))]
        [Route("api/playerachievements/{playerID}/{achievementName}")]
        public IHttpActionResult PutPlayerAchievement(string playerID, string achievementName, PlayerAchievement playerAchievement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (playerID != playerAchievement.PlayerID)
            {
                return BadRequest();
            }

            _repository.InsertPlayerAchievement(playerAchievement);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PlayerAchievements/01
        [ResponseType(typeof(PlayerAchievement))]
        [Route("api/playerachievements/{playerID}")]
        public IHttpActionResult PostPlayerAchievement(string playerID, PlayerAchievement playerAchievement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.InsertPlayerAchievement(playerAchievement);

            
            return CreatedAtRoute("DefaultApi", new { id = playerAchievement.PlayerID }, playerAchievement);
        }

        // DELETE: api/PlayerAchievements/5/European Cup
        [ResponseType(typeof(PlayerAchievement))]
        [Route("api/playerachievements/{playerID}/{achievementName}")]

        public IHttpActionResult DeletePlayerAchievement(string playerID, string achievementName)
        {
            PlayerAchievement playerAchievement = _repository.GetPlayerAchievement(playerID, achievementName);
            if (playerAchievement == null)
            {
                return NotFound();
            }


            _repository.DeletePlayerAchievement(playerID, achievementName);

            return Ok(playerAchievement);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
               // db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlayerAchievementExists(string playerID, string achievementName)
        {
            return _repository.GetPlayerAchievementsByPlayerID(playerID).Count(e => e.PlayerID == playerID) > 0;
        }
    }
}