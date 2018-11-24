using System;
using System.Collections.Generic;
using System.IO;
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
        readonly StreamWriter _standardOut;
        public UserDLTest()
        {
            _mockUsers = getMockUsers();
            _standardOut = new StreamWriter(Console.OpenStandardOutput())
            {
                AutoFlush = true
            };
            Console.SetOut(_standardOut);
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


        [Fact]
        public void ShouldNotPrintWithoutValue()
        {
            User user_input = null;
            var mockUserMapper = new Mock<IUserMapper>();
            mockUserMapper.Setup(user => user.Load(It.IsAny<string>())).Returns(It.IsAny<List<User>>);
            IUserDL userDL = new UserDL(It.IsAny<string>(), mockUserMapper.Object);
            using (StringWriter sw = new StringWriter())
            {
                _standardOut.Flush();
                Console.SetOut(sw);
                userDL.PrintUser(user_input);

                Assert.Empty(sw.ToString());
            }

        }

        [Fact]
        public void ShouldPrintAUser()
        {
            User user_input = new User(
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
            );
            var mockUserMapper = new Mock<IUserMapper>();
            mockUserMapper.Setup(user => user.Load(It.IsAny<string>())).Returns(It.IsAny<List<User>>);
            IUserDL userDL = new UserDL(It.IsAny<string>(), mockUserMapper.Object);
            using (StringWriter sw = new StringWriter())
            {
                _standardOut.Flush();
                Console.SetOut(sw);
                userDL.PrintUser(user_input);
                string expected = string.Format(@"The information of user {0}: {1}", user_input.name, Environment.NewLine) +
                    string.Format(@"id:{0}, 
url:{1}, 
external_id:{2}, 
name:{3}, 
alias:{4}, 
created_at:{5}, 
active:{6}, 
verified:{7}, 
shared:{8}, 
locale:{9}, 
timezone:{10},
last_login_at:{11}, 
email:{12}, 
phone:{13}, 
signature:{14}, 
organization_id:{15}, 
tags:[{16}], 
suspended:{17}, 
role:{18} ",
              user_input._id, user_input.url, user_input.external_id, user_input.name, user_input.alias, user_input.created_at,
              user_input.active, user_input.verified, user_input.shared, user_input.locale, user_input.timezone,
              user_input.last_login_at, user_input.email, user_input.phone, user_input.signature, user_input.organization_id,
              user_input.tags == null ? "" : string.Join(",", user_input.tags.ToArray()), user_input.suspended, user_input.role);
                Assert.Contains(expected, sw.ToString());
            }
        }
    }
}
