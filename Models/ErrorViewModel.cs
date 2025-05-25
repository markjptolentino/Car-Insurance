// Models/ErrorViewModel.cs
namespace CarInsurance.Models
{
    // Model for error view data
    public class ErrorViewModel
    {
        // Property to store the request ID
        public string? RequestId { get; set; }

        // Property to store the error message
        public string? ErrorMessage { get; set; }

        // Property to indicate if RequestId is available
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}