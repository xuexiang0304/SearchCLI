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
    public class OrganizationMapperTest
    {
        readonly IOrganizationMapper _organizationMapper;
        readonly string _organizationFilePath;

        public OrganizationMapperTest()
        {
            _organizationMapper = new OrganizationMapper();
            _organizationFilePath = @"../../../Data/organizations.json";
        }

        [Fact]
        public void ShouldLoadMoreThanZoneOrganizations()
        {
            List<Organization> organizations = _organizationMapper.Load(_organizationFilePath);

            Assert.True(organizations.Count > 0);
        }

        [Fact]
        public void ShouldLoadOrganizationsThrowDirectoryNotFoundException()
        {
            string filePath = @"../Data/organizations.json";

            Assert.Throws<DirectoryNotFoundException>(() => _organizationMapper.Load(filePath));
        }

        [Fact]
        public void ShouldLoadOrganizationsSuccess()
        {
            List<Organization> organizations = _organizationMapper.Load(_organizationFilePath);

            Organization organization = new Organization(
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
            );

            organizations.First().Should().BeEquivalentTo(organization);

        }
    }
}
