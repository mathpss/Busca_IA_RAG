using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace RagPdfApi.Service
{
    public class PdfPigService
    {
        public static string PdfToString(IFormFile file)
        {
            StringBuilder fullText = new();
            using var stream = file.OpenReadStream();

            using (PdfDocument document = PdfDocument.Open(stream))
            {
                foreach (Page page in document.GetPages())
                {

                    IEnumerable<Word> words = page.GetWords();
                    fullText.Append(string.Join(" ", words.Select(x => x.Text)));

                }
            return fullText.ToString();
            }
            
        }
        

        public static string TitleOrName(IFormFile file)
        {
            var stream = file.OpenReadStream();

            using (PdfDocument document = PdfDocument.Open(stream))
            {
                var title = document.Information.Title;

                title ??= file.FileName;

            return title;
            }
        }
    }
}