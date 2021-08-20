using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.CoursesFeatures.Queries
{
    public class CoursesPDFReportQuery : IRequest<Stream>
    {

    }

    public class CoursesPDFReportQueryHandler : IRequestHandler<CoursesPDFReportQuery, Stream>
    {
        private readonly OnlineCoursesContext _courseContext;
        public CoursesPDFReportQueryHandler(OnlineCoursesContext courseContext)
        {
            _courseContext = courseContext;
        }
        public async Task<Stream> Handle(CoursesPDFReportQuery request, CancellationToken cancellationToken)
        {
            //TODO: Optimize the query iterates all the elements
            var courses = await _courseContext.Courses.Include(course=> course.Prices).Take(10).ToListAsync();

            //stream document
            MemoryStream docStream = new MemoryStream();

            //size page of document
            Rectangle pageSize = new Rectangle(PageSize.A4);

            //create document
            Document document = new Document(pageSize, 0, 0, 50, 100);

            //enable document writing
            PdfWriter writerDoc = PdfWriter.GetInstance(document, docStream);
            writerDoc.CloseStream = false;

            /* open document edition*/
            document.Open();

            //Add document name
            document.AddTitle("Courses report");


            #region document report header

            PdfPTable table_ReportdHeader = new PdfPTable(1);//table for header repor
            table_ReportdHeader.WidthPercentage = 80;//with size of table

            Font font_HeaderReport = new Font(Font.HELVETICA, 14f, Font.BOLD, BaseColor.Blue);//font for header
            PdfPCell cell_ReportHeader = new PdfPCell(new Phrase("All courses in constext", font_HeaderReport)); //header text
            cell_ReportHeader.Border = Rectangle.NO_BORDER; //delete border for cell

            table_ReportdHeader.AddCell(cell_ReportHeader);//all cell to table
            document.Add(table_ReportdHeader);//add table to document

            #endregion

            #region  table courses
            PdfPTable table_CoursesHeaders = new PdfPTable(3);
            float[] widths = new float[] { 60f, 20f, 20f };
            table_CoursesHeaders.SetWidthPercentage(widths, pageSize);

            /* start table headers*/

            Font font_CourseHeader = new Font(Font.HELVETICA, 12f, Font.BOLD, BaseColor.Black);

            PdfPCell cell_CourseName = new PdfPCell(new Phrase("Course Name", font_CourseHeader));
            table_CoursesHeaders.AddCell(cell_CourseName);

            PdfPCell cell_CoursPrice = new PdfPCell(new Phrase("Price", font_CourseHeader));
            table_CoursesHeaders.AddCell(cell_CoursPrice);

            PdfPCell cell_CoursePromotion = new PdfPCell(new Phrase("Price promotion", font_CourseHeader));
            table_CoursesHeaders.AddCell(cell_CoursePromotion);

            /* finish table headers*/

            table_CoursesHeaders.WidthPercentage = 90;

            /* start add courses item to table*/
            Font font_CourseRow = new Font(Font.HELVETICA, 12f, Font.NORMAL, BaseColor.Black);

                var cell_C_Title = new PdfPCell();
                var cell_C_Price = new PdfPCell();
                var cell_C_Promotion = new PdfPCell();

            foreach (var item in courses)
            {
                cell_C_Title.Phrase = new Phrase(item.Title);
                cell_C_Price.Phrase = new Phrase(item.Prices.CurrentPrice.ToString("C"));
                cell_C_Promotion.Phrase = new Phrase(item.Prices.Promotion.ToString("C"));

                table_CoursesHeaders.AddCell(cell_C_Title);
                table_CoursesHeaders.AddCell(cell_C_Price);
                table_CoursesHeaders.AddCell(cell_C_Promotion);
            }
            /* finish add courses item to table*/


            //table_CoursesHeaders.SpacingBefore = 10;
            document.Add(table_CoursesHeaders);
            #endregion







            /* close document edition*/
            document.Close();


            // build the document from the stream
            byte[] bytesDoc = docStream.ToArray();
            docStream.Write(bytesDoc, 0, bytesDoc.Length);
            docStream.Position = 0;

            return docStream;

        }
    }
}