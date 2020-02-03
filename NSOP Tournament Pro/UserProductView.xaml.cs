using NSOP_Torunament_Pro_Library;
using NSOP_Tournament_Pro_Library;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NSOP_Tournament_Pro
{
    /// <summary>
    /// Interaction logic for UserProductView.xaml
    /// </summary>
    public partial class UserProductView : UserControl
    {
        public UserProductView()
        {
            InitializeComponent();
        }

        private void LstPicture_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, MouseButtonEventArgs e)
        {
            var mainWnd = Application.Current.MainWindow as MainWindow;
            switch ((sender as Border).Name)
            {
                case "brdAllProduct":
                    mainWnd.GetAllProduct();
                    // Show all produckts
                    break;
                case "brdSubscription":
                    mainWnd.ShowSubscriptionProduct();
                    // Show all Subscriptions
                    break;
                case "brdTokens":
                    mainWnd.ShowTokenProduct();
                    // Show all Tokens
                    break;
                case "brdOther":
                   
                    // Shos other Products
                    break;
            }
        }

        internal void ShowBroduct(Product product)
        {

            lstProduct.ItemsSource = PopulateProducts(product.StartProductsList);
            lstProduct.Items.Refresh();
            lstProduct.Visibility = Visibility.Visible;
        }

        private List<UserSalesBox> PopulateProducts(List<Product> startProductsList)
        {
            List<UserSalesBox> _uAB = new List<UserSalesBox>();
            foreach (var item in startProductsList)
            {
                UserSalesBox _u = new UserSalesBox
                {
                    ProductName = item.ProductName,
                    Description = item.Description,
                    Price = item.Price,
                    TopLeft = item.Price.ToString(),
                    TopRight = item.Price.ToString(),
                    ProductType = item.ProductType,
                    BottomLeft = "Info",
                    BottomRight = "Info",
                    BottomCenter = "BUY NOW",
                    ProductPicture = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject(item.Picture))),
                    Width = 200,
                    Margin = new Thickness(25, 25, 25, 25)
                };
                // _u.SalePicture = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject("i_Sale")));
                _uAB.Add(_u);
            }
            return _uAB;
        }
    }
}
