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
                case "brdToken":
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

            lstProduct.ItemsSource = PopulateProducts(product._StartProductsList);
            lstProduct.Items.Refresh();
            lstProduct.Visibility = Visibility.Visible;
        }

        private List<UserSalesBox> PopulateProducts(List<Product> startProductsList)
        {
            List<UserSalesBox> _uAB = new List<UserSalesBox>();
            foreach (var item in startProductsList)
            {
                UserSalesBox _u = new UserSalesBox();

                _u.ProductName = item.ProductName;
                _u.Description = item.Description;
                _u.Price = item.Price;
                _u.TopLeft = "";
                _u.TopRight = "";
                _u.ProductType = item.ProductName;
                _u.BottomLeft = "Info";
                _u.BottomRight = "Info";
                _u.BottomCenter = "BUY NOW";
                _u.ProductPicture = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject(item.Picture)));
                _u.Width = 200;
                _u.Margin = new Thickness(25, 25, 25, 25);
                // _u.SalePicture = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject("i_Sale")));
                _uAB.Add(_u);
            }
            return _uAB;
        }
    }
}
