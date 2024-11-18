using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExportarDocumento.Controllers
{
    public class ExportarController : Controller
    {
        public IActionResult IndexPdf()
        {
            try
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = @"C:\Users\CENSI USMP\AppData\Local\Programs\Python\Python312\python.exe", // Ruta exacta del ejecutable de Python
                    Arguments = "generate_pdf.py", // Nombre del script Python
                    WorkingDirectory = @"C:\Users\CENSI USMP\Desktop\ExportarDocumento\ExportarDocumento", // Ruta del script
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                var process = Process.Start(startInfo);
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    var error = process.StandardError.ReadToEnd();
                    throw new Exception($"Error al ejecutar el script: {error}");
                }

                var pdfFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pdf", "output.pdf");
                if (!System.IO.File.Exists(pdfFilePath))
                {
                    return Content("El archivo PDF no fue generado.");
                }

                return File(System.IO.File.ReadAllBytes(pdfFilePath), "application/pdf", "reporte.pdf");
            }
            catch (Exception ex)
            {
                return Content($"Error al generar PDF: {ex.Message}");
            }
        }
    }
}