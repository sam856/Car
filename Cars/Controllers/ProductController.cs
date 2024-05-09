using Cars.Interfaces;
using Cars.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cars.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProducts products;
        public ProductController(IProducts products)
        {
            this.products = products;
        }
        [HttpGet("Companies")]      
         public async Task<IActionResult> AllCompanies()
        {
            List<Company> companies = products.GetAll();
            return Ok(companies);
        }

        
    }
}
