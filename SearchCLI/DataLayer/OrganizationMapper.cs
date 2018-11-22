using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SearchCLI.Entity;
using SearchCLI.IDataLayer;
namespace SearchCLI.DataLayer
{
    public class OrganizationMapper:IOrganizationMapper
    {
        /// <summary>
        /// Load the json string from json file and conver to a list of Organizations.
        /// </summary>
        /// <returns>a list of Organizations.</returns>
        /// <param name="filePath">File path.</param>
        public List<Organization> Load(string filePath)
        {
            List<Organization> organizations = new List<Organization>();
            using (StreamReader r = new StreamReader(filePath))
            {
                var json = r.ReadToEnd();
                organizations = JsonConvert.DeserializeObject<List<Organization>>(json);
            }
            return organizations;
        }
    }
}
