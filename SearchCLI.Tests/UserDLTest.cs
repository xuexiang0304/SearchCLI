using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using SearchCLI.DataLayer;
using SearchCLI.DomainLayer;
using SearchCLI.Entity;
using SearchCLI.IDataLayer;
using SearchCLI.IDomainLayer;
using Xunit;

namespace SearchCLI.Tests
{
    public class UserDLTest
    {
        readonly List<User> _mockUsers;

        public UserDLTest()
        {
            _mockUsers = getMockUsers();
        }

        public List<User> getMockUsers(){
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
        public void ShouldReturnNoUsersWithEmptyDataSource()
        {
            var mockUserMapper = new Mock<IUserMapper>();
            List<User> expectedUsers = new List<User>();
            mockUserMapper.Setup(user => user.Load(It.IsAny<string>())).Returns(expectedUsers);

            IUserDL userDL = new UserDL(It.IsAny<string>(), mockUserMapper.Object);

            List<User> userResults = userDL.WildcardSearchUsers(It.IsAny<string>());

            Assert.True(userResults.Count == 0);
        }

        [Fact]
        public void ShouldReturnNoUserWithSearch()
        {
            var mockUserMapper = new Mock<IUserMapper>();
            mockUserMapper.Setup(user => user.Load(It.IsAny<string>())).Returns(_mockUsers);
            IUserDL userDL = new UserDL(It.IsAny<string>(), mockUserMapper.Object);

            List<User> usersResult = userDL.WildcardSearchUsers("abc");

            Assert.True(usersResult.Count == 0);
        }

        [Fact]
        public void ShouldReturnNoUserWithExactSearch()
        {
            var mockUserMapper = new Mock<IUserMapper>();
            mockUserMapper.Setup(user => user.Load(It.IsAny<string>())).Returns(_mockUsers);
            IUserDL userDL = new UserDL(It.IsAny<string>(), mockUserMapper.Object);

            List<User> usersResult = userDL.WildcardSearchUsers("cjonibarlow");

            Assert.True(usersResult.Count == 0);
        }

        [Fact]
        public void ShouldReturnAUser(){
            var mockUserMapper = new Mock<IUserMapper>();
            mockUserMapper.Setup(user => user.Load(It.IsAny<string>())).Returns(_mockUsers);
            IUserDL userDL = new UserDL(It.IsAny<string>(), mockUserMapper.Object);

            List<User> usersResult = userDL.WildcardSearchUsers("Cross Barlow");
            List<User> expectUsers = new List<User>
            {
                _mockUsers[1]
            };

            usersResult.Should().BeEquivalentTo(expectUsers);
        }


        [Fact]
        public void ShouldReturnMutipleUser()
        {
            var mockUserMapper = new Mock<IUserMapper>();
            mockUserMapper.Setup(user => user.Load(It.IsAny<string>())).Returns(_mockUsers);
            IUserDL userDL = new UserDL(It.IsAny<string>(), mockUserMapper.Object);

            List<User> usersResult = userDL.WildcardSearchUsers("Miss");
            List<User> expectUsers = new List<User>
            {
                _mockUsers[0],
                _mockUsers[1]
            };

            usersResult.Should().BeEquivalentTo(expectUsers);
        }

        [Fact]
        public void ShouldReturnUsersSearchWithNumber()
        {
            var mockUserMapper = new Mock<IUserMapper>();
            mockUserMapper.Setup(user => user.Load(It.IsAny<string>())).Returns(_mockUsers);
            IUserDL userDL = new UserDL(It.IsAny<string>(), mockUserMapper.Object);

            List<User> usersResult = userDL.WildcardSearchUsers("119");
            List<User> expectUsers = new List<User>
            {
                _mockUsers[0],
                _mockUsers[1]
            };


            usersResult.Should().BeEquivalentTo(expectUsers);
        }

        [Fact]
        public void ShouldReturnUsersWithBoolValue()
        {
            var mockUserMapper = new Mock<IUserMapper>();
            mockUserMapper.Setup(user => user.Load(It.IsAny<string>())).Returns(_mockUsers);
            IUserDL userDL = new UserDL(It.IsAny<string>(), mockUserMapper.Object);

            List<User> usersResult = userDL.WildcardSearchUsers("true");
            List<User> expectUsers = new List<User>
            {
                _mockUsers[0],
                _mockUsers[1]
            };


            usersResult.Should().BeEquivalentTo(expectUsers);
        }
    }
}
