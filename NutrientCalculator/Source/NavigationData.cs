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

        public static readonly string RESULT = "result";
        public static readonly string PAGE_ID = "pageID";
        public static readonly string TARGET_PAGE_ID = "targetPageID";
        public static readonly string PROFILE_NAME = "profileName";
        public static readonly string PROFILE_IMAGE = "profileImage";
        public static readonly string PROFILE_GENDER = "gender";

        public static readonly string ACCEPT = "accept";
        public static readonly string REJECT = "reject";

        private Dictionary<string, string> data;

        public NavigationData()
        {
            data = new Dictionary<string, string>();
        }

        /// <summary>
        /// Create a new navigation data object with an ID and result to be interpreted by the page navigated to.
        /// </summary>
        /// <param name="PageID">The ID given to this page.</param>
        /// <param name="result">The result fo the navigation.</param>
        public NavigationData(string PageID, string result, string targetPageID) : this()
        {
            data.Add(PAGE_ID, PageID);
            data.Add(RESULT, result);
            data.Add(TARGET_PAGE_ID, targetPageID);
        }

        /// <summary>
        /// Add data to this navigation data object to be read from the page navigated to.
        /// </summary>
        public void AddData(string key, string value)
        {
            data.Add(key, value);
        }

        /// <summary>
        /// Get the given value from the navigation data.
        /// </summary>
        public string Get(string key)
        {
            string value = "";
            data.TryGetValue(key, out value);
            return value;
        }

        /// <summary>
        /// Retrieve the ID of the page navigated from.
        /// </summary>
        public string GetPageID()
        {
            return Get(PAGE_ID);
        }

        /// <summary>
        /// Retrieve the ID of the page being navigated to.
        /// </summary>
        public string GetTargetPageID()
        {
            return Get(TARGET_PAGE_ID);
        }

        /// <summary>
        /// The navigation emits an 'accepted' signal, the form navigated from was accepted.
        /// </summary>
        public bool Accepted()
        {
            string val = Get(NavigationData.RESULT);
            if (val == NavigationData.ACCEPT)
                return true;
            else
                return false;
        }

        /// <summary>
        /// The navigation emits a 'rejected' signal, the form navigated from was rejected.
        /// </summary>
        /// <returns></returns>
        public bool Rejected()
        {
            return !Accepted();
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