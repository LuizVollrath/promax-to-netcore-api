namespace Promax.NetCore.Infra.Database.Context
{
    public interface IPromaxContext : ICommonContext
    {
        int GetCurrentUser();
    }
}
