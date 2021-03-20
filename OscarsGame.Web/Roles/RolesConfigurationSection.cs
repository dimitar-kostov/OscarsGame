using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace OscarsGame.Roles
{
    public class RolesConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("roles")]
        public RoleConfigurationElementCollection Roles
        {
            get
            {
                return (RoleConfigurationElementCollection)this["roles"];
            }
            set
            {
                this["roles"] = value;
            }
        }

        public static RolesConfigurationSection GetConfig()
        {
            return (RolesConfigurationSection)ConfigurationManager.GetSection("WebConfigRoleProvider");
        }
    }
}