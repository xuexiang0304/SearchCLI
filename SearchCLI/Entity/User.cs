using System;
using System.Collections.Generic;

namespace SearchCLI.Entity
{
    public class User
    {
        public int _id { get; set; }
        public string url { get; set; }
        public string external_id { get; set; }
        public string name { get; set; }
        public string alias { get; set; }
        public string created_at { get; set; }
        public bool? active { get; set; }
        public bool? verified { get; set; }
        public bool? shared { get; set; }
        public string locale { get; set; }
        public string timezone { get; set; }
        public string last_login_at { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string signature { get; set; }
        public int? organization_id { get; set; }
        public List<string> tags { get; set; }
        public bool? suspended { get; set; }
        public string role { get; set; }

        public User (int _id, string url, string external_id, string name, string alias, string created_at, bool active,
                     bool verified, bool shared, string locale, string timezone, string last_login_at, string email,
                    string phone, string signature, int organization_id, List<string> tags, bool suspended,string role){
            this._id = _id;
            this.url = url;
            this.external_id = external_id;
            this.name = name;
            this.alias = alias;
            this.created_at = created_at;
            this.active = active;
            this.verified = verified;
            this.shared = shared;
            this.locale = locale;
            this.timezone = timezone;
            this.last_login_at = last_login_at;
            this.email = email;
            this.phone = phone;
            this.signature = signature;
            this.organization_id = organization_id;
            this.tags = tags;
            this.suspended = suspended;
            this.role = role;
        }

    }
}
