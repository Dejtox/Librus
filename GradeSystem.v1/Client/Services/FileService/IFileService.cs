namespace GradeSystem.v1.Client.Services.FileService
{
    public interface IFileService
    {
        Task<string> UploadProductImage(MultipartFormDataContent content);
    }
}
