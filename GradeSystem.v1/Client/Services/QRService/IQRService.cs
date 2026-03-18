namespace GradeSystem.v1.Client.Services.QRService
{
    public interface IQRService
    {
        Task<byte[]> DownloadQrPdfAsync(string qrCode);
    }
}
