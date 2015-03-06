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
    public class AchievementsController : ApiController
    {
        private IAchievementRepository _repository;

        public AchievementsController()
            : this(new AchievementRepository())
        {
        }

        public AchievementsController(IAchievementRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Achievements
        public IEnumerable<Achievement> GetAchievements()
        {
            return _repository.GetAchievements();
        }

        // GET: api/Achievements/5
        [ResponseType(typeof(Achievement))]
        public IHttpActionResult GetAchievement(string id)
        {
            Achievement achievement = _repository.GetAchievementByName(id);
            if (achievement == null)
            {
                return NotFound();
            }

            return Ok(achievement);
        }

        // PUT: api/Achievements/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAchievement(string id, Achievement achievement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != achievement.Name)
            {
                return BadRequest();
            }

            _repository.EditAchievement(achievement);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Achievements
        [ResponseType(typeof(Achievement))]
        public IHttpActionResult PostAchievement(Achievement achievement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.InsertAchievement(achievement);

            return CreatedAtRoute("DefaultApi", new { id = achievement.Name }, achievement);
        }

        // DELETE: api/Achievements/5
        [ResponseType(typeof(Achievement))]
        public IHttpActionResult DeleteAchievement(string id)
        {
            Achievement achievement = _repository.GetAchievementByName(id);
            if (achievement == null)
            {
                return NotFound();
            }

            _repository.DeleteAchievement(id);

            return Ok(achievement);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AchievementExists(string id)
        {
            return _repository.GetAchievements().Count(e => e.Name == id) > 0;
        }
    }
}