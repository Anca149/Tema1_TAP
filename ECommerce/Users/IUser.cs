namespace ECommerce.Users
{
    public interface IUser
    {
        string Name { get; }
        void ShowMenu();
    }
}