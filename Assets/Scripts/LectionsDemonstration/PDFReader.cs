
namespace LectionsDemonstration
{
    public class PDFReader
    {
        // public static void ExtractImagesFromPDF(string sourcePdf, string outputPath)
        // {
        //     // NOTE:  This will only get the first image it finds per page.
        //     PdfReader pdf = new PdfReader(sourcePdf);
        //     RandomAccessFileOrArray raf = new iTextSharp.text.pdf.RandomAccessFileOrArray(sourcePdf);
        //
        //     try
        //     {
        //         for (int pageNumber = 1; pageNumber <= pdf.NumberOfPages; pageNumber++)
        //         {
        //             PdfDictionary pg = pdf.GetPageN(pageNumber);
        //
        //             // recursively search pages, forms and groups for images.
        //             PdfObject obj = FindImageInPDFDictionary(pg);
        //             if (obj != null)
        //             {
        //
        //                 int XrefIndex = Convert.ToInt32(((PRIndirectReference)obj).Number.ToString(System.Globalization.CultureInfo.InvariantCulture));
        //                 PdfObject pdfObj = pdf.GetPdfObject(XrefIndex);
        //                 PdfStream pdfStrem = (PdfStream)pdfObj;
        //                 byte[] bytes = PdfReader.GetStreamBytesRaw((PRStream)pdfStrem);
        //                 if ((bytes != null))
        //                 {
        //                     using (MemoryStream memStream = new System.IO.MemoryStream(bytes))
        //                     {
        //                         memStream.Position = 0;
        //                         Image img = Image.FromStream(memStream);
        //                         // must save the file while stream is open.
        //                         if (!Directory.Exists(outputPath))
        //                             Directory.CreateDirectory(outputPath);
        //
        //                         string path = Path.Combine(outputPath, String.Format(@"{0}.jpg", pageNumber));
        //                         System.Drawing.Imaging.EncoderParameters parms = new System.Drawing.Imaging.EncoderParameters(1);
        //                         parms.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Compression, 0);
        //                         System.Drawing.Imaging.ImageCodecInfo jpegEncoder = Utilities.GetImageEncoder("JPEG");
        //                         img.Save(path, jpegEncoder, parms);
        //                     }
        //                 }
        //             }
        //         }
        //     }
        //     catch
        //     {
        //         throw;
        //     }
        //     finally
        //     {
        //         pdf.Close();
        //         raf.Close();
        //     }
        //
        //
        // }
        //
        // private static PdfObject FindImageInPDFDictionary(PdfDictionary pg)
        // {
        //     PdfDictionary res =
        //         (PdfDictionary) PdfReader.GetPdfObject(pg.Get(PdfName.RESOURCES));
        //
        //
        //     PdfDictionary xobj =
        //         (PdfDictionary) PdfReader.GetPdfObject(res.Get(PdfName.XOBJECT));
        //     if (xobj != null)
        //     {
        //         foreach (PdfName name in xobj.Keys)
        //         {
        //
        //             PdfObject obj = xobj.Get(name);
        //             if (obj.IsIndirect())
        //             {
        //                 PdfDictionary tg = (PdfDictionary) PdfReader.GetPdfObject(obj);
        //
        //                 PdfName type =
        //                     (PdfName) PdfReader.GetPdfObject(tg.Get(PdfName.SUBTYPE));
        //
        //                 //image at the root of the pdf
        //                 if (PdfName.IMAGE.Equals(type))
        //                 {
        //                     return obj;
        //                 } // image inside a form
        //                 else if (PdfName.FORM.Equals(type))
        //                 {
        //                     return FindImageInPDFDictionary(tg);
        //                 } //image inside a group
        //                 else if (PdfName.GROUP.Equals(type))
        //                 {
        //                     return FindImageInPDFDictionary(tg);
        //                 }
        //
        //             }
        //         }
        //     }
        //
        //     return null;
        //
        // }
    }
}