namespace RestaurantSystem.Services.Contacts
{
    using System.Threading.Tasks;

    using RestaurantSystem.Web.ViewModels.Contacts;

    public interface IContactService
    {
        Task SendMessageAsync(MessageInputVewModel messageInput, string sender);
    }
}
