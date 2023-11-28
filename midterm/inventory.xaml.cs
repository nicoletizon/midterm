using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace midterm
{
    public class CombinedData
    {
        // Product properties
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int NoofStock { get; set; }
        public decimal Cost { get; set; }
        public decimal SellingPrice { get; set; }
        public string Description { get; set; }

    }

    public partial class inventory : Window
    {
        DataClasses1DataContext db = new DataClasses1DataContext();

        public inventory()
        {
            InitializeComponent();
            LoadData(); // Load data immediately after logging in
        }

        private void LoadData()
        {
            try
            {
                var products = db.Table_Products.ToList();

                // Combine data from different tables into a single list
                var combinedDataList = new List<CombinedData>();

                foreach (var product in products)
                {
                    combinedDataList.Add(new CombinedData
                    {
                        ProductID = product.ProductID,
                        ProductName = product.Name,
                        NoofStock = (int)product.No__of_Stock,
                        Cost = (decimal)product.Cost,
                        SellingPrice = (decimal)product.Selling_Price,
                        Description = product.Description
                    });
                }

                // Bind the combined data to the ListViews
                Search.ItemsSource = combinedDataList.Where(item => item.ProductID != 0); // Filter out non-product items
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
