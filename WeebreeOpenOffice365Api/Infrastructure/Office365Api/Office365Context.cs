namespace WeebreeOpen.Office365Api.Infrastructure.Office365Api
{
    using System;
    using System.Linq;
    using WeebreeOpen.Office365Api.Infrastructure.Office365Api.Repository;

    public class Office365Context
    {
        public Office365Context(string userName, string password)
        {
            this.UserName = userName;
            this.Password = password;
        }

        public string UserName { get; private set; }

        public string Password { get; private set; }

        public MailRepository MailRepository
        {
            get
            {
                if (this.mailRepository == null)
                {
                    this.mailRepository = new MailRepository(this);
                }
                return this.mailRepository;
            }
        }
        private MailRepository mailRepository;
    }
}
