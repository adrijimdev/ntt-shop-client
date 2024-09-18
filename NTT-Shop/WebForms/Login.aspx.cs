using Newtonsoft.Json;
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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                Session.Abandon();
            }
        }

        protected void tbx_TextChanged(object sender, EventArgs e)
        {
            if (tbxUser.Text.Length > 0)
            {
                lblMust.Visible = false;
                btnLogin.Enabled = true;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string url = @"https://localhost:44300/api/login/getLogin";

            string userEQ = tbxUser.Text;
            string passEQ = tbxPassword.Text;
            
            string data = "{\"login\":\"" + userEQ + "\",\"password\":\"" + passEQ + "\"}"; // esto es crear un Json de forma manual. También podemos crear una clase con las mismas propiedades y pasarla a JSON de forma automática.

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
                    if (bodyContent["response"].ToString().Equals("true"))
                    {
                        Session["idUser"] = bodyContent["idUser"];
                        Session["language"] = bodyContent["language"];
                        Session["rate"] = bodyContent["rate"];
                        Response.Redirect("Profile.aspx");
                    }
                    else
                    {
                        string script = "alert(\"Login incorrecto\");";
                        ScriptManager.RegisterStartupScript(this, GetType(),
                                              "ServerControlScript", script, true);
                    }



                    //API_NTT_SHOP.Models.Login login = new API_NTT_SHOP.Models.Login();
                    //login.result = result.["response"];

                    //Aquí debemos transformar result en nuestro objeto de resultados para obtener el resultado en Json y pasarlo a una clase, ejemplo una clase Login.
                    //Ejemplo de transformación: Login login = (Login)JsonConvert.DeserializeObject(result, typeof(Login));
                    //Login login = (Login)JsonConvert.DeserializeObject(result, typeof(Login));
                    //Response.Redirect("Profile.aspx");
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }
    }
}