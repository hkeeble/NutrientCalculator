using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.IsolatedStorage;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

using Newtonsoft.Json;

namespace Assignment
{
    class UserProfileList
    {
        public List<UserProfile> Profiles { get; set; }

        public UserProfileList()
        {
            Profiles = new List<UserProfile>();
        }

        public void ReadProfiles()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings() { ContractResolver = new PrivateMembersContractResolver() };

            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                Profiles = new List<UserProfile>();

                string[] profileNames = storage.GetDirectoryNames(@"profiles\*");
                for (int i = 0; i < profileNames.Length; i++)
                {
                    using(StreamReader reader = new StreamReader(new IsolatedStorageFileStream(@"profiles\" + profileNames[i] + @"\" + profileNames[i] + ".json", FileMode.Open, FileAccess.Read, storage)))
                    {
                        string data = reader.ReadToEnd();
                        Profiles.Add(JsonConvert.DeserializeObject<UserProfile>(data, settings));
                        Profiles[Profiles.Count - 1].LoadImage(); // Load the bitmap image for this profile
                    }
                }
            }

            foreach (UserProfile profile in Profiles)
            {
                Debug.WriteLine(profile.Name);
            }
        }
    }
}
