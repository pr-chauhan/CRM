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
    public class OldCityApiController : ApiController
    {
        private CraModel context = new CraModel();

        // GET: api/CityApi
        public IHttpActionResult GetCities()
        {
            List<City> lstCity = context.Cities.ToList();
            return Ok(lstCity);
        }

        // GET: api/CityApi/5
        [ResponseType(typeof(City))]
        public IHttpActionResult GetCity(int id)
        {
            City city = context.Cities.Find(id);
            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }

        // PUT: api/CityApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCity(City city)//int id,
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (id != city.City_ID)
            //{
            //    return BadRequest();
            //}

            context.Entry(city).State = EntityState.Modified;

            try
            {
                context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!CityExists(id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CityApi
        [ResponseType(typeof(City))]
        public  IHttpActionResult PostCity(City city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Cities.Add(city);

            try
            {
                context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CityExists(city.City_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = city.City_ID }, city);
        }

        // DELETE: api/CityApi/5
        [ResponseType(typeof(City))]
        public IHttpActionResult DeleteCity(int id)
        {
            City city = context.Cities.Find(id);
            if (city == null)
            {
                return NotFound();
            }

            context.Cities.Remove(city);
            context.SaveChangesAsync();

            return Ok(city);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CityExists(int id)
        {
            return context.Cities.Count(e => e.City_ID == id) > 0;
        }
    }
}