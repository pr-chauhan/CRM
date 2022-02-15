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
    public class ItemApiController : ApiController
    {
        private CraModel db = new CraModel();
        // GET: api/ItemApi
        public IQueryable<Item> GetItems()
        {
            return db.Items;
        }
        // GET: api/StatesApi/5
        [ResponseType(typeof(Item))]
        public async Task<IHttpActionResult> GetItem(int id)
        {
            Item itemDet = await db.Items.FindAsync(id);
            if (itemDet == null)
            {
                return NotFound();
            }

            return Ok(itemDet);
        }

        // PUT: api/StatesApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutState(Item itemDet)//int id, 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int id = itemDet.Item_ID;
            if (id != itemDet.Item_ID)
            {
                return BadRequest();
            }

            db.Entry(itemDet).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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
       
        public async Task<IHttpActionResult> PostState(Item itemDet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Items.Add(itemDet);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ItemExists(itemDet.Item_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = itemDet.Item_ID }, itemDet);
        }

        // DELETE: api/StatesApi/5
        [ResponseType(typeof(State))]
        public async Task<IHttpActionResult> DeleteState(int id)
        {
            Item itemDet = await db.Items.FindAsync(id);
            if (itemDet == null)
            {
                return NotFound();
            }

            db.Items.Remove(itemDet);
            await db.SaveChangesAsync();

            return Ok(itemDet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        // POST: api/StatesApi
        [ResponseType(typeof(State))]
        private bool ItemExists(int id)
        {
            return db.Items.Count(e => e.Item_ID == id) > 0;
        }

    }
}
