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
    public class ItemApiController : ApiController
    {
        private readonly CraModel db = new CraModel();

        // GET: api/ItemApi
        public IQueryable<Item> GetItems()
        {
            return db.Items;
        }

        // GET: api/ItemApi/5
        [ResponseType(typeof(Item))]
        public async Task<IHttpActionResult> GetItem(int id)
        {
            Item item = await db.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        // PUT: api/ItemApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutItem( Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int id = item.Item_ID;
            if (id != item.Item_ID)
            {
                return BadRequest();
            }

            db.Entry(item).State = EntityState.Modified;
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

        // POST: api/ItemApi
        [ResponseType(typeof(Item))]
        public async Task<IHttpActionResult> PostItem(Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Items.Add(item);
            await db.SaveChangesAsync();
            return CreatedAtRoute("DefaultApi", new { id = item.Item_ID }, item);
        }

        // DELETE: api/ItemApi/5
        [ResponseType(typeof(Item))]
        public async Task<IHttpActionResult> DeleteItem(int id)
        {
            Item item = await db.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            db.Items.Remove(item);
            await db.SaveChangesAsync();
            return Ok(item);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemExists(int id)
        {
            return db.Items.Count(e => e.Item_ID == id) > 0;
        }
    }
}