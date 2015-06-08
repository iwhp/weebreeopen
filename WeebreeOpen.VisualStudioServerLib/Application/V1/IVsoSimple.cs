namespace WeebreeOpen.VisualStudioServerLib.Application.V1
{
    using System.Threading.Tasks;

    public interface IVsoSimple
    {
        Task<T> Get<T>(string url);
    }
}