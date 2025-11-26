using System.ComponentModel.DataAnnotations;

namespace ServiceA;

public class UploadRequest
{
    [Required]
    public IFormFile File { get; set; }
}
