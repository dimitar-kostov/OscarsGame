using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace OscarsGame.Roles
{
    public class RoleConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("name")]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("user")]
        public string User
        {
            get
            {
                return (string)this["user"];
            }
            set
            {
                this["user"] = value;
            }
        }
    }
}