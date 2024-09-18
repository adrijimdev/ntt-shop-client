using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NTT_Shop.WebForms
{
    public partial class OrderDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGridView();
        }

        public void LoadGridView()
        {
            gvOrders.DataSource = GetList();
            gvOrders.DataBind();
        }

        public List<Models.Detail> GetList()
        {
            List<Models.Detail> list = new List<Models.Detail>();
            string url = @"https://localhost:44300/api/orders/getDetailsFromOrder";

            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "POST";
                string data = "{\"idOrder\":\"" + Session["idOrder"] + "\",\"language\":\"" + Session["language"] + "\"}";

                httpRequest.Accept = "application/json";
                httpRequest.ContentType = "application/json";

                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(data);
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    JObject resultado = JObject.Parse(result);
                    var clientarray = resultado["details"].Value<JArray>();
                    list = clientarray.ToObject<List<Models.Detail>>();
                }
            }
            catch (Exception ex)
            {

            }

            return list;
        }
    }
}