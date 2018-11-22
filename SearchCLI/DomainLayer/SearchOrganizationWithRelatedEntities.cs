using System;
using System.Collections.Generic;
using SearchCLI.Entity;
using SearchCLI.IDomainLayer;

namespace SearchCLI.DomainLayer
{
    public class SearchOrganizationWithRelatedEntities: ISearchOrganizationWithRelatedEnties
    {
        readonly IOrganizationDL _organizationDL;
        public SearchOrganizationWithRelatedEntities(IOrganizationDL organizationDL)
        {
            this._organizationDL = organizationDL;
        }

        /// <summary>
        /// Wildcards the search organization with related entities.
        /// </summary>
        /// <returns>A list of organization with related entities.</returns>
        /// <param name="searchStr">Search string.</param>
        /// <param name="users">Users.</param>
        /// <param name="tickets">Tickets.</param>
        public List<OrganizationResult> WildcardSearchOrganizationWithRelatedEntities(string searchStr, List<User> users, List<Ticket> tickets)
        {
            List<Organization> organizations = _organizationDL.WildcardSearchOrganizations(searchStr);
            List<OrganizationResult> organizationResults = new List<OrganizationResult>();

            foreach (Organization organization in organizations)
            {
                List<User> organizationUsers = users.FindAll(u => u.organization_id == organization._id);
                List<Ticket> organizationTickets = tickets.FindAll(t => t.organization_id == organization._id);

                OrganizationResult organizationResult = new OrganizationResult(organization, organizationUsers, organizationTickets);
                organizationResults.Add(organizationResult);
            }

            return organizationResults;
        }

    }
}
