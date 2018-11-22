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
    public class OrganizationDLTest
    {
        readonly List<Organization> _mockOrganizations;
        public OrganizationDLTest()
        {
            _mockOrganizations = MockOrganizations();
        }

        private List<Organization> MockOrganizations(){
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

        [Fact]
        public void ShouldReturnNoOrganizationsWithEmptyResource()
        {
            var mockOrganizationMapper = new Mock<IOrganizationMapper>();
            List<Organization> expectedOrganizations = new List<Organization>();
            mockOrganizationMapper.Setup(user => user.Load(It.IsAny<string>())).Returns(expectedOrganizations);

            IOrganizationDL organizationDL = new OrganizationDL(It.IsAny<string>(), mockOrganizationMapper.Object);

            List<Organization> organizationResults = organizationDL.WildcardSearchOrganizations(It.IsAny<string>());

            Assert.True(organizationResults.Count == 0);
        }

        [Fact]
        public void ShouldReturnNoOrganizationsWithSearch()
        {
            var mockOrganizationMapper = new Mock<IOrganizationMapper>();
            mockOrganizationMapper.Setup(user => user.Load(It.IsAny<string>())).Returns(_mockOrganizations);

            IOrganizationDL organizationDL = new OrganizationDL(It.IsAny<string>(), mockOrganizationMapper.Object);

            List<Organization> organizationResults = organizationDL.WildcardSearchOrganizations("abc");

            Assert.True(organizationResults.Count == 0);
  
        }

        [Fact]
        public void ShouldReturnNoOrganizationsWithExactMatch()
        {
            var mockOrganizationMapper = new Mock<IOrganizationMapper>();
            mockOrganizationMapper.Setup(user => user.Load(It.IsAny<string>())).Returns(_mockOrganizations);

            IOrganizationDL organizationDL = new OrganizationDL(It.IsAny<string>(), mockOrganizationMapper.Object);

            List<Organization> organizationResults = organizationDL.WildcardSearchOrganizations("kage");

            Assert.True(organizationResults.Count == 0);

        }

        [Fact]
        public void ShouldReturnAOrganization()
        {
            var mockOrganizationMapper = new Mock<IOrganizationMapper>();
            mockOrganizationMapper.Setup(user => user.Load(It.IsAny<string>())).Returns(_mockOrganizations);

            IOrganizationDL organizationDL = new OrganizationDL(It.IsAny<string>(), mockOrganizationMapper.Object);

            List<Organization> organizationResults = organizationDL.WildcardSearchOrganizations("West");

            List<Organization> expectOrganizations = new List<Organization>
            {
                _mockOrganizations[0]
            };

            organizationResults.Should().BeEquivalentTo(expectOrganizations);
        }


        [Fact]
        public void ShouldReturnMutipleOrganizations()
        {
            var mockOrganizationMapper = new Mock<IOrganizationMapper>();
            mockOrganizationMapper.Setup(user => user.Load(It.IsAny<string>())).Returns(_mockOrganizations);

            IOrganizationDL organizationDL = new OrganizationDL(It.IsAny<string>(), mockOrganizationMapper.Object);

            List<Organization> organizationResults = organizationDL.WildcardSearchOrganizations("MegaCorp");

            List<Organization> expectOrganizations = new List<Organization>
            {
                _mockOrganizations[0],
                _mockOrganizations[1]
            };

            organizationResults.Should().BeEquivalentTo(expectOrganizations);
        }

        [Fact]
        public void ShouldReturnOrganizationsSearchWithNumber()
        {
            var mockOrganizationMapper = new Mock<IOrganizationMapper>();
            mockOrganizationMapper.Setup(user => user.Load(It.IsAny<string>())).Returns(_mockOrganizations);

            IOrganizationDL organizationDL = new OrganizationDL(It.IsAny<string>(), mockOrganizationMapper.Object);

            List<Organization> organizationResults = organizationDL.WildcardSearchOrganizations("102");

            List<Organization> expectOrganizations = new List<Organization>
            {
                _mockOrganizations[1]
            };

            organizationResults.Should().BeEquivalentTo(expectOrganizations);
        }

        [Fact]
        public void ShouldReturnOrganizationsWithBoolValue()
        {
            var mockOrganizationMapper = new Mock<IOrganizationMapper>();
            mockOrganizationMapper.Setup(user => user.Load(It.IsAny<string>())).Returns(_mockOrganizations);

            IOrganizationDL organizationDL = new OrganizationDL(It.IsAny<string>(), mockOrganizationMapper.Object);

            List<Organization> organizationResults = organizationDL.WildcardSearchOrganizations("false");

            List<Organization> expectOrganizations = new List<Organization>
            {
                _mockOrganizations[0],
                _mockOrganizations[1]
            };

            organizationResults.Should().BeEquivalentTo(expectOrganizations);
        }
    }
}
