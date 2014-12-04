namespace WeebreeOpen.Office365Api.Infrastructure.Office365Api
{
    using System;
    using System.Linq;
    using WeebreeOpen.Office365Api.Infrastructure.Office365Api;

    public class Office365UnitOfWork
    {
        public Office365UnitOfWork(string userName, string password)
        {
            this.Office365Context = new Office365Context(userName, password);
        }

        public Office365Context Office365Context { get; private set; }
    }
}
