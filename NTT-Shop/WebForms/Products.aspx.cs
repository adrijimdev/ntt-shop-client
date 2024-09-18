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
    public partial class Products : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGridView();                
            } 
        }

        public void LoadGridView()
        {
            List<NTTShop.Models.Product> productList = new List<NTTShop.Models.Product>();

            string url = @"https://localhost:44300/api/products/productsForUser";

            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "POST";
                string data = "{\"language\":\"" + Session["language"] + "\",\"rate\":\"" + Session["rate"] + "\"}";

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
                    //var listProducts = JObject.Parse(result).SelectToken("products").ToObject<Dictionary<string, string>>();
                    //foreach (Models.Product item in listProducts)
                    //{
                    //    productList.Add(item);
                    //}
                    //Dictionary<string, dynamic> bodyContent = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(result);
                    JObject resultado = JObject.Parse(result);
                    var clientarray = resultado["products"].Value<JArray>();
                    productList = clientarray.ToObject<List<NTTShop.Models.Product>>();
                    //List<JToken> list = bodyContent["products"];
                    //foreach (JToken product in list)
                    //{ 
                    //    Models.Product prod = JsonConvert.DeserializeObject<Models.Product>(product.ToString());
                    //    productList.Add(prod);
                    //}
                }
            }
            catch (Exception ex)
            {
               
            }

            gvProducts.DataSource = productList;
            gvProducts.DataBind();
        }

        protected void gvProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProducts.PageIndex = e.NewPageIndex;
            LoadGridView();
        }

        protected void gvProducts_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvProducts.EditIndex = e.NewEditIndex;
            LoadGridView();
        }

        protected void gvProducts_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //Obtener objeto fila seleccionado para actualizar:
            GridViewRow objFila = this.gvProducts.Rows[e.RowIndex];

            //Obtener valores del objeto fila modificados en pantalla:
            HiddenField hddIdProduct = (HiddenField)objFila.FindControl("hddIdProduct");
            TextBox name = (TextBox)objFila.FindControl("tbxName");
            TextBox description = (TextBox)objFila.FindControl("tbxDescription");
            TextBox price = (TextBox)objFila.FindControl("tbxPrice");

            //En este punto, llamar a métodos de API/Base de datos para actualizar los datos.
            //Obtener datos de hddIdLanguage -> hddIdLanguage.value;
            //Obtener datos de description -> description.Text;
            //Obtener datos de iso -> iso.Text;

            // Mostrar mensaje:
            string script = "alert(\"Actualizado Correctamente!\");";
            ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script, true);

            //Recargar Grid en modo no-edición
            this.gvProducts.EditIndex = -1;
            LoadGridView();
        }

        protected void gvProducts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.gvProducts.EditIndex = -1;
            GridViewRow objFila = this.gvProducts.Rows[e.RowIndex];
            HiddenField hddIdLanguage = (HiddenField)objFila.FindControl("hddIdLanguage");

            //Borrar invocando a método de api de borrado con el id:
            // hddIdLanguage.value;


            // Mostrar mensaje:
            string script = "alert(\"Eliminado Correctamente!\");";
            ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script, true);
        }

        protected void gvProducts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddToCart")
            {
                Models.Item item = new Models.Item();
                NTTShop.Models.Product product = new NTTShop.Models.Product();
                List<Models.Item> cart = new List<Models.Item>();
                cart = (List<Models.Item>)Session["cart"];
                if (cart == null)
                { 
                    cart = new List<Models.Item>();
                }
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                
                //Obtener objeto fila seleccionado para actualizar:
                GridViewRow objFila = this.gvProducts.Rows[rowIndex];
                
                //Obtener valores del objeto fila modificados en pantalla:
                HiddenField hddIdProduct = (HiddenField)objFila.FindControl("hddIdProduct");
                Label lblName = (Label)objFila.FindControl("lblName");
                Label lblDescription = (Label)objFila.FindControl("lblDescription");
                Label lblPrice = (Label)objFila.FindControl("lblPrice");
                TextBox tbxUnits = (TextBox)objFila.FindControl("tbxUnits");
                int idProduct = Convert.ToInt32(hddIdProduct.Value);
                int units = Convert.ToInt32(tbxUnits.Text);


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
                    if (CheckProduct(idProduct) == true)
                    {
                        for (int i = 0; i < cart.Count; i++)
                        {
                            if (cart[i].idProduct == idProduct)
                            {
                                if (cart[i].units + units > CheckStock(idProduct))
                                {
                                    string error = "alert(\"No hay stock suficiente para satisfacer la demanda.\");";
                                    ScriptManager.RegisterStartupScript(this, GetType(),
                                    "ServerControlScript", error, true);
                                }
                                else
                                {
                                    cart[i].units += units;
                                    Session["cart"] = cart;
                                }
                            }
                        }
                    }

                    else
                    {
                        item.idProduct = idProduct;
                        item.name = lblName.Text;
                        item.description = lblDescription.Text;
                        item.price = Convert.ToDecimal(lblPrice.Text);
                        item.units = units;
                        item.subtotal = item.units * item.price;

                        if (Session["cart"] == null)
                        {

                            cart.Add(item);
                            Session["cart"] = cart;
                        }

                        else
                        {
                            cart = (List<Models.Item>)Session["cart"];
                            cart.Add(item);
                            Session["cart"] = cart;
                        }
                    }

                    string script = "alert(\"Producto añadido correctamente!\");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                    "ServerControlScript", script, true);
                }
            }
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

        protected bool CheckProduct(int idProduct)
        {
            List<Models.Item> cart = new List<Models.Item>();
            cart = (List<Models.Item>)Session["cart"];
            bool check = false;
            if (cart == null)
            {
                check = false;
            }
                
            else
            {
                for (int i = 0; i < cart.Count; i++)
                {
                    if (cart[i].idProduct == idProduct)
                    {
                        check = true;
                    }
                        
                }
            }
            return check;
        }
        protected void gvProducts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //protected void btnCarrito_Click()
        //{ 

        //}
    }
}