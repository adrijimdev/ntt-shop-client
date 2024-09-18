using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;

namespace NTT_Shop.WebForms
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("description");
            dt.Columns.Add("iso");
            List<API_NTT_SHOP.Models.Language> languages = new List<API_NTT_SHOP.Models.Language>();
            languages = GetAllLanguages();

            foreach (var item in languages)
            {
                DataRow row = dt.NewRow();
                row["description"] = item.description;
                row["iso"] = item.iso;
                dt.Rows.Add(row);
            }

            ListItem i;
            foreach (DataRow r in dt.Rows)
            {
                i = new ListItem(r["description"].ToString(), r["iso"].ToString());
                ddlLanguage.Items.Add(i);
            }
        }

        private List<API_NTT_SHOP.Models.Language> GetAllLanguages()
        {
            List<API_NTT_SHOP.Models.Language> languages = new List<API_NTT_SHOP.Models.Language>();

            string url = @"https://localhost:44300/api/languages/getAllLanguages";

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
                    var list = resultado["languages"].Value<JArray>();
                    languages = list.ToObject<List<API_NTT_SHOP.Models.Language>>();
                }
            }
            catch (Exception ex)
            {

            }

            return languages;
        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string url = @"https://localhost:44300/api/users/insertUser";

            //string token = generarToken();
            string data = "{\"user\": { \"login\":\"" + tbxUser.Text + "\",\"email\":\"" + tbxEmail.Text + "\",\"password\": \"" + tbxPassword.Text + "\", \"name\": \"" + tbxName.Text + "\",\"surname1\": \"" + tbxSurname.Text + "\",\"language\": \"" + ddlLanguage.SelectedValue + "\"}}"; // esto es crear un Json de forma manual. También podemos crear una clase con las mismas propiedades y pasarla a JSON de forma automática.

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
                    if (bodyContent["httpCode"].ToString().Equals("200"))
                    {
                        Response.Redirect("Login.aspx");
                    }
                    else
                    {
                        string script = "alert(\"Error al crear el usuario\");";
                        ScriptManager.RegisterStartupScript(this, GetType(),
                                              "ServerControlScript", script, true);
                    }
                    //var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                    //httpRequest.Method = "POST";



                    //httpRequest.Accept = "application/json";
                    ////httpRequest.Headers["Authorization"] = "Bearer " + token; //En nuestro caso no hay seguridad por token, esto no hace falta por ahora.
                    //httpRequest.ContentType = "application/json";



                    //using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                    //{
                    //    streamWriter.Write(data);
                    //}

                    //var httpResponse = (HttpWebResponse)httpRequest.GetResponse();



                    //using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    //{
                    //    var result = streamReader.ReadToEnd();

                    //    //API_NTT_SHOP.Models. login = new API_NTT_SHOP.Models.Login

                    //    //Aquí debemos transformar result en nuestro objeto de resultados para obtener el resultado en Json y pasarlo a una clase, ejemplo una clase Login.
                    //    //Ejemplo de transformación: Login login = (Login)JsonConvert.DeserializeObject(result, typeof(Login));
                    //    API_NTT_SHOP.Models.User user = (API_NTT_SHOP.Models.User)JsonConvert.DeserializeObject(result, typeof(API_NTT_SHOP.Models.User));
                    //    string script = "alert(\"Usuario creado correctamente\");";
                    //    ScriptManager.RegisterStartupScript(this, GetType(),
                    //                          "ServerControlScript", script, true);
                    //}
                }
            }
            catch (Exception ex)
            {
                string script = "alert(\"Error al crear el nuevo usuario\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                      "ServerControlScript", script, true);
            }
        }
    }
}