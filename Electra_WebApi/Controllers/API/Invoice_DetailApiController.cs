using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using EntityClass;

namespace Electra_WebApi.Controllers
{
    public class Invoice_DetailApiController : ApiController
    {
        private readonly CraModel db = new CraModel();

        // GET: api/Invoice_DetailApi
        public IQueryable<Invoice_Detail> GetInvoice_Detail()
        {
            return db.Invoice_Detail;
        }

        // GET: api/Invoice_DetailApi/5
        [ResponseType(typeof(Invoice_Detail))]
        public async Task<IHttpActionResult> GetInvoice_Detail(Int32 id, string Financial_Yr)
        {
            Invoice_Detail invoice_Detail = await db.Invoice_Detail.FirstOrDefaultAsync(e => e.Financial_Yr == Financial_Yr && e.Invoice_Id == id);
            if (invoice_Detail == null)
            {
                return NotFound();
            }
            return Ok(invoice_Detail);
        }

        // PUT: api/Invoice_DetailApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutInvoice_Detail(Invoice_Detail invoice_Detail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int id = (int)invoice_Detail.Invoice_Id;
            if (id != (int)invoice_Detail.Invoice_Id)
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
                if (!Invoice_DetailExists((int)invoice_Detail.Invoice_Id, invoice_Detail.Financial_Yr))
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
            return CreatedAtRoute("DefaultApi", new { id = invoice_Detail.Invoice_Id, Financial_Yr = invoice_Detail.Financial_Yr }, invoice_Detail);
        }

        // DELETE: api/Invoice_DetailApi/5
        [ResponseType(typeof(Invoice_Detail))]
        public async Task<IHttpActionResult> DeleteInvoice_Detail(Int32 id, string Financial_Yr)
        {
            List<Invoice_Detail> invoice_Detail = await db.Invoice_Detail.Where(e => e.Financial_Yr == Financial_Yr && e.Invoice_Id == id).ToListAsync();
            if (invoice_Detail == null)
            {
                return NotFound();
            }
            for (int i = 0; i < invoice_Detail.Count; i++)
            {
                db.Invoice_Detail.Remove(invoice_Detail[i]);
            }
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

        private bool Invoice_DetailExists(Int32 id, string Financial_Yr)
        {
            return db.Invoice_Detail.Count(e => e.Financial_Yr == Financial_Yr && e.Invoice_Id == id) > 0;
        }
    }
}