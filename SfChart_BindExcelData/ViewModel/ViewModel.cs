using Syncfusion.XlsIO;
using System.Collections.ObjectModel;

namespace SfChart_BindExcelData
{
    public class ViewModel
    {
        public ObservableCollection<ProductSales> ProductAData { get; set; }
        public ObservableCollection<ProductSales> ProductBData { get; set; }
        public ObservableCollection<ProductSales> ProductCData { get; set; }

        public ViewModel()
        {
            // Initialize data collections
            ProductAData = new ObservableCollection<ProductSales>();
            ProductBData = new ObservableCollection<ProductSales>();
            ProductCData = new ObservableCollection<ProductSales>();

            // Load Excel data
            LoadExcelData("Resource\\Data.xlsx");
        }

        private void LoadExcelData(string filePath)
        {
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                IWorkbook workbook = application.Workbooks.Open(filePath);
                IWorksheet worksheet = workbook.Worksheets[0]; // First worksheet
                int lastRow = worksheet.UsedRange.LastRow; // Get the last row with data dynamically
                for (int i = 2; i <= lastRow; i++) // Assuming headers are in Row 1
                {
                    string month = worksheet[$"A{i}"].Text;
                    ProductAData.Add(new ProductSales { Month = month, Value = worksheet[$"B{i}"].Number });
                    ProductBData.Add(new ProductSales { Month = month, Value = worksheet[$"C{i}"].Number });
                    ProductCData.Add(new ProductSales { Month = month, Value = worksheet[$"D{i}"].Number });
                }

                workbook.Close();
            }
        }
    }
}
