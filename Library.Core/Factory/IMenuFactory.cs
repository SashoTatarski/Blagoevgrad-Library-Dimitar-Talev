namespace Library.Core.Factory
{
    public interface IMenuFactory
    {
        void CheckAuthenticationForCommand(string commandAsString);
        string GenerateMenu(Models.Contracts.IAccount account);
    }
}