using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Security;

namespace OscarsGame.Roles
{
    public sealed class WebConfigRoleProvider : RoleProvider
    {
        private Dictionary<string, List<string>> _roles = new Dictionary<string, List<string>>();

        public override void Initialize(string name, NameValueCollection config)
        {
            // 
            // Initialize values from web.config. 
            // 
            if (config == null)
                throw new ArgumentNullException("config");

            if (name == null || name.Length == 0)
                name = "WebConfigRoleProvider";

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Sample WebConfig Role provider");
            }

            // Initialize the abstract base class. 
            base.Initialize(name, config);

            if (config["applicationName"] == null || config["applicationName"].Trim() == "")
            {
                _applicationName = System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath;
            }
            else
            {
                _applicationName = config["applicationName"];
            }

            if (config["writeExceptionsToEventLog"] != null)
            {
                if (config["writeExceptionsToEventLog"].ToUpper() == "TRUE")
                {
                    WriteExceptionsToEventLog = true;
                }
            }

            // 
            // Initialize Roles
            //

            RolesConfigurationSection section = RolesConfigurationSection.GetConfig();

            foreach (var roleElement in  section.Roles.Cast<RoleConfigurationElement>())
            {
                if (!_roles.TryGetValue(roleElement.Name, out List<string> users))
                {
                    users = new List<string>();
                    _roles.Add(roleElement.Name, users);
                }

                users.Add(roleElement.User);
            }
        }

        private string _applicationName;

        public override string ApplicationName
        {
            get { return _applicationName; }
            set { _applicationName = value; }
        }

        public bool WriteExceptionsToEventLog { get; set; }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotSupportedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotSupportedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotSupportedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            if (!_roles.TryGetValue(roleName, out List<string> users))
            {
                users = new List<string>();
            }

            return users.Where(x => x.StartsWith(usernameToMatch, StringComparison.OrdinalIgnoreCase)).ToArray();
        }

        public override string[] GetAllRoles()
        {
            return _roles.Keys.ToArray();
        }

        public override string[] GetRolesForUser(string username)
        {
            return _roles.Where(x => x.Value.Contains(username)).Select(x => x.Key).ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            if (!_roles.TryGetValue(roleName, out List<string> users))
            {
                users = new List<string>();
            }

            return users.ToArray();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            if (!_roles.TryGetValue(roleName, out List<string> users))
            {
                return false;
            }

            return users.Contains(username);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotSupportedException();
        }

        public override bool RoleExists(string roleName)
        {
            return _roles.ContainsKey(roleName);
        }
    }
}