using System;
using System.Collections.Generic;

namespace SearchCLI.Entity
{
    public class Organization
    {
        public int _id { get; set; }
        public string url { get; set; }
        public string external_id { get; set; }
        public string name { get; set; }
        public List<string> domain_names { get; set; }
        public string created_at { get; set; }
        public string details { get; set; }
        public string shared_tickets { get; set; }
        public List<string> tags { get; set; }

        public Organization()
        {
        }
    }
}
