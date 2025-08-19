using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
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
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;

        [Phone]
        [StringLength(25)]
        public string? Phone { get; set; }

        [DataType(DataType.Date)]
        public DateOnly? HireDate { get; set; }

        [StringLength(100)]
        public string? Department { get; set; }

        [Range(0, 10000000)]
        [DataType(DataType.Currency)]
        public decimal? Salary { get; set; }

        [StringLength(255)]
        public string? Title { get; set; }
    }
}
