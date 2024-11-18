import os
from weasyprint import HTML

# Ruta de salida del archivo PDF
output_pdf_path = 'C:/Users/CENSI USMP/Desktop/ExportarDocumento/ExportarDocumento/wwwroot/pdf/output.pdf'

# Asegúrate de que la carpeta exista
output_directory = os.path.dirname(output_pdf_path)
if not os.path.exists(output_directory):
    os.makedirs(output_directory)

# El contenido HTML que quieres convertir a PDF
html_content = """
<html>
    <head><title>Mi Documento PDF</title></head>
    <body><h1>¡Hola Mundo!</h1></body>
</html>
"""

# Generar el archivo PDF
HTML(string=html_content).write_pdf(output_pdf_path)
print("PDF generado exitosamente")
