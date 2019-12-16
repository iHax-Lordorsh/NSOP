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
    /// Interaction logic for UserSalesBox.xaml
    /// </summary>
    public partial class UserSalesBox : UserControl
    {
        private string _1 = "";
        public string Header { get => _1;  set { txtHeader.Text = value.ToString(); _1 = value; } }

        private string _2 = "";
        public string Info { get => _2; set { txtInfo.Text = value.ToString(); _2 = value; } }

        private string _3 = "";
        public string TopLeft { get => _3; set { lblTopLeft.Content = value.ToString(); _3 = value; } }

        private string _8 = "";
        public string TopRight { get => _8; set { lblTopRight.Content = value.ToString(); _8 = value; } }

        private string _4 = "";

        public string TopCenter { get => _4; set { lblTopCenter.Content = value.ToString(); _4 = value; } }

        private string _5 = "";
        public string BottomLeft { get => _5; set { lblBottomLeft.Content = value.ToString(); _5 = value; } }

        private string _6 = "BUY NOW";
        public string BottomCenter { get => _6; set { lblBottomCenter.Content = value.ToString(); _6 = value; } }

        private string _7 = "";
        public string BottomRight { get => _7; set { lblBottomRight.Content = value.ToString(); _7 = value; } }

        private byte[] _Background = null;
        public byte[] ProductPicture
        {
            get => _Background; set
            {
                _Background = value;
                System.Windows.Controls.Image IMG = new System.Windows.Controls.Image();
                ImageBrush myBrush = new ImageBrush();
                IMG.Source = DataAccess.ConvertByteArrayToBitMapImage(ProductPicture);
                myBrush.ImageSource = IMG.Source;
                brdItem.Background = myBrush;
            }
        }

        private byte[] _SalePicture = null;
        public byte[] SalePicture
        {
            get => _SalePicture; set
            {
                _SalePicture = value;
                System.Windows.Controls.Image IMG = new System.Windows.Controls.Image();
                ImageBrush myBrush = new ImageBrush();
                IMG.Source = DataAccess.ConvertByteArrayToBitMapImage(SalePicture);
                myBrush.ImageSource = IMG.Source;
                brdSale.Background = myBrush;
            }
        }

        private double _12;
        public double Price { get => _12; set => _12 = value; }

        public List<UserSalesBox> Products = new List<UserSalesBox>();

        public UserSalesBox()
        {
            InitializeComponent();
        }

        public UserSalesBox Fill(int i)
        {
            switch (i)
            {
                case 0:
                    this.Header = "WEBSITE";
                    this.Info = "All you can eat";
                    this.Price = 1299;
                    this.TopLeft = "1299,-";
                    this.TopRight = "1299,-";
                    this.TopCenter = "SUBSCRIPTION";
                    this.BottomLeft = "SALE";
                    this.BottomRight = "SALE";
                    this.BottomCenter = "BUY NOW";
                    this.ProductPicture = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject("Website")));
                    this.SalePicture = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject("Sales_1")));
                    break;
                case 1:
                    this.Header = "TOKENS";
                    this.Info = "All you can eat";
                    this.Price = 399;
                    this.TopLeft = "399,-";
                    this.TopRight = "399,-";
                    this.TopCenter = "SUBSCRIPTION";
                    this.BottomLeft = "SALE";
                    this.BottomRight = "SALE";
                    this.BottomCenter = "BUY NOW";
                    this.ProductPicture = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject("Tokens")));
                    this.SalePicture = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject("Sales_1")));
                    break;
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                    this.Header = "TICKETS";
                    this.Info = "All you can eat";
                    this.Price = 99;
                    this.TopLeft = "99,-";
                    this.TopRight = "99,-";
                    this.TopCenter = "SUBSCRIPTION";
                    this.BottomLeft = "SALE";
                    this.BottomRight = "SALE";
                    this.BottomCenter = "BUY NOW";
                    this.ProductPicture = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject("Tickets_icon")));
                    this.SalePicture = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject("Sales_1")));
                    break;
            }
            this.Width = 150;
            this.Margin = new Thickness(30, 54, 50, 30);
            return this;
        }
    }
}
