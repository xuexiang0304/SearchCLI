using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SearchCLI.Entity;
using SearchCLI.IDataLayer;
using SearchCLI.IDomainLayer;
namespace SearchCLI.DomainLayer
{
    public class UserDL: IUserDL
    {
        readonly string userFilePath;
        readonly IUserMapper userMapper;

        public UserDL(string userFilePath, IUserMapper userMapper){
            this.userFilePath = userFilePath;
            this.userMapper = userMapper;
        }
        /// <summary>
        /// Wildcards the search users.
        /// only return a list of User objects.
        /// </summary>
        /// <returns>The search users.</returns>
        /// <param name="searchStr">Search string.</param>
        public List<User> WildcardSearchUsers(string searchStr){
            List<User> users = new List<User>();
            List<User> usersource = userMapper.Load(userFilePath);
            Regex rx = new Regex(@"(^|\s+)"+searchStr+@"(\s+|$)");
            users = usersource.FindAll(user => user._id.ToString() == searchStr
                                       || (user.url != null && rx.IsMatch(user.url))
                                       || (user.external_id != null && rx.IsMatch(user.external_id))
                                       || (user.name != null && rx.IsMatch(user.name))
                                       || (user.alias != null && rx.IsMatch(user.alias))
                                       || (user.created_at != null && rx.IsMatch(user.created_at))
                                       || (user.active != null && user.active.ToString().ToLower() == searchStr.ToLower())
                                       || (user.verified != null && user.verified.ToString().ToLower() == searchStr.ToLower())
                                       || (user.shared != null && user.shared.ToString().ToLower() == searchStr.ToLower())
                                       || (user.locale != null && rx.IsMatch(user.locale))
                                       || (user.timezone != null && rx.IsMatch(user.timezone))
                                       || (user.last_login_at != null && rx.IsMatch(user.last_login_at))
                                       || (user.email != null && rx.IsMatch(user.email))
                                       || (user.phone != null && rx.IsMatch(user.phone))
                                       || (user.signature != null && rx.IsMatch(user.signature))
                                       || (user.organization_id != null && user.organization_id.ToString() == searchStr)
                                       || (user.tags != null && rx.IsMatch(string.Join(" ", user.tags.ToArray())))
                                       || (user.suspended != null && user.suspended.ToString().ToLower() == searchStr.ToLower())
                                       || (user.role != null && rx.IsMatch(user.role)));
            
            return users;
        }

        /// <summary>
        /// Prints the user with a certain format
        /// </summary>
        /// <param name="user">User Object</param>
        public void PrintUser(User user)
        {
            Console.WriteLine();
            Console.WriteLine("The information of user {0}: ", user.name);
            Console.WriteLine(@"id:{0}, 
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
          user._id, user.url, user.external_id, user.name, user.alias, user.created_at,
          user.active, user.verified, user.shared, user.locale, user.timezone,
          user.last_login_at, user.email, user.phone, user.signature, user.organization_id,
          user.tags == null ? "" : string.Join(",", user.tags.ToArray()), user.suspended, user.role
                             );
            Console.WriteLine();
        }
    } 
}
