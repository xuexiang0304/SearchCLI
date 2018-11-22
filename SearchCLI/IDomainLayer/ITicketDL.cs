using System;
using System.Collections.Generic;
using SearchCLI.Entity;

namespace SearchCLI.IDomainLayer
{
    public interface ITicketDL
    {
        List<Ticket> WildcardSearchTickets(string searchStr);
        void PrintTicket(Ticket ticket);
    }
}
