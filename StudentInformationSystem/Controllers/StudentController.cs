using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentInformationSystem.Models;

namespace StudentInformationSystem.Controllers
{
	public class StudentController : Controller
	{
		private readonly AppDbContext _context;
		public StudentController(AppDbContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			// Get all students from DB and pass to view
			var students = await _context.Students.ToListAsync();
			return View(students);
		}

		[HttpGet]
		public IActionResult RegisterStudent()
		{
			// Show empty registration form
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> RegisterStudent(StudentClass student)
		{
			if (ModelState.IsValid)
			{
				// Add student to DB and save
				_context.Add(student);
				await _context.SaveChangesAsync();

				// Redirect to student list after successful registration
				return RedirectToAction(nameof(Index));
			}
			// If validation fails, show form again with validation messages
			return View(student);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
				return NotFound();

			// Find student by id
			var student = await _context.Students.FindAsync(id);
			if (student == null)
				return NotFound();

			// Show form with student data to edit
			return View(student);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, StudentClass student)
		{
			if (id != student.StudentID)
				return NotFound();

			if (ModelState.IsValid)
			{
				try
				{
					// Update student data in DB
					_context.Update(student);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!StudentExists(student.StudentID))
						return NotFound();
					else
						throw;
				}
				// Redirect to list after edit success
				return RedirectToAction(nameof(Index));
			}
			// If validation fails, show form again
			return View(student);
		}

		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
				return NotFound();

			// Get student to confirm delete
			var student = await _context.Students.FirstOrDefaultAsync(m => m.StudentID == id);
			if (student == null)
				return NotFound();

			// Show delete confirmation page
			return View(student);
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var student = await _context.Students.FindAsync(id);

			if (student != null)
			{
				_context.Students.Remove(student);
				await _context.SaveChangesAsync();
			}

			// Redirect to list after delete
			return RedirectToAction(nameof(Index));
		}
		private bool StudentExists(int id)
		{
			return _context.Students.Any(e => e.StudentID == id);
		}
	}
}
