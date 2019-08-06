namespace Library.Core.Factory
{
    public interface IMenuFactory
    {
        string GenerateMenu(Models.Contracts.IAccount account);
    }
}