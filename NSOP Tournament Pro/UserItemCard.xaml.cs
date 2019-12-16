using NSOP_Tournament_Pro_Library;
using System.Windows.Controls;
using System.Windows.Media;
using Image = System.Windows.Controls.Image;

namespace NSOP_Tournament_Pro
{
    /// <summary>
    /// Interaction logic for UserItemCard.xaml
    /// </summary>
    public partial class UserItemCard : UserControl
    {
        private string _ActionType = "";
        public string ActionType { get => _ActionType; set => _ActionType = value; }

        private string _ClassType = "";
        public string ClassType { get => _ClassType; set => _ClassType = value; }

        // **********
        // HEADER
        // **********
        private string _1 = "";
        public string TopLeft { get => _1; set { lblTopLeft.Content = value; _1 = value; } }

        private string _2 = "";
        public string TopCenter { get => _2; set { lblTopCenter.Content = value; _2 = value; } }

        private string _3 = "";
        public string TopRight { get => _3; set { lblTopRight.Content = value; _3 = value; } }

        // **********
        // MIDDLE
        // **********
        private string _41 = "";
        public string Header1 { get => _41; set { lbl_Header_1.Content = value; _41 = value; } }
        private string _4 = "";
        public string CenterTop { get => _4; set { lblCenterTop.Content = value; _4 = value; } }

        private string _51 = "";
        public string Header2 { get => _51; set { lbl_Header_2.Content = value; _51 = value; } }
        private string _5 = "";
        public string CenterMiddle { get => _5; set { lblCenterMiddle.Content = value; _5 = value; } }

        private string _61 = "";
        public string Header3 { get => _61; set { lbl_Header_3.Content = value; _61 = value; } }
        private string _6 = "";
        public string CenterBottom { get => _6; set { lblCenterBottom.Content = value; _6 = value; } }

        // **********
        // FOTTER
        // **********
        private string _7 = "";
        public string BottomLeft { get => _7; set { lblBottomLeft.Content = value; _7 = value; } }

        private string _8 = "";
        public string BottomCenter { get => _8; set { lblBottomCenter.Content = value; _8 = value; } }

        private string _9 = "";
        public string BottomRight { get => _9; set { lblBottomRight.Content = value; _9 = value; } }

        // **********
        // PICTURES
        // **********

        private byte[] _img1 = null;
        public byte[] Picture { get => _img1; set 
            {
                _img1 = value;
                Image IMG = new Image();
                ImageBrush myBrush = new ImageBrush();
                IMG.Source = DataAccess.ConvertByteArrayToBitMapImage(Picture);
                myBrush.ImageSource = IMG.Source;
                brdPicture.Background = myBrush;
            } }

        private byte[] _img2 = null;
        public byte[] PictureSmall
        {
            get => _img2; set
            {
                _img2 = value;
                Image IMG = new Image();
                ImageBrush myBrush = new ImageBrush();
                IMG.Source = DataAccess.ConvertByteArrayToBitMapImage(PictureSmall);
                myBrush.ImageSource = IMG.Source;
                brdSmallPicture.Background = myBrush;
            }
        }

        public UserItemCard()
        {
            InitializeComponent();
        }
    }
}
