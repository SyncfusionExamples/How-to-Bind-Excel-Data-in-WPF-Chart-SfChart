# How-to-Bind-Excel-Data-in-WPF-Chart-SfChart

This article explains how to bind data from an Excel file to a [Syncfusion WPF SfChart]( https://www.syncfusion.com/wpf-controls/charts) control. By following the steps outlined below, you will be able to load Excel data and display it in the chart.

## Steps to Bind Excel Data in WPF Charts

1. Install Required NuGet Packages

To work with Excel data in WPF, you need to install the following NuGet packages.

•	`Syncfusion.SfChart.WPF` for the WPF chart control.
•	`Syncfusion.XlsIO.WPF` for reading Excel files.

You can install these packages using the [NuGet Package Manager](https://www.nuget.org/).


2. Create the Data Model

Define a data model to hold the Excel data. For example, a class ProductSales can represent each row of data.

**[XAML]**

 ```
public class ProductSales
{
    public string Month { get; set; }
    public double Value { get; set; }
} 
 ```
 

3. Add Excel File to the Project and set Excel File Properties

    1. Right-click on the project in the Solution Explorer.
    2. Select Add > Existing Item... and browse to the Excel file (e.g., Data.xlsx) you want to include.
    3. Right-click on the added Excel file in the Solution Explorer and select Properties.
    4. Set the `Build Action` property to `Embedded resource` and the `Copy to Output Directory` property to `Copy if newer` or (`Copy always` if you want the file copied every time you build).

4. Read Data from the Excel File

Create a method to read data from the Excel file and convert it into a collection of ProductSales objects. The following example uses the `Syncfusion.XlsIO.WPF` library.

**[C#]**

 ```
using Syncfusion.XlsIO;
using System.Collections.ObjectModel;

. . .

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
        LoadExcelData("Data.xlsx");
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

 ```

5. Configuring the Syncfusion WPF Chart

Let’s configure the Syncfusion WPF Charts control using this [documentation](https://help.syncfusion.com/wpf/charts/getting-started).

**[XAML]**
 
 ```
<syncfusion:SfChart Header="Product Sales Report">
. . .

    <syncfusion:SfChart.PrimaryAxis>
        <syncfusion:CategoryAxis Header="Month" />
    </syncfusion:SfChart.PrimaryAxis>

    <syncfusion:SfChart.SecondaryAxis>
        <syncfusion:NumericalAxis Header="Sales" />
    </syncfusion:SfChart.SecondaryAxis>
    
    <syncfusion:ColumnSeries ItemsSource="{Binding ProductAData}" 
                             XBindingPath="Month" 
                             YBindingPath="Value" />

    <syncfusion:ColumnSeries ItemsSource="{Binding ProductBData}" 
                             XBindingPath="Month" 
                             YBindingPath="Value"/>

    <syncfusion:ColumnSeries ItemsSource="{Binding ProductCData}" 
                             XBindingPath="Month" 
                             YBindingPath="Value" />
    
. . .
</syncfusion:SfChart>

 ```

## Output

The following image illustrates the [WPF Chart]( https://help.syncfusion.com/cr/wpf/Syncfusion.UI.Xaml.Charts.SfChart.html) successfully bind Excel data to the WPF SfChart control.
 
 ![Bind Excel data in WPF Chart](https://support.syncfusion.com/kb/agent/attachment/article/18452/inline?token=eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjM0MDcwIiwib3JnaWQiOiIzIiwiaXNzIjoic3VwcG9ydC5zeW5jZnVzaW9uLmNvbSJ9.7YFfaueQm4BYLGMVmwvIvbP6AEpAtQ3e1zh8hkAGk2g)

 ## Troubleshooting

#### Path too long exception

If you are facing a path too long exception when building this example project, close Visual Studio and rename the repository to a shorter name before building the project.

For more details, refer to the KB on [how to bind excel data in WPF chart control?]().