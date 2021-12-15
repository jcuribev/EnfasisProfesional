using Tutorias.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tutorias.Models;
using System.Linq;
using System.Threading.Tasks;


namespace Tutorias.Controllers
{

    public class TutorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TutorsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Index(string sortOrder, string currentFilter ,string searchString, int? pageNumber)
        {
            //El ordenamiento por defecto de los tutores es en base a su califiación promedio de más alto a más bajo
            ViewData["CurrentSort"] = sortOrder;
            ViewData["ScoreSortParm"] = "AverageScore";
            ViewData["NameSortParm"] = "Name";

            if(searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var tutors = from t in _context.Tutors select t;

            if (!String.IsNullOrEmpty(searchString))
            {
                tutors = tutors.Where(t => t.UserName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "AverageScore":
                    tutors = tutors.OrderByDescending(t => t.AverageScore);
                    break;
                case "Name":
                    tutors = tutors.OrderBy(t => t.UserName);
                    break;
                default:
                    tutors = tutors.OrderByDescending(t => t.AverageScore);
                    break;
            }
            int pageSize = 6;
            return View(await PaginatedList<Tutor>.CreateAsync(tutors.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> Details(string? id)
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

            IEnumerable<TutorCategory> tutorCategories = _context.TutorCategories.Where(c => c.TutorID.Equals(id)).ToArray();
            List<Category> categories = new List<Category>();

            foreach (TutorCategory tutorCategory in tutorCategories)
            {
                categories.Add(_context.Categories.Where(s => s.ID.Equals(tutorCategory.CategoryID)).FirstOrDefault());
            }

            IEnumerable<TutorSubject> tutorSubjects = _context.TutorSubjects.Where(c => c.TutorID.Equals(id)).ToArray();
            List<List<Subject>> subjects = new List<List<Subject>>();

            foreach (TutorSubject tutorSubject in tutorSubjects)
            {
                subjects.Add(_context.Subjects.Where(s => s.ID.Equals(tutorSubject.SubjectID)).ToList());
            }

            var result = subjects.SelectMany(i => i);

            List<Tutorship> tutorships = _context.Tutorships.Where(t => t.TutorID.Equals(tutor.Id) && t.Sent).ToList();

            List<TutorshipItem> items = new List<TutorshipItem>();

            foreach (Tutorship tutorship in tutorships)
            {
                Student student = _context.Students.Where(s => s.Id.Equals(tutorship.StudentID)).FirstOrDefault();

                TutorshipItem item = new TutorshipItem()
                {
                    StudentName = student.UserName,
                    Description = tutorship.Description,
                    Score = tutorship.Score,
                };

                items.Add(item);
            }

            var tupleModel = new Tuple<Tutor, IEnumerable<Category>, IEnumerable<Subject>, IEnumerable<TutorshipItem>>(tutor, categories, result, items);

            return View(tupleModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Route("Tutors/Create")]
        public async Task<IActionResult> Create([Bind("Name", "Email", "Description")] Tutor tutor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(tutor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View();
        }

        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutor = await _context.Tutors.FindAsync(id);
            if (tutor == null)
            {
                return NotFound();
            }
            return View(tutor);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutorToUpdate = await _context.Tutors.FirstOrDefaultAsync(t => t.Id.Equals(id));
            if (await TryUpdateModelAsync<Tutor>(tutorToUpdate, "", s => s.UserName, s => s.Email, s => s.Description))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(tutorToUpdate);
        }

        public async Task<IActionResult> Delete(string? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutor = await _context.Tutors.AsNoTracking().FirstOrDefaultAsync(m => m.Id.Equals(id));

            if (tutor == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(tutor);
        }

        // POST: tutors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tutor = await _context.Tutors.FindAsync(id);
            if (tutor == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Tutors.Remove(tutor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }
    }
}