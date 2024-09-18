using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NTT_Shop.WebForms
{
    public partial class Carrito : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadGridView();
        }

        public void LoadGridView()
        {
            if (GetList() == null)
            {
                string empty = "alert(\"El carrito está vacío\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                "ServerControlScript", empty, true);
                btnConfirm.Enabled = false;
            }
            else
            {
                gvCarrito.DataSource = GetList();
                gvCarrito.DataBind();
                decimal total = 0;
                for (int i = 0; i < GetList().Count; i++)
                    total += GetList()[i].subtotal;
                lblTotal.Text = total.ToString();
            }
        }

        public List<Models.Item> GetList()
        {
            List<Models.Item> list = new List<Models.Item>();
            list = (List<Models.Item>)Session["cart"];
            return list;
        }

        protected void gvCarrito_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCarrito.PageIndex = e.NewPageIndex;
            LoadGridView();
        }

        protected void gvCarrito_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvCarrito.EditIndex = e.NewEditIndex;
            LoadGridView();
        }

        protected void gvCarrito_Buttons(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Update")
            {
                List<Models.Item> carrito = new List<Models.Item>();
                carrito = (List<Models.Item>)Session["cart"];
                int rowIndex = int.Parse(e.CommandArgument.ToString());                
                GridViewRow objFila = this.gvCarrito.Rows[rowIndex];
                HiddenField hddIdProduct = (HiddenField)objFila.FindControl("hddIdProduct");
                Label lblPrice = (Label)objFila.FindControl("lblPrice");
                TextBox tbxUnits = (TextBox)objFila.FindControl("tbxUnits");
                int idProduct = Convert.ToInt32(hddIdProduct.Value);
                if (!Regex.IsMatch(tbxUnits.Text, @"^[0-9]+$"))
                {
                    string error = "alert(\"Debe introducir un valor númerico en el campo unidades\");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                    "ServerControlScript", error, true);
                }
                else if (Convert.ToInt32(tbxUnits.Text) > CheckStock(idProduct))
                {
                    string error = "alert(\"No hay stock suficiente para satisfacer la demanda.\");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                    "ServerControlScript", error, true);
                }
                else
                {
                    carrito[rowIndex].units = int.Parse(tbxUnits.Text);

                    decimal subtotal = Convert.ToInt32(tbxUnits.Text) * Convert.ToDecimal(lblPrice.Text);
                    carrito[rowIndex].subtotal = subtotal;

                    Session["cart"] = carrito;
                    string script = "alert(\"Actualizado correctamente!\");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                                          "ServerControlScript", script, true);
                    Response.Redirect("Carrito.aspx");
                }
            }

            if (e.CommandName == "Delete")
            {
                List<Models.Item> carrito = new List<Models.Item>();
                carrito = (List<Models.Item>)Session["cart"];
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                GridViewRow objFila = this.gvCarrito.Rows[rowIndex];
                HiddenField hddIdProduct = (HiddenField)objFila.FindControl("hddIdProduct");
                int idProduct = int.Parse(hddIdProduct.Value);

                for (int i = 0; i < carrito.Count; i++)
                {
                    if (carrito[i].idProduct == idProduct)
                    {
                        carrito.Remove(carrito[i]);
                        break;
                    }
                }

                Session["cart"] = carrito;
                string script = "alert(\"Prodcuto eliminado correctamente!\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                      "ServerControlScript", script, true);
                Response.Redirect("Carrito.aspx");
            }
        }

       
        protected void gvCarrito_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.gvCarrito.EditIndex = -1;
            GridViewRow objFila = this.gvCarrito.Rows[e.RowIndex];
            HiddenField hddIdLanguage = (HiddenField)objFila.FindControl("hddIdLanguage");

            //Borrar invocando a método de api de borrado con el id:
            // hddIdLanguage.value;


            // Mostrar mensaje:
            string script = "alert(\"Eliminado Correctamente!\");";
            ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script, true);
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            string url = @"https://localhost:44300/api/orders/insertOrder";
            Models.insertOrder insertOrder = new Models.insertOrder();
            //if (Session["idUser"] != null)
            //{
            //    //Converting your session variable value to integer
                
            //}
            insertOrder.idUser = Convert.ToInt32(Session["idUser"]);
            insertOrder.totalPrice = Convert.ToDecimal(lblTotal.Text);

            string data = JsonConvert.SerializeObject(insertOrder); 

            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "POST";

                httpRequest.Accept = "application/json";
                httpRequest.ContentType = "application/json";

                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(data);
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            }
            catch (Exception ex)
            {

            }            

            int idOrder = GetLastOrder(); //Llamo al método que me devolverá la id del último pedido creado para poder crear los detalles del pedido
            string url2 = @"https://localhost:44300/api/orders/insertOrderDetail";
            List<Models.Item> itemList = new List<Models.Item>();
            itemList = (List<Models.Item>)Session["cart"];

            for (int i = 0; i < itemList.Count; i++)
            {
                Models.insertOrderDetail detail = new Models.insertOrderDetail();
                detail.idOrder = idOrder;
                detail.idProduct = itemList[i].idProduct;
                detail.price = itemList[i].price;
                detail.units = itemList[i].units;
                string data2 = JsonConvert.SerializeObject(detail);

                try
                {
                    var httpRequest = (HttpWebRequest)WebRequest.Create(url2);
                    httpRequest.Method = "POST";
                    httpRequest.Accept = "application/json";
                    httpRequest.ContentType = "application/json";

                    using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                    {
                        streamWriter.Write(data2);
                    }

                    var httpResponse = (HttpWebResponse)httpRequest.GetResponse();                    
                }
                catch (Exception ex)
                {
                }
            }
            List<Models.Item> emptyCart = new List<Models.Item>();
            Session["cart"] = emptyCart;
            Response.Redirect("Products.aspx");
        }

        protected int GetLastOrder()
        {
            List<Models.Order> orders = new List<Models.Order>();
            int idOrder = 0;
            string url = @"https://localhost:44300/api/orders/getAllOrders";

            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";
                httpRequest.Accept = "application/json";
                httpRequest.ContentType = "application/json";
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    JObject resultado = JObject.Parse(result);
                    var orderarray = resultado["orders"].Value<JArray>();
                    orders = orderarray.ToObject<List<Models.Order>>();
                }
            }
            catch (Exception e)
            { 
            }

            for (int i = 0; i < orders.Count; i++)
            { 
                if (i == orders.Count - 1)
                    idOrder = orders[i].idOrder;
            }
            return idOrder;
        }

        private int CheckStock(int idProduct)
        {
            string url = @"https://localhost:44300/api/products/getProductStock";
            string data = "{\"idProduct\":" + idProduct + "}";

            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "POST";

                httpRequest.Accept = "application/json";
                //httpRequest.Headers["Authorization"] = "Bearer " + token; //En nuestro caso no hay seguridad por token, esto no hace falta por ahora.
                httpRequest.ContentType = "application/json";

                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(data);
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    Dictionary<string, string> bodyContent = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                    Session["stock"] = Convert.ToInt32(bodyContent["stock"]);
                }
            }
            catch (Exception ex)
            {

            }
            int stock = Convert.ToInt32(Session["stock"]);
            return stock;
        }
    }
}