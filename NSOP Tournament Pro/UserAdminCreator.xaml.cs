using NSOP_Torunament_Pro_Library;
using NSOP_Tournament_Pro_Library;
using System;
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
    /// Interaction logic for UserAdminCreator.xaml
    /// </summary>
    public partial class UserAdminCreator : UserControl
    {
        private string _Id = "";
        private string _Name = "";
        private string _Picture;
        private string _Info = "";
        private string _Description = "";
        private int _Price = 0;
        private int _Discount = 0;
        private int _Quantity = 0;

        private DateTime _StartDate;
        private DateTime _EndDate;
        private DateTime _Expire;

        public string ID { get => _Id; set { _Id = value; lblProductCode.Content = ID; } }
        public string ProductName { get => _Name; set { _Name = value; } }
        public string Picture { get => _Picture; set { _Picture = value; } }
        public string Info { get => _Info; set { _Info = value; } }

        public string Description { get => _Description; set { _Description = value; } }
        public int Price { get => _Price; set { _Price = value; txt_SalesPrice.Text = Price.ToString(); } }
        public int Discount { get => _Discount; set { _Discount = value; } }
        public int Quantity { get => _Quantity; set { _Quantity = value; } }

        public DateTime Expires { get => _Expire; set { _Expire = value; } }
        public DateTime StartDate { get => _StartDate; set { _StartDate = value; } }
        public DateTime EndDate { get => _EndDate; set { _EndDate = value; } }

        public List<Border> listPicture = new List<Border>();
        public string[] _productID = new string[9];
        public Label[] _productName = new Label[9];
        public Label[] _productPrice = new Label[9];
        public ComboBox[] _productQTY = new ComboBox[9];

        public UserAdminCreator()
        {
            InitializeComponent();
            Fill();
        }

        private List<Border> GetItemPicture()
        {
            List<Border> _B = new List<Border>();
            Border _b0 = new Border
            {
                Name = "i_Admin",
                Width = 100,
                Height = 100,
                Background = BrushBackground("i_Admin")
            };
            _B.Add(_b0);
            Border _b1 = new Border
            {
                Name = "i_Website",
                Width = 100,
                Height = 100,
                Background = BrushBackground("i_Website")
            };
            _B.Add(_b1);
            Border _b2 = new Border
            {
                Name = "i_Tokens",
                Width = 100,
                Height = 100,
                Background = BrushBackground("i_Tokens")
            };
            _B.Add(_b2);
            Border _b3 = new Border
            {
                Name = "i_Ticket",
                Width = 100,
                Height = 100,
                Background = BrushBackground("i_Ticket")
            };
            _B.Add(_b3);
            Border _b4 = new Border
            {
                Name = "i_Person",
                Width = 100,
                Height = 100,
                Background = BrushBackground("i_Person")
            };
            _B.Add(_b4);
            return _B;
        }

        internal void Fill()
        {
            _productID[0] = "";
            _productID[1] = "";
            _productID[2] = "";
            _productID[3] = "";
            _productID[4] = "";
            _productID[5] = "";
            _productID[6] = "";
            _productID[7] = "";
            _productID[8] = "";

            _productName[0] = new Label();
            _productName[1] = lbl_Name_1;
            _productName[2] = lbl_Name_2;
            _productName[3] = lbl_Name_3;
            _productName[4] = lbl_Name_4;
            _productName[5] = lbl_Name_5;
            _productName[6] = lbl_Name_6;
            _productName[7] = lbl_Name_7;
            _productName[8] = lbl_Name_8;

            _productPrice[0] = new Label();
            _productPrice[1] = lbl_Price_1;
            _productPrice[2] = lbl_Price_2;
            _productPrice[3] = lbl_Price_3;
            _productPrice[4] = lbl_Price_4;
            _productPrice[5] = lbl_Price_5;
            _productPrice[6] = lbl_Price_6;
            _productPrice[7] = lbl_Price_7;
            _productPrice[8] = lbl_Price_8;
            foreach (var item in _productPrice)
            {
                item.Content = "1";
            }
            _productQTY[0] = new ComboBox();
            _productQTY[1] = cbx_QTY_1;
            _productQTY[2] = cbx_QTY_2;
            _productQTY[3] = cbx_QTY_3;
            _productQTY[4] = cbx_QTY_4;
            _productQTY[5] = cbx_QTY_5;
            _productQTY[6] = cbx_QTY_6;
            _productQTY[7] = cbx_QTY_7;
            _productQTY[8] = cbx_QTY_8;
            foreach (var item in _productQTY)
            {
                item.ItemsSource = DataAccess.GetNumbers(12);
                item.SelectedIndex = 0;
            }
            _productQTY[8].ItemsSource = DataAccess.GetNumbers(100);
            listPicture = GetItemPicture();
            lstProduct.ItemsSource = listPicture;
            lstProduct.Items.Refresh();
            lstProduct.SelectedIndex = -1;
      
        }


        private void Create_Click(object sender, MouseButtonEventArgs e)
        {
            switch ((sender as Border).Name)
            {
                case "brdProduct":
                    grdProduct.Visibility = Visibility.Visible;
                    // Populate Administration Creating screen
                    var mainWnd = Application.Current.MainWindow as MainWindow;
                    this.Fill();
                    mainWnd.GetStartProducts();
                    break;
                case "brdGadget":
                    grdProduct.Visibility = Visibility.Hidden;
                    break;
                case "brdAnnounsment":
                    grdProduct.Visibility = Visibility.Hidden;
                    break;
            }
        }
        private void GenerateCode_Click(object sender, RoutedEventArgs e)
        {
            ID = DataAccess.FillID(DataAccess.IdType.Product);
        }
        private void LstPicture_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstProduct.SelectedIndex > -1)
            {
                foreach (var item in listPicture)
                {
                    if (item == (sender as ListBox).SelectedItem)
                    {
                        Picture = (item as Border).Name;
                        brdPicture.Background = (item as Border).Background;
                        lstProduct.Visibility = Visibility.Hidden;
                        lstProduct.SelectedIndex = -1;
                        break;
                    }
                }
            }
        }
        private void SaveProduct_Click(object sender, RoutedEventArgs e)
        {
            var mainWnd = Application.Current.MainWindow as MainWindow;
            mainWnd.SaveNewProduct();
        }
        private void Textbox_LostFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
            (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Focus();
            //(sender as TextBox).Clear();
            (sender as TextBox).Background = (SolidColorBrush)FindResource("ActiveText");
            (sender as TextBox).Foreground = (SolidColorBrush)FindResource("DeactiveText");
        }
        private void SelectionChange(object sender, RoutedEventArgs e)
        {
            switch ((sender as TextBox).Name)
            {
                case "txt_Name": // Name
                    if ((sender as TextBox).Text.Length >= 3)
                    {
                        // Code is correct you can now verify
                        (sender as TextBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
                        (sender as TextBox).Tag = 1;
                        _Name = (sender as TextBox).Text.ToString();
                    }
                    else
                    {
                        // You type the wrong code
                        (sender as TextBox).Background = (SolidColorBrush)FindResource("ErrorBackground");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ErrorText");
                        (sender as TextBox).Tag = 0;
                        _Name = (sender as TextBox).Text.ToString();
                    }
                    break;
                case "txt_Description":
                    if ((sender as TextBox).Text.Length >= 3)
                    {
                        // Code is correct you can now verify
                        (sender as TextBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
                        (sender as TextBox).Tag = 1;
                        _Description = (sender as TextBox).Text.ToString();
                    }
                    else
                    {
                        // You type the wrong code
                        (sender as TextBox).Background = (SolidColorBrush)FindResource("ErrorBackground");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ErrorText");
                        (sender as TextBox).Tag = 0;
                        _Description = (sender as TextBox).Text.ToString();
                    }
                    break;
            }
        }
        private void brdPicture_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            lstProduct.Visibility = Visibility.Visible;
        }
        public static System.Windows.Media.Brush BrushBackground(string fileName)
        {
            System.Windows.Controls.Image IMG = new System.Windows.Controls.Image();
            ImageBrush myBrush = new ImageBrush();
            if (fileName == "" || fileName == null)
            {
                fileName = "Avatar_1";
            }

            IMG.Source = DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject(fileName));
            myBrush.ImageSource = IMG.Source;
            return myBrush;
        }
        public static System.Windows.Media.Brush ByteToBrushBackground(byte[] picture)
        {
            ImageBrush _myBrush = new ImageBrush();
            System.Windows.Controls.Image _IMG = new System.Windows.Controls.Image
            {
                Source = DataAccess.ConvertByteArrayToBitMapImage(picture)
            };
            _myBrush.ImageSource = _IMG.Source;

            return _myBrush;
        }
        private void txt_SalesPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CheckIsNumeric(e);
        }
        private void CheckIsNumeric(TextCompositionEventArgs e)
        {
            if (!(int.TryParse(e.Text, out _) || e.Text == "."))
            {
                e.Handled = true;
            }
        }
        private void QTY_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            int _tp = 0;
            int _teller = -1;
            
            foreach (var item in _productQTY)
            {
                try
                {
                    _teller++;
                    if (_teller <= 6) // price
                    {
                        _tp += (Convert.ToInt16(item.SelectedItem.ToString()) * Convert.ToInt16(_productPrice[_teller].Content.ToString()));
                    }
                    else if (_teller == 7) // price
                    {
                        int _qty = Convert.ToInt16(item.SelectedItem.ToString());
                        if (_qty > 0)
                        {
                            _tp *= _qty;
                            Price = _tp;
                        }
                        else Price = 0;
                        lbl_Price_7.Content = Price.ToString();
                    } 
                    else if (_teller == 8) // prosent
                    {
                        _productPrice[_teller].Content = item.SelectedItem.ToString();
                        _Discount = Convert.ToInt16(item.SelectedItem.ToString());
                        int _prosent = Convert.ToInt16(item.SelectedItem.ToString());
                        if (_prosent > 0)
                        {
                            float x = (((Price *100)  / 100) * _prosent); //DataAccess.RoundUp((Price / 100), 2);

                            Price = (int)DataAccess.RoundUp((Price -(x/100)), 1);
                        }
                        else Price = Price;
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        internal void ShowProductCreator(Product product)
        {
            int _teller = 0;
            foreach (var item in product._StartProductsList)
            {
                _teller++;
                _productID[_teller] = item.ID;
                _productName[_teller].Content = item.ProductName;
                _productPrice[_teller].Content = item.Price.ToString();
                _productQTY[_teller].SelectedIndex = item.Quantity;
            }
            _productQTY[7].SelectedIndex = 1;
        }

        private void QTY_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as ComboBox).Focus();
        }

        private void TextInput_Change(object sender, TextCompositionEventArgs e)
        {
            int _Value = Convert.ToInt16((sender as TextBox).Name.Substring((sender as TextBox).Name.Length - 1, (sender as TextBox).Name.Length));
           
        }

        private void SourceUpdate_Change(object sender, DataTransferEventArgs e)
        {

            switch ((sender as TextBox).Name)
            {

            }
        }

    
    }
}
