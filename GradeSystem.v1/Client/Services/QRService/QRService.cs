using System.Net.Http;
using System.Threading.Tasks;

namespace GradeSystem.v1.Client.Services.QRService
{
    public class QRService : IQRService
    {
        private readonly HttpClient _httpClient;

        public QRService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<byte[]> DownloadQrPdfAsync(string qrCode)
        {
            var response = await _httpClient.GetAsync($"api/qr/pdf/{qrCode}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Unable to download QR PDF.");
            }

            return await response.Content.ReadAsByteArrayAsync(); // zwracamy bajty PDF-a
        }
    }
}
