using Microsoft.AspNetCore.Mvc;
using Tutorias.Data;
using Microsoft.EntityFrameworkCore;
using Tutorias.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Tutorias.Controllers
{
    public class TutorshipController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public TutorshipController(ApplicationDbContext context,
                                            SignInManager<ApplicationUser> signInManager,
                                            RoleManager<IdentityRole> roleManager,
                                            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return BadRequest();
            }
            var student = await _userManager.FindByNameAsync(User.Identity.Name);

            List<Tutorship> tutorships = _context.Tutorships.Where(t => t.StudentID.Equals(student.Id) && !t.Sent).ToList();
            List<TutorshipItem> items = new List<TutorshipItem>();


            foreach (var tutorship in tutorships)
            {
                Tutor t = _context.Tutors.Where(t => t.Id.Equals(tutorship.TutorID)).FirstOrDefault();

                TutorshipItem item = new TutorshipItem()
                {
                    TutorshipId = tutorship.ID,
                    TutorName = t.UserName,
                };

                items.Add(item);
            }

            return View(items);
        }
        public async Task<IActionResult> Details(string id)
        {
            Tutorship tutorship = _context.Tutorships.FirstOrDefault(t => t.ID.Equals(id));

            Tutor tutor = _context.Tutors.FirstOrDefault(t => t.Id.Equals(tutorship.TutorID));

            var tupleModel = new Tuple<Tutorship, Tutor>(tutorship, tutor);

            return View(tupleModel);
        }

        public async Task<IActionResult> Rate(string Id, float score, string description)
        {
            Tutorship tutorship = _context.Tutorships.FirstOrDefault(t => t.ID.Equals(Id));

            tutorship.ID = Id;
            tutorship.Score = score;
            tutorship.Sent = true;
            tutorship.Description = description;

            _context.Tutorships.Remove(tutorship);

            _context.SaveChanges();

            _context.Tutorships.Add(tutorship);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Remove(string Id)
        {
            Tutorship tutorship = _context.Tutorships.FirstOrDefault(t => t.ID.Equals(Id));
            _context.Tutorships.Remove(tutorship);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }



    }
}
