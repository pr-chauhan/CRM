using EntityClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Electra_WebApi.Controllers
{
    public class InvoiceApiController : ApiController
    {
        private CraModel db = new CraModel();

        // GET: api/StatesApi
        public IQueryable<Invoice> GetInvoices()
        {
            return db.Invoices;
        }
    }
}
