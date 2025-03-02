using Microsoft.AspNetCore.Mvc;
using Student_Management_System.IServices;
using Student_Management_System.Models;

namespace Student_Management_System.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ILogger<CoursesController> _logger;

        public CoursesController(ICourseService courseService,ILogger<CoursesController> logger)
        {
            _courseService = courseService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try 
            {
                var courses = await _courseService.GetAllCoursesAsync();
                return View(courses);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error fetching Courses.");
                TempData["ErrorMessage"] = "An error occurred while fetching Courses.";
                return RedirectToAction("Error", "Home");
            }
          
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            try 
            {
                if (ModelState.IsValid)
                {
                    await _courseService.AddCourseAsync(course);
                    TempData["SuccessMessage"] = "Course created successfully!";
                    _logger.Log(LogLevel.Information, "Course created successfully!", course);
                    return RedirectToAction(nameof(Index));
                    
                }
               
            } 

            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error creating course.");
                TempData["ErrorMessage"] = "An error occurred while creating the course.";
            }
            return View(course);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try 
            {
                
                var course = await _courseService.GetCourseByIdAsync(id);
                if (course == null) return NotFound();
                return View(course);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error fetching course for editing.");
                TempData["ErrorMessage"] = "An error occurred while fetching the course details.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Course course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _courseService.UpdateCourseAsync(course);
                    TempData["SuccessMessage"] = "Course updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
               
            }
            catch (Exception ex) 

            {
                _logger.LogError(ex, "Course updating student.");
                TempData["ErrorMessage"] = "An error occurred while updating the course.";
            }

            return View(course);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try 
            {
                var course = await _courseService.GetCourseByIdAsync(id);
                if (course == null) return NotFound();
                return View(course);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching course for deletion.");
                TempData["ErrorMessage"] = "An error occurred while fetching the course details.";
                return RedirectToAction(nameof(Index));
            }
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _courseService.DeleteCourseAsync(id);
                TempData["SuccessMessage"] = "Course deleted successfully!";
            }
           catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting course.");
                TempData["ErrorMessage"] = "An error occurred while deleting the course.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
