using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SearchCLI.Entity;
using SearchCLI.IDataLayer;
using SearchCLI.IDomainLayer;

namespace SearchCLI.DomainLayer
{
    public class OrganizationDL: IOrganizationDL
    {
        readonly string organizationFilePath;
        readonly IOrganizationMapper organizationMapper;

        public OrganizationDL(string organizationFilePath, IOrganizationMapper organizationMapper)
        {
            this.organizationFilePath = organizationFilePath;
            this.organizationMapper = organizationMapper;
        }

        /// <summary>
        /// Wildcards the search organizations.
        /// </summary>
        /// <returns>The organizations search result without related entities.</returns>
        /// <param name="searchStr">Search string.</param>
        public List<Organization> WildcardSearchOrganizations(string searchStr)
        {
            List<Organization> organizations = new List<Organization>();
            List<Organization> organizationsource = organizationMapper.Load(organizationFilePath);
            Regex rx = new Regex(@"(^|\s+)" + searchStr + @"(\s+|$)");
            organizations = organizationsource.FindAll(organization => organization._id.ToString() == searchStr
                                                       || (organization.url != null && rx.IsMatch(organization.url))
                                                       || (organization.external_id != null && rx.IsMatch(organization.external_id))
                                                       || (organization.name != null && rx.IsMatch(organization.name))
                                                       || (organization.domain_names != null && rx.IsMatch(string.Join(" ", organization.domain_names.ToArray())))
                                                       || (organization.created_at != null && rx.IsMatch(organization.created_at))
                                                       || (organization.details != null && rx.IsMatch(organization.details))
                                                       || (organization.shared_tickets != null && organization.shared_tickets.ToString().ToLower() == searchStr.ToLower())
                                                       || (organization.tags != null && rx.IsMatch(string.Join(" ", organization.tags.ToArray()))));
            return organizations;
        }

        /// <summary>
        /// Prints the organization with a certain format
        /// </summary>
        /// <param name="organization">Organization.</param>
        public void PrintOrganization(Organization organization){
            Console.WriteLine();
            Console.WriteLine("The information of organization ({0}) is shown below:", organization.name);
            Console.WriteLine(@"id:{0},
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
            Console.WriteLine();
        }
    }
}
