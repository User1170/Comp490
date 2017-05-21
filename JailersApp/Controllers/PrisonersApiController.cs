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
using JailersApp.Models;

namespace JailersApp.Controllers
{
    public class PrisonersApiController : ApiController
    {
        private JailersAppContext db = new JailersAppContext();

        // GET: api/PrisonersApi
        public IQueryable<Prisoner> GetPrisoners()
        {
            return db.Prisoners;
        }

        // GET: api/PrisonersApi/5
        [ResponseType(typeof(Prisoner))]
        public IHttpActionResult GetPrisoner(int id)
        {
            Prisoner prisoner = db.Prisoners.Find(id);
            if (prisoner == null)
            {
                return NotFound();
            }

            return Ok(prisoner);
        }

        // PUT: api/PrisonersApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPrisoner(int id, Prisoner prisoner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prisoner.Id)
            {
                return BadRequest();
            }

            db.Entry(prisoner).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrisonerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PrisonersApi
        [ResponseType(typeof(Prisoner))]
        public IHttpActionResult PostPrisoner(Prisoner prisoner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Prisoners.Add(prisoner);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = prisoner.Id }, prisoner);
        }

        // DELETE: api/PrisonersApi/5
        [ResponseType(typeof(Prisoner))]
        public IHttpActionResult DeletePrisoner(int id)
        {
            Prisoner prisoner = db.Prisoners.Find(id);
            if (prisoner == null)
            {
                return NotFound();
            }

            db.Prisoners.Remove(prisoner);
            db.SaveChanges();

            return Ok(prisoner);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PrisonerExists(int id)
        {
            return db.Prisoners.Count(e => e.Id == id) > 0;
        }
    }
}