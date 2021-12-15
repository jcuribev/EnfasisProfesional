using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Tutorias.Models;
using Tutorias.Data;
using Microsoft.AspNetCore.Identity;
using NETCore.MailKit.Core;
using System;
using System.IO;

namespace Tutorias.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Tutor> _tutorManager;
        private readonly UserManager<Student> _studentManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HomeController(ILogger<HomeController> logger,
                                ApplicationDbContext dbContext,
                                UserManager<Tutor> tutorManager,
                                UserManager<Student> studentManager,
                                SignInManager<ApplicationUser> signInManager,
                                IEmailService emailService,
                                RoleManager<IdentityRole> roleManager,
                                UserManager<ApplicationUser> userManager,
                                IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _context = dbContext;
            _signInManager = signInManager;
            _emailService = emailService;
            _roleManager = roleManager;
            _tutorManager = tutorManager;
            _studentManager = studentManager;
            _userManager = userManager;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string Email, string Password)
        {
            var user = await _userManager.FindByEmailAsync(Email);

            if (user != null)
            {
                var SignInResult = await _signInManager.PasswordSignInAsync(user, Password, true, false);

                if (SignInResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return BadRequest();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult RegisterTutor()
        {
            return View();
        }
        public IActionResult RegisterStudent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterTutor(string Username,
                                                       string Email,
                                                       string Password,
                                                       string Description,
                                                       string PhoneNumber,
                                                       string? Twitter,
                                                       string? Facebook,
                                                       string? Instagram)
        {
            string Id = Guid.NewGuid().ToString();

            var files = HttpContext.Request.Form.Files;

            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = "picture";
            string path = "";

            if (files.Count > 0)
            {
                foreach (var file in files)
                {
                    string extension = Path.GetExtension(file.FileName);
                    fileName = fileName + extension;
                    path = Path.Combine("/Images/profile_pictures/" + Id + "/");

                    Directory.CreateDirectory(wwwRootPath + path);

                    path = path + fileName;

                    using (var fileStream = new FileStream(wwwRootPath + path, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }

            ApplicationUser user = new Tutor()
            {
                Id = Id,
                UserName = Username,
                Email = Email,
                Description = Description,
                PhoneNumber = PhoneNumber,
                TwitterLink = Twitter,
                FacebookLink = Facebook,
                InstagramLink = Instagram,
                ImagePath = path
            };

            var result = await _userManager.CreateAsync(user, Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Tutor");

                return RedirectToAction("RegisterCategories", new { id = user.Id });
            }

            return View();
        }

        [HttpGet]
        public IActionResult RegisterCategories(string id)
        {
            IEnumerable<Category> categories = _context.Categories.ToList();

            IEnumerable<CheckboxItem> items = categories.Select(c => new CheckboxItem()
            {
                Id = c.ID,
                Name = c.Name,
                IsChecked = false,
            }).ToList();

            var tupleModel = new Tuple<string, IEnumerable<CheckboxItem>>(id, items);

            return View(tupleModel);
        }

        [HttpPost]
        public async Task<IActionResult> SaveCategories(string id, List<string> checkBoxes)
        {

            foreach (var checkBox in checkBoxes)
            {
                TutorCategory tutorCategory = new TutorCategory
                {
                    ID = Guid.NewGuid().ToString(),
                    TutorID = id,
                    CategoryID = checkBox,
                };

                if (ModelState.IsValid)
                {
                    _context.Add(tutorCategory);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction("RegisterSubjects", new { id = id });
        }

        [HttpGet]
        public IActionResult RegisterSubjects(string id)
        {
            IEnumerable<TutorCategory> tutorCategories = _context.TutorCategories.Where(c => c.TutorID.Equals(id)).ToArray();
            List<Category> categories = new List<Category>();

            foreach (TutorCategory tutorCategory in tutorCategories)
            {
                categories.Add(_context.Categories.Where(s => s.ID.Equals(tutorCategory.CategoryID)).FirstOrDefault());
            }

            List<List<Subject>> subjects = new List<List<Subject>>();

            foreach (Category category in categories)
            {
                subjects.Add(_context.Subjects.Where(s => s.CategoryName.Equals(category.Name)).ToList());
            }

            var result = subjects.SelectMany(i => i);

            IEnumerable<CheckboxItem> items = result.Select(c => new CheckboxItem()
            {
                Id = c.ID,
                Name = c.Name,
                IsChecked = false,
            }).ToList();

            var tupleModel = new Tuple<string, IEnumerable<CheckboxItem>>(id, items);

            return View(tupleModel);
        }

        [HttpPost]
        public async Task<IActionResult> SaveSubjects(string id, List<string> checkBoxes)
        {
            foreach (var checkBox in checkBoxes)
            {
                TutorSubject tutorSubject = new TutorSubject()
                {
                    ID = Guid.NewGuid().ToString(),
                    TutorID = id,
                    SubjectID = checkBox,
                };

                if (ModelState.IsValid)
                {
                    _context.Add(tutorSubject);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction("FinishRegistration", new { id = id });
        }

        [HttpPost]
        public async Task<IActionResult> RegisterStudent(string Username, string Email, string Password)
        {
            ApplicationUser user = new Student
            {
                Id = Guid.NewGuid().ToString(),
                UserName = Username,
                Email = Email,
            };

            var result = await _userManager.CreateAsync(user, Password);

            if (result.Succeeded)
            {

                await _userManager.AddToRoleAsync(user, "Student");

                return RedirectToAction("FinishRegistration", new { id = user.Id });
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> FinishRegistration(string id)
        {
            ApplicationUser user = _context.AppUsers.Find(id);

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var link = Url.Action(nameof(VerifyEmail), "Home", new { userId = user.Id, code }, Request.Scheme, Request.Host.ToString());

            await _emailService.SendAsync(user.Email, "Verifica tu correo", $"<a href=\"{link}\">Verificar Email<a>", true);

            return RedirectToAction("EmailVerification");
        }

        public async Task<IActionResult> VerifyEmail(string UserId, string Code)
        {
            var user = await _userManager.FindByIdAsync(UserId);

            if (user == null)
            {
                return BadRequest();
            }

            var result = await _userManager.ConfirmEmailAsync(user, Code);

            if (result.Succeeded)
            {
                return View();
            }

            return BadRequest();
        }

        public IActionResult EmailVerification()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}