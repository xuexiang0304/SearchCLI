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
        public List<Organization> Load(){
            string filePath = "../Data/organizations.json";
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
