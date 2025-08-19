using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Web.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(200)]
        public string Email { get; set; } = string.Empty;

        [Phone]
        [StringLength(30)]
        public string? Phone { get; set; }

        [StringLength(100)]
        public string? Department { get; set; }

        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; } = DateTime.UtcNow.Date;

        [Range(0, double.MaxValue)]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
    }
}

