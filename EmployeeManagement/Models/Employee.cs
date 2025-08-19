using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
	public class Employee
	{
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string FullName { get; set; } = string.Empty;

		[Required]
		[StringLength(100)]
		public string Department { get; set; } = string.Empty;

		[Range(0, double.MaxValue)]
		public decimal Salary { get; set; }

		[DataType(DataType.Date)]
		public DateTime HireDate { get; set; } = DateTime.UtcNow.Date;
	}
}
