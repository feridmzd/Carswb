using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplicationCar.DAL;
using WebApplicationCar.Models;

namespace WebApplicationCar.Controllers
{
    public class HomeController : Controller
    {
         
        
        AppDbContext dbcontext;

        public HomeController(AppDbContext dbContext)
        {
            dbcontext = dbContext;
        }

        public IActionResult Index()
        {
            return View(dbcontext.Cars.ToList());
        }

      
    }
}