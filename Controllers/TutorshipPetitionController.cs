using Microsoft.AspNetCore.Mvc;
using Tutorias.Data;
using Microsoft.EntityFrameworkCore;
using Tutorias.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Tutorias.Controllers
{
    public class TutorshipPetitionController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public TutorshipPetitionController(ApplicationDbContext context,
                                            SignInManager<ApplicationUser> signInManager,
                                            RoleManager<IdentityRole> roleManager,
                                            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tutor = await _userManager.FindByNameAsync(User.Identity.Name);

            var requests = _context.TutorshipPetitions.Where(r => r.TutorID == tutor.Id);

            List<PetitionItem> items = new List<PetitionItem>();

            foreach (var r in requests)
            {
                Student student = _context.Students.Where(s => s.Id.Equals(r.StudentID)).FirstOrDefault();

                var item = (new PetitionItem()
                {
                    PetitionId = r.ID,
                    Message = r.Message,
                    SenderName = student.UserName,
                    SenderEmail = student.Email,
                });

                items.Add(item);
            }

            return View(items);
        }

        public async Task<IActionResult> Create(string id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var tutor = await _context.Tutors.FirstOrDefaultAsync(m => m.Id.Equals(id));

            if (tutor == null)
            {
                return NotFound();
            }

            if (!_signInManager.IsSignedIn(User))
            {
                return BadRequest();
            }

            var student = await _userManager.FindByNameAsync(User.Identity.Name);

            ViewData["studentEmail"] = student.Email;
            ViewData["tutorName"] = tutor.UserName;

            return View(tutor);
        }

        [HttpPost]
        public async Task<IActionResult> PlacePetition(string tutorId, string message)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return BadRequest();
            }

            var student = await _userManager.FindByNameAsync(User.Identity.Name);

            if (!_signInManager.IsSignedIn(User))
            {
                return BadRequest();
            }

            TutorshipPetition request = new TutorshipPetition()
            {
                ID = Guid.NewGuid().ToString(),
                TutorID = tutorId,
                StudentID = student.Id,
                Message = message,
            };

            _context.TutorshipPetitions.Add(request);
            await _context.SaveChangesAsync();

            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> AcceptPetition()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return BadRequest();
            }

            var tutor = await _userManager.FindByNameAsync(User.Identity.Name);

            TutorshipPetition petition = await _context.TutorshipPetitions.FirstOrDefaultAsync(p => p.TutorID.Equals(tutor.Id));

            Tutorship tutorship = new Tutorship()
            {
                ID = Guid.NewGuid().ToString(),
                TutorID = petition.TutorID,
                StudentID = petition.StudentID,
            };

            _context.Tutorships.Add(tutorship);

            _context.SaveChanges();

            RemovePetition(petition.ID);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult RemovePetition(string id)
        {
            TutorshipPetition tutorshipPetition = _context.TutorshipPetitions.FirstOrDefault(t => t.ID.Equals(id));
            _context.TutorshipPetitions.Remove(tutorshipPetition);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
