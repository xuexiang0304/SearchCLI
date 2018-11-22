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
        public bool? shared_tickets { get; set; }
        public List<string> tags { get; set; }

        public Organization(int _id, string url, string external_id, string name, List<string> domain_names,
                           string created_at, string details, bool? shared_tickets, List<string> tags){
            this._id = _id;
            this.url = url;
            this.external_id = external_id;
            this.name = name;
            this.domain_names = domain_names;
            this.created_at = created_at;
            this.details = details;
            this.shared_tickets = shared_tickets;
            this.tags = tags;
        }
    }
}
