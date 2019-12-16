using NSOP_Torunament_Pro_Library;
using NSOP_Tournament_Pro_Library;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
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
    /// Interaction logic for UserSubscription.xaml
    /// </summary>
    public partial class UserSubscription : UserControl
    {
         public List<UserSalesBox> productList = new List<UserSalesBox>();
        
        public UserSubscription()
        {
            InitializeComponent();
            Setup();
        }

        private void Setup()
        {
            // Fill List
            lstProducts.ItemsSource = productList;
        }

        private void Product_Click(object sender, MouseButtonEventArgs e)
        {

        }

        internal void FillSalesBoxes()
        {
            for (int i = 0; i < 8; i++)
            {
                UserSalesBox _p = new UserSalesBox().Fill(i);
                productList.Add(_p);
                
            }
            lstProducts.ItemsSource = productList;
            lstProducts.Items.Refresh();
            lstProducts.SelectedItem = -1;
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
    }
}
