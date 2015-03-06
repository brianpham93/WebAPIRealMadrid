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
    public class PlayersController : ApiController
    {
        private IPlayerRepository _repository;

        public PlayersController()
            : this(new PlayerRepository())
        {
        }

        public PlayersController(IPlayerRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Players
        public IEnumerable<Player> GetPlayers()
        {
            return _repository.GetPlayers();
        }

        // GET: api/Players/5
        [ResponseType(typeof(Player))]
        public IHttpActionResult GetPlayer(string id)
        {
            Player player = _repository.GetPlayerByID(id);
            if (player == null)
            {
                return NotFound();
            }

            return Ok(player);
        }

        // PUT: api/Players/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPlayer(string id, Player player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != player.ID)
            {
                return BadRequest();
            }

           // _repository.Entry(player).State = EntityState.Modified;
            _repository.EditPlayer(player);
           
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Players
        [ResponseType(typeof(Player))]
        public IHttpActionResult PostPlayer(Player player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.InsertPlayer(player);
           
            return CreatedAtRoute("DefaultApi", new { id = player.ID }, player);
        }

        // DELETE: api/Players/5
        [ResponseType(typeof(Player))]
        public IHttpActionResult DeletePlayer(string id)
        {
            Player player = _repository.GetPlayerByID(id);
            if (player == null)
            {
                return NotFound();
            }

            _repository.DeletePlayer(id);            

            return Ok(player);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlayerExists(string id)
        {
            return _repository.GetPlayers().Count(e => e.ID == id) > 0;
        }
    }
}