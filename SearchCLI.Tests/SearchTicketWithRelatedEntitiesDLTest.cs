using System;
using System.Collections.Generic;
using Moq;
using SearchCLI.DomainLayer;
using SearchCLI.Entity;
using SearchCLI.IDomainLayer;
using Xunit;

namespace SearchCLI.Tests
{
    public class SearchTicketWithRelatedEntitiesDLTest
    {
        readonly List<Organization> _mockOrganization;
        readonly List<User> _mockUser;

        public SearchTicketWithRelatedEntitiesDLTest()
        {
            _mockOrganization = GetMockOrganization();
            _mockUser = GetMockUsers();
        }

        private List<Organization> GetMockOrganization()
        {
            List<Organization> organizations = new List<Organization>{
              new Organization(
                     101,
                    "http://initech.zendesk.com/api/v2/organizations/101.json",
                    "9270ed79-35eb-4a38-a46f-35725197ea8d",
                    "Enthaze",
                    new List<string>(new string[] {
                    "kage.com",
                    "ecratic.com",
                    "endipin.com",
                    "zentix.com"
                    }),
                    "2016-05-21T11:10:28 -10:00",
                    "MegaCorp",
                    false,
                    new List<string>(new string[] {
                        "Fulton",
                        "West",
                        "Rodriguez",
                        "Farley"
                    })
                ),
                new Organization(
                    102,
                    "http://initech.zendesk.com/api/v2/organizations/102.json",
                    "7cd6b8d4-2999-4ff2-8cfd-44d05b449226",
                    "Nutralab",
                    new List<string>(new string[] {
                        "trollery.com",
                        "datagen.com",
                        "bluegrain.com",
                        "dadabase.com"
                    }),
                    "2016-05-21T11:10:28 -10:00",
                    "MegaCorp",
                    false,
                    new List<string>(new string[] {
                      "Cherry",
                      "Collier",
                      "Fuentes",
                      "Trevino"
                    })
                    )
                };
            return organizations;
        }

        public List<User> GetMockUsers()
        {
            List<User> expectedUsers = new List<User>
            {
                new User(
                1,
                "http://initech.zendesk.com/api/v2/users/1.json",
                "74341f74-9c79-49d5-9611-87ef9b6eb75f",
                "Francisca Rasmussen",
                "Miss Coffey",
                "2016-04-15T05:19:46 -10:00",
                true,
                true,
                false,
                "en-AU",
                "Sri Lanka",
                "2013-08-04T01:03:27 -10:00",
                "coffeyrasmussen@flotonic.com",
                "8335-422-718",
                "Don't Worry Be Happy!",
                119,
                new List<string>(new string[]{
                "Springville",
                "Sutton",
                "Hartsville/Hartley",
                "Diaperville"}),
                true,
                "admin"
            ),
                new User(
                2,
                "http://initech.zendesk.com/api/v2/users/2.json",
                "c9995ea4-ff72-46e0-ab77-dfe0ae1ef6c2",
                "Cross Barlow",
                "Miss Joni",
                "2016-04-15T05:19:46 -10:00",
                true,
                true,
                false,
                "en-AU",
                "Sri Lanka",
                "2013-08-04T01:03:27 -10:00",
                "cjonibarlow@flotonic.com",
                "8335-422-718",
                "Don't Worry Be Happy!",
                119,
                new List<string>(new string[]{
                  "Foxworth",
                  "Woodlands",
                  "Herlong",
                  "Henrietta"}),
                true,
                "admin"
            )
            };

            return expectedUsers;
        }

        [Fact]
        public void ShouldReturnNoUserResultWithNoMatchedTickets()
        {
            var mockTicketDL = new Mock<ITicketDL>();
            List<Ticket> searchTicketResult = new List<Ticket>();
            mockTicketDL.Setup(ticket => ticket.WildcardSearchTickets(It.IsAny<string>())).Returns(searchTicketResult);

            ISearchTicketWithRelatedEntities ticketWEntitiesDL = new SearchTicketWIthRelatedEntities(mockTicketDL.Object);

            List<TicketResult> ticketWEntitiesResults = ticketWEntitiesDL.WildcardSearchTicketWithRelatedEntities(It.IsAny<string>(), _mockUser, _mockOrganization);

            Assert.True(ticketWEntitiesResults.Count == 0);
        }

        [Fact]
        public void ShouldReturnATicketResultWithNoEmptyEntities()
        {
            var mockTicketDL = new Mock<ITicketDL>();
            List<Ticket> searchTicketResult = new List<Ticket>{
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
                )
            };
            mockTicketDL.Setup(ticket => ticket.WildcardSearchTickets(It.IsAny<string>())).Returns(searchTicketResult);

            ISearchTicketWithRelatedEntities ticketWEntitiesDL = new SearchTicketWIthRelatedEntities(mockTicketDL.Object);

            List<TicketResult> ticketWEntitiesResults = ticketWEntitiesDL.WildcardSearchTicketWithRelatedEntities(It.IsAny<string>(), _mockUser, _mockOrganization);

            Assert.True(ticketWEntitiesResults.Count == 1 && ticketWEntitiesResults[0].organization == null
                        && ticketWEntitiesResults[0].ticketAssignee == null && ticketWEntitiesResults[0].ticketSubmitter == null);
        }

        [Fact]
        public void ShouldReturnATicketResultJustWithOrganization()
        {
            var mockTicketDL = new Mock<ITicketDL>();
            List<Ticket> searchTicketResult = new List<Ticket>{
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
                    101,
                    new List<string>(new string[]{
                        "Ohio",
                        "Pennsylvania",
                        "American Samoa",
                        "Northern Mariana Islands"
                    }),
                    false,
                    "2016-07-31T02:37:50 -10:00",
                    "web"
                )
            };
            mockTicketDL.Setup(ticket => ticket.WildcardSearchTickets(It.IsAny<string>())).Returns(searchTicketResult);

            ISearchTicketWithRelatedEntities ticketWEntitiesDL = new SearchTicketWIthRelatedEntities(mockTicketDL.Object);

            List<TicketResult> ticketWEntitiesResults = ticketWEntitiesDL.WildcardSearchTicketWithRelatedEntities(It.IsAny<string>(), _mockUser, _mockOrganization);

            Assert.True(ticketWEntitiesResults.Count == 1 && ticketWEntitiesResults[0].organization._id == 101
                        && ticketWEntitiesResults[0].ticketAssignee ==  null && ticketWEntitiesResults[0].ticketSubmitter == null);
        }

        [Fact]
        public void ShouldReturnATicketResultWithSubmitter()
        {
            var mockTicketDL = new Mock<ITicketDL>();
            List<Ticket> searchTicketResult = new List<Ticket>{
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
                    1,
                    24,
                    111,
                    new List<string>(new string[]{
                        "Ohio",
                        "Pennsylvania",
                        "American Samoa",
                        "Northern Mariana Islands"
                    }),
                    false,
                    "2016-07-31T02:37:50 -10:00",
                    "web"
                )
            };
            mockTicketDL.Setup(ticket => ticket.WildcardSearchTickets(It.IsAny<string>())).Returns(searchTicketResult);

            ISearchTicketWithRelatedEntities ticketWEntitiesDL = new SearchTicketWIthRelatedEntities(mockTicketDL.Object);

            List<TicketResult> ticketWEntitiesResults = ticketWEntitiesDL.WildcardSearchTicketWithRelatedEntities(It.IsAny<string>(), _mockUser, _mockOrganization);
            Assert.True(ticketWEntitiesResults.Count == 1 && ticketWEntitiesResults[0].organization == null
                        && ticketWEntitiesResults[0].ticketSubmitter._id == 1 && ticketWEntitiesResults[0].ticketAssignee == null);
        }

        [Fact]
        public void ShouldReturnATicketResultWithAssignee()
        {
            var mockTicketDL = new Mock<ITicketDL>();
            List<Ticket> searchTicketResult = new List<Ticket>{
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
                    1,
                    111,
                    new List<string>(new string[]{
                        "Ohio",
                        "Pennsylvania",
                        "American Samoa",
                        "Northern Mariana Islands"
                    }),
                    false,
                    "2016-07-31T02:37:50 -10:00",
                    "web"
                )
            };
            mockTicketDL.Setup(ticket => ticket.WildcardSearchTickets(It.IsAny<string>())).Returns(searchTicketResult);

            ISearchTicketWithRelatedEntities ticketWEntitiesDL = new SearchTicketWIthRelatedEntities(mockTicketDL.Object);
            List<TicketResult> ticketWEntitiesResults = ticketWEntitiesDL.WildcardSearchTicketWithRelatedEntities(It.IsAny<string>(), _mockUser, _mockOrganization);

            Assert.True(ticketWEntitiesResults.Count == 1 && ticketWEntitiesResults[0].organization == null
                        && ticketWEntitiesResults[0].ticketSubmitter == null && ticketWEntitiesResults[0].ticketAssignee._id == 1);
        }

        [Fact]
        public void ShouldReturnMutipleUserResultWithEntities()
        {
            var mockTicketDL = new Mock<ITicketDL>();
            List<Ticket> searchTicketResult = new List<Ticket>{
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
                    1,
                    24,
                    101,
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
                    2,
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
            mockTicketDL.Setup(ticket => ticket.WildcardSearchTickets(It.IsAny<string>())).Returns(searchTicketResult);

            ISearchTicketWithRelatedEntities ticketWEntitiesDL = new SearchTicketWIthRelatedEntities(mockTicketDL.Object);
            List<TicketResult> ticketWEntitiesResults = ticketWEntitiesDL.WildcardSearchTicketWithRelatedEntities(It.IsAny<string>(), _mockUser, _mockOrganization);

            Assert.True(ticketWEntitiesResults.Count == 2 && ticketWEntitiesResults[0].organization._id == 101 && ticketWEntitiesResults[1].organization == null
                        && ticketWEntitiesResults[0].ticketSubmitter._id == 1 && ticketWEntitiesResults[0].ticketAssignee == null
                        && ticketWEntitiesResults[1].ticketSubmitter == null && ticketWEntitiesResults[1].ticketAssignee._id == 2);
        }
    }
}
