using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Image = System.Windows.Controls.Image;
using System.Text;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Mail;
using System.Collections;
using System.Resources;
using System.Reflection;
using NSOP_Tournament_Pro_Library;
using NSOP_Torunament_Pro_Library;
using NSOP_Torunament_Pro_Library.Properties;

namespace NSOP_Tournament_Pro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private readonly FilterInfoCollection CaptureDevice; // list of webcam
        //private VideoCaptureDevice FinalFrame;
  
        VideoCaptureDevice LocalWebCam;
        public FilterInfoCollection LoaclWebCamsCollection;

        //private readonly List<string> countries = new List<string>();
        //private readonly List<CultureInfo> cultures = new List<CultureInfo>();
        public Client client = new Client();
        public Person _adminPerson = new Person();
 
        void Cam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                System.Drawing.Image img = (Bitmap)eventArgs.Frame.Clone();

                MemoryStream ms = new MemoryStream();
                img.Save(ms, ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = ms;
                bi.EndInit();

                bi.Freeze();
                Dispatcher.BeginInvoke(new ThreadStart(delegate
                {
                    frameHolder.Source = new CroppedBitmap(bi, new Int32Rect(205, 125, 250, 250));
                    USS.Picture = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), frameHolder.Source);

                }));
            }
            catch (Exception ex)
            {
                string x = ex.ToString();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            Setup();
        }

        private void Setup()
        {
            // Visible Settings
            brdVerify.Visibility = Visibility.Hidden;
            brdLoggin.Visibility = Visibility.Visible;
            USS.Visibility = Visibility.Hidden;
            USS.Setup();
            // Value Settings
            brdClubPicture.Tag = "";


            // Populate
            cbxNationality.ItemsSource = DataAccess.GetCountries();
            cbxNationality.Text = "Norway [NO]";
            lstAvatar.ItemsSource = UpdateAvatars();

            // User Initialize
            uSubscription.FillSalesBoxes();
        }

        private List<Border> UpdateAvatars()
        {
            List<Border> _B = new List<Border>();
            Border _b1 = new Border
            {
                Name = "Avatar_1",
                Width = 75,
                Height = 75,
                Background = BrushBackground("Avatar_1")
            };
            _B.Add(_b1);
            Border _b2 = new Border
            {
                Name = "Avatar_2",
                Width = 75,
                Height = 75,
                Background = BrushBackground("Avatar_2")
            };
            _B.Add(_b2);
            Border _b3 = new Border
            {
                Name = "Avatar_3",
                Width = 75,
                Height = 75,
                Background = BrushBackground("Avatar_3")
            };
            _B.Add(_b3);
            return _B;
        }
        /// <summary>
        ///  "10.10.20.41:5960 90.149.221.94:53053"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            LocalWebCam.Stop();

        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoaclWebCamsCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            LocalWebCam = new VideoCaptureDevice(LoaclWebCamsCollection[0].MonikerString);
            LocalWebCam.NewFrame += new NewFrameEventHandler(Cam_NewFrame);

            LocalWebCam.Start();
        }
        private void Streambtn_Click(object sender, RoutedEventArgs e)
        {

        }
 
        private Tournament FillTournament()
        {
            Tournament _T = new Tournament
            {
                // xxx needs filling
              //  Picture = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), UPC.imgBilde.Source),
              //  Nationality = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), UPC.imgBilde.Source)
            };
            return _T;
        }

        private void Img_Loggin_PW_View_MouseEnter(object sender, MouseEventArgs e)
        {
            txt_Loggin_6.Text = txt_Loggin_PW_1.Password;
            txt_Loggin_6.Visibility = Visibility.Visible;
            txt_Loggin_PW_1.Visibility = Visibility.Hidden;
        }
        private void Img_Loggin_PW_View_MouseLeave(object sender, MouseEventArgs e)
        {
            txt_Loggin_6.Text = "";
            txt_Loggin_6.Visibility = Visibility.Hidden;
            txt_Loggin_PW_1.Visibility = Visibility.Visible;
        }
        private void PerformPersonAction(Person _p)
        {
            string _Header;
            string _Sub;
            string _Text;
            string _Fotter;
            switch (DataAccess.ParseEnum<DataAccess.ActionType>(_p.ActionType))
            {
                case DataAccess.ActionType.BadEMail:
                    _Header = "BAD EMAIL ADDRESS";
                    _Sub = "REGISTRATION FAILURE";
                    _Text = "EMAIL ADDRESS DOESN'T EXIST";
                    _Fotter = "MAKE CHANGES AND TRY AGAIN";
                    ShowErrorMessage(_Header, _Sub, _Text, _Fotter);
                    break;
                case DataAccess.ActionType.Verify:
                    brdVerify.Visibility = Visibility.Visible;
                    btn_Verify.Tag = _p.ClubID.ToString();
                    break;
                case DataAccess.ActionType.New:
                    break;
                case DataAccess.ActionType.ClubUpdate:
                    UpdateAdmin(_p);
                    UpdateAdminSite();
                    break;
                case DataAccess.ActionType.Delete:
                    break;
                case DataAccess.ActionType.Get:
                    break;
                case DataAccess.ActionType.Getall:
                    break;
                case DataAccess.ActionType.Registrer:
                    ShowAdminScreen();
                    break;
                case DataAccess.ActionType.LoggInn:
                    break;
                case DataAccess.ActionType.True:
                    // Loggin Player, procced to admin screen
                    ShowAdminScreen();
                    break;
                case DataAccess.ActionType.False:
                    // Player Cant logg inn
                    _Header = "PERSON NOT FOUND";
                    _Sub = "LOGGIN FAILURE";
                    _Text = "NO PERSON WAS FOUND WITH USERNAME OR PASSWORD";
                    _Fotter = "MAKE CHANGES AND TRY AGAIN";
                    ShowErrorMessage(_Header, _Sub, _Text, _Fotter);
                    break;
                case DataAccess.ActionType.PersonExist:
                    // Person already exist
                    _Header = "PERSON EXIST";
                    _Sub = "REGISTRATION FAILURE";
                    _Text = "PERSON WITH USERNAME ALREADY EXIST";
                    _Fotter = "MAKE CHANGES AND TRY AGAIN";
                    ShowErrorMessage(_Header, _Sub, _Text, _Fotter);
                    break;
                case DataAccess.ActionType.PersonCreated:
                    ShowAdminScreen();
                    break;
            }
        }
        public static System.Windows.Media.Brush BrushBackground(string fileName)
        {
            Image IMG = new Image();
            ImageBrush myBrush = new ImageBrush();
            if (fileName == "" || fileName == null)
            {
                fileName = "Avatar_1";
            }

            IMG.Source = DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject(fileName));
            myBrush.ImageSource = IMG.Source;
            return myBrush;
        }
        public static System.Windows.Media.Brush ByteToBrushBackground(byte[] clubPicture)
        {
            ImageBrush _myBrush = new ImageBrush();
            Image _IMG = new Image
            {
                Source = DataAccess.ConvertByteArrayToBitMapImage(clubPicture)
            };
            _myBrush.ImageSource = _IMG.Source;

            return _myBrush;
        }
        public static Image ImageBackground(string fileName)
        {
            Image IMG = new Image
            {
                Source = DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject(fileName))
            };
            return IMG;
        }

        // **************************   SCREENS
        private void ShowErrorMessage(string header, string sub, string text, string fotter)
        {
            brdErrorMessage.Visibility = Visibility.Visible;
            // start a timer to remove this message
            lbl_Error_Header.Content = header.ToUpper();
            lbl_Error_Fotter.Content = fotter.ToUpper();
            lbl_Error_Sub.Content = sub.ToUpper();
            lbl_Error_Text.Content = text.ToUpper();
            // populate message
        }
        private void ShowAdminScreen()
        {
            brdLoggin.Visibility = Visibility.Hidden;
            brdErrorMessage.Visibility = Visibility.Hidden;
            brdBackground.IsEnabled = true;
            UpdateAdminSite();

        }
        // **************************   UPDATES
        private void UpdateAdmin(Person _person)
        {
            // Populate Personalia
            _adminPerson.ClassType = "Person";
            _adminPerson.ActionType = _person.ActionType;
            _adminPerson.PlayerID = _person.PlayerID;
            _adminPerson.FirstName = _person.FirstName;
            _adminPerson.LastName = _person.LastName;
            _adminPerson.Picture = _person.Picture;
            _adminPerson.Mobile = _person.Mobile;
            _adminPerson.EMail = _person.EMail;
            _adminPerson.Gender = _person.Gender;
            _adminPerson.Nationality = _person.Nationality;
            _adminPerson.Iso3166Name = _person.Iso3166Name;
            _adminPerson.BornDate = _person.BornDate;
            _adminPerson.RegDate = _person.RegDate;
            _adminPerson.NickName = _person.NickName;
            _adminPerson.UserName = _person.UserName;
            _adminPerson.PassWord = _person.PassWord;
            _adminPerson.UserID = _person.UserID;
            _adminPerson.IsPlayerRemoved = _person.IsPlayerRemoved;
            _adminPerson.IsLoggedInn = _person.IsLoggedInn;
            _adminPerson.ClubID = _person.ClubID;
            _adminPerson.ClubName = _person.ClubName;
            _adminPerson.ClubPicture = _person.ClubPicture;

            // Populate Rank
            _adminPerson.ClubRank = _person.ClubRank;
            _adminPerson.ClubPoints = _person.ClubPoints;
            _adminPerson.ClubMembership = _person.ClubMembership;
            _adminPerson.ClubMembershipExpires = _person.ClubMembershipExpires;
            _adminPerson.ClubRegDate = _person.ClubRegDate;
            _adminPerson.NSOPID = _person.NSOPID;
            _adminPerson.NSOPRank = _person.NSOPRank;
            _adminPerson.NSOPPoints = _person.NSOPPoints;
            _adminPerson.NSOPMembership = _person.NSOPMembership;
            _adminPerson.NSOPMembershipExpires = _person.NSOPMembershipExpires;
            _adminPerson.NSOPRegDate = _person.NSOPRegDate;
            _adminPerson.NationalID = _person.NationalID;
            _adminPerson.NationalRank = _person.NationalRank;
            _adminPerson.NationalPoints = _person.NationalPoints;
            _adminPerson.NationalMembership = _person.NationalMembership;
            _adminPerson.NationalMembershipExpires = _person.NationalMembershipExpires;
            _adminPerson.NationalRegDate = _person.NationalRegDate;
            _adminPerson.WorldID = _person.WorldID;
            _adminPerson.WorldRank = _person.WorldRank;
            _adminPerson.WorldPoints = _person.WorldPoints;
            _adminPerson.WorldMembership = _person.WorldMembership;
            _adminPerson.WorldMembershipExpires = _person.WorldMembershipExpires;
            _adminPerson.WorldRegDate = _person.WorldRegDate;

            // Populate Lifetime
            _adminPerson.LifetimePlayed = _person.LifetimePlayed;
            _adminPerson.LifetimeWins = _person.LifetimeWins;
            _adminPerson.LifetimeFinalTables = _person.LifetimeFinalTables;
            _adminPerson.LifetimeCashed = _person.LifetimeCashed;
            _adminPerson.LifetimeBubbles = _person.LifetimeBubbles;
            _adminPerson.LifetimeFirstOuts = _person.LifetimeFirstOuts;
            _adminPerson.LifetimeSevenDeuces = _person.LifetimeSevenDeuces;
            _adminPerson.LifetimeBadBeats = _person.LifetimeBadBeats;
            _adminPerson.LifetimeTakeOuts = _person.LifetimeTakeOuts;
            _adminPerson.LifetimeHunted = _person.LifetimeHunted;
            _adminPerson.LifetimeMoneyEarned = _person.LifetimeMoneyEarned;
            _adminPerson.LifetimeMoneySpent = _person.LifetimeMoneySpent;

            // Populate Modul Access
            _adminPerson.AdminSubscribtion = _person.AdminSubscribtion;
            _adminPerson.WebSideSubscribtion = _person.WebSideSubscribtion;
            _adminPerson.LastLogin = _person.LastLogin;
            _adminPerson.Tokens = _person.Tokens;
            _adminPerson.TournamentCreatorQTY = _person.TournamentCreatorQTY;
            _adminPerson.PersonRegQTY = _person.PersonRegQTY;
            _adminPerson.TicketSaleQTY = _person.TicketSaleQTY;
            _adminPerson.BlindStructureQTY = _person.BlindStructureQTY;
            _adminPerson.PayoutStructureQTY = _person.PayoutStructureQTY;
            _adminPerson.PointStructureQTY = _person.PointStructureQTY;
            _adminPerson.AdvertiseModuleQTY = _person.AdvertiseModuleQTY;
            _adminPerson.TableManagerQTY = _person.TableManagerQTY;
            _adminPerson.StandUserName = _person.StandUserName;
            _adminPerson.StandPassWord = _person.StandPassWord;
        }
        private void UpdateAdminSite()
        {
            // Getting all picktures
            // Backround
            brdBackground.Background = BrushBackground("BkGround" + GetRandom.Instance.Next(7).ToString());
            // Header
            brdImageHeader_Left.Background = BrushBackground("NSOP");
            brdImageHeader_Right.Background = BrushBackground("NSOP");
            // **************
            // Club
            // **************
            lblClubID.Content = _adminPerson.ClubID;
            lblClubName.Content = _adminPerson.ClubName;
            txtEdit_9.Text = _adminPerson.ClubName;
            brdClubPicture.Background = BrushBackground(_adminPerson.ClubPicture);
            brdEditClubPicture.Background = BrushBackground(_adminPerson.ClubPicture);
            brdEditPicture.Background = ByteToBrushBackground(_adminPerson.Picture);
            txtEdit_1.Text = _adminPerson.FirstName;
            txtEdit_2.Text = _adminPerson.LastName;
            txtEdit_8.Text = _adminPerson.EMail;
            txtEdit_3.Text = _adminPerson.Mobile;
            dpBorn.Text = _adminPerson.BornDate.ToShortDateString();
            cbxNationality.Text = _adminPerson.Nationality + $" [{_adminPerson.Iso3166Name}]";
            txtEdit_4.Text = _adminPerson.NickName;
            txtEdit_6.Text = _adminPerson.StandUserName;
            txtEdit_7.Text = _adminPerson.StandPassWord;
            // **************
            // Subscriptions
            // **************
            // Administrator
            UModule_1.TopLeft = "ADMINISTRATOR";
            if (_adminPerson.AdminSubscribtion.ToUniversalTime() >= DateTime.Now.ToUniversalTime())
            {
                UModule_1.TopRight = "1";
            }
            else UModule_1.TopRight = "0";

            UModule_1.BottomLeft = _adminPerson.AdminSubscribtion.ToShortDateString();
            UModule_1.BottomRight = "RENEW ->>>";
            UModule_1.Picture = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject("Admin")));

            // Website
            UModule_2.TopLeft = "WEBSITE";
            if (_adminPerson.AdminSubscribtion.ToUniversalTime() >= DateTime.Now.ToUniversalTime())
            {
                UModule_2.TopRight = "1";
            }
            else UModule_2.TopRight = "0";

            UModule_2.BottomLeft = _adminPerson.WebSideSubscribtion.ToShortDateString();
            UModule_2.BottomRight = "RENEW ->>>";
            UModule_2.Picture = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject("Website")));

            // Tokens
            UModule_3.TopLeft = "TOKENS";
            UModule_3.TopRight = _adminPerson.Tokens.ToString();
            UModule_3.Center = _adminPerson.Tokens.ToString();
            UModule_3.BottomRight = "BUY ->>>";
            UModule_3.Picture = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject("Tokens")));

            // SubscrGadgetiptions
            UModule_4.Picture = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject("NSOP")));
            UModule_5.Picture = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject("NSOP")));
            UModule_6.Picture = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject("NSOP")));

            // ***********
            // Right side
            // ***********

            // Tournamnet
            // Website
            UModule_Right_1.TopLeft = "TOURNAMENTS";
            UModule_Right_1.TopRight = _adminPerson.TournamentCreatorQTY.ToString();
            UModule_Right_1.BottomRight = "VIEW / CREATE TOURNAMENT ->>>";
            UModule_Right_1.Background = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject("a3")));

            UModule_Right_2.TopLeft = "MENMERS";
            UModule_Right_2.TopRight = _adminPerson.PersonRegQTY.ToString();
            UModule_Right_2.BottomRight = "REISTRER _adminPerson ->>>";
            UModule_Right_2.Background = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject("members")));

            UModule_Right_3.TopLeft = "TICKET SALE";
            UModule_Right_3.TopRight = _adminPerson.TicketSaleQTY.ToString();
            UModule_Right_3.BottomRight = "SELL TICKETS ->>>";
            UModule_Right_3.Background = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject("ticket")));

            UModule_Right_4.TopLeft = "WALL OF FAME  |  RANK  |  LEAGUE";
            UModule_Right_4.BottomRight = "GO TO SITE ->>>";
            UModule_Right_4.Background = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject("a0")));
        }
        private void UpdateAdminPerson()
        {
            _adminPerson.ClassType = DataAccess.IdType.Person.ToString();
            _adminPerson.PlayerID = DataAccess.FillID(DataAccess.IdType.Person);
            _adminPerson.ClubID = DataAccess.FillID(DataAccess.IdType.Club);
            _adminPerson.ClubName = txt_Loggin_2.Text.ToString().ToUpper() + " POKER CLUB";
            _adminPerson.FirstName = txt_Loggin_1.Text.ToString();
            _adminPerson.LastName = txt_Loggin_2.Text.ToString();
            _adminPerson.EMail = txt_Loggin_3.Text.ToString();
            _adminPerson.Mobile = txt_Loggin_4.Text.ToString();
            _adminPerson.UserName = txt_Loggin_5.Text.ToString();
            _adminPerson.UserID = "";
            _adminPerson.PassWord = txt_Loggin_PW_1.Password.ToString();
            _adminPerson.Picture = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject("profile")));
            _adminPerson.ClubPicture = "Avatar_1";
            _adminPerson.StandUserName = txt_Loggin_1.Text.ToString().Substring(0, 1) + txt_Loggin_2.Text.ToString().Substring(0, 1) + DateTime.Now.Year.ToString();
            _adminPerson.StandPassWord = DataAccess.GetVerificationCode();
        }
        internal void UpdateData(byte[] buffer, string _packet)
        {
            switch (DataAccess.ParseEnum<DataAccess.PacketType>(_packet))
            {
                case DataAccess.PacketType.Person:
                    PerformPersonAction(new Person(buffer));
                    break;
                case DataAccess.PacketType.PersonList:
                    break;
                case DataAccess.PacketType.Tournament:
                    break;
                case DataAccess.PacketType.Blinds:
                    break;
                case DataAccess.PacketType.Payouts:
                    break;
                case DataAccess.PacketType.Points:
                    break;
                case DataAccess.PacketType.DataVerify:
                    break;
                case DataAccess.PacketType.Action:
                    break;
                case DataAccess.PacketType.Packet:
                    break;
            }
        }
        // **************************   CLICKS
        private void OpenAvatar_Click(object sender, MouseButtonEventArgs e)
        {
            lstAvatar.Visibility = Visibility.Visible;
        }
        private void Save_Click(object sender, EventArgs e)
        {
            //   if (UPC.imgBilde.Source != null)
            //   {
            //       Bitmap varBmp = new Bitmap(PictureBox1.Image);
            //       _ = new Bitmap(varBmp);
            //       varBmp.Dispose();
            //       varBmp = null;
            //       varBmp.Save(@"C:\a.png", ImageFormat.Png);
            //   }
            //   else
            //   { MessageBox.Show("null exception"); }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void SendToutnament_Click(object sender, RoutedEventArgs e)
        {
            client.SendObject(FillTournament().ToBytes());
        }
        private void Loggin_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            switch (b.Name.ToString().ToLower())
            {
                case "btn_loggin_loggin":
                    btn_Loggin.Content = "LOGG INN";
                    btn_Loggin_Loggin.Style = (Style)FindResource("ButtonPushed");
                    btn_Loggin_Loggin.Foreground = (SolidColorBrush)FindResource("ActiveText");

                    btn_Loggin_Registrer.Style = (Style)FindResource("ButtonUnPushed");
                    btn_Loggin_Registrer.Foreground = (SolidColorBrush)FindResource("DeactiveText");

                    stk_Loggin_1.Visibility = Visibility.Hidden;
                    stk_Loggin_2.Visibility = Visibility.Visible;
                    stk_Loggin_3.Visibility = Visibility.Hidden;

                    break;
                case "btn_loggin_registrer":
                    btn_Loggin.Content = "NEW ACCOUNT";
                    btn_Loggin_Registrer.Style = (Style)FindResource("ButtonPushed");
                    btn_Loggin_Registrer.Foreground = (SolidColorBrush)FindResource("ActiveText");

                    btn_Loggin_Loggin.Style = (Style)FindResource("ButtonUnPushed");
                    btn_Loggin_Loggin.Foreground = (SolidColorBrush)FindResource("DeactiveText");
                    stk_Loggin_1.Visibility = Visibility.Visible;
                    stk_Loggin_2.Visibility = Visibility.Visible;
                    stk_Loggin_3.Visibility = Visibility.Visible;
                    break;
            }
        }
        private void LogginRequest_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Content.ToString().ToUpper())
            {
                case "LOGG INN":
                    if (txt_Loggin_5.Text.Length >= 7 && txt_Loggin_PW_1.Password.Length >= 8)
                    {
                        //Check if Person exists
                        UpdateAdminPerson();
                        _adminPerson.ActionType = "LoggInn";
                        client.SendObject(_adminPerson.ToBytes());
                    }
                    break;
                case "NEW ACCOUNT":
                    if ((int)txt_Loggin_1.Tag == 1 && (int)txt_Loggin_2.Tag == 1 && (int)txt_Loggin_3.Tag == 1 && (int)txt_Loggin_4.Tag == 1 && (int)txt_Loggin_5.Tag == 1 && (int)txt_Loggin_PW_1.Tag == 1 && (int)txt_Loggin_PW_2.Tag == 1)
                    {
                        //Registrer Player
                        UpdateAdminPerson();
                        _adminPerson.ActionType = "Registrer";
                        client.SendObject(_adminPerson.ToBytes());
                    }
                    break;
            }
        }
        private void Info_Click(object sender, RoutedEventArgs e)
        {
            switch (((Button)sender as Button).Name)
            {
                case "Btn_Info_1":
                    break;
                case "Btn_Info_2":
                    break;
                case "Btn_Info_3":
                    break;
                case "Btn_Info_4":
                    break;
                case "Btn_Info_5":
                    break;
                case "Btn_Info_6":
                    break;
                case "Btn_Info_7":
                    break;
                case "Btn_Info_8":
                    break;
                case "Btn_Info_9":
                    break;
                case "Btn_Info_10":
                    break;
                case "Btn_Info_11":
                    break;
                case "Btn_Info_12":
                    break;
                case "Btn_Info_13":
                    break;
            }
        }
        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            switch (((Button)sender as Button).Name)
            {
                case "Btn_Buy_1":
                    break;
                case "Btn_Buy_2":
                    break;
                case "Btn_Buy_3":
                    break;
                case "Btn_Buy_4":
                    break;
                case "Btn_Buy_5":
                    break;
                case "Btn_Buy_6":
                    break;
                case "Btn_Buy_7":
                    break;
                case "Btn_Buy_8":
                    break;
                case "Btn_Buy_9":
                    break;
                case "Btn_Buy_10":
                    break;
                case "Btn_Buy_11":
                    break;
                case "Btn_Buy_12":
                    break;
                case "Btn_Buy_13":
                    break;
            }
        }
        private void Screen_Selection_Click(object sender, MouseButtonEventArgs e)
        {
            switch (((Border)sender as Border).Name.ToUpper())
            {
                case "BRD_1": // Torunament Creator
                    break;
                case "BRD_2": // Ticket Sale
                    break;
                case "BRD_3": // Person Registrator
                    break;
                case "BRD_4": // League / Rank / Wall Of Fame
                    break;
                case "BRD_5": // Buy Tokens
                    break;
            }
        }
        private void Btn_close_Click(object sender, RoutedEventArgs e)
        {
            brdLoggin.Visibility = Visibility.Hidden;
        }
        private void BtnTakePicture_Click(object sender, RoutedEventArgs e)
        {
            USS.Visibility = Visibility.Visible;
        }
        private void Verify_Click(object sender, RoutedEventArgs e)
        {
            UpdateAdminPerson();
            _adminPerson.ActionType = "VerifyOK";
            client.SendObject(_adminPerson.ToBytes());
        }
        private void Btn_ExitAll_Click(object sender, RoutedEventArgs e)
        {
            brdVerify.Visibility = Visibility.Hidden;
        }
        private void Gender_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            switch (b.Name.ToString())
            {
                case "btnMale":
                    btnMale.Tag = "MALE";
                    _adminPerson.Gender = "MALE";
                    btnMale.Style = (Style)FindResource("ButtonPushed");
                    btnMale.Foreground = (SolidColorBrush)FindResource("ActiveText");

                    btnFemale.Style = (Style)FindResource("ButtonUnPushed");
                    btnFemale.Foreground = (SolidColorBrush)FindResource("DeactiveText");
                    break;
                case "btnFemale":
                    btnMale.Tag = "FEMALE";
                    _adminPerson.Gender = "FEMALE";
                    btnFemale.Style = (Style)FindResource("ButtonPushed");
                    btnFemale.Foreground = (SolidColorBrush)FindResource("ActiveText");

                    btnMale.Style = (Style)FindResource("ButtonUnPushed");
                    btnMale.Foreground = (SolidColorBrush)FindResource("DeactiveText");
                    break;
            }
        }
        private void Admin_Click(object sender, MouseButtonEventArgs e)
        {
            switch (((Border)sender as Border).Name.ToString().ToUpper())
            {
                case "BRDEXIT": // exit program
                    this.Close();
                    break;
                case "BRDEDITCLUBDATA": // Enter Clubdata edit
                    brdEditClub.Visibility = Visibility.Visible;
                    break;
                case "BRDCLUBDATAEXIT": // Leave Clubdata Edit
                    brdEditClub.Visibility = Visibility.Hidden;
                    break;
            }
        }
        private void BtnEditClubTakePicture_Click(object sender, RoutedEventArgs e)
        {
            switch (((Button)sender as Button).Content.ToString().ToUpper())
            {
                case "EDIT PICTURE":
                    ((Button)sender as Button).Content = "TAKE PICTURE";
                    grdEditPicture.Visibility = Visibility.Hidden;
                    break;
                case "TAKE PICTURE":
                    ((Button)sender as Button).Content = "EDIT PICTURE";
                    grdEditPicture.Visibility = Visibility.Visible;
                    _adminPerson.Picture = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), frameHolder.Source);
                    brdEditPicture.Background = ByteToBrushBackground(_adminPerson.Picture);
                    break;
            }
        }
        private void BtnSaveClubChanges_Click(object sender, RoutedEventArgs e)
        {
            // Populate admin and save changes
            _adminPerson.ActionType = "ClubUpdate";
            client.SendObject(_adminPerson.ToBytes());

        }
        private void UModule_Click(object sender, MouseButtonEventArgs e)
        {
            switch (((UserModuleSimple)sender as UserModuleSimple).Name.ToString().ToUpper())
            {
                case "UMODULE_1": // Admin subscription
                    break;
                case "UMODULE_2": // Web subscription
                    break;
                case "UMODULE_3": // Buy Tokens
                    break;
                case "UMODULE_4": // Gadget 1
                    break;
                case "UMODULE_5": // Gadget 2
                    break;
                case "UMODULE_6": // Gadget 3
                    break;
            }
        }
        private void UModule_Right_Click(object sender, MouseButtonEventArgs e)
        {
            switch (((UserModuleSimple)sender as UserModuleSimple).Name.ToString().ToUpper())
            {
                case "UMODULE_RIGHT_1": // Create Tournament
                    break;
                case "UMODULE_RIGHT_2": // Create Person
                    break;
                case "UMODULE_RIGHT_3": // Ticket Sale
                    break;
                case "UMODULE_RIGHT_4": // Wall of fame , Rank
                    break;
            }
        }
        // **************************   SELECTION CHANGED
        private void LstAvatar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lstAvatar.Visibility = Visibility.Hidden;
        }
        private void TxtVerify_Code_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text != btn_Verify.Tag.ToString())
            {
                // You type the wrong code
                (sender as TextBox).Background = (SolidColorBrush)FindResource("ErrorBackground");
                (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ErrorText");
                btn_Verify.Visibility = Visibility.Hidden;
            }
            else
            {
                // Code is correct you can now verify
                (sender as TextBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
                (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
                btn_Verify.Visibility = Visibility.Visible;
            }
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
                _adminPerson.Nationality = cbxNationality.Tag.ToString();
                _adminPerson.Iso3166Name = _Iso3166Name;
            }
        }
        private void DpBorn_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            _adminPerson.BornDate = DateTime.Parse(dpBorn.Text.ToString());
        }
        private void Textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            switch ((sender as TextBox).Name.ToLower())
            {
                case "txtVerify_Code": // Verification new account
                    if ((sender as TextBox).Text != btn_Verify.Tag.ToString())
                    {
                        // You type the wrong code
                        (sender as TextBox).Background = (SolidColorBrush)FindResource("ErrorBackground");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ErrorText");
                        btn_Verify.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        // Code is correct you can now verify
                        (sender as TextBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
                        btn_Verify.Visibility = Visibility.Visible;
                    }
                    break;
                case "txt_loggin_1": // FirstName
                case "txtEdit_1": // FirstName
                    if ((sender as TextBox).Text.Length >= 2)
                    {
                        (sender as TextBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
                        (sender as TextBox).Tag = 1;
                        _adminPerson.FirstName = (sender as TextBox).Text.ToString();
                    }
                    else
                    {
                        (sender as TextBox).Background = (SolidColorBrush)FindResource("ErrorBackground");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ErrorText");
                        (sender as TextBox).Tag = 0;
                    }
                    break;
                case "txt_loggin_2": // LastName
                case "txtEdit_2": // LastName
                    if ((sender as TextBox).Text.Length >= 2)
                    {
                        (sender as TextBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
                        (sender as TextBox).Tag = 1;
                        _adminPerson.LastName = (sender as TextBox).Text.ToString();
                    }
                    else
                    {
                        (sender as TextBox).Background = (SolidColorBrush)FindResource("ErrorBackground");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ErrorText");
                        (sender as TextBox).Tag = 0;
                    }
                    break;
                case "txt_loggin_3": // EMail
                    if ((sender as TextBox).Text.Contains("@") && (sender as TextBox).Text.Contains(".") && (sender as TextBox).Text.Length >= 7)
                    {
                        (sender as TextBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
                        (sender as TextBox).Tag = 1;
                        _adminPerson.EMail = (sender as TextBox).Text.ToString();
                        _adminPerson.UserName = (sender as TextBox).Text.ToString();
                    }
                    else
                    {
                        (sender as TextBox).Background = (SolidColorBrush)FindResource("ErrorBackground");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ErrorText");
                        (sender as TextBox).Tag = 0;
                    }
                    try
                    {
                        txt_Loggin_5.Text = (sender as TextBox).Text;
                    }
                    catch (Exception)
                    {
                    }
                    break;
                case "txt_loggin_4": // Mobile
                case "txtEdit_3": // Mobile
                    if ((sender as TextBox).Text.Length >= 8 && (sender as TextBox).Text.Length <= 10)
                    {
                        (sender as TextBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
                        (sender as TextBox).Tag = 1;
                        _adminPerson.Mobile = (sender as TextBox).Text.ToString();
                    }
                    else
                    {
                        (sender as TextBox).Background = (SolidColorBrush)FindResource("ErrorBackground");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ErrorText");
                        (sender as TextBox).Tag = 0;
                    }
                    break;
                case "txt_loggin_5": // UserName
                    if ((sender as TextBox).Text.Length >= 7)
                    {
                        (sender as TextBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
                        (sender as TextBox).Tag = 1;
                        _adminPerson.UserName = (sender as TextBox).Text.ToString();
                        _adminPerson.EMail = (sender as TextBox).Text.ToString();
                    }
                    else
                    {
                        (sender as TextBox).Background = (SolidColorBrush)FindResource("ErrorBackground");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ErrorText");
                        (sender as TextBox).Tag = 0;
                    }
                    break;
                case "txtEdit_4": // Nickname
                    if ((sender as TextBox).Text.Length >= 0 && (sender as TextBox).Text.Length <= 20)
                    {
                        (sender as TextBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
                        (sender as TextBox).Tag = 1;
                        _adminPerson.NickName = (sender as TextBox).Text.ToString();
                    }
                    else
                    {
                        (sender as TextBox).Background = (SolidColorBrush)FindResource("ErrorBackground");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ErrorText");
                        (sender as TextBox).Tag = 0;
                    }
                    break;
                case "txtEdit_5": // Password
                    if ((sender as TextBox).Text.Length >= 7)
                    {
                        (sender as TextBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
                        (sender as TextBox).Tag = 1;
                        _adminPerson.PassWord = (sender as TextBox).Text.ToString();
                    }
                    else
                    {
                        (sender as TextBox).Background = (SolidColorBrush)FindResource("ErrorBackground");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ErrorText");
                        (sender as TextBox).Tag = 0;
                    }
                    break;
                case "txtEdit_6": // Stand Username
                    if ((sender as TextBox).Text.Length >= 0 && (sender as TextBox).Text.Length <= 12)
                    {
                        (sender as TextBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
                        (sender as TextBox).Tag = 1;
                        _adminPerson.StandUserName = (sender as TextBox).Text.ToString();
                    }
                    else
                    {
                        (sender as TextBox).Background = (SolidColorBrush)FindResource("ErrorBackground");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ErrorText");
                        (sender as TextBox).Tag = 0;
                    }
                    break;
                case "txtEdit_7": // Stand Password
                    if ((sender as TextBox).Text.Length >= 0 && (sender as TextBox).Text.Length <= 12)
                    {
                        (sender as TextBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
                        (sender as TextBox).Tag = 1;
                        _adminPerson.StandPassWord = (sender as TextBox).Text.ToString();
                    }
                    else
                    {
                        (sender as TextBox).Background = (SolidColorBrush)FindResource("ErrorBackground");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ErrorText");
                        (sender as TextBox).Tag = 0;
                    }
                    break;
            }
        }
        private void PW_Changed(object sender, RoutedEventArgs e)
        {
            switch ((sender as PasswordBox).Name)
            {
                case "txt_Loggin_PW_1": // PAssword
                    if ((sender as PasswordBox).Password.Length >= 8)
                    {
                        (sender as PasswordBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
                        (sender as PasswordBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
                        (sender as PasswordBox).Tag = 1;
                        _adminPerson.PassWord = txt_Loggin_PW_1.Password.ToString();
                    }
                    else
                    {
                        (sender as PasswordBox).Background = (SolidColorBrush)FindResource("ErrorBackground");
                        (sender as PasswordBox).Foreground = (SolidColorBrush)FindResource("ErrorText");
                        (sender as PasswordBox).Tag = 0;
                    }
                    break;
                case "txt_Loggin_PW_2": // validated password
                    if ((sender as PasswordBox).Password == txt_Loggin_PW_1.Password)
                    {
                        (sender as PasswordBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
                        (sender as PasswordBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
                        (sender as PasswordBox).Tag = 1;
                    }
                    else
                    {
                        (sender as PasswordBox).Background = (SolidColorBrush)FindResource("ErrorBackground");
                        (sender as PasswordBox).Foreground = (SolidColorBrush)FindResource("ErrorText");
                        (sender as PasswordBox).Tag = 0;
                    }
                    break;
            }
        }
        // **************************   LOST / GOT FOCUS
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
        private void PW_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as PasswordBox).Focus();
            (sender as PasswordBox).Clear();
            (sender as PasswordBox).Background = (SolidColorBrush)FindResource("ActiveText");
            (sender as PasswordBox).Foreground = (SolidColorBrush)FindResource("DeactiveText");
        }
        private void PW_LostFocus(object sender, RoutedEventArgs e)
        {
            (sender as PasswordBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
            (sender as PasswordBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
        }
        // **************************   PREVIEW
        private void PW_Preview(object sender, TextCompositionEventArgs e)
        {
            CheckIsNumeric(e);
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

    }
}
