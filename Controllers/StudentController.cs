using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Student_Management_System.IServices;
using Student_Management_System.Models;
using Student_Management_System.Services;

namespace Student_Management_System.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly ILogService _logService;
        

        public StudentsController(IStudentService studentService, ICourseService courseService, ILogService logService)
        {
            _studentService = studentService;
            
            _logService = logService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var students = await _studentService.GetAllStudentsAsync();
                return View(students);
            }
            catch (Exception ex)
            {
                await _logService.LogAsync("Error", "Error fetching students.", ex.ToString());
                TempData["ErrorMessage"] = "An error occurred while fetching students.";
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> Create()
        {

           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Email,DOB,CourseId")] Student student)
        {
            try
            {
                              
                if (ModelState.IsValid)
                {
                    await _studentService.AddStudentAsync(student);
                    await _logService.LogAsync("Information", $"Student {student.Name} created successfully.");
                    TempData["SuccessMessage"] = "Student created successfully!";
                    return RedirectToAction(nameof(Index));
                }
               
            }
            catch (Exception ex)
            {
                await _logService.LogAsync("Error", "Error while creating student.", ex.ToString());
                TempData["ErrorMessage"] = "An error occurred while creating the student.";
            }

            return View(student);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var student = await _studentService.GetStudentByIdAsync(id.Value);
                if (student == null)
                {
                    return NotFound();
                }
               
                return View(student);
            }
            catch (Exception ex)
            {
                await _logService.LogAsync("Error", "Error fetching student for editing.", ex.ToString());
                TempData["ErrorMessage"] = "An error occurred while fetching the student details.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _studentService.UpdateStudentAsync(student);
                    await _logService.LogAsync("Information", $"Student {student.Name} updated successfully.");
                    TempData["SuccessMessage"] = "Student updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                await _logService.LogAsync("Error", "Error updating student.", ex.ToString());
                TempData["ErrorMessage"] = "An error occurred while updating the student.";
            }

            return View(student);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var course = await _studentService.GetStudentByIdAsync(id);
                if (course == null) return NotFound();
                return View(course);
            }
            catch (Exception ex)
            {
                await _logService.LogAsync("Error", "Error fetching student for deletion.", ex.ToString());
                TempData["ErrorMessage"] = "An error occurred while fetching the student details.";
                return RedirectToAction(nameof(Index));
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _studentService.DeleteStudentAsync(id);
                TempData["SuccessMessage"] = "Student deleted successfully!";
            }
            catch (Exception ex)
            {
                await _logService.LogAsync("Error", "Error deleting student.", ex.ToString());
              
                TempData["ErrorMessage"] = "An error occurred while deleting the student.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
