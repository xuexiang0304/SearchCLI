using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SearchCLI.Entity;
using SearchCLI.IDataLayer;
namespace SearchCLI.DataLayer
{
    public class TicketMapper: ITicketMapper
    {
        public List<Ticket> Load(){
            string filePath = "../Data/tickets.json";
            List<Ticket> tickets = new List<Ticket>();
            using (StreamReader r = new StreamReader(filePath))
            {
                var json = r.ReadToEnd();
                tickets = JsonConvert.DeserializeObject<List<Ticket>>(json);
            }
            return tickets;
        }
    }
}
