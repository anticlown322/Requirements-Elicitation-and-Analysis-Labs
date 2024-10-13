namespace PaymentMicroservice.WebAPI.Models;

/// <summary>
/// Indicates result of the operation and provides message if needed.
/// </summary>
public class ResultModel
{
    /// <summary>
    /// Was request successful and without errors during processing.
    /// </summary>
    public bool IsSuccessful { get; set; }

    /// <summary>
    /// Additional info that helps to understand the result of the operation.
    /// </summary>
    public string? Message { get; set; }
}