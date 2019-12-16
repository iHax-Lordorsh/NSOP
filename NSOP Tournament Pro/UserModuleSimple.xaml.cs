using NSOP_Tournament_Pro_Library;
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
    /// Interaction logic for UserModuleSimple.xaml
    /// </summary>
    public partial class UserModuleSimple : UserControl
    {

        private string _1 = "";
        public string TopLeft { get => _1; set { lblTopLeft.Content = value.ToString(); _1 = value; } }

        private string _2 = "";
        public string TopRight { get => _2; set { lblTopRight.Content = value.ToString(); _2 = value; } }

        private string _3 = "";
        public string Center { get => _3; set { lblCenter.Content = value.ToString(); _3 = value; } }

        private string _4 = "";
        public string BottomLeft { get => _4; set { lblBottomLeft.Content = value.ToString(); _4 = value; } }

        private string _5 = "";
        public string BottomRight { get => _5; set { lblBottomRight.Content = value.ToString(); _5 = value; } }

        private byte[] _Image = null;
        public byte[] Picture
        {
            get => _Image; set
            {
                _Image = value;
                Image IMG = new Image();
                ImageBrush myBrush = new ImageBrush();
                IMG.Source = DataAccess.ConvertByteArrayToBitMapImage(Picture);
                //imgBilde.Source = DataAccess.ConvertByteArrayToBitMapImage(Picture);
                myBrush.ImageSource = IMG.Source;
                brdPicture.Background = myBrush;
            }
        }

        private byte[] _Background = null;
        public new byte[] Background
        {
            get => _Background; set
            {
                _Background = value;
                Image IMG = new Image();
                ImageBrush myBrush = new ImageBrush();
                IMG.Source = DataAccess.ConvertByteArrayToBitMapImage(Background);
                //imgBilde.Source = DataAccess.ConvertByteArrayToBitMapImage(Picture);
                myBrush.ImageSource = IMG.Source;
                brdOutline.Background = myBrush;
            }
        }

        public UserModuleSimple()
        {
            InitializeComponent();
        }

    }
}
