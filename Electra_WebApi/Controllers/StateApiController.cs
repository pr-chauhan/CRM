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
    public class StateApiController : ApiController
    {
        private CraModel db = new CraModel();

        // GET: api/StatesApi
        public IQueryable<State> GetStates()
        {
            return db.States;
        }

        // GET: api/StatesApi/5
        [ResponseType(typeof(State))]
        public async Task<IHttpActionResult> GetState(int id)
        {
            State state = await db.States.FindAsync(id);
            if (state == null)
            {
                return NotFound();
            }

            return Ok(state);
        }

        // PUT: api/StatesApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutState(State state)//int id, 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int id = state.State_ID;
            if (id != state.State_ID)
            {
                return BadRequest();
            }

            db.Entry(state).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StateExists(id))
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

        // POST: api/StatesApi
        [ResponseType(typeof(State))]
        public async Task<IHttpActionResult> PostState(State state)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.States.Add(state);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StateExists(state.State_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = state.State_ID }, state);
        }

        // DELETE: api/StatesApi/5
        [ResponseType(typeof(State))]
        public async Task<IHttpActionResult> DeleteState(int id)
        {
            State state = await db.States.FindAsync(id);
            if (state == null)
            {
                return NotFound();
            }

            db.States.Remove(state);
            await db.SaveChangesAsync();

            return Ok(state);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StateExists(int id)
        {
            return db.States.Count(e => e.State_ID == id) > 0;
        }
    }
}