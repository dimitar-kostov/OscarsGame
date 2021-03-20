using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace OscarsGame.Roles
{
    public class RoleConfigurationElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new RoleConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var roleConfigurationElement = (RoleConfigurationElement)element;
            return string.Format("{0}_{1}", roleConfigurationElement.Name, roleConfigurationElement.User);
        }
    }
}