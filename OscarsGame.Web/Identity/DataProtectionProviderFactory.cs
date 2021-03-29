using Microsoft.Owin.Security.DataProtection;
using System;

namespace OscarsGame.Web.Identity
{
    public class DataProtectionProviderFactory : IDisposable
    {
        public IDataProtectionProvider DataProtectionProvider { get; private set; }

        public DataProtectionProviderFactory(IDataProtectionProvider dataProtectionProvider)
        {
            DataProtectionProvider = dataProtectionProvider;
        }
        public void Dispose()
        {
        }
    }
}