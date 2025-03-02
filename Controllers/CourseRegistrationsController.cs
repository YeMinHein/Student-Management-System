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
        private readonly ILogService _logService;

        public CourseRegistrationsController(ICourseRegistrationService registrationService,
                                             IStudentService studentService,
                                             ICourseService courseService,
                                             ILogService logService)
        {
            _registrationService = registrationService;
            _studentService = studentService;
            _courseService = courseService;
            _logService = logService;
        }

        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate, int? studentId, int? courseId)
        {
            try
            {
                var registrations = await _registrationService.GetAllRegistrationsAsync();
                if (startDate.HasValue)
                {
                    registrations = registrations.Where(r => r.CreatedDate >= startDate.Value).ToList();
                }

                if (endDate.HasValue)
                {
                    registrations = registrations.Where(r => r.CreatedDate <= endDate.Value).ToList();
                }

                if (studentId.HasValue && studentId != 0)
                {
                    registrations = registrations.Where(r => r.StudentId == studentId.Value).ToList();
                }

                if (courseId.HasValue && courseId != 0)
                {
                    registrations = registrations.Where(r => r.CourseId == courseId.Value).ToList();
                }

                ViewBag.Students = new SelectList(await _studentService.GetAllStudentsAsync(), "Id", "Name", studentId);
                ViewBag.Courses = new SelectList(await _courseService.GetAllCoursesAsync(), "Id", "CourseName", courseId);

                return View(registrations);
            }
            catch (Exception ex)
            {
                await _logService.LogAsync("Error", "\"Error fetching registrations.", ex.ToString());
                TempData["ErrorMessage"] = "An  error occurred while fetching registrations.";
                return RedirectToAction("Error", "Home");
            } 
        }


        // GET: CourseRegistrations/Create
        public async Task<IActionResult> Create()
        {
            try
            {
                ViewBag.Students = new SelectList(await _studentService.GetAllStudentsAsync(), "Id", "Name");
                ViewBag.Courses = new SelectList(await _courseService.GetAllCoursesAsync(), "Id", "CourseName");
                return View();
            }
            catch (Exception ex)
            {
                // Log the exception
                await _logService.LogAsync("Error","Error fetching data for Create.",ex.ToString());
                TempData["ErrorMessage"] = "An error occurred while loading data for registration.";
                return RedirectToAction("Error", "Home");
                
            }
        }

        // POST: CourseRegistrations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseRegistration registration)
        {
            
                try
                {
                    await _registrationService.AddRegistrationAsync(registration);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Log the error
                    await _logService.LogAsync("Error","Error adding registration.",ex.ToString());
                    TempData["ErrorMessage"] = "Error adding registration";
                    return RedirectToAction("Error", "Home");
                }
            

            // In case of ModelState is not valid, return the View with the current model
            ViewBag.Students = new SelectList(await _studentService.GetAllStudentsAsync(), "Id", "Name", registration.StudentId);
            ViewBag.Courses = new SelectList(await _courseService.GetAllCoursesAsync(), "Id", "CourseName", registration.CourseId);
            return View(registration);
        }

        // GET: CourseRegistrations/Delete
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var registration = await _registrationService.GetRegistrationByIdAsync(id);
                if (registration == null)
                {
                    return NotFound();
                }
                return View(registration);
            }
            catch (Exception ex)
            {
                // Log the error
                await _logService.LogAsync("Error", "Error fetching registration for delete",ex.ToString());
                TempData["ErrorMessage"] = "An error occurred while fetching the registration details.";
                return RedirectToAction("Error", "Home");
   
            }
        }

        // POST: CourseRegistrations/DeleteConfirmed
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _registrationService.DeleteRegistrationAsync(id);
                TempData["SuccessMessage"] = "Registration deleted successfully!";
            }
            catch (Exception ex)
            {
                await _logService.LogAsync("Error", "Error deleting registration.", ex.ToString());

                TempData["ErrorMessage"] = "An error occurred while deleting the registration.";
            }

            return RedirectToAction(nameof(Index));
        }

    }
    }

