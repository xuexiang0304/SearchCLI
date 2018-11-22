using System;
using System.Collections.Generic;
using SearchCLI.DataLayer;
using SearchCLI.Entity;
using SearchCLI.IDataLayer;
using Xunit;
using FluentAssertions;
using System.IO;
using System.Linq;

namespace SearchCLI.Tests
{
    public class UserMapperTest
    {
        readonly IUserMapper _userMapper;
        readonly string _userFilePath;
        public UserMapperTest()
        {
            _userMapper = new UserMapper();
            _userFilePath = @"../../../Data/users.json";
        }

        [Fact]
        public void ShouldLoadMoreThanOneUsers(){
            List<User> users = _userMapper.Load(_userFilePath);

            Assert.True(users.Count > 0);
        }

        [Fact]
        public void ShouldLoadUsersThrowDirectoryNotFoundException()
        {
            string filePath = @"../Data/users.json";

            Assert.Throws<DirectoryNotFoundException>(() => _userMapper.Load(filePath));
        }

        [Fact]
        public void ShouldLoadUsersSuccess(){
            List<User> users = _userMapper.Load(_userFilePath);

            User expected = new User
            (
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

            users.First().Should().BeEquivalentTo(expected);

        }
    }
}
