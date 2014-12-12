using System.Collections.Generic;

namespace Assignment
{
    /// <summary>
    /// Contains string values representing navigation data labels.
    /// </summary>
    public class NavigationData
    {
        private const char ITEM_DELIM = '&';
        private const char KEY_DELIM = '=';

        public static readonly string PROFILE_NAME = "profileName";
        public static readonly string PROFILE_IMAGE = "profileImage";

        private Dictionary<string, string> data;

        public NavigationData()
        {
            data = new Dictionary<string, string>();
        }

        public void AddData(string key, string value)
        {
            data.Add(key, value);
        }

        /// <summary>
        /// Build the data in the object into a string formatted for inclusion in a URI.
        /// </summary>
        /// <returns></returns>
        public string Build()
        {
            string result = "";
            foreach(string key in data.Keys)
            {
                result += key + KEY_DELIM + data[key] + ITEM_DELIM;
            }
            string r =  result.Remove(result.Length - 1, 1);
            return r;
        }

        /// <summary>
        /// Use to parse a given navigation URI, and return the data it contains.
        /// </summary>
        /// <param name="uri">The URI of the navigation.</param>
        /// <returns></returns>
        static public NavigationData ParseURI(string uri)
        {
            NavigationData nData = new NavigationData();

            string[] pairs = uri.Split('?'); // Retrieve data from the URI

            if (pairs.Length == 1) // If no data, return blank
                return nData;
            else
            {
                string[] items = pairs[1].Split(ITEM_DELIM);

                foreach (string item in items)
                {
                    string[] keyVal = item.Split(KEY_DELIM);
                    nData.data.Add(keyVal[0], keyVal[1]);
                }

                return nData;
            }
        }
    }
}