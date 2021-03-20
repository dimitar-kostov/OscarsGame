using Microsoft.Practices.Unity;
using OscarsGame.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OscarsGame
{
    public class WebContainerManager
    {
        public void RegisterTypes(IUnityContainer container)
        {          
            BusinessContainerManager containerManager = new BusinessContainerManager();
            containerManager.RegisterTypes(container);
        }
    }
}