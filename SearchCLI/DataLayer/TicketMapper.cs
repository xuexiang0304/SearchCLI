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
        /// <summary>
        /// Load the json string from the json file and convert to a list of Tickets.
        /// </summary>
        /// <returns>A list of Tickets.</returns>
        /// <param name="filePath">File path.</param>
        public List<Ticket> Load(string filePath)
        {
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
