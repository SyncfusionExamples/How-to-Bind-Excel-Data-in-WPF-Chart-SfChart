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
                    double value1 = worksheet[$"B{i}"].Number;
                    double value2 = worksheet[$"C{i}"].Number;
                    double value3 = worksheet[$"D{i}"].Number;

                    ProductAData.Add(new ProductSales { Month = month, Value = value1 });
                    ProductBData.Add(new ProductSales { Month = month, Value = value2 });
                    ProductCData.Add(new ProductSales { Month = month, Value = value3 });
                }

                workbook.Close();
            }
        }
    }
}
