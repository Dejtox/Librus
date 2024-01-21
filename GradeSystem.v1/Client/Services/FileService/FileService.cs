using Syncfusion.Blazor.Notifications.Internal;
using System.Text.Json;

namespace GradeSystem.v1.Client.Services.FileService
{

    public class FileService : IFileService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;

        public async Task<Stream> Getimage(string Filename)
        {
            var response = await _client.GetAsync($"api/upload/{Filename}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("File Not Find");
            }
            else
            {     
                var file =  response.Content.ReadAsStream();
                return file;
            }

        }
        public FileService(HttpClient client)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async Task<string> UploadProductImage(MultipartFormDataContent content)
        {
            var postResult = await _client.PostAsync("api/upload", content);
            var postContent = await postResult.Content.ReadAsStringAsync();
            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
            else
            {
                var imgUrl = Path.Combine("https://localhost:44338/", postContent);
                return imgUrl;
            }
        }
    }
    
}

