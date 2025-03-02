using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Student_Management_System.IServices;
using Student_Management_System.Models;

namespace Student_Management_System.Controllers
{
    public class CourseRegistrationsController : Controller
    {
        private readonly ICourseRegistrationService _registrationService;
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;

        public CourseRegistrationsController(ICourseRegistrationService registrationService,
                                             IStudentService studentService,
                                             ICourseService courseService)
        {
            _registrationService = registrationService;
            _studentService = studentService;
            _courseService = courseService;
        }

        public async Task<IActionResult> Index()
        {
            var registrations = await _registrationService.GetAllRegistrationsAsync();
            return View(registrations);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Students = new SelectList(await _studentService.GetAllStudentsAsync(), "Id", "Name");
            ViewBag.Courses = new SelectList(await _courseService.GetAllCoursesAsync(), "Id", "CourseName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseRegistration registration)
        {
            if (ModelState.IsValid)
            {
                await _registrationService.AddRegistrationAsync(registration);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Students = new SelectList(await _studentService.GetAllStudentsAsync(), "Id", "Name", registration.StudentId);
            ViewBag.Courses = new SelectList(await _courseService.GetAllCoursesAsync(), "Id", "CourseName", registration.CourseId);
            return View(registration);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var registration = await _registrationService.GetRegistrationByIdAsync(id);
            if (registration == null)
            {
                return NotFound();
            }
            return View(registration);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _registrationService.DeleteRegistrationAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
