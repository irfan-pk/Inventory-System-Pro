using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Inventory_System_Pro
{
    public static class SaleReturnPdfGenerator
    {
        public static string GenerateSaleReturnPdf(int saleReturnID, string connectionString, string filePath)
        {
            DataTable dt =
                GetSaleReturnData(
                    saleReturnID,
                    connectionString);

            if (dt.Rows.Count == 0)
                throw new Exception(
                    "Sale return record was not found.");

            DataRow header = dt.Rows[0];

            string directory =
                Path.GetDirectoryName(filePath);

            if (!string.IsNullOrWhiteSpace(directory))
                Directory.CreateDirectory(directory);

            PdfDocument document = new PdfDocument();
            document.Info.Title = "Sale Return - " +
                header["ReturnNumber"].ToString();

            PdfPage page = document.AddPage ();

            XGraphics gfx = XGraphics.FromPdfPage(page);

            string returnNumber =
                header["ReturnNumber"].ToString();

            string fileName =
                $"SaleReturn_{returnNumber}.pdf";

            //PdfDocument document =
            //    new PdfDocument();

            document.Info.Title =
                "Sale Return - " + returnNumber;

            //PdfPage page =
            //    document.AddPage();

            //XGraphics gfx =
            //    XGraphics.FromPdfPage(page);

            XFont titleFont =
                new XFont(
                    "Arial",
                    18,
                    XFontStyleEx.Bold);

            XFont headerFont =
                new XFont(
                    "Arial",
                    10,
                    XFontStyleEx.Bold);

            XFont normalFont =
                new XFont(
                    "Arial",
                    9,
                    XFontStyleEx.Regular);

            double y = 35;

            // --------------------------------
            // Company Header
            // --------------------------------

            gfx.DrawString(
                "INVENTORY SYSTEM PRO",
                titleFont,
                XBrushes.Black,
                new XRect(
                    0,
                    y,
                    page.Width.Point,
                    25),
                XStringFormats.Center);

            y += 28;

            gfx.DrawString(
                "SALE RETURN",
                headerFont,
                XBrushes.Black,
                new XRect(
                    0,
                    y,
                    page.Width.Point,
                    20),
                XStringFormats.Center);

            y += 35;

            // --------------------------------
            // Return information
            // --------------------------------

            gfx.DrawString(
                "Return Number:",
                headerFont,
                XBrushes.Black,
                40,
                y);

            gfx.DrawString(
                returnNumber,
                normalFont,
                XBrushes.Black,
                135,
                y);

            gfx.DrawString(
                "Return Date:",
                headerFont,
                XBrushes.Black,
                330,
                y);

            gfx.DrawString(
                Convert.ToDateTime(
                    header["ReturnDate"])
                    .ToString("dd-MMM-yyyy"),
                normalFont,
                XBrushes.Black,
                410,
                y);

            y += 20;

            gfx.DrawString(
                "Sale Number:",
                headerFont,
                XBrushes.Black,
                40,
                y);

            gfx.DrawString(
                header["SaleNumber"].ToString(),
                normalFont,
                XBrushes.Black,
                135,
                y);

            y += 20;

            gfx.DrawString(
                "Customer:",
                headerFont,
                XBrushes.Black,
                40,
                y);

            gfx.DrawString(
                header["CustomerName"].ToString(),
                normalFont,
                XBrushes.Black,
                135,
                y);

            y += 20;

            gfx.DrawString(
                "Warehouse:",
                headerFont,
                XBrushes.Black,
                40,
                y);

            gfx.DrawString(
                header["WarehouseName"].ToString(),
                normalFont,
                XBrushes.Black,
                135,
                y);

            y += 30;

            // --------------------------------
            // Table Header
            // --------------------------------

            gfx.DrawLine(
                XPens.Black,
                40,
                y,
                page.Width.Point - 40,
                y);

            y += 17;

            gfx.DrawString(
                "Code",
                headerFont,
                XBrushes.Black,
                40,
                y);

            gfx.DrawString(
                "Product",
                headerFont,
                XBrushes.Black,
                100,
                y);

            gfx.DrawString(
                "Qty",
                headerFont,
                XBrushes.Black,
                300,
                y);

            gfx.DrawString(
                "Price",
                headerFont,
                XBrushes.Black,
                355,
                y);

            gfx.DrawString(
                "Discount",
                headerFont,
                XBrushes.Black,
                420,
                y);

            gfx.DrawString(
                "Net",
                headerFont,
                XBrushes.Black,
                500,
                y);

            y += 8;

            gfx.DrawLine(
                XPens.Black,
                40,
                y,
                page.Width.Point - 40,
                y);

            y += 17;

            // --------------------------------
            // Details
            // --------------------------------

            foreach (DataRow row in dt.Rows)
            {
                gfx.DrawString(
                    row["ProductCode"].ToString(),
                    normalFont,
                    XBrushes.Black,
                    40,
                    y);

                gfx.DrawString(
                    row["ProductName"].ToString(),
                    normalFont,
                    XBrushes.Black,
                    new XRect(
                        100,
                        y - 10,
                        190,
                        15),
                    XStringFormats.TopLeft);

                gfx.DrawString(
                    Convert.ToDecimal(
                        row["Qty"])
                        .ToString("N3"),
                    normalFont,
                    XBrushes.Black,
                    300,
                    y);

                gfx.DrawString(
                    Convert.ToDecimal(
                        row["UnitPrice"])
                        .ToString("N2"),
                    normalFont,
                    XBrushes.Black,
                    355,
                    y);

                gfx.DrawString(
                    Convert.ToDecimal(
                        row["DetailDiscount"])
                        .ToString("N2"),
                    normalFont,
                    XBrushes.Black,
                    420,
                    y);

                gfx.DrawString(
                    Convert.ToDecimal(
                        row["DetailNetAmount"])
                        .ToString("N2"),
                    normalFont,
                    XBrushes.Black,
                    500,
                    y);

                y += 18;

                // Basic page overflow handling
                if (y > page.Height.Point - 120)
                {
                    page = document.AddPage();

                    gfx.Dispose();

                    gfx =
                        XGraphics.FromPdfPage(page);

                    y = 40;
                }
            }

            // --------------------------------
            // Totals
            // --------------------------------

            y += 10;

            gfx.DrawLine(
                XPens.Black,
                40,
                y,
                page.Width.Point - 40,
                y);

            y += 22;

            DrawTotal(
                gfx,
                "Gross Amount",
                header["GrossAmount"],
                ref y,
                headerFont,
                normalFont);

            DrawTotal(
                gfx,
                "Discount",
                header["DiscountAmount"],
                ref y,
                headerFont,
                normalFont);

            DrawTotal(
                gfx,
                "Tax",
                header["TaxAmount"],
                ref y,
                headerFont,
                normalFont);

            DrawTotal(
                gfx,
                "Net Amount",
                header["NetAmount"],
                ref y,
                headerFont,
                normalFont);

            DrawTotal(
                gfx,
                "Refund Amount",
                header["RefundAmount"],
                ref y,
                headerFont,
                normalFont);

            // --------------------------------
            // Remarks
            // --------------------------------

            y += 15;

            gfx.DrawString(
                "Remarks:",
                headerFont,
                XBrushes.Black,
                40,
                y);

            y += 17;

            gfx.DrawString(
                header["Remarks"] == DBNull.Value
                    ? ""
                    : header["Remarks"].ToString(),
                normalFont,
                XBrushes.Black,
                new XRect(
                    40,
                    y,
                    page.Width.Point - 80,
                    40),
                XStringFormats.TopLeft);

            y += 55;

            gfx.DrawString(
                "STATUS: " +
                header["Status"].ToString(),
                headerFont,
                XBrushes.Black,
                new XRect(
                    0,
                    y,
                    page.Width.Point,
                    20),
                XStringFormats.Center);

            gfx.Dispose();

            document.Save(filePath);
            document.Close();

            return filePath;
        }

        private static void DrawTotal(
            XGraphics gfx,
            string caption,
            object value,
            ref double y,
            XFont headerFont,
            XFont normalFont)
        {
            gfx.DrawString(
                caption + ":",
                headerFont,
                XBrushes.Black,
                390,
                y);

            gfx.DrawString(
                Convert.ToDecimal(value)
                    .ToString("N2"),
                normalFont,
                XBrushes.Black,
                510,
                y);

            y += 18;
        }

        private static DataTable GetSaleReturnData(
            int saleReturnID,
            string connectionString)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn =
                   new SqlConnection(connectionString))
            using (SqlCommand cmd =
                   new SqlCommand(@"
                SELECT
                    sr.SaleReturnID,
                    sr.ReturnNumber,
                    sr.ReturnDate,
                    s.SaleNumber,

                    c.CustomerName,
                    w.WarehouseName,

                    sr.GrossAmount,
                    sr.DiscountAmount,
                    sr.TaxAmount,
                    sr.NetAmount,
                    sr.RefundAmount,
                    sr.Status,
                    sr.Remarks,

                    srd.SaleReturnDetailID,
                    srd.SaleDetailID,
                    srd.ProductID,

                    p.ProductCode,
                    p.ProductName,

                    srd.Qty,
                    srd.UnitPrice,

                    srd.DiscountAmount AS DetailDiscount,
                    srd.TaxAmount AS DetailTax,
                    srd.NetAmount AS DetailNetAmount

                FROM dbo.SaleReturns sr

                INNER JOIN dbo.Sales s
                    ON s.SaleID = sr.SaleID

                INNER JOIN dbo.Customers c
                    ON c.CustomerID = sr.CustomerID

                INNER JOIN dbo.Warehouses w
                    ON w.WarehouseID =
                       sr.WarehouseID

                INNER JOIN dbo.SaleReturnDetails srd
                    ON srd.SaleReturnID =
                       sr.SaleReturnID

                INNER JOIN dbo.Products p
                    ON p.ProductID =
                       srd.ProductID

                WHERE sr.SaleReturnID =
                      @SaleReturnID

                ORDER BY
                    srd.SaleReturnDetailID;",
                cn))
            {
                cmd.Parameters.Add(
                    "@SaleReturnID",
                    SqlDbType.Int).Value =
                    saleReturnID;

                using (SqlDataAdapter da =
                       new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }
    }
}