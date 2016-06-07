using iTextSharp.text;
using iTextSharp.text.pdf;
using Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Export
{
    internal class PDF:Export
    {
        private Document document = null;
        private PdfWriter writer = null;
        iTextSharp.text.Font fontStyle = null;
        PdfPTable table = null;
        MemoryStream stream = null;

        public override void InitializeConnection()
        {
            document = new Document();
            stream = new MemoryStream();
            writer = PdfWriter.GetInstance(document, stream);
        }
        public override void OpenConnection()
        {
            document.Open();
        }
        public override void CreateTable(dynamic propList)
        {
            fontStyle = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_BOLD, 10);

            table = new PdfPTable(propList.Count);
            float[] widths = new float[] { 4f, 4f, 4f };
            table.WidthPercentage = 100;

            //table.SetWidths(widths);

            foreach (var column in propList)
            {

                table.AddCell(new Phrase(column.fieldName, fontStyle));
            }
        }
        public override void PopulateTable(dynamic list, dynamic propList) {

            fontStyle = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10);

            foreach (var pageItems in list)
            {
                for (int j = 0; j <= propList.Count - 1; j++)
                {
                    string value = pageItems[propList[j].fieldName];

                    table.AddCell(new PdfPCell(new Phrase(value, fontStyle)));

                }
            }
            
            document.Add(table);
        }
        public override void CloseConnection(string path)
        {
            path = string.Concat(path, GetFileExtension());

            document.Close();

            Log.WriteFileStream(path,stream);
        }
        private string GetFileExtension()
        {

            return ".pdf";
        }
    }
}
