using Cars.Interfaces;
using Cars.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [HttpPost("AddTofavourite")]
        [Authorize(Roles = "UserRole,CompanyRole")]
        public async Task<IActionResult> AddTofavourite([FromForm]int id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            products.AddToFavourite(id, userId);
            return Ok("Added Successfully");
        }
        [HttpGet("GetProductByid")]
        public async Task<IActionResult> GetProductByid( int id)
        {
           return Ok( products.GetbyId(id));
        }
        [HttpPost("RemoveFtomFavourite")]
        [Authorize(Roles = "UserRole,CompanyRole")]
        public async Task<IActionResult> RemoveTofavourite(int id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            products.RemoveFromFavourite(id,userId);
            return Ok("Removed Successfully");
        }




    }
}
