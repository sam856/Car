using Cars.Models;

namespace Cars.Interfaces
{
    public interface IProducts
    {
        List<Company> GetAll();
        Products GetbyId(int id);
        void AddToFavourite(int ProductId, string UserId);
        void RemoveFromFavourite(int ProductId, string UserId);

    }
}
