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
    public partial class MyOrders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadGridView();
        }

        public void LoadGridView()
        {
            gvOrders.DataSource = GetList();
            gvOrders.DataBind();
        }
        //OnRowCommand="gvOrders_Details"
        public List<Models.Order> GetList()
        {
            List<Models.Order> list = new List<Models.Order>();
            string url = @"https://localhost:44300/api/orders/getOrdersFromUser";

            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "POST";
                string data = "{\"idUser\":\"" + Session["idUser"] + "\"}";

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
                    var clientarray = resultado["orders"].Value<JArray>();
                    list = clientarray.ToObject<List<Models.Order>>();
                }
            }
            catch (Exception ex)
            {

            }

            return list;
        }

        protected void gvOrders_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOrders.PageIndex = e.NewPageIndex;
            LoadGridView();
        }

        protected void gvOrders_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvOrders.EditIndex = e.NewEditIndex;
            LoadGridView();
        }

        protected void gvOrders_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.gvOrders.EditIndex = -1;
            GridViewRow objFila = this.gvOrders.Rows[e.RowIndex];
            HiddenField hddIdLanguage = (HiddenField)objFila.FindControl("hddIdLanguage");

            //Borrar invocando a método de api de borrado con el id:
            // hddIdLanguage.value;


            // Mostrar mensaje:
            string script = "alert(\"Eliminado Correctamente!\");";
            ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script, true);
        }

        protected void gvOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Details")
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                GridViewRow objFila = this.gvOrders.Rows[rowIndex];
                Label lblIdOrder = (Label)objFila.FindControl("lblIdOrder");
                Session["idOrder"] = lblIdOrder.Text;
                Response.Redirect("OrderDetails.aspx");
            }
        }
    }
}