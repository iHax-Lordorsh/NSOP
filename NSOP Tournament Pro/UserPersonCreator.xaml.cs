using NSOP_Tournament_Pro_Library;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
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
    /// Interaction logic for UserPersonCreator.xaml
    /// </summary>
    public partial class UserPersonCreator : UserControl
    {

        private Person _person = new Person();
        public Person PersonData { get => _person; set => _person = value; }

        public UserPersonCreator()
        {
            InitializeComponent();
            Setup();
        }

        private void Setup()
        {
            cbxNationality.ItemsSource = DataAccess.GetCountries();
            cbxNationality.Text = "Norway [NO]";
        }

        private void BtnTakePicture_Click(object sender, RoutedEventArgs e)
        {
            switch (((Button)sender as Button).Content.ToString().ToUpper())
            {
                case "EDIT PICTURE":
                    ((Button)sender as Button).Content = "SAVE PERSON";
                    grdEditPicture.Visibility = Visibility.Hidden;
                    break;
                case "SAVE PERSON":
                    ((Button)sender as Button).Content = "EDIT PICTURE";
                    grdEditPicture.Visibility = Visibility.Visible;
                    PersonData.Picture = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), frameHolder.Source);
                    brdEditPicture.Background = ByteToBrushBackground(PersonData.Picture);
                    break;
            }
        }

        private System.Windows.Media.Brush ByteToBrushBackground(byte[] picture)
        {
            ImageBrush _myBrush = new ImageBrush();
            System.Windows.Controls.Image _IMG = new System.Windows.Controls.Image
            {
                Source = DataAccess.ConvertByteArrayToBitMapImage(picture)
            };
            _myBrush.ImageSource = _IMG.Source;

            return _myBrush;
        }

        private void CbxNationality_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxNationality.SelectedIndex != -1)
            {
                string _Iso3166Name = cbxNationality.SelectedItem.ToString().Substring(cbxNationality.SelectedItem.ToString().Length - 3, 2);
                CultureInfo _Culture = new CultureInfo(_Iso3166Name.ToLower());
                try
                {
                    imgNationality.Source = DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject(_Iso3166Name.ToLower(), _Culture));
                }
                catch (Exception)
                {
                }
                cbxNationality.Tag = cbxNationality.SelectedItem.ToString().Substring(0, cbxNationality.SelectedItem.ToString().Length - 4);
                imgNationality.Tag = _Iso3166Name;
                PersonData.Nationality = cbxNationality.Tag.ToString();
                PersonData.Iso3166Name = _Iso3166Name;
            }
        }

        private void Gender_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            GenderSelection(b.Name.ToString());
        }

        private void GenderSelection(string gender)
        {
            switch (gender)
            {
                case "btnMale":
                case "MALE":
                    btnMale.Tag = "MALE";
                    PersonData.Gender = "MALE";
                    btnMale.Style = (Style)FindResource("ButtonPushed");
                    btnMale.Foreground = (SolidColorBrush)FindResource("ActiveText");

                    btnFemale.Style = (Style)FindResource("ButtonUnPushed");
                    btnFemale.Foreground = (SolidColorBrush)FindResource("DeactiveText");
                    break;
                case "btnFemale":
                case "FEMALE":
                    btnMale.Tag = "FEMALE";
                    PersonData.Gender = "FEMALE";
                    btnFemale.Style = (Style)FindResource("ButtonPushed");
                    btnFemale.Foreground = (SolidColorBrush)FindResource("ActiveText");

                    btnMale.Style = (Style)FindResource("ButtonUnPushed");
                    btnMale.Foreground = (SolidColorBrush)FindResource("DeactiveText");
                    break;
            }
        }

        private void DpBorn_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                PersonData.BornDate = DateTime.Parse(dpBorn.Text.ToString());
            }
            catch (Exception)
            {
            }
        }

        private void Textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            switch ((sender as TextBox).Name)
            {
                case "txt_Edit_1": // FirstName
                    if ((sender as TextBox).Text.Length >= 2)
                    {
                        (sender as TextBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
                        (sender as TextBox).Tag = 1;
                        PersonData.FirstName = (sender as TextBox).Text;
                    }
                    else
                    {
                        (sender as TextBox).Background = (SolidColorBrush)FindResource("ErrorBackground");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ErrorText");
                        (sender as TextBox).Tag = 0;
                    }
                    break;
                case "txt_Edit_2": // LastName
                    if ((sender as TextBox).Text.Length >= 2)
                    {
                        (sender as TextBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
                        (sender as TextBox).Tag = 1;
                        PersonData.LastName = (sender as TextBox).Text;
                    }
                    else
                    {
                        (sender as TextBox).Background = (SolidColorBrush)FindResource("ErrorBackground");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ErrorText");
                        (sender as TextBox).Tag = 0;
                    }
                    break;
                case "txt_Edit_8": // EMail
                    if ((sender as TextBox).Text.Contains("@") && (sender as TextBox).Text.Contains(".") && (sender as TextBox).Text.Length >= 7)
                    {
                        (sender as TextBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
                        (sender as TextBox).Tag = 1;

                    }
                    else
                    {
                        (sender as TextBox).Background = (SolidColorBrush)FindResource("ErrorBackground");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ErrorText");
                        (sender as TextBox).Tag = 0;
                    }
                    break;
                case "txt_Edit_5": // EMail
                    if ((sender as TextBox).Text.ToString().ToUpper() == txt_Edit_8.Text.ToString().ToUpper())
                    {
                        (sender as TextBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
                        (sender as TextBox).Tag = 1;
                        PersonData.EMail = (sender as TextBox).Text;
                        var mainWnd = Application.Current.MainWindow as MainWindow;
                        mainWnd.CheckIfPersonExist(PersonData);
                    }
                    else
                    {
                        (sender as TextBox).Background = (SolidColorBrush)FindResource("ErrorBackground");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ErrorText");
                        (sender as TextBox).Tag = 0;
                    }
                    break;
                case "txt_Edit_3": // Mobile
                    if ((sender as TextBox).Text.Length >= 8 && (sender as TextBox).Text.Length <= 10)
                    {
                        (sender as TextBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
                        (sender as TextBox).Tag = 1;
                        PersonData.Mobile = (sender as TextBox).Text;
                    }
                    else
                    {
                        (sender as TextBox).Background = (SolidColorBrush)FindResource("ErrorBackground");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ErrorText");
                        (sender as TextBox).Tag = 0;
                    }
                    break;
                case "txt_Edit_4": // Nickname
                    if ((sender as TextBox).Text.Length >= 0 && (sender as TextBox).Text.Length <= 20)
                    {
                        (sender as TextBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
                        (sender as TextBox).Tag = 1;
                        PersonData.NickName = (sender as TextBox).Text;
                    }
                    else
                    {
                        (sender as TextBox).Background = (SolidColorBrush)FindResource("ErrorBackground");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ErrorText");
                        (sender as TextBox).Tag = 0;
                    }
                    break;
            }
            CheckIfAllOK();
        }

        private void CheckIfAllOK()
        {
            try
            {
                if (Convert.ToInt32(txt_Edit_1.Tag.ToString()) == 1 &&
                    Convert.ToInt32(txt_Edit_2.Tag.ToString()) == 1 &&
                    Convert.ToInt32(txt_Edit_3.Tag.ToString()) == 1 &&
                    Convert.ToInt32(txt_Edit_5.Tag.ToString()) == 1 &&
                    Convert.ToInt32(txt_Edit_8.Tag.ToString()) == 1 &&
                    dpBorn.Text.Length > 8 &&
                    PersonData.Gender.Length > 0 &&
                    PersonData.Nationality.Length > 0)
                {
                    btnSavePerson.Visibility = Visibility.Visible;
                }
                else btnSavePerson.Visibility = Visibility.Hidden;
            }
            catch (Exception)
            {
            }
        }

        private void Textbox_LostFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
            (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
        }

        private void Button_Click(object sender, MouseButtonEventArgs e)
        {

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
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Focus();
            //(sender as TextBox).Clear();
            (sender as TextBox).Background = (SolidColorBrush)FindResource("ActiveText");
            (sender as TextBox).Foreground = (SolidColorBrush)FindResource("DeactiveText");
        }

        private void BtnSavePerson_Click(object sender, RoutedEventArgs e)
        {
            var mainWnd = Application.Current.MainWindow as MainWindow;
            mainWnd.SaveNewPerson(PersonData);
        }

        private void Chk_ClubID_Checked(object sender, RoutedEventArgs e)
        {
            var mainWnd = Application.Current.MainWindow as MainWindow;
            txt_Edit_6.Text = mainWnd._adminPerson.ClubID;
        }

        internal void UpdatePerson(Person person)
        {
            txt_Edit_1.Text = person.FirstName;
            txt_Edit_2.Text = person.LastName;
            txt_Edit_3.Text = person.Mobile;
            txt_Edit_5.Text = person.EMail;
            txt_Edit_4.Text = person.NickName;
            dpBorn.Text = person.BornDate.ToShortDateString();
            GenderSelection(person.Gender);
            cbxNationality.Text = person.Nationality + $"[{person.Iso3166Name}]";
            brdEditPicture.Background = ByteToBrushBackground(person.Picture);
            if (person.ClubID.Length>0)
            {
                chk_ClubID.IsChecked = true;
                txt_Edit_6.Text = person.ClubID;
            }
        }

        private void chk_ClubID_Unchecked(object sender, RoutedEventArgs e)
        {
            txt_Edit_6.Text = "";
        }
    }
}
