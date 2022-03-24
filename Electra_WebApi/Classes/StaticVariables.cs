namespace Electra_WebApi
{
    public class StaticVariables
    {
       //public string ServerSuffix { get; set; } = "http://localhost:8084/";
         public string ServerSuffix { get; set; } = "https://localhost:44305/";
        //public string ServerSuffix { get; set; } = "http://Electra.somee.com/";
        ////public string ServerSuffix { get; set; } = "www.bsite.net/dharmesh3600/";

        public string CityApiName { get; set; } = "CityApi";
        public string StateApiName { get; set; } = "StateApi";
        public string UserDetailApiName { get; set; } = "UserDetailApi";
        public string ItemApiName { get; set; } = "ItemApi";
        public string ConsigneeApiName { get; set; } = "ConsigneeApi";
        public string InvoiceApiName { get; set; } = "InvoiceApi";
        public string Invoice_DetailApiName { get; set; } = "Invoice_DetailApi";
        public static string From_Date { get; set; }
        public static string To_Date { get; set; }

        public static string Financial_Year { get; set; }

        public static string Invoice_No { get; set; }

        public static string Consignee_ID { get; set; }

        public static string Option { get; set; }

        public static string UserName { get; set; } = null;


    }
}