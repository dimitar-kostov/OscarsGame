using System;

namespace OscarsGame.Domain.Entities
{
    public class ExternalLogin
    {
        private User _user;

        #region Scalar Properties
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public Guid UserId { get; set; }
        #endregion

        #region Navigation Properties
        public virtual User User
        {
            get { return _user; }
            set
            {
                _user = value;
                if (value != null)
                {
                    UserId = value.UserId;
                }
            }
        }
        #endregion
    }
}