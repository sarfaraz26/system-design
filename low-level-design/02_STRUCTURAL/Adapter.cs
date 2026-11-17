// The Adapter pattern in C# is a structural design pattern 
// that allows objects with incompatible interfaces to work together.

public interface IFileStorageProvider
{
    Task UploadAysnc(string fileName, byte[] fileContent);
}

public class CloudBoxProvider
{
    public async Task SaveDocumentAsync(string documentName, Stream documentStream)
    {
        Console.WriteLine($"Document {documentName} uploaded successfully.");
        await Task.CompletedTask;
    }
}

public class CloudBoxProviderAdapter : IFileStorageProvider
{
    private readonly CloudBoxProvider _cloudBoxProvider;

    public CloudBoxProviderAdapter(CloudBoxProvider cloudBoxProvider)
    {
        _cloudBoxProvider = cloudBoxProvider;
    }

    public async Task UploadAysnc(string fileName, byte[] fileContent)
    {
        Stream documentInStream = new MemoryStream(fileContent);
        await _cloudBoxProvider.SaveDocumentAsync(fileName, documentInStream);
    }
}


public class FileUploadService
{
    public async Task UploadFile(IFormFile file)
    {
        //Upload file logic
    }
}


[ApiController]
[Route("api/[controller]")] 
public class FileUploadController : ControllerBase
{
    private readonly ILogger<FileUploadController> _logger;
    private readonly FileUploadService _service;

    public FileUploadController(ILogger<FileUploadController> logger, FileUploadService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpPost]
    [Route("upload-items")]
    public async Task<IActionResult> UploadItems(IFormFile file)
    {
        try
        {
            await _service.UploadFile(file);
            return Ok(new { Id = Guid.NewGuid(), FileName = file.FileName });
        }
        catch(Exception ex)
        {
            _logger.loggError("Exception occured : {message} at {stackTrace}", ex.Message, ex.StackTrace);

            return StatusCode(StatusCode.Status500InternalServerError, new {
                Error = "Something went wrong",
                Details = "ex.Message";
            })
        }
    }
}

//Program.cs
builder.Services.AddScoped<FileUploadService>();
builder.Services.AddScoped<IFileStorageProvider, CloudBoxProviderAdapter>();