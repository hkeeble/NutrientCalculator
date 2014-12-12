using System;
using System.Net;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

using OAuth;
using System.Security.Cryptography;

namespace Assignment
{
    enum HTTPMethod
    {
        GET,
        POST
    }

    struct RequestParams
    {
        SortedDictionary<string, string> parameters;

        /// <summary>
        /// Constructs a new set of request parameters. Adds all default OAuth 1.0 parameters.
        /// </summary>
        /// <param name="APIkey"></param>
        /// <param name="signatureMethod"></param>
        /// <param name="oAuthVersion"></param>
        public RequestParams(string APIkey, string signatureMethod, string oAuthVersion)
        {
            parameters = new SortedDictionary<string, string>();

            Random rand = new Random(DateTime.Now.Millisecond);

            parameters.Add("oauth_consumer_key", APIkey);
            parameters.Add("oauth_timestamp", OAuthTools.GetTimestamp());
            parameters.Add("oauth_nonce", OAuthTools.GetNonce());
            parameters.Add("oauth_signature_method", signatureMethod);
            parameters.Add("oauth_version", oAuthVersion);
        }

        /// <summary>
        /// Constructs parameter list from set of parameters.
        /// </summary>
        public RequestParams(SortedDictionary<string, string> parameters)
        {
            this.parameters = parameters;
        }

        /// <summary>
        /// Adds a parameter
        /// </summary>
        /// <param name="param">Name of parameter</param>
        /// <param name="value">Value to set parameter to.</param>
        public void AddParam(string param, string value)
        {
            parameters.Add(param, value);
        }

        /// <summary>
        /// Removes parameter with the given name.
        /// </summary>
        public void RemoveParam(string param)
        {
            parameters.Remove(param);
        }

        /// <summary>
        /// Retrieves the parameter with a given name.
        /// </summary>
        public KeyValuePair<string, string> GetParam(string param)
        {
            string val;
            parameters.TryGetValue(param, out val);

            if (val.Length != 0)
                return new KeyValuePair<string, string>(param, val);
            else
                return new KeyValuePair<string, string>("", "");
        }

        /// <summary>
        /// Constructs the parameters into a string usable in a request URL (un-escaped).
        /// </summary>
        public string ConstructParams()
        {
            string concat = "";

            foreach (KeyValuePair<string, string> param in parameters)
            {
                concat += "&" + param.Key + "=" + param.Value;
            }

            concat = concat.Remove(0, 1);

            return concat;
        }

        /// <summary>
        /// Adds a signature to the paramters, based upon the parameters currently stored in this list.
        /// </summary>
        /// <param name="signatureMethod">Method to use to generate the signature.</param>
        /// <param name="httpMethod">The HTTP method used by these parameters.</param>
        /// <param name="requestURL">The request URL.</param>
        /// <param name="secretKey">The secret key.</param>
        public void AddSignature(OAuthSignatureMethod signatureMethod, HTTPMethod httpMethod, string requestURL, string secretKey)
        {
            // Construct the signature base
            string encodedURL = OAuthTools.UrlEncodeStrict(requestURL);
            string encodedParams = OAuthTools.UrlEncodeStrict(ConstructParams());
            string signatureBase = httpMethod.ToString() + "&" + encodedURL + "&" + encodedParams;

            // Construct the signature
            string signature = OAuthTools.GetSignature(signatureMethod, OAuthSignatureTreatment.Escaped, signatureBase, secretKey);

            // Add the signature to the parameters
            AddParam("oauth_signature", signature);
        }
    }
}