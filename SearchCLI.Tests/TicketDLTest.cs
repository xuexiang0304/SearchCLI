using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using SearchCLI.DomainLayer;
using SearchCLI.Entity;
using SearchCLI.IDataLayer;
using SearchCLI.IDomainLayer;
using Xunit;

namespace SearchCLI.Tests
{
    public class TicketDLTest
    {
        readonly List<Ticket> _mockTickets;
        public TicketDLTest()
        {
            _mockTickets = GetMockTickets();
        }

        private List<Ticket> GetMockTickets(){
            List<Ticket> tickets = new List<Ticket>{
                new Ticket(
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
                ),
                new Ticket(
                    "1a227508-9f39-427c-8f57-1b72f3fab87c",
                    "http://initech.zendesk.com/api/v2/tickets/1a227508-9f39-427c-8f57-1b72f3fab87c.json",
                    "93e5ca820-cd1f-4a02-a18f-11b18e7bb49a",
                    "2016-04-28T11:19:34 -10:00",
                    "incident",
                    "A Catastrophe in Micronesia",
                    "Aliquip excepteur fugiat ex minim ea aute eu labore. Sunt eiusmod esse eu non commodo est veniam consequat.",
                    "high",
                    "pending",
                    38,
                    24,
                    116,
                    new List<string>(new string[]{
                        "Puerto Rico",
                        "Idaho",
                        "Oklahoma",
                        "Louisiana"
                    }),
                    false,
                    "2016-07-31T02:37:50 -10:00",
                    "chat"
                )
            };

            return tickets;
        }

        [Fact]
        public void ShouldReturnNoTicketsWithEmptyResource()
        {
            var mockTicketMapper = new Mock<ITicketMapper>();
            List<Ticket> expectedTickets = new List<Ticket>();
            mockTicketMapper.Setup(user => user.Load(It.IsAny<string>())).Returns(expectedTickets);

            ITicketDL ticketDL = new TicketDL(It.IsAny<string>(), mockTicketMapper.Object);

            List<Ticket> ticketResults = ticketDL.WildcardSearchTickets(It.IsAny<string>());

            Assert.True(ticketResults.Count == 0);
        }

        [Fact]
        public void ShouldReturnNoTicketsWithSearch()
        {
            var mockTicketMapper = new Mock<ITicketMapper>();
            mockTicketMapper.Setup(user => user.Load(It.IsAny<string>())).Returns(_mockTickets);

            ITicketDL ticketDL = new TicketDL(It.IsAny<string>(), mockTicketMapper.Object);

            List<Ticket> ticketResults = ticketDL.WildcardSearchTickets("abc");

            Assert.True(ticketResults.Count == 0);
        }

        [Fact]
        public void ShouldReturnNoTicketsWithExactMatch()
        {
            var mockTicketMapper = new Mock<ITicketMapper>();
            mockTicketMapper.Setup(user => user.Load(It.IsAny<string>())).Returns(_mockTickets);

            ITicketDL ticketDL = new TicketDL(It.IsAny<string>(), mockTicketMapper.Object);

            List<Ticket> ticketResults = ticketDL.WildcardSearchTickets("93e5ca820");

            Assert.True(ticketResults.Count == 0);
        }

        [Fact]
        public void ShouldReturnATicket()
        {
            var mockTicketMapper = new Mock<ITicketMapper>();
            mockTicketMapper.Setup(user => user.Load(It.IsAny<string>())).Returns(_mockTickets);

            ITicketDL ticketDL = new TicketDL(It.IsAny<string>(), mockTicketMapper.Object);
            List<Ticket> ticketResults = ticketDL.WildcardSearchTickets("Micronesia");
            List<Ticket> expectTickets = new List<Ticket>
            {
                _mockTickets[1]
            };

            ticketResults.Should().BeEquivalentTo(expectTickets);
        }


        [Fact]
        public void ShouldReturnMutipleTickets()
        {
            var mockTicketMapper = new Mock<ITicketMapper>();
            mockTicketMapper.Setup(user => user.Load(It.IsAny<string>())).Returns(_mockTickets);

            ITicketDL ticketDL = new TicketDL(It.IsAny<string>(), mockTicketMapper.Object);
            List<Ticket> ticketResults = ticketDL.WildcardSearchTickets("Catastrophe");
            List<Ticket> expectTickets = new List<Ticket>
            {
                _mockTickets[0],
                _mockTickets[1]

            };

            ticketResults.Should().BeEquivalentTo(expectTickets);
        }

        [Fact]
        public void ShouldReturnTicketsSearchWithNumber()
        {
            var mockTicketMapper = new Mock<ITicketMapper>();
            mockTicketMapper.Setup(user => user.Load(It.IsAny<string>())).Returns(_mockTickets);

            ITicketDL ticketDL = new TicketDL(It.IsAny<string>(), mockTicketMapper.Object);
            List<Ticket> ticketResults = ticketDL.WildcardSearchTickets("116");
            List<Ticket> expectTickets = new List<Ticket>
            {
                _mockTickets[0],
                _mockTickets[1]

            };

            ticketResults.Should().BeEquivalentTo(expectTickets);
        }

        [Fact]
        public void ShouldReturnTicketsWithBoolValue()
        {
            var mockTicketMapper = new Mock<ITicketMapper>();
            mockTicketMapper.Setup(user => user.Load(It.IsAny<string>())).Returns(_mockTickets);

            ITicketDL ticketDL = new TicketDL(It.IsAny<string>(), mockTicketMapper.Object);
            List<Ticket> ticketResults = ticketDL.WildcardSearchTickets("false");
            List<Ticket> expectTickets = new List<Ticket>
            {
                _mockTickets[0],
                _mockTickets[1]

            };

            ticketResults.Should().BeEquivalentTo(expectTickets);
        }
    }
}
