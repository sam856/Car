using Cars.Models;

namespace Cars.Interfaces
{
    public interface IProducts
    {
        List<Company> GetAll();
        Products GetbyId(int id);
    }
}
