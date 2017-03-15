using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace PowerBI_REST_GUITool
{
    public partial class MainForm : Form
    {
        //Step 1 - Replace {client id} with your client app ID. 
        //To learn how to get a client app ID, see Register a client app (https://msdn.microsoft.com/en-US/library/dn877542.aspx#clientID)
        private static string clientID = string.Empty;

        //RedirectUri you used when you registered your app.
        //For a client app, a redirect uri gives AAD more details on the specific application that it will authenticate.
        private static string redirectUri = "https://login.live.com/oauth20_desktop.srf";

        //Resource Uri for Power BI API
        private static string resourceUri = Properties.Settings.Default.PowerBiAPI;

        //OAuth2 authority Uri
        private static string authority = Properties.Settings.Default.AADAuthorityUri;

        private static AuthenticationContext authContext = null;
        private static string token = String.Empty;

        //Uri for Power BI datasets
        private static string datasetsUri = Properties.Settings.Default.PowerBiDataset;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnPUT_Click(object sender, EventArgs e)
        {
            if (clientID == string.Empty)
            {
                clientID = tbClientID.Text;
                tbClientID.Enabled = false;
            }
                
            // PUT
            tbPayload.Text = UpdateDataset(tbPayload.Text);
        }

        private void btnPOST_Click(object sender, EventArgs e)
        {
            if (clientID == string.Empty)
            {
                clientID = tbClientID.Text;
                tbClientID.Enabled = false;
            }

            // POST
            tbPayload.Text = CreateDataset(tbPayload.Text);
        }

        private void btnGETDatasets_Click(object sender, EventArgs e)
        {
            if (clientID == string.Empty)
            {
                clientID = tbClientID.Text;
                tbClientID.Enabled = false;
            }

            // GET
            tbPayload.Text = GetDatasetsString();
        }

        private void btnGetTables_Click(object sender, EventArgs e)
        {
            if (clientID == string.Empty)
            {
                clientID = tbClientID.Text;
                tbClientID.Enabled = false;
            }

            // GET
            //Get a dataset id from a Dataset name. The dataset id is used for UpdateTableSchema, AddRows, and DeleteRows
            string datasetId = ((GetDatasets().value).GetDataset(tbPayload.Text)).Id;

            tbPayload.Text = GetTablesString(datasetId);

            //table[] tables = GetTables(datasetId).value;
            //StringBuilder sb = new StringBuilder();
            //foreach (table table in tables)
            //{
            //    sb.AppendLine(String.Format("Name: {0}", table.Name));
            //}

            //tbPayload.Text = sb.ToString();
        }

        //The Create Dataset operation creates a new Dataset from a JSON schema definition and returns the Dataset ID 
        //and the properties of the dataset created.
        //POST https://api.powerbi.com/v1.0/myorg/datasets
        //Create Dataset operation: https://msdn.microsoft.com/en-US/library/mt203562(Azure.100).aspx
        static string CreateDataset(string jsonText)
        {
            //In a production application, use more specific exception handling.           
            //Create a POST web request
            HttpWebRequest request = DatasetRequest(String.Format("{0}/datasets?defaultRetentionPolicy=basicFIFO", datasetsUri), "POST", AccessToken());

            //POST request using the json schema from Product
            return PostRequest(request, jsonText);
        }

        //The Create Dataset operation creates a new Dataset from a JSON schema definition and returns the Dataset ID 
        //and the properties of the dataset created.
        //POST https://api.powerbi.com/v1.0/myorg/datasets
        //Create Dataset operation: https://msdn.microsoft.com/en-US/library/mt203562(Azure.100).aspx
        static string UpdateDataset(string jsonText)
        {
            //In a production application, use more specific exception handling.           
            //Create a POST web request
            HttpWebRequest request = DatasetRequest(String.Format("{0}/datasets", datasetsUri), "PUT", AccessToken());

            //POST request using the json schema from Product
            return PostRequest(request, jsonText);
        }

        //The Get Datasets operation returns a JSON list of all Dataset objects that includes a name and id.
        //GET https://api.powerbi.com/v1.0/myorg/datasets
        //Get Dataset operation: https://msdn.microsoft.com/en-US/library/mt203567.aspx
        static string GetDatasetsString()
        {
            //Create a GET web request to list all datasets
            HttpWebRequest request = DatasetRequest(String.Format("{0}/datasets", datasetsUri), "GET", AccessToken());

            //Get HttpWebResponse from GET request
            string responseContent = GetResponse(request);

            return responseContent;
        }

        //The Get Datasets operation returns a JSON list of all Dataset objects that includes a name and id.
        //GET https://api.powerbi.com/v1.0/myorg/datasets
        //Get Dataset operation: https://msdn.microsoft.com/en-US/library/mt203567.aspx
        static Datasets GetDatasets()
        {
            Datasets response = null;

            //Create a GET web request to list all datasets
            HttpWebRequest request = DatasetRequest(String.Format("{0}/datasets", datasetsUri), "GET", AccessToken());

            //Get HttpWebResponse from GET request
            string responseContent = GetResponse(request);

            JavaScriptSerializer json = new JavaScriptSerializer();
            response = (Datasets)json.Deserialize(responseContent, typeof(Datasets));

            return response;
        }

        //The Get Tables operation returns a JSON list of Tables for the specified Dataset.
        //GET https://api.powerbi.com/v1.0/myorg/datasets/{dataset_id}/tables
        //Get Tables operation: https://msdn.microsoft.com/en-US/library/mt203556.aspx
        static string GetTablesString(string datasetId)
        {
            //Create a GET web request to list all datasets
            HttpWebRequest request = DatasetRequest(String.Format("{0}/datasets/{1}/tables", datasetsUri, datasetId), "GET", AccessToken());

            //Get HttpWebResponse from GET request
            string responseContent = GetResponse(request);
            
            return responseContent;
        }

        //The Get Tables operation returns a JSON list of Tables for the specified Dataset.
        //GET https://api.powerbi.com/v1.0/myorg/datasets/{dataset_id}/tables
        //Get Tables operation: https://msdn.microsoft.com/en-US/library/mt203556.aspx
        static Tables GetTables(string datasetId)
        {
            Tables response = null;

            //Create a GET web request to list all datasets
            HttpWebRequest request = DatasetRequest(String.Format("{0}/datasets/{1}/tables", datasetsUri, datasetId), "GET", AccessToken());

            //Get HttpWebResponse from GET request
            string responseContent = GetResponse(request);

            JavaScriptSerializer json = new JavaScriptSerializer();
            response = (Tables)json.Deserialize(responseContent, typeof(Tables));

            return response;
        }

        private static HttpWebRequest DatasetRequest(string datasetsUri, string method, string accessToken)
        {
            HttpWebRequest request = System.Net.WebRequest.Create(datasetsUri) as System.Net.HttpWebRequest;
            request.KeepAlive = true;
            request.Method = method;
            request.ContentLength = 0;
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", String.Format("Bearer {0}", accessToken));

            return request;
        }

        /// <summary>
        /// Use AuthenticationContext to get an access token
        /// </summary>
        /// <returns></returns>
        static string AccessToken()
        {
            if (token == String.Empty)
            {
                //Get Azure access token
                // Create an instance of TokenCache to cache the access token
                TokenCache TC = new TokenCache();
                // Create an instance of AuthenticationContext to acquire an Azure access token
                authContext = new AuthenticationContext(authority, TC);
                // Call AcquireToken to get an Azure token from Azure Active Directory token issuance endpoint
                token = authContext.AcquireToken(resourceUri, clientID, new Uri(redirectUri), PromptBehavior.Auto).AccessToken;
            }
            else
            {
                // Get the token in the cache
                token = authContext.AcquireTokenSilent(resourceUri, clientID).AccessToken;
            }

            return token;
        }

        private static string GetResponse(HttpWebRequest request)
        {
            string response = string.Empty;

            using (HttpWebResponse httpResponse = request.GetResponse() as System.Net.HttpWebResponse)
            {
                //Get StreamReader that holds the response stream
                using (StreamReader reader = new System.IO.StreamReader(httpResponse.GetResponseStream()))
                {
                    response = reader.ReadToEnd();
                }
            }

            return response;
        }

        private static string PostRequest(HttpWebRequest request, string json)
        {
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(json);
            request.ContentLength = byteArray.Length;

            //Write JSON byte[] into a Stream
            using (Stream writer = request.GetRequestStream())
            {
                writer.Write(byteArray, 0, byteArray.Length);
            }

            return GetResponse(request);
        }

    }

    public class Datasets
    {
        public dataset[] value { get; set; }
    }

    public class dataset
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class Tables
    {
        public table[] value { get; set; }
    }

    public class table
    {
        public string Name { get; set; }
    }

    public class Groups
    {
        public group[] value { get; set; }
    }

    public class group
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
