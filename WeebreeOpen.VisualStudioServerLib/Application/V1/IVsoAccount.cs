namespace WeebreeOpen.VisualStudioServerLib.Application.V1
{
    using System.Threading.Tasks;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Common;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Model;

    public interface IVsoAccount
    {
        Task<Account> GetAccount(string name);

        Task<JsonCollection<Account>> GetAccountList();
    }
}