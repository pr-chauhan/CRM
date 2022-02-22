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
using EntityClass;

namespace Electra_WebApi.Controllers
{
    public class Invoice_DetailApiController : ApiController
    {
        private CraModel db = new CraModel();

        // GET: api/Invoice_DetailApi
        public IQueryable<Invoice_Detail> GetInvoice_Detail()
        {
            return db.Invoice_Detail;
        }

        // GET: api/Invoice_DetailApi/5
        [ResponseType(typeof(Invoice_Detail))]
        public async Task<IHttpActionResult> GetInvoice_Detail(int id)
        {
            Invoice_Detail invoice_Detail = await db.Invoice_Detail.FindAsync(id);
            if (invoice_Detail == null)
            {
                return NotFound();
            }

            return Ok(invoice_Detail);
        }

        // PUT: api/Invoice_DetailApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutInvoice_Detail(int id, Invoice_Detail invoice_Detail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != invoice_Detail.ID)
            {
                return BadRequest();
            }

            db.Entry(invoice_Detail).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Invoice_DetailExists(id))
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

        // POST: api/Invoice_DetailApi
        [ResponseType(typeof(Invoice_Detail))]
        public async Task<IHttpActionResult> PostInvoice_Detail(Invoice_Detail invoice_Detail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Invoice_Detail.Add(invoice_Detail);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = invoice_Detail.ID }, invoice_Detail);
        }

        // DELETE: api/Invoice_DetailApi/5
        [ResponseType(typeof(Invoice_Detail))]
        public async Task<IHttpActionResult> DeleteInvoice_Detail(int id)
        {
            Invoice_Detail invoice_Detail = await db.Invoice_Detail.FindAsync(id);
            if (invoice_Detail == null)
            {
                return NotFound();
            }

            db.Invoice_Detail.Remove(invoice_Detail);
            await db.SaveChangesAsync();

            return Ok(invoice_Detail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Invoice_DetailExists(int id)
        {
            return db.Invoice_Detail.Count(e => e.ID == id) > 0;
        }
    }
}