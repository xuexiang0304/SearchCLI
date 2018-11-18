using System;
using System.Collections.Generic;
using SearchCLI.Entity;

namespace SearchCLI.IDataLayer
{
    public interface ITicketMapper
    {
        List<Ticket> Load();
    }
}
