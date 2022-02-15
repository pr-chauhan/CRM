using EntityClass;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Electra_WebApi.Controllers
{
    public class ConsigneeApiController : ApiController
    {
        private CraModel db = new CraModel();

        // GET: api/ConsigneesApi
        public IQueryable<Consignee> GetConsignees()
        {
            return db.Consignees;
        }

        // GET: api/ConsigneesApi/5
        [ResponseType(typeof(State))]
        public async Task<IHttpActionResult> GetConsignee(int id)
        {
            Consignee consigneeDet = await db.Consignees.FindAsync(id);
            if (consigneeDet == null)
            {
                return NotFound();
            }

            return Ok(consigneeDet);
        }

        // PUT: api/ConsigneesApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutState(Consignee consigneeDet)//int id, 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int id = consigneeDet.Consignee_ID;
            if (id != consigneeDet.Consignee_ID)
            {
                return BadRequest();
            }

            db.Entry(consigneeDet).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsigneeExists(id))
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

        // POST: api/ConsigneesApi
        [ResponseType(typeof(State))]
        public async Task<IHttpActionResult> PostState(Consignee ConsigneeDet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Consignees.Add(ConsigneeDet);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ConsigneeExists(ConsigneeDet.Consignee_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = ConsigneeDet.Consignee_ID }, ConsigneeDet);
        }

        // DELETE: api/ConsigneesApi/5
        [ResponseType(typeof(State))]
        public async Task<IHttpActionResult> DeleteConsignee(int id)
        {
            Consignee ConsigneeDet = await db.Consignees.FindAsync(id);
            if (ConsigneeDet == null)
            {
                return NotFound();
            }

            db.Consignees.Remove(ConsigneeDet);
            await db.SaveChangesAsync();

            return Ok(ConsigneeDet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ConsigneeExists(int id)
        {
            return db.Consignees.Count(e => e.Consignee_ID == id) > 0;
        }
    }
}
