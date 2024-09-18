using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NTT_Shop.WebForms
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProfile();
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
        }

        public void LoadProfile()
        {
            string url = @"https://localhost:44300/api/users/getUser";
            string data = "{\"pkUser\":\"" + Session["idUser"] + "\"}"; // esto es crear un Json de forma manual. También podemos crear una clase con las mismas propiedades y pasarla a JSON de forma automática.

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
                    var userResult = JObject.Parse(result).SelectToken("user").ToObject<Dictionary<string, string>>();

                    tbxProUser.Attributes.Add("placeholder", userResult["login"]);
                    tbxProMail.Attributes.Add("placeholder", userResult["email"]);
                    tbxProName.Attributes.Add("placeholder", userResult["name"]);
                    tbxProSurname1.Attributes.Add("placeholder", userResult["surname1"]);
                    tbxProSurname2.Attributes.Add("placeholder", userResult["surname2"]);
                    tbxProAddress.Attributes.Add("placeholder", userResult["address"]);
                    tbxProProvince.Attributes.Add("placeholder", userResult["province"]);
                    tbxProTown.Attributes.Add("placeholder", userResult["town"]);
                    tbxProPostalCode.Attributes.Add("placeholder", userResult["postalcode"]);
                    tbxProPhone.Attributes.Add("placeholder", userResult["phone"]);
                }
            }
            catch (Exception ex)
            {

            }
        }
        
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string url = @"https://localhost:44300/api/users/updateUser";

            string data = "{\"user\": { \"pkUser\":\"" + Session["idUSer"] + "\",\"login\":\"" + tbxProUser.Text + "\",\"email\":\"" + tbxProMail.Text + "\", \"name\": \"" + tbxProName.Text + "\",\"surname1\": \"" + tbxProSurname1.Text + "\",\"surname2\": \"" + tbxProSurname2.Text + "\",\"address\":\"" + tbxProAddress.Text + "\",\"province\":\"" + tbxProProvince.Text + "\", \"town\": \"" + tbxProTown.Text + "\",\"postalcode\": \"" + tbxProPostalCode.Text + "\",\"phone\": \"" + tbxProPhone.Text + "\",\"language\":\"" + ddlLanguage.SelectedValue + "\"}}"; // esto es crear un Json de forma manual. También podemos crear una clase con las mismas propiedades y pasarla a JSON de forma automática.
            
            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "PUT";

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
                }
                Session["language"] = ddlLanguage.SelectedValue;
            }
            catch (Exception ex)
            {

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
    }
}