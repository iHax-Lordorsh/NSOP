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
using NSOP_Torunament_cro_Library;
using NSOP_Tournament_Pro_Library;

namespace NSOP_Tournament_Pro
{
    /// <summary>
    /// Interaction logic for UserSalesStand.xaml
    /// </summary>
    public partial class UserSalesStand : UserControl
    {
        private Club _ClubData = new Club();
        public Club ClubData { get => _ClubData; set => _ClubData = value; }

        private Person _PersonData = new Person();
        public Person PersonData { get => _PersonData; set => _PersonData = value; }
               
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
                    PersonData.Gender = "MALE";
                    btnMale.Style = (Style)FindResource("ButtonPushed");
                    btnMale.Foreground = (SolidColorBrush)FindResource("ActiveText");

                    btnFemale.Style = (Style)FindResource("ButtonUnPushed");
                    btnFemale.Foreground = (SolidColorBrush)FindResource("DeactiveText");
                    break;
                case "btnFemale":
                    PersonData.Gender = "FEMALE";
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
                imgNationality.Source = DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject(_Iso3166Name.ToLower(), _Culture));
                PersonData.Nationality = cbxNationality.SelectedItem.ToString().Substring(0, cbxNationality.SelectedItem.ToString().Length - 4);
                PersonData.Iso3166Name = _Iso3166Name;
            }
        }
        private void Birthday_Changed(object sender, SelectionChangedEventArgs e)
        {
            PersonData.BornDate = DateTime.Parse((sender as DatePicker).Text.ToString());
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
                        PersonData.FirstName = (sender as TextBox).Text.ToString();
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
                        PersonData.LastName = (sender as TextBox).Text.ToString();
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
                            PersonData.Mobile = (sender as TextBox).Text.Substring(0, 10);
                        }
                        else PersonData.Mobile = (sender as TextBox).Text.ToString();
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
                        PersonData.EMail = (sender as TextBox).Text.ToString();
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
                    PersonData.NickName = (sender as TextBox).Text.ToString();
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
                FirstName = PersonData.FirstName,
                LastName = PersonData.LastName,
                Picture = PersonData.Picture,
                Mobile = PersonData.Mobile,
                EMail = PersonData.EMail,
                Gender = PersonData.Gender,
                Nationality = PersonData.Nationality,
                Iso3166Name = PersonData.Iso3166Name,
                BornDate = PersonData.BornDate,
                RegDate = DateTime.Parse(DateTime.Now.ToShortDateString()),
                NickName = PersonData.NickName,
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
            PersonData.Picture = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), mainWnd.frameHolder.Source);
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
