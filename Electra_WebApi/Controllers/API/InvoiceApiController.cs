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

namespace Electra_WebApi.Controllers.API
{
    public class InvoiceApiController : ApiController
    {
        private CraModel db = new CraModel();

        // GET: api/InvoiceApi
        public IQueryable<Invoice> GetInvoices()
        {
            return db.Invoices.OrderByDescending(x=> new { x.Invoice_Date, x.Invoice_ID });
        }

        // GET: api/InvoiceApi/5
        [ResponseType(typeof(Invoice))]
        public async Task<IHttpActionResult> GetInvoice(Int32 id, string Financial_Yr)
        {
            Invoice invoice = await db.Invoices.FirstOrDefaultAsync(e => e.Financial_Yr == Financial_Yr && e.Invoice_ID == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return Ok(invoice);
        }

        // PUT: api/InvoiceApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutInvoice(Invoice invoice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int id = invoice.Invoice_ID;
            if (id != invoice.Invoice_ID )
            {
                return BadRequest();
            }

            db.Entry(invoice).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(invoice.Invoice_ID, invoice.Financial_Yr))
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

        // POST: api/InvoiceApi
        [ResponseType(typeof(Invoice))]
        public async Task<IHttpActionResult> PostInvoice(Invoice invoice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Invoices.Add(invoice);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InvoiceExists(invoice.Invoice_ID, invoice.Financial_Yr))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = invoice.Invoice_ID, Financial_Yr = invoice.Financial_Yr }, invoice);
        }

        // DELETE: api/InvoiceApi/5
        [ResponseType(typeof(Invoice))]
        public async Task<IHttpActionResult> DeleteInvoice( Int32 id, string Financial_Yr)
        {
            Invoice invoice = await db.Invoices.FirstOrDefaultAsync(e=> e.Financial_Yr == Financial_Yr && e.Invoice_ID == id);
            if (invoice == null)
            {
                return NotFound();
            }

            db.Invoices.Remove(invoice);
            await db.SaveChangesAsync();

            return Ok(invoice);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InvoiceExists(Int32 id, string Financial_Yr)
        {
            return db.Invoices.Count(e => e.Financial_Yr == Financial_Yr && e.Invoice_ID == id) > 0;
        }
    }
}