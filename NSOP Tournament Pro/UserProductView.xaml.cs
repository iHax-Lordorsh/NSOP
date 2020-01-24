using System;
using System.Collections.Generic;
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
            switch ((sender as Border).Name)
            {
                case "brdAllProduct":
                    // Show all produckts
                    break;
                case "brdSubscription":
                    // Show all Subscriptions
                    break;
                case "brdToken":
                    // Show all Tokens
                    break;
                case "brdOther":
                    // Shos other Products
                    break;
            }
        }
    }
}
