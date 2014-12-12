using System;
using System.Net;
using System.Linq;
using System.Text;
using System.Windows;
using System.Collections.Generic;
using System.Threading.Tasks;

using OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Assignment;

namespace FatSecretAPI
{
    delegate void StringDownloadEventHandler(object sender, DownloadStringCompletedEventArgs args);
    delegate void StringDownloadProgessEventHandler(object sender, DownloadProgressChangedEventArgs args);


    class FatSecretService
    {
        private JsonSerializerSettings serializerSettings;

        public FatSecretService()
        {
            serializerSettings = new JsonSerializerSettings();
            serializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
        }

        /// <summary>
        /// Retrieves food from the server with the given ID.
        /// </summary>
        /// <param name="ID">ID of the food to retrieve.</param>
        /// <param name="handler">Handler event to handle the completion of this request.</param>
        /// <param name="progressHandler">Optional handler for progress on this request.</param>
        public void GetFood(string ID, StringDownloadEventHandler handler, StringDownloadProgessEventHandler progressHandler = null)
        {
            RequestParams paramList = CreateBaseRequest(Method.FOOD_GET, Format.JSON);

            paramList.AddParam("food_id", ID); // Add the search parameter

            // Calculate a signature
            paramList.AddSignature(OAuthSignatureMethod.HmacSha1, HTTPMethod.GET, REQUEST_URL, API_SECRET);

            // Send the request
            SendRequest(CreateRequestURI(paramList), handler, progressHandler);
        }

        /// <summary>
        /// Searches the server for food data.
        /// </summary>
        /// <param name="searchTerms">Search terms (server will try to match this to food names).</param>
        /// <param name="handler">Handler event to handle the completion of this request.</param>
        /// <param name="progressHandler">Optional handler for progress on this request.</param>
        public void SearchFood(string searchTerms, int maxResults, int pageNumber, StringDownloadEventHandler handler, StringDownloadProgessEventHandler progressHandler = null)
        {
            RequestParams paramList = CreateBaseRequest(Method.FOODS_SEARCH, Format.JSON);

            paramList.AddParam("search_expression", searchTerms);
            paramList.AddParam("page_number", Convert.ToString(pageNumber));
            paramList.AddParam("max_results", Convert.ToString(maxResults));
            paramList.AddSignature(OAuthSignatureMethod.HmacSha1, HTTPMethod.GET, REQUEST_URL, API_SECRET);

            SendRequest(CreateRequestURI(paramList), handler, progressHandler);
        }

        public void GetRecipe(string ID, StringDownloadEventHandler handler, StringDownloadProgessEventHandler progressHandler = null)
        {
            RequestParams paramList = CreateBaseRequest(Method.RECIPE_GET, Format.JSON);
            paramList.AddParam("recipe_id", ID);
            paramList.AddSignature(OAuthSignatureMethod.HmacSha1, HTTPMethod.GET, REQUEST_URL, API_SECRET);

            SendRequest(CreateRequestURI(paramList), handler, progressHandler);
        }

        public void SearchRecipes(string searchTerms, int maxResults, int pageNumber, StringDownloadEventHandler handler, StringDownloadProgessEventHandler progressHandler = null)
        {
            RequestParams paramList = CreateBaseRequest(Method.RECIPES_SEARCH, Format.JSON);
            paramList.AddParam("search_expression", searchTerms);
            paramList.AddParam("max_results", Convert.ToString(maxResults));
            paramList.AddParam("page_number", Convert.ToString(pageNumber));
            paramList.AddSignature(OAuthSignatureMethod.HmacSha1, HTTPMethod.GET, REQUEST_URL, API_SECRET);

            SendRequest(CreateRequestURI(paramList), handler, progressHandler);
        }

        public Uri CreateRequestURI(RequestParams param)
        {
            return new Uri(REQUEST_URL + '?' + param.ConstructParams());
        }

        private void SendRequest(Uri requestURI, StringDownloadEventHandler handler, StringDownloadProgessEventHandler progressHandler)
        {
            WebClient client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(handler);
            if(progressHandler != null)
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(progressHandler);
            client.DownloadStringAsync(requestURI);
        }

        private RequestParams CreateBaseRequest(Method method, Format format)
        {
            RequestParams param = new RequestParams(API_KEY, SIG_METHOD, OAUTH_VERS);
            param.AddParam("method", method.ToString());
            param.AddParam("format", format.ToString());
            return param;
        }

        public Food DeserializeFood(string data)
        {
            JToken root = JObject.Parse(data);
            JToken food = root["food"];
            return JsonConvert.DeserializeObject<Food>(food.ToString(), serializerSettings);
        }

        public Recipe DeserializeRecipe(string data)
        {
            JToken root = JObject.Parse(data);
            JToken recipe = root["recipe"];
            return JsonConvert.DeserializeObject<Recipe>(recipe.ToString(), serializerSettings);
        }

        public FoodSearchResults DeserializeFoodSearch(string data)
        {
            JToken root = JObject.Parse(data);
            JToken foods = root["foods"];
            return JsonConvert.DeserializeObject<FoodSearchResults>(foods.ToString(), serializerSettings);
        }

        public RecipeSearchResults DeserializeRecipeSearch(string data)
        {
            JToken root = JObject.Parse(data);
            JToken recipes = root["recipes"];
            return JsonConvert.DeserializeObject<RecipeSearchResults>(recipes.ToString(), serializerSettings);
        }

        // API constants
        private const string API_KEY = "b76477001e254d1393b0ac1d3bce7aba";
        private const string API_SECRET = "560c3e46951148af8dbb2434d116535f";
        private const string REQUEST_URL = "http://platform.fatsecret.com/rest/server.api";
        private const string SIG_METHOD = "HMAC-SHA1";
        private const string OAUTH_VERS = "1.0";

        // Method constants (type-safe enum pattern!)
        public sealed class Method
        {
            private readonly string name;

            public static readonly Method FOOD_GET = new Method("food.get");
            public static readonly Method FOODS_SEARCH = new Method("foods.search");
            public static readonly Method RECIPE_GET = new Method("recipe.get");
            public static readonly Method RECIPES_SEARCH = new Method("recipes.search");

            private Method(string name) { this.name = name; }
            public override string ToString() { return name; }
        }

        public sealed class Format
        {
            private readonly string name;

            public static readonly Format XML = new Format("xml");
            public static readonly Format JSON = new Format("json");

            private Format(string name) { this.name = name; }
            public override string ToString() { return name; }
        }
    }
}