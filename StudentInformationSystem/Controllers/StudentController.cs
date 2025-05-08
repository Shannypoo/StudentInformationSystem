using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using StudentInformationSystem.Models;

namespace StudentInformationSystem.Controllers
{
	public class StudentController : Controller
	{
		//List
		public static List<StudentClass> students = new List<StudentClass>();
		public IActionResult Index()
		{

			return View(students);
		}
		public IActionResult Create() 
		{
			return View();
		}
		[HttpPost]
		//Add Students
		public IActionResult Create(StudentClass student) 
		{

			if (ModelState.IsValid)
			{
				student.StudentID = students.Count + 1;
				students.Add(student);
				return RedirectToAction("Index");

			}
			return View(student);
		}
		//Edit Students
		public IActionResult Edit(int id)
		{ 
			var student = students.FirstOrDefault(p => p.StudentID == id);
			if (student == null)
				return NotFound();
			return View(student);
		
		}
		[HttpPost]
		public IActionResult Edit(int id, StudentClass student) 
		{
			if (id != student.StudentID)
				return NotFound();

			if (ModelState.IsValid) 
			{ 
					var existingStudent = students.FirstOrDefault(p => p.StudentID == id);
				if (existingStudent == null)
					return NotFound();

				existingStudent.Name = student.Name;
				existingStudent.Age = student.Age;
				existingStudent.Course = student.Course;

				return RedirectToAction("Index");
			}
			return View(student);
	
		}
		//Delete
		public IActionResult Delete(int id) 
		{
			var student = students.FirstOrDefault(p => p.StudentID == id);
			if(student == null)
				return NotFound();
			return View(student);
		}
		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteConfirmed(int id) 
		{
			var student = students.FirstOrDefault(p => p.StudentID == id);
			if (student != null) 
			{
				students.Remove(student);
			
			}
			return RedirectToAction("Index");
		}
	}
}
