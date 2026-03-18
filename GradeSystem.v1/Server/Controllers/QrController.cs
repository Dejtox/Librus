using Microsoft.AspNetCore.Mvc;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace YourApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QrController : ControllerBase
    {
        [HttpGet("pdf/{qrCode}")]
        public IActionResult GenerateQrPdf(string qrCode)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            var fullUrl = $"{baseUrl}/LendBook/{qrCode}";

            // ✅ 1. Generowanie QR jako Bitmapa
            using var qrGenerator = new QRCodeGenerator();
            using var qrData = qrGenerator.CreateQrCode(fullUrl, QRCodeGenerator.ECCLevel.Q);
            using var qrCodeImg = new QRCode(qrData).GetGraphic(20);
            using var stream = new MemoryStream();
            qrCodeImg.Save(stream, ImageFormat.Png);
            var qrImageBytes = stream.ToArray();

            // ✅ 2. Tworzenie PDF
            using var doc = new PdfDocument();
            var page = doc.AddPage();
            var gfx = XGraphics.FromPdfPage(page);

            var font = new XFont("Arial", 14, XFontStyle.Bold);
            gfx.DrawString("Book QR Code", font, XBrushes.Black,
                new XRect(0, 20, page.Width, 20), XStringFormats.TopCenter);

            // Wstawiamy QR kod jako obraz
            using var qrImgStream = new MemoryStream(qrImageBytes);
            var xImage = XImage.FromStream(() => qrImgStream);
            gfx.DrawImage(xImage, (page.Width - 150) / 2, 60, 150, 150);

            // Dodajemy link jako tekst
            var smallFont = new XFont("Arial", 10, XFontStyle.Regular);
            gfx.DrawString(fullUrl, smallFont, XBrushes.Black,
                new XRect(40, 220, page.Width - 80, 60), XStringFormats.TopCenter);

            // ✅ 3. Zapisujemy PDF do pamięci
            using var output = new MemoryStream();
            doc.Save(output, false);
            var pdfBytes = output.ToArray();

            return File(pdfBytes, "application/pdf", $"QRCode_{qrCode}.pdf");
        }
    }
}
