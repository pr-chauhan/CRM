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
    public class CityApiController : ApiController
    {
        private CraModel db = new CraModel();

        // GET: api/CityApi
        public IQueryable<City> GetCities()
        {
            return db.Cities;
        }

        // GET: api/CityApi/5
        [ResponseType(typeof(City))]
        public async Task<IHttpActionResult> GetCity(int id)
        {
            City city = await db.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }

        // PUT: api/CityApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCity( City city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int id = city.City_ID;
            if (id != city.City_ID)
            {
                return BadRequest();
            }

            db.Entry(city).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
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

        // POST: api/CityApi
        [ResponseType(typeof(City))]
        public async Task<IHttpActionResult> PostCity(City city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cities.Add(city);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = city.City_ID }, city);
        }

        // DELETE: api/CityApi/5
        [ResponseType(typeof(City))]
        public async Task<IHttpActionResult> DeleteCity(int id)
        {
            City city = await db.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            db.Cities.Remove(city);
            await db.SaveChangesAsync();

            return Ok(city);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CityExists(int id)
        {
            return db.Cities.Count(e => e.City_ID == id) > 0;
        }
    }
}