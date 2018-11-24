using System;
using System.Collections.Generic;
using Moq;
using SearchCLI.DomainLayer;
using SearchCLI.Entity;
using SearchCLI.IDomainLayer;
using Xunit;

namespace SearchCLI.Tests
{
    public class PrintDLTest
    {
        readonly List<UserResult> _mockUserResults;
        public PrintDLTest()
        {
            _mockUserResults = GetMockUserResults();
        }

        private List<UserResult> GetMockUserResults(){
            List<UserResult> userResults = new List<UserResult>{
                new UserResult(
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
                new Organization(
                    119,
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
                    new List<Ticket>{
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
                        1,
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
                },
                    new List<Ticket>{
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
                        1,
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

                }               
                )
            };

            return userResults;
        }

        [Fact]
        public void ShouldNotCallPrintUserWithNoResult(){
            var mockTicketDL = new Mock<ITicketDL>();
            mockTicketDL.Setup(ticket => ticket.PrintTicket(It.IsAny<Ticket>())).Verifiable();
            var mockUserDL = new Mock<IUserDL>();
            mockUserDL.Setup(user => user.PrintUser(It.IsAny<User>())).Verifiable();
            var mockOrganization = new Mock<IOrganizationDL>();
            mockOrganization.Setup(o => o.PrintOrganization(It.IsAny<Organization>())).Verifiable();

            IPrintDL printDl = new PrintDL(mockUserDL.Object, mockTicketDL.Object, mockOrganization.Object);

            printDl.PrintUserResult(new List<UserResult>());

            mockUserDL.Verify(u => u.PrintUser(It.IsAny<User>()), Times.Never);

        }

        [Fact]
        public void ShouldCallPrintUserOnceWithOneResult()
        {
            var mockTicketDL = new Mock<ITicketDL>();
            mockTicketDL.Setup(ticket => ticket.PrintTicket(It.IsAny<Ticket>())).Verifiable();
            var mockUserDL = new Mock<IUserDL>();
            mockUserDL.Setup(user => user.PrintUser(It.IsAny<User>())).Verifiable();
            var mockOrganization = new Mock<IOrganizationDL>();
            mockOrganization.Setup(o => o.PrintOrganization(It.IsAny<Organization>())).Verifiable();
            IPrintDL printDl = new PrintDL(mockUserDL.Object, mockTicketDL.Object, mockOrganization.Object);

            printDl.PrintUserResult(_mockUserResults);

            mockUserDL.Verify(u => u.PrintUser(It.IsAny<User>()), Times.Once);

        }

        [Fact]
        public void ShouldCallPrintOrganizationOnceTimesWithProvidedResult()
        {
            var mockTicketDL = new Mock<ITicketDL>();
            mockTicketDL.Setup(ticket => ticket.PrintTicket(It.IsAny<Ticket>())).Verifiable();
            var mockUserDL = new Mock<IUserDL>();
            mockUserDL.Setup(user => user.PrintUser(It.IsAny<User>())).Verifiable();
            var mockOrganization = new Mock<IOrganizationDL>();
            mockOrganization.Setup(o => o.PrintOrganization(It.IsAny<Organization>())).Verifiable();
            IPrintDL printDl = new PrintDL(mockUserDL.Object, mockTicketDL.Object, mockOrganization.Object);

            printDl.PrintUserResult(_mockUserResults);

            mockOrganization.Verify(o => o.PrintOrganization(It.IsAny<Organization>()), Times.Once);

        }

        [Fact]
        public void ShouldCallPrintTicketThreeTimesWithProvidedResult()
        {
            var mockTicketDL = new Mock<ITicketDL>();
            mockTicketDL.Setup(ticket => ticket.PrintTicket(It.IsAny<Ticket>())).Verifiable();
            var mockUserDL = new Mock<IUserDL>();
            mockUserDL.Setup(user => user.PrintUser(It.IsAny<User>())).Verifiable();
            var mockOrganization = new Mock<IOrganizationDL>();
            mockOrganization.Setup(o => o.PrintOrganization(It.IsAny<Organization>())).Verifiable();
            IPrintDL printDl = new PrintDL(mockUserDL.Object, mockTicketDL.Object, mockOrganization.Object);

            printDl.PrintUserResult(_mockUserResults);

            mockTicketDL.Verify(ticket => ticket.PrintTicket(It.IsAny<Ticket>()), Times.Exactly(3));

        }
    }
}
