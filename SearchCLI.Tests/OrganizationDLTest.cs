using System;
using System.Collections.Generic;
using System.IO;
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
        readonly StreamWriter _standardOut;
        public OrganizationDLTest()
        {
            _mockOrganizations = MockOrganizations();
            _standardOut = new StreamWriter(Console.OpenStandardOutput())
            {
                AutoFlush = true
            };
            Console.SetOut(_standardOut);
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

        [Fact]
        public void ShouldNotPrintWithoutValue()
        {
            Organization organization_input = null;
            var mockOrganizationMapper = new Mock<IOrganizationMapper>();
            mockOrganizationMapper.Setup(o => o.Load(It.IsAny<string>())).Returns(It.IsAny<List<Organization>>);
            IOrganizationDL organizationDL = new OrganizationDL(It.IsAny<string>(), mockOrganizationMapper.Object);
            using (StringWriter sw = new StringWriter())
            {
                _standardOut.Flush();
                Console.SetOut(sw);
                organizationDL.PrintOrganization(organization_input);

                Assert.Empty(sw.ToString());
            }

        }

        [Fact]
        public void ShouldPrintAUser()
        {
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
            var mockOrganizationMapper = new Mock<IOrganizationMapper>();
            mockOrganizationMapper.Setup(o => o.Load(It.IsAny<string>())).Returns(It.IsAny<List<Organization>>);
            IOrganizationDL organizationDL = new OrganizationDL(It.IsAny<string>(), mockOrganizationMapper.Object);

            using (StringWriter sw = new StringWriter())
            {
                _standardOut.Flush();
                Console.SetOut(sw);
                organizationDL.PrintOrganization(organization);
                string expected = string.Format(@"The information of organization ({0}) is shown below:{1}", organization.name, Environment.NewLine) +
                    string.Format(@"id:{0},
url:{1},
external_id:{2},
name:{3},
domain_names:[{4}],
created_at:{5},
details:{6},
shared_tickets:{7},
tags:[{8}]",
                                  organization._id, organization.url, organization.external_id, organization.name,
                                  organization.domain_names == null ? "" : string.Join(",", organization.domain_names.ToArray()),
                                  organization.created_at, organization.details, organization.details,
                                  organization.tags == null ? "" : string.Join(",", organization.tags.ToArray()));
                Assert.Contains(expected, sw.ToString());
            }
        }
    }
}
