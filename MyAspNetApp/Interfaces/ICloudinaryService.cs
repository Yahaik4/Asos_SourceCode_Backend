public interface ICloudinaryService
{
    Task<string> UploadImageAsync(Stream fileStream, string fileName);
}
