using System;
using System.Collections.Generic;
using Moq;
using SearchCLI.DomainLayer;
using SearchCLI.Entity;
using SearchCLI.IDomainLayer;
using Xunit;
using Xunit.Abstractions;

namespace SearchCLI.Tests
{
    public class SearchUserWIthRelatedEntitiesDLTest
    {
        private readonly ITestOutputHelper output;
        readonly List<Organization> _mockOrganization;
        readonly List<Ticket> _mockTickets;

        public SearchUserWIthRelatedEntitiesDLTest(ITestOutputHelper _output)
        {
            _mockOrganization = GetMockOrganization();
            _mockTickets = GetMockTickets();
            output = _output;
        }

        private List<Organization> GetMockOrganization(){
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
        public void ShouldReturnNoUserResultWithNoMatchedUsers(){
            var mockUserDL = new Mock<IUserDL>();
            List<User> searchUserResult = new List<User>();
            mockUserDL.Setup(user => user.WildcardSearchUsers(It.IsAny<string>())).Returns(searchUserResult);

            ISearchUserWithRelatedEntities userWEntitiesDL = new SearchUserWithRelatedEntities(mockUserDL.Object);

            List<UserResult> userWEntitiesResults = userWEntitiesDL.WildcardSearchUserWithRelatedEntities(It.IsAny<string>(), _mockOrganization, _mockTickets);

            Assert.True(userWEntitiesResults.Count == 0);
        }

        [Fact]
        public void ShouldReturnAUserResultWithNoEmptyEntities(){
            var mockUserDL = new Mock<IUserDL>();
            List<User> searchUserResult = new List<User>{
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
                )
            };
            mockUserDL.Setup(user => user.WildcardSearchUsers(It.IsAny<string>())).Returns(searchUserResult);
            ISearchUserWithRelatedEntities userWEntitiesDL = new SearchUserWithRelatedEntities(mockUserDL.Object);

            List<UserResult> userWEntitiesResults = userWEntitiesDL.WildcardSearchUserWithRelatedEntities(It.IsAny<string>(), _mockOrganization, _mockTickets);
            Assert.True(userWEntitiesResults.Count == 1 && userWEntitiesResults[0].organization == null 
                        && userWEntitiesResults[0].submitttedTickets.Count == 0 && userWEntitiesResults[0].assingedTickets.Count == 0);
        }

        [Fact]
        public void ShouldReturnAUserResultJustWithOrganization()
        {
            var mockUserDL = new Mock<IUserDL>();
            List<User> searchUserResult = new List<User>{
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
                    101,
                    new List<string>(new string[]{
                    "Springville",
                    "Sutton",
                    "Hartsville/Hartley",
                    "Diaperville"}),
                    true,
                    "admin"
                )
            };
            mockUserDL.Setup(user => user.WildcardSearchUsers(It.IsAny<string>())).Returns(searchUserResult);
            ISearchUserWithRelatedEntities userWEntitiesDL = new SearchUserWithRelatedEntities(mockUserDL.Object);

            List<UserResult> userWEntitiesResults = userWEntitiesDL.WildcardSearchUserWithRelatedEntities(It.IsAny<string>(), _mockOrganization, _mockTickets);
            Assert.True(userWEntitiesResults.Count == 1 && userWEntitiesResults[0].organization._id == 101
                        && userWEntitiesResults[0].submitttedTickets.Count == 0 && userWEntitiesResults[0].assingedTickets.Count == 0);
        }

        [Fact]
        public void ShouldReturnAUserResultWithNoSubmittedTicket()
        {
            var mockUserDL = new Mock<IUserDL>();
            List<User> searchUserResult = new List<User>{
                new User(
                    38,
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
                )
            };
            mockUserDL.Setup(user => user.WildcardSearchUsers(It.IsAny<string>())).Returns(searchUserResult);
            ISearchUserWithRelatedEntities userWEntitiesDL = new SearchUserWithRelatedEntities(mockUserDL.Object);

            List<UserResult> userWEntitiesResults = userWEntitiesDL.WildcardSearchUserWithRelatedEntities(It.IsAny<string>(), _mockOrganization, _mockTickets);
            Assert.True(userWEntitiesResults.Count == 1 && userWEntitiesResults[0].organization == null
                        && userWEntitiesResults[0].submitttedTickets.Count == 2 && userWEntitiesResults[0].assingedTickets.Count == 0);
        }

        [Fact]
        public void ShouldReturnAUserResultWithAssignedTicket()
        {
            var mockUserDL = new Mock<IUserDL>();
            List<User> searchUserResult = new List<User>{
                new User(
                    24,
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
                )
            };
            mockUserDL.Setup(user => user.WildcardSearchUsers(It.IsAny<string>())).Returns(searchUserResult);
            ISearchUserWithRelatedEntities userWEntitiesDL = new SearchUserWithRelatedEntities(mockUserDL.Object);

            List<UserResult> userWEntitiesResults = userWEntitiesDL.WildcardSearchUserWithRelatedEntities(It.IsAny<string>(), _mockOrganization, _mockTickets);
            Assert.True(userWEntitiesResults.Count == 1 && userWEntitiesResults[0].organization == null
                        && userWEntitiesResults[0].submitttedTickets.Count == 0 && userWEntitiesResults[0].assingedTickets.Count == 2);
        }

        [Fact]
        public void ShouldReturnMutipleUserResultWithEntities()
        {
            var mockUserDL = new Mock<IUserDL>();
            List<User> searchUserResult = new List<User>{
                new User(
                    38,
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
                    101,
                    new List<string>(new string[]{
                    "Springville",
                    "Sutton",
                    "Hartsville/Hartley",
                    "Diaperville"}),
                    true,
                    "admin"
                ),
                new User(
                    24,
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
                )
            };
            mockUserDL.Setup(user => user.WildcardSearchUsers(It.IsAny<string>())).Returns(searchUserResult);
            ISearchUserWithRelatedEntities userWEntitiesDL = new SearchUserWithRelatedEntities(mockUserDL.Object);

            List<UserResult> userWEntitiesResults = userWEntitiesDL.WildcardSearchUserWithRelatedEntities(It.IsAny<string>(), _mockOrganization, _mockTickets);
            Assert.True(userWEntitiesResults.Count == 2 && userWEntitiesResults[0].organization._id == 101 && userWEntitiesResults[1].organization == null
                        && userWEntitiesResults[0].submitttedTickets.Count == 2 && userWEntitiesResults[0].assingedTickets.Count == 0
                       && userWEntitiesResults[1].submitttedTickets.Count == 0 && userWEntitiesResults[1].assingedTickets.Count == 2);
        }
    }
}
