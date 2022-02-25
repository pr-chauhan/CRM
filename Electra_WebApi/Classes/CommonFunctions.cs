using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;

namespace Electra_WebApi
{
    public class CommonFunctions
    {
        private readonly StaticVariables staticVariables = new StaticVariables();
        public List<T> ExecuteIndex<T>(HttpClient client, string apiName)
        {
            client.BaseAddress = new Uri(staticVariables.ServerSuffix + "api/" + apiName);
            var response = client.GetAsync(apiName);
            response.Wait();
            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<T>>();
                display.Wait();
                return display.Result;
            }
            return null;
        }
        //public T ExecuteDetail<T>(HttpClient client, int id, string apiName)
        //{
        //    client.BaseAddress = new Uri(staticVariables.ServerSuffix + "api/" + apiName);
        //    var response = client.GetAsync(apiName + "/" + id);
        //    response.Wait();

        //    var test = response.Result;
        //    if (test.IsSuccessStatusCode)
        //    {
        //        var display = test.Content.ReadAsAsync<T>();
        //        display.Wait();
        //        return display.Result;
        //    }
        //    return default(T);
        //}

        public T ExecuteDetailByID<T>(HttpClient client, string id, string apiName)
        {
            client.BaseAddress = new Uri(staticVariables.ServerSuffix + "api/" + apiName);
            var response = client.GetAsync(apiName + "?id=" + id);
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<T>();
                display.Wait();
                return display.Result;
            }
            return default;
        }

        public HttpResponseMessage ExecutePost<T>(HttpClient client, T collection, string apiName)
        {
            //collection.DoE = DateTime.Now;
            //collection.DoM = DateTime.Now;
            //collection.E_UserID = "admin";
            //collection.M_UserID = "admin";
            client.BaseAddress = new Uri(staticVariables.ServerSuffix + "api/" + apiName);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var putdata = client.PostAsJsonAsync(apiName, collection);
            putdata.Wait();
            return putdata.Result;
        }

        public HttpResponseMessage ExecutePut<T>(HttpClient client, T collection, string apiName)
        {
            //collection.DoE = DateTime.Now;
            //collection.DoM = DateTime.Now;
            //collection.E_UserID = "admin";
            //collection.M_UserID = "admin";
            client.BaseAddress = new Uri(staticVariables.ServerSuffix + "api/" + apiName);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var putdata = client.PutAsJsonAsync(apiName, collection);
            putdata.Wait();
            return putdata.Result;
        }
        public HttpResponseMessage ExecuteDeleteByID(HttpClient client, string id, string apiName)
        {
            client.BaseAddress = new Uri(staticVariables.ServerSuffix + "api/" + apiName);
            var response = client.DeleteAsync(apiName + "?id=" + id);
            response.Wait();
            return response.Result;
        }

        public EntityRepository<TEntity> AttachInstance<TEntity>() where TEntity : class
        {
            return new EntityRepository<TEntity>(WebApiApplication.db);
        }

        public string GetCityNameByID(int city_id)
        {
            var state = WebApiApplication.db.Cities.Where(x => x.City_ID.Equals(city_id)).ToList();
            return state[0].City_Name.ToString();
        }
        public string GetConsigneeNameByID(int Consignee_id)
        {
            var state = WebApiApplication.db.Consignees.Where(x => x.Consignee_ID.Equals(Consignee_id)).ToList();
            return state[0].Consignee_Name.ToString();
        }
        public string GetConsigneeAddressByID(int Consignee_id)
        {
            var state = WebApiApplication.db.Consignees.Where(x => x.Consignee_ID.Equals(Consignee_id)).ToList();
            return state[0].address.ToString();
        }
        public string GetInvoiceMaxNo( string financial_yr)
        {
            var state = WebApiApplication.db.Invoices.Where(x=> x != null).Where(x=> x.Financial_Yr.Equals(financial_yr)).Select(x=> x.Invoice_ID).DefaultIfEmpty().Max() + 1;
            return state.ToString();
        }
        public string GetStateNameByID(int state_ID)
        {
            var state = WebApiApplication.db.States.Where(x => x.State_ID.Equals(state_ID)).ToList();
            return state[0].State_Name.ToString();
        }

        public bool ValidateValue<T>(string ColumnName, string parameter) where T : class, new()
        {
            Expression<Func<T, bool>> filter = null;
            var cls = AttachInstance<T>().Get(filter).AsQueryable();
            var chkExst = cls.Where(x=> x.GetType().GetProperty(ColumnName).GetValue(x, null) != null).Count(y => y.GetType().GetProperty(ColumnName).GetValue(y, null).ToString() == parameter);
            var returnVal = chkExst > 0;
            return returnVal;
        }
        public List<SelectListItem> GetGstType()
        {
            var Gsttypes = new List<SelectListItem>
            {
                new SelectListItem() { Value = string.Empty, Text = "--Select--" },
                new SelectListItem() { Value = "CGST/SGST", Text = "CGST/SGST" },
                new SelectListItem() { Value = "UGST", Text = "UGST" },
                new SelectListItem() { Value = "IGST", Text = "IGST" }
            };
            return Gsttypes;
        }
    }
}