using EntityClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Electra_WebApi.Controllers
{
    public class CityApiController : ApiController
    {
        CraModel context = new CraModel();
        
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetCities()
        {
            List<City> lstCity = context.Cities.ToList();
            return Ok(lstCity);
        }

        public IHttpActionResult CreateCity(City city)
        {
            context.Cities.Add(city);
            context.SaveChangesAsync();
            return Ok();
        }

    }
}
