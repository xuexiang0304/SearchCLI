using System;
using System.Collections.Generic;
using SearchCLI.Entity;
using SearchCLI.IDomainLayer;

namespace SearchCLI.DomainLayer
{
    public class SearchUserWithRelatedEntities: ISearchUserWithRelatedEntities
    {
        readonly IUserDL _userDL;
        public SearchUserWithRelatedEntities(IUserDL userDL)
        {
            this._userDL = userDL;
        }

        /// <summary>
        /// Wildcards the search user with related entities.
        /// </summary>
        /// <returns>The list of users with related entities.</returns>
        /// <param name="searchStr">Search string.</param>
        /// <param name="organizations">Organizations.</param>
        /// <param name="tickets">Tickets.</param>
        public List<UserResult> WildcardSearchUserWithRelatedEntities(string searchStr, List<Organization> organizations, List<Ticket> tickets)
        {
            List<User> users = _userDL.WildcardSearchUsers(searchStr);
            List<UserResult> userResults = new List<UserResult>();

            foreach (User user in users)
            {
                Organization organization = organizations.Find(o => o._id == user.organization_id);
                List<Ticket> submittedTickets = tickets.FindAll(t => t.submitter_id == user._id);
                List<Ticket> assignedTickets = tickets.FindAll(t => t.assignee_id == user._id);
                UserResult userResult = new UserResult(user, organization, submittedTickets, assignedTickets);
                userResults.Add(userResult);
            }
            return userResults;
        }
    }
}
