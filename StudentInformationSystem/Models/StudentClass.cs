using System.ComponentModel.DataAnnotations;

namespace StudentInformationSystem.Models
{
	public class StudentClass
	{
		[Key]
		public int StudentID { get; set; } //Primary Key
		public string FullName { get; set; }
		public string DateOfBirth { get; set; }
		public string Gender { get; set; }
		public string Address { get; set; }
		public string ContactNumber { get; set; }
	}
}
