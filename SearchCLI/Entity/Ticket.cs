using System;
using System.Collections.Generic;

namespace SearchCLI.Entity
{
    public class Ticket
    {
        public string _id { get; set; }
        public string url { get; set; }
        public string external_id { get; set; }
        public string created_at { get; set; }
        public string type { get; set; }
        public string subject { get; set; }
        public string description { get; set; }
        public string priority { get; set; }
        public string status { get; set; }
        public int? submitter_id { get; set; }
        public int? assignee_id { get; set; }
        public int? organization_id { get; set; }
        public List<string> tags { get; set; }
        public bool? has_incidents { get; set; }
        public string due_at { get; set; }
        public string via { get; set; }

        public Ticket(string _id, string url, string external_id, string created_at, string type, string subject,
                     string description, string priority, string status, int? submitter_id, int? assignee_id,
                      int? organization_id, List<string> tags, bool? has_incidents, string due_at, string via){
            this._id = _id;
            this.url = url;
            this.external_id = external_id;
            this.created_at = created_at;
            this.type = type;
            this.subject = subject;
            this.description = description;
            this.priority = priority;
            this.status = status;
            this.submitter_id = submitter_id;
            this.assignee_id = assignee_id;
            this.organization_id = organization_id;
            this.tags = tags;
            this.has_incidents = has_incidents;
            this.due_at = due_at;
            this.via = via;
        }
    }
}
