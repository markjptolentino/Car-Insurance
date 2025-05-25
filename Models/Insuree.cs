// Models/Insuree.cs
using System;
using System.ComponentModel.DataAnnotations;

// Namespace for the MVC application
namespace CarInsurance.Models
{
    // Model representing an Insuree entity
    public class Insuree
    {
        // Unique identifier for the insuree
        [Key]
        public int Id { get; set; }

        // Insuree's first name
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string? FirstName { get; set; }

        // Insuree's last name
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string? LastName { get; set; }

        // Insuree's email address
        [Required(ErrorMessage = "Email address is required.")]
        [StringLength(50, ErrorMessage = "Email address cannot exceed 50 characters.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? EmailAddress { get; set; }

        // Insuree's date of birth
        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }

        // Car year
        [Required(ErrorMessage = "Car year is required.")]
        [Range(1900, 2026, ErrorMessage = "Car year must be between 1900 and 2026.")]
        public int CarYear { get; set; }

        // Car make
        [Required(ErrorMessage = "Car make is required.")]
        [StringLength(50, ErrorMessage = "Car make cannot exceed 50 characters.")]
        public string? CarMake { get; set; }

        // Car model
        [Required(ErrorMessage = "Car model is required.")]
        [StringLength(50, ErrorMessage = "Car model cannot exceed 50 characters.")]
        public string? CarModel { get; set; }

        // DUI history
        [Required(ErrorMessage = "DUI status is required.")]
        public bool DUI { get; set; }

        // Number of speeding tickets
        [Required(ErrorMessage = "Speeding tickets count is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Speeding tickets cannot be negative.")]
        public int SpeedingTickets { get; set; }

        // Coverage type (true = Full, false = Liability)
        [Required(ErrorMessage = "Coverage type is required.")]
        public bool CoverageType { get; set; }

        // Insurance quote amount
        [Required(ErrorMessage = "Quote is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Quote cannot be negative.")]
        public decimal Quote { get; set; }
    }
}