namespace RestaurantSystem.Services.Seeder
{
    using System.Threading.Tasks;

    public interface IDataSeeder
    {
        Task AddRestaurantAsync();

        bool CheckDataBase();
    }
}
