using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BMGroupAPI.Models;

namespace BMGroupAPI.Controllers
{
    public class EllectivesController : ApiController
    {
        private BMGroupAPIContext db = new BMGroupAPIContext();

        // GET: api/Ellectives
        public IQueryable<Ellective> GetEllectives()
        {
            return db.Ellectives;
        }

        // GET: api/Ellectives/5
        [ResponseType(typeof(Ellective))]
        public async Task<IHttpActionResult> GetEllective(int id)
        {
            Ellective ellective = await db.Ellectives.FindAsync(id);
            if (ellective == null)
            {
                return NotFound();
            }

            return Ok(ellective);
        }

        // PUT: api/Ellectives/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEllective(int id, Ellective ellective)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ellective.EllectiveId)
            {
                return BadRequest();
            }

            db.Entry(ellective).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EllectiveExists(id))
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

        // POST: api/Ellectives
        [ResponseType(typeof(Ellective))]
        public async Task<IHttpActionResult> PostEllective(Ellective ellective)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ellectives.Add(ellective);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = ellective.EllectiveId }, ellective);
        }

        // DELETE: api/Ellectives/5
        [ResponseType(typeof(Ellective))]
        public async Task<IHttpActionResult> DeleteEllective(int id)
        {
            Ellective ellective = await db.Ellectives.FindAsync(id);
            if (ellective == null)
            {
                return NotFound();
            }

            db.Ellectives.Remove(ellective);
            await db.SaveChangesAsync();

            return Ok(ellective);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EllectiveExists(int id)
        {
            return db.Ellectives.Count(e => e.EllectiveId == id) > 0;
        }
    }
}