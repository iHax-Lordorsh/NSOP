using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using AForge.Video;
using AForge.Video.DirectShow;
using NSOP_Tournament_Pro_Library;

namespace NSOP_Tournament_Pro
{
    /// <summary>
    /// Interaction logic for UserSalesStand.xaml
    /// </summary>
    public partial class UserSalesStand : UserControl
    {
        private byte[] _Image = null;
        public byte[] Picture
        {
            get => _Image; set
            {
                _Image = value;
                System.Windows.Controls.Image IMG = new System.Windows.Controls.Image();
                ImageBrush myBrush = new ImageBrush();
                IMG.Source = DataAccess.ConvertByteArrayToBitMapImage(Picture);
                myBrush.ImageSource = IMG.Source;
                brdPicture.Background = myBrush;
            }
        }
        public string Gender { get; private set; }
        public byte[] NationalityPicture { get; private set; }
        public string Nationality { get; private set; }
        public string Iso3166Name { get; private set; }
        public DateTime BornDate { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Mobile { get; private set; }
        public string EMail { get; private set; }
        public string NickName { get; private set; }
        public string ClubID { get; private set; }
        public string PlayerID { get; private set; }

        public UserSalesStand()
        {
            InitializeComponent();

            
        }

        public void Setup()
        {
            ////cbxNationality.ItemsSource = DataAccess.GetCountries();
            ////cbxNationality.Text = "Norway [NO]";
        }

        private void Gender_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            switch (b.Name.ToString())
            {
                case "btnMale":
                    Gender = "MALE";
                    btnMale.Style = (Style)FindResource("ButtonPushed");
                    btnMale.Foreground = (SolidColorBrush)FindResource("ActiveText");

                    btnFemale.Style = (Style)FindResource("ButtonUnPushed");
                    btnFemale.Foreground = (SolidColorBrush)FindResource("DeactiveText");
                    break;
                case "btnFemale":
                    Gender = "FEMALE";
                    btnFemale.Style = (Style)FindResource("ButtonPushed");
                    btnFemale.Foreground = (SolidColorBrush)FindResource("ActiveText");

                    btnMale.Style = (Style)FindResource("ButtonUnPushed");
                    btnMale.Foreground = (SolidColorBrush)FindResource("DeactiveText");
                    break;
            }
        }
        private void CbxNationality_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxNationality.SelectedIndex != -1)
            {
                string _Iso3166Name = cbxNationality.SelectedItem.ToString().Substring(cbxNationality.SelectedItem.ToString().Length - 3, 2);
                CultureInfo _Culture = new CultureInfo(_Iso3166Name.ToLower());
                //              _ = new BitmapImage();
                //                BitmapImage bImage = DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject(_Iso3166Name.ToLower(), _Culture));
                NationalityPicture = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject(_Iso3166Name.ToLower(), _Culture)));
                Nationality = cbxNationality.SelectedItem.ToString().Substring(0, cbxNationality.SelectedItem.ToString().Length - 4);
                Iso3166Name = _Iso3166Name;
            }
        }
        private void Birthday_Changed(object sender, SelectionChangedEventArgs e)
        {
            BornDate = DateTime.Parse((sender as DatePicker).Text.ToString());
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
        private void Textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            switch ((sender as TextBox).Name.ToLower())
            {
                case "txt_1": // FirstName
                    if ((sender as TextBox).Text.Length >= 2)
                    {
                        FirstName = (sender as TextBox).Text.ToString();
                        (sender as TextBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
                    }
                    else
                    {
                        (sender as TextBox).Background = (SolidColorBrush)FindResource("ErrorBackground");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ErrorText");
                    }
                    break;
                case "txt_2": // LastName
                    if ((sender as TextBox).Text.Length >= 2)
                    {
                        LastName = (sender as TextBox).Text.ToString();
                        (sender as TextBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
                    }
                    else
                    {
                        (sender as TextBox).Background = (SolidColorBrush)FindResource("ErrorBackground");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ErrorText");
                    }
                    break;
                case "txt_3": // Phone Number
                    if ((sender as TextBox).Text.Length >= 8 && (sender as TextBox).Text.Length <= 10)
                    {
                        if ((sender as TextBox).Text.Length > 10)
                        {
                            Mobile = (sender as TextBox).Text.Substring(0, 10);
                        }
                        else Mobile = (sender as TextBox).Text.ToString();
                        (sender as TextBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
                    }
                    else
                    {
                        (sender as TextBox).Background = (SolidColorBrush)FindResource("ErrorBackground");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ErrorText");
                    }
                    break;
                case "txt_4": // E-mail
                    if ((sender as TextBox).Text.Contains("@") && (sender as TextBox).Text.Contains(".") && (sender as TextBox).Text.Length >= 7)
                    {
                        EMail = (sender as TextBox).Text.ToString();
                        (sender as TextBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
                    }
                    else
                    {
                        (sender as TextBox).Background = (SolidColorBrush)FindResource("ErrorBackground");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ErrorText");
                    }
                    break;
                case "txt_5": // Nickname
                    NickName = (sender as TextBox).Text.ToString();
                    (sender as TextBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
                    (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
                    break;
            }
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            _ = new Button();
            Button btn = (Button)sender;
            Person _p = FillPerson();
            switch (btn.Name)
            {
                case "btnNew":
                    _p.ActionType = "New";
                    break;
                case "btnUpdate":
                    _p.ActionType = "Update";
                    _p.ClubID = ClubID;
                    _p.PlayerID = PlayerID;
                    break;
                case "btnDelete":
                    _p.ActionType = "Delete";
                    _p.ClubID = ClubID;
                    //       _p.PlayerID = UPC.PlayerID;
                    _p.PlayerID = PlayerID;
                    break;
                case "btnGet":
                    _p.ActionType = "Get";
                    _p.ClubID = "0000 0000 0000 000C";
                    //                    _p.PlayerID = txtIDSearch.Text.ToString();
                    break;
                case "btnGetAll":
                    break;
            }
            try
            {
                var mainWnd = Application.Current.MainWindow as MainWindow;
                mainWnd.client.SendObject(_p.ToBytes());
                //Client.Send(FillTournament().ToBytes());
            }
            catch (Exception)
            {
                // xxx plz reconnect
            }
        }
        private void ChangeTab_Click(object sender, MouseButtonEventArgs e)
        {
            switch (((Border)sender).Name )
            {
                case "brd_1":// Search result
                    ChangeLeftBottomDockpanelBackground(1);
                    break;
                case "brd_2":// player Registrator
                    ChangeLeftBottomDockpanelBackground(2);
                   // Webcam();
                    break;
                case "brd_3": // Empty
                    ChangeLeftBottomDockpanelBackground(3);
                    break;
                case "brd_4": // Tournament Search
                    ChangeCenterTopDockpanelBackground(1);
                    break;
                case "brd_5": // Tournament Today
                    ChangeCenterTopDockpanelBackground(2);
                    break;
                case "brd_6": //  Predefined Setup
                    ChangeCenterTopDockpanelBackground(3);
                    break;
            }
        }

        private void ChangeCenterTopDockpanelBackground(int vborder)
        {
            // WFH.Visibility = Visibility.Hidden;
            brd_4.Background = (SolidColorBrush)FindResource("DeactiveText");
            brd_5.Background = (SolidColorBrush)FindResource("DeactiveText");
            brd_6.Background = (SolidColorBrush)FindResource("DeactiveText");
            switch (vborder)
            {
                case 1:
                    brd_4.Background = (LinearGradientBrush)FindResource("BorderBackground");
                    break;
                case 2:
                    brd_5.Background = (LinearGradientBrush)FindResource("BorderBackground");
                    break;
                case 3:
                    brd_6.Background = (LinearGradientBrush)FindResource("BorderBackground");
                    break;
            }
        }

        private void ChangeLeftBottomDockpanelBackground(int vborder)
        {
            brd_1.Background = (SolidColorBrush)FindResource("DeactiveText");
            brd_2.Background = (SolidColorBrush)FindResource("DeactiveText");
            brd_3.Background = (SolidColorBrush)FindResource("DeactiveText");
            grdPersonRegistrateNew.Visibility = Visibility.Hidden;
            grdPersonSearchResult.Visibility = Visibility.Hidden;
            switch (vborder)
            {
                case 1:
                    brd_1.Background = (LinearGradientBrush)FindResource("BorderBackground");
                    grdPersonSearchResult.Visibility = Visibility.Visible;
                    break;
                case 2:
                    brd_2.Background = (LinearGradientBrush)FindResource("BorderBackground");
                    grdPersonRegistrateNew.Visibility = Visibility.Visible;
                    break;
                case 3:
                    brd_3.Background = (LinearGradientBrush)FindResource("BorderBackground");
                    break;
            }
        }
        private Person FillPerson()
        {
            // First time registration
            Person _p = new Person
            {
                ClassType = "Person",
                FirstName = FirstName,
                LastName = LastName,
                Picture = Picture,
                Mobile = Mobile,
                EMail = EMail,
                Gender = Gender,
                Nationality = Nationality,
                Iso3166Name = Iso3166Name,
                BornDate = BornDate,
                RegDate = DateTime.Parse(DateTime.Now.ToShortDateString()),
                NickName = NickName,
                IsPlayerRemoved = false
            };

            // check if picture is taken
            if (_p.Picture == null)
            {

                _p.Picture = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject("profile")));
            }

            return _p;
        }

        private void TakePicture_Click(object sender, RoutedEventArgs e)
        {
            var mainWnd = Application.Current.MainWindow as MainWindow;
            Picture = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), mainWnd.frameHolder.Source);
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
