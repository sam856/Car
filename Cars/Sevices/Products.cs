using Cars.Interfaces;
using Cars.Models;
using Cars.NewFolder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cars.Sevices
{
    public class Products :IProducts
    {
        private readonly AppDbContext dbContext;
        public Products(AppDbContext dbContext)
        {
            this.dbContext= dbContext;
        }

        public List<Company> GetAll()
        {
           

            List<Company> companies = dbContext.Company
        .Include(company => company.Products).ThenInclude(xa => xa.Images)
        .ToList();


            return companies;
        }



        public Products GetbyId(int id )
        {

            Products Car = dbContext.products.Include(Image => Image.Images)
                .FirstOrDefault(Id => Id.Id == id);

            return Car;


        }
    }
}
