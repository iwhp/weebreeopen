namespace WeebreeOpen.Application
{
    using System;
    using System.Linq;
    using WeebreeOpen.Office365Api.Infrastructure.Office365Api;

    public class Office365Service
    {
        public Office365Service(string userName, string password)
        {
            this.Office365UnitOfWork = new Office365UnitOfWork(userName, password);
        }

        public Office365Service(Office365UnitOfWork office365UnitOfWork)
        {
            this.Office365UnitOfWork = office365UnitOfWork;
        }

        public Office365UnitOfWork Office365UnitOfWork { get; private set; }
    }
}
