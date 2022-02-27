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
            //var state = WebApiApplication.db.Cities.Where(x => x.City_ID.Equals(city_id)).ToList();
            //return state[0].City_Name.ToString();
            return string.Empty;
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
        public string GetInvoiceMaxNo(string financial_yr)
        {
            var state = WebApiApplication.db.Invoices.Where(x => x != null).Where(x => x.Financial_Yr.Equals(financial_yr)).Select(x => x.Invoice_ID).DefaultIfEmpty().Max() + 1;
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
            var chkExst = cls.Where(x => x.GetType().GetProperty(ColumnName).GetValue(x, null) != null).Count(y => y.GetType().GetProperty(ColumnName).GetValue(y, null).ToString() == parameter);
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
        public string words_money(double num)
        {
            double dollars = 0;
            Int32 cents = 0;
            string dollars_result = string.Empty;
            string cents_result = string.Empty;

            dollars = Convert.ToInt32(num);
            dollars_result = words_1_all(dollars);
            if (dollars_result.Length == 0)
            {
                dollars_result = "zero";
            }
            if (dollars_result == "one")
            {
                dollars_result += " rupee only";
            }
            else
            {
                dollars_result += " rupees only";
            }
            cents = Convert.ToInt32((num - dollars) * 100);
            cents_result = words_1_all(cents);
            if (cents_result.Length > 0)
            {
                cents_result += " paisa Only";
                cents_result = " and " + cents_result;
            }
            var resultFinal = dollars_result + cents_result;
            resultFinal = TitleCaseString(resultFinal);
            return resultFinal;
        }
        private string words_1_all(double num)
        {
            var  power_value = new Int32[] { 1000000000, 1000000 , 100000 , 1000 ,1};
            var  power_name = new string[] { "billion", "million", "lac", "thousand" , "" };
            int digits = 0;
            string result = string.Empty;
            for (int i = 0; i < 5; i++)
            {
                if (num >= power_value[i])
                {
                    digits = Convert.ToInt32(num / power_value[i]);
                    if (result.Length > 0)
                    {
                        result += " ";
                    }
                    result += words_1_999(digits) + " " + power_name[i];
                    num = num - digits * power_value[i];
                }
            }
            return result.Trim();
        }
        private string words_1_19(Int32 num)
        {
            var result = string.Empty;
            switch (num)
            {
                case 1:
                    result = "one";
                    break;
                case 2:
                    result = "two";
                    break;
                case 3:
                    result = "three";
                    break;
                case 4:
                    result = "four";
                    break;
                case 5:
                    result = "five";
                    break;
                case 6:
                    result = "six";
                    break;
                case 7:
                    result = "seven";
                    break;
                case 8:
                    result = "eight";
                    break;
                case 9:
                    result = "nine";
                    break;
                case 10:
                    result = "ten";
                    break;
                case 11:
                    result = "eleven";
                    break;
                case 12:
                    result = "twelve";
                    break;
                case 13:
                    result = "thirteen";
                    break;
                case 14:
                    result = "fourteen";
                    break;
                case 15:
                    result = "fifteen";
                    break;
                case 16:
                    result = "sixteen";
                    break;
                case 17:
                    result = "seventeen";
                    break;
                case 18:
                    result = "eighteen";
                    break;
                case 19:
                    result = "nineteen";
                    break;
            }
            return result;
        }
        private string words_1_99(Int32 num)
        {
            string result = string.Empty;
            Int32 tens = 0;
            tens = num / 10;
            if (tens <= 1)
            {
                result += " " + words_1_19(num);
            }
            else
            {
                switch (tens)
                {
                    case 2:
                        result = "twenty";
                        break;
                    case 3:
                        result = "thirty";
                        break;
                    case 4:
                        result = "fourty";
                        break;
                    case 5:
                        result = "fifty";
                        break;
                    case 6:
                        result = "sixty";
                        break;
                    case 7:
                        result = "seventy";
                        break;
                    case 8:
                        result = "eighty";
                        break;
                    case 9:
                        result = "ninety";
                        break;
                }
                result += " " + words_1_19(num - tens * 10);
            }
            return result.Trim();
        }
        private string words_1_999(Int32 num)
        {
            Int32 hundreds = 0;
            Int32 remainder = 0;
            string result = string.Empty;
            hundreds = num / 100;
            remainder = num - hundreds * 100;
            if (hundreds > 0)
            {
                result = words_1_19(hundreds) + " hundred ";
            }
            if (remainder > 0)
            {
                result += words_1_99(remainder);
            }
            return result.Trim();
        }

        private static String TitleCaseString(String s)
        {
            if (s == null) return s;

            String[] words = s.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length == 0) continue;

                Char firstChar = Char.ToUpper(words[i][0]);
                String rest = "";
                if (words[i].Length > 1)
                {
                    rest = words[i].Substring(1).ToLower();
                }
                words[i] = firstChar + rest;
            }
            return String.Join(" ", words);
        }
    }
}