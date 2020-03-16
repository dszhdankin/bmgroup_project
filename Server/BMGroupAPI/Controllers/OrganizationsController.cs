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
    public class OrganizationsController : ApiController
    {
        private BMGroupAPIContext db = new BMGroupAPIContext();

        // GET: api/Organizations
        public IQueryable<Organization> GetOrganizations()
        {
            return db.Organizations;
        }

        // GET: api/Organizations/5
        [ResponseType(typeof(Organization))]
        public async Task<IHttpActionResult> GetOrganization(int id)
        {
            Organization organization = await db.Organizations.FindAsync(id);
            if (organization == null)
            {
                return NotFound();
            }

            return Ok(organization);
        }

        // PUT: api/Organizations/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOrganization(int id, Organization organization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != organization.Id)
            {
                return BadRequest();
            }

            db.Entry(organization).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationExists(id))
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

        // POST: api/Organizations
        [ResponseType(typeof(Organization))]
        public async Task<IHttpActionResult> PostOrganization(Organization organization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Organizations.Add(organization);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = organization.Id }, organization);
        }

        // DELETE: api/Organizations/5
        [ResponseType(typeof(Organization))]
        public async Task<IHttpActionResult> DeleteOrganization(int id)
        {
            Organization organization = await db.Organizations.FindAsync(id);
            if (organization == null)
            {
                return NotFound();
            }

            db.Organizations.Remove(organization);
            await db.SaveChangesAsync();

            return Ok(organization);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrganizationExists(int id)
        {
            return db.Organizations.Count(e => e.Id == id) > 0;
        }
    }
}