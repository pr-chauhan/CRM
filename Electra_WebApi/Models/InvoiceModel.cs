using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityClass;

namespace Electra_WebApi.Models
{
    public class InvoiceModel
    {
        public Invoice invoice { get; set; }
        public Invoice_Detail invoice_Detail { get; set; }

    }
}