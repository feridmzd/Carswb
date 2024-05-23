using Microsoft.AspNetCore.Mvc;
using WebApplicationCar.DAL;
using WebApplicationCar.Models;
using WebApplicationCar.ViewModel;

namespace WebApplicationCar.Areas.Manage.Controllers
{

    [Area("Manage")]
    public class CarsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public CarsController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View(_context.Cars.ToList());
        }


        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateCarVm carvm)


        {
            if (!carvm.ImgFile.ContentType.Contains("image/"))
            {

                ModelState.AddModelError("ImgFile", "Sekil sehvdir");
            }

            string path = _environment.WebRootPath + @"\Upload\";
            string filename = Guid.NewGuid() + carvm.ImgFile.FileName;
            using (FileStream filestream = new FileStream(path + filename, FileMode.Create))

            {

                carvm.ImgFile.CopyTo(filestream);




            }
            if (!ModelState.IsValid)

            {
                return View();

            }

            Cars car = new Cars()
            {
                Name = carvm.Name,
                Description = carvm.Description,
                ImgUrl = filename,



            };
            
            _context.Add(car);
            _context.SaveChanges();
            return RedirectToAction("Index");



        }

      
        public IActionResult Delete(int id)
        {
            var cars = _context.Cars.FirstOrDefault(x => x.Id == id);

            if (cars != null)
            {

                string path = _environment.WebRootPath + @"\Upload\" + cars.ImgUrl;
                FileInfo fileInfo = new FileInfo(path);


                if (fileInfo.Exists)
                {
                    fileInfo.Delete();

                }
                _context.Remove(cars);
                _context.SaveChanges();




            return RedirectToAction("Index");
            }
            return View();

        }





        public IActionResult Update(int id)
        {
            Cars cars = _context.Cars.FirstOrDefault(c => c.Id == id);

            UpdateCarVm updateCar = new UpdateCarVm()
            {
                Id = cars.Id,
                Name = cars.Name,
                Description = cars.Description,
                ImgUrl = cars.ImgUrl,



            };




            if (cars == null)
            {
                return RedirectToAction("Index");
            }

            return View(updateCar);
        }






        [HttpPost]
        public IActionResult Update(UpdateCarVm teamsVm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }






            var oldteam = _context.Cars.FirstOrDefault(x => x.Id == teamsVm.Id);


           

            if (!teamsVm.ImgFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImgFile", "Sekil elave edile bilmedi");

            }

            string path = _environment.WebRootPath + @"\Upload\";
            string filename = Guid.NewGuid() + teamsVm.ImgFile.FileName;


            using (FileStream stream = new FileStream(path + filename, FileMode.Create))
            {
                teamsVm.ImgFile.CopyTo(stream);
            }

            teamsVm.ImgUrl= filename;

            if (oldteam.ImgUrl != null)
            {
                path = _environment.WebRootPath + @"\Upload\" + oldteam.ImgUrl;
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }

            }

            if (oldteam == null) { return RedirectToAction("Index"); }
            {

                oldteam.Name = teamsVm.Name;
                oldteam.Description = teamsVm.Description;

                oldteam.ImgUrl = teamsVm.ImgUrl;



            }

            _context.SaveChanges();
        
            return RedirectToAction("Index");

        }
    }
} 


