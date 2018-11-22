using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using SearchCLI.DataLayer;
using SearchCLI.Entity;
using SearchCLI.IDataLayer;
using Xunit;

namespace SearchCLI.Tests
{
    public class TicketMapperTest
    {
        readonly ITicketMapper _ticketMapper;
        readonly string _ticketFilePath;

        public TicketMapperTest()
        {
            _ticketMapper = new TicketMapper();
            _ticketFilePath = @"../../../Data/tickets.json";
        }

        [Fact]
        public void ShouldLoadMoreThanZoneTickets()
        {
            List<Ticket> tickets = _ticketMapper.Load(_ticketFilePath);

            Assert.True(tickets.Count > 0);
        }

        [Fact]
        public void ShouldLoadTicketsThrowDirectoryNotFoundException()
        {
            string filePath = @"../Data/tickets.json";

            Assert.Throws<DirectoryNotFoundException>(() => _ticketMapper.Load(filePath));
        }

        [Fact]
        public void ShouldLoadTicketsSuccess()
        {
            List<Ticket> tickets = _ticketMapper.Load(_ticketFilePath);

            Ticket ticket = new Ticket(
                "436bf9b0-1147-4c0a-8439-6f79833bff5b",
                "http://initech.zendesk.com/api/v2/tickets/436bf9b0-1147-4c0a-8439-6f79833bff5b.json",
                "9210cdc9-4bee-485f-a078-35396cd74063",
                "2016-04-28T11:19:34 -10:00",
                "incident",
                "A Catastrophe in Korea (North)",
                "Nostrud ad sit velit cupidatat laboris ipsum nisi amet laboris ex exercitation amet et proident. Ipsum fugiat aute dolore tempor nostrud velit ipsum.",
                "high",
                "pending",
                38,
                24,
                116,
                new List<string>(new string[]{
                    "Ohio",
                    "Pennsylvania",
                    "American Samoa",
                    "Northern Mariana Islands"
                }),
                false,
                "2016-07-31T02:37:50 -10:00",
                "web"
            );

            tickets.First().Should().BeEquivalentTo(ticket);

        }
    }
}
