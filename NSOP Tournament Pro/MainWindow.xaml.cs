﻿using AForge.Video;
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
using NSOP_Torunament_cro_Library;

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
        public Club _adminClub = new Club();
        private string _Header;
        private string _Sub;
        private string _Text;
        private string _Fotter;

        private List<UserItemList> UTList;// = new List<UserItemList>();
        public string VerificationCode { get; set; }
        public DataAccess.Request VerificationLocation { get; set; }

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
                    uPersonCreate.frameHolder.Source = frameHolder.Source;

                }));
            }
            catch (Exception ex)
            {
                string x = ex.ToString();
            }
        }

        private List<Border> listAvatar = new List<Border>();
        private readonly List<string> listNationalities = new List<string>();

        public MainWindow()
        {
            InitializeComponent();

            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ru-RU");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en"); 


            Loaded += MainWindow_Loaded;
            Setup();
        }

        private void Setup()
        {
            // Visible Settings
            brd_ForgotPassword.Visibility = Visibility.Hidden;
            brd_Verify.Visibility = Visibility.Hidden;
            brdLoggin.Visibility = Visibility.Visible;
            btn_LogIn.Visibility = Visibility.Visible;
            btn_NewAccount.Visibility = Visibility.Hidden;
            brd_ResetPassword.Visibility = Visibility.Hidden;
            USS.Visibility = Visibility.Hidden;
            USS.Setup();
            // Value Settings
            brdClubPicture.Tag = "";
            brdEditClubPicture.Tag = "";

            // Populate
            cbxNationality.ItemsSource = DataAccess.GetCountries();
            cbxNationality.Text = "Norway [NO]";
            listAvatar = UpdateAvatars();
            lstAvatar.ItemsSource = listAvatar;
            lstAvatar.SelectedIndex = -1;
            // User Initialize

            brdEditClub.Visibility = Visibility.Hidden;
            uProductView.Visibility = Visibility.Hidden;
            uAdminCreator.Visibility = Visibility.Hidden;

            UTList = new List<UserItemList>();
            lstClubAction.ItemsSource = UTList;
            lstClubAction.Items.Refresh();
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
            txt_Login_6.Text = txt_Login_PW_1.Password;
            txt_Login_6.Visibility = Visibility.Visible;
            txt_Login_PW_1.Visibility = Visibility.Hidden;
        }

        private void Img_Loggin_PW_View_MouseLeave(object sender, MouseEventArgs e)
        {
            txt_Login_6.Text = "";
            txt_Login_6.Visibility = Visibility.Hidden;
            txt_Login_PW_1.Visibility = Visibility.Visible;
        }

        internal void ShowAllProduct(Product product)
        {
            uProductView.ShowBroduct(product);
        }

        internal void GetStartProducts()
        {
            client.SendObject(UpdateCommunicationPacket(DataAccess.Request.GetStartProduct, new Product().ToBytes(), DataAccess.ClassType.Product, ""));
        }

        internal void SaveNewProduct()
        {
            client.SendObject(UpdateCommunicationPacket(DataAccess.Request.SaveNew, PopulateProduct(uAdminCreator).ToBytes(), DataAccess.ClassType.Product, ""));
        }

        private Product PopulateProduct(UserAdminCreator vAC)
        {
            Product _p = new Product
            {
                ID = vAC.ID,
                ProductName = vAC.ProductName,
                Picture = vAC.Picture,
                Information = vAC.Information,
                Description = vAC.Description,
                Price = vAC.Price,
                Discount = Convert.ToInt16(vAC.ProductQTY[8].SelectedItem),
                Quantity = Convert.ToInt16(vAC.ProductQTY[7].SelectedItem),
                ID_1 = vAC.ProductID[1],
                ID_2 = vAC.ProductID[2],
                ID_3 = vAC.ProductID[3],
                ID_4 = vAC.ProductID[4],
                ID_5 = vAC.ProductID[5],
                ID_6 = vAC.ProductID[6],
                Qty_1 = Convert.ToInt16(vAC.ProductQTY[1].SelectedItem),
                Qty_2 = Convert.ToInt16(vAC.ProductQTY[2].SelectedItem),
                Qty_3 = Convert.ToInt16(vAC.ProductQTY[3].SelectedItem),
                Qty_4 = Convert.ToInt16(vAC.ProductQTY[4].SelectedItem),
                Qty_5 = Convert.ToInt16(vAC.ProductQTY[5].SelectedItem),
                Qty_6 = Convert.ToInt16(vAC.ProductQTY[6].SelectedItem),
                ProductType = vAC.ProductType
            };
            if (vAC.Expires != "")
            {
                _p.Expires = vAC.Expires;
            }
            else _p.Expires = "";
            if (vAC.IsStartDate)
            {
                _p.StartDate = vAC.StartDate;
            };
            if (vAC.IsEndDate)
            {
                _p.EndDate = vAC.EndDate;
            };
            return _p;
        }

        //private void PerformPersonAction(Person _p)
        //{
        //    string _Header;
        //    string _Sub;
        //    string _Text;
        //    string _Fotter;
        //    UpdateAdmin(_p);
        //    switch (DataAccess.ParseEnum<DataAccess.Request>(_p.ActionType))
        //    {
        //        case DataAccess.Request.ResetPassword:
        //            break;
        //        case DataAccess.Request.BadEMail:
        //            _Header = "BAD EMAIL ADDRESS";
        //            _Sub = "REGISTRATION FAILURE";
        //            _Text = "EMAIL ADDRESS DOESN'T EXIST";
        //            _Fotter = "MAKE CHANGES AND TRY AGAIN";
        //            ShowErrorMessage(_Header, _Sub, _Text, _Fotter);
        //            break;
        //        case DataAccess.Request.Verify:
        //            brdVerify.Visibility = Visibility.Visible;
        //            btn_Verify.Tag = _p.ClubID.ToString();
        //            break;
        //        case DataAccess.Request.New:
        //            break;
        //        case DataAccess.Request.ClubUpdate:
        //            //UpdateAdminSite();
        //            break;
        //        case DataAccess.Request.Delete:
        //            break;
        //        case DataAccess.Request.Get:
        //            break;
        //        case DataAccess.Request.Getall:
        //            break;
        //        case DataAccess.Request.Registrer:
        //            //ShowAdminScreen();
        //            break;
        //        case DataAccess.Request.LogIn:
        //            break;
        //        case DataAccess.Request.LoggInOK:
        //            // Loggin Player, procced to admin screen
        //            //ShowAdminScreen();
        //            break;
        //        case DataAccess.Request.LoggInFailed:
        //            // Player Cant logg inn
        //            _Header = "PERSON NOT FOUND";
        //            _Sub = "LOGGIN FAILURE";
        //            _Text = "NO PERSON WAS FOUND WITH USERNAME OR PASSWORD";
        //            _Fotter = "MAKE CHANGES AND TRY AGAIN";
        //            ShowErrorMessage(_Header, _Sub, _Text, _Fotter);
        //            break;
        //        case DataAccess.Request.PersonExist:
        //            // Person already exist
        //            _Header = "PERSON EXIST";
        //            _Sub = "REGISTRATION FAILURE";
        //            _Text = "PERSON WITH USERNAME ALREADY EXIST";
        //            _Fotter = "MAKE CHANGES AND TRY AGAIN";
        //            ShowErrorMessage(_Header, _Sub, _Text, _Fotter);
        //            break;
        //        case DataAccess.Request.PersonCreated:
        //            //ShowAdminScreen();
        //            break;
        //        case DataAccess.Request.PersonUpdate:
        ///////////// xxx remove
        //            //ShowAdminScreen();
        //            break;
        //        case DataAccess.Request.VerifyOK:
        //            break;
        //    }
        //}

        internal void ResetVerification(CommunicationManager cp)
        {
            brd_ForgotPassword.Visibility = Visibility.Hidden;
            brd_Verify.Visibility = Visibility.Visible;
            VerificationCode = cp.Info;
            VerificationLocation = cp.Request;
        }
      
        internal void PersonExist()
        {
            _Header = "ACCOUNT CREATION FAILED";
            _Sub = "PERSON ALREADY EXIST";
            _Text = "LOG IN TO YOUR ACCOUNT OR TRY TO RESET YOUR PASSWORD.";
            _Fotter = "MAKE CHANGES AND TRY AGAIN";
            ShowErrorMessage(_Header, _Sub, _Text, _Fotter);
        }

        internal void LoggInFailed()
        {
            _Header = "PERSON NOT FOUND";
            _Sub = "LOGGIN FAILURE";
            _Text = "NO PERSON WAS FOUND WITH USERNAME OR PASSWORD";
            _Fotter = "MAKE CHANGES AND TRY AGAIN";
            ShowErrorMessage(_Header, _Sub, _Text, _Fotter);
        }

        internal void NewAccountVerify(string info, DataAccess.Request request)
        {
            brd_Verify.Visibility = Visibility.Visible;
            brd_ForgotPassword.Visibility = Visibility.Hidden;
            brd_ResetPassword.Visibility = Visibility.Hidden;
            VerificationCode = info;
            VerificationLocation = request;
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

        internal void AddClubAction(string topLeft, string topCenter, string topRight,byte[] picture, string centerTop, string centerMiddle, string centerBottom, byte[] smallPicture, string bottomLeft, string BottomCenter, string BottomRight)
        {
            UserItemList _uTL = new UserItemList
            {
                TopLeft = topLeft,
                TopCenter = topCenter,
                TopRight = topRight,
                CenterTop = centerTop,
                CenterMiddle = centerMiddle,
                CenterBottom = centerBottom,
                BottomLeft = bottomLeft,
                BottomCenter = BottomCenter,
                BottomRight = BottomRight,
                Picture = picture,
                PictureSmall = smallPicture
            };
            _uTL.Width = 485;
            UTList.Add(_uTL);
            lstClubAction.Items.Refresh();
        }

        internal void ShowAdminScreen(Person person)
        {
            UpdateAdmin(person);
            UpdateAdminSite();
            brd_Verify.Visibility = Visibility.Hidden;
            brd_ResetPassword.Visibility = Visibility.Hidden;
            brdLoggin.Visibility = Visibility.Hidden;
            brdErrorMessage.Visibility = Visibility.Hidden;
            brdBackground.IsEnabled = true;
        }
        internal void ShowAdminCreator(Product product)
        {
            uAdminCreator.ShowProductCreator(product);
        }
        public void GetAllProduct()
        {
            // Get all Produxt
            client.SendObject(UpdateCommunicationPacket(DataAccess.Request.GetProductsAll, PopulateProduct(uAdminCreator).ToBytes(), DataAccess.ClassType.Product, ""));

        }
        public void ShowSubscriptionProduct()
        {
            // Get all Produxt
            client.SendObject(UpdateCommunicationPacket(DataAccess.Request.GetProductSubscription, PopulateProduct(uAdminCreator).ToBytes(), DataAccess.ClassType.Product, ""));
        }
        public void ShowTokenProduct()
        {
            // Get all Produxt
            client.SendObject(UpdateCommunicationPacket(DataAccess.Request.GetProductToken, PopulateProduct(uAdminCreator).ToBytes(), DataAccess.ClassType.Product, ""));
        }
        // **************************   UPDATES
        private void UpdateAdmin(Person _person)
        {
            // Populate Personalia
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
            _adminPerson.PassWord = _person.PassWord;
            _adminPerson.UserID = _person.UserID;
            _adminPerson.IsPlayerRemoved = _person.IsPlayerRemoved;
            _adminPerson.IsLoggedInn = _person.IsLoggedInn;

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
            _adminPerson.LastLogin = _person.LastLogin;
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
            lblClubID.Content = _adminClub.ID;
            lblClubName.Content = _adminClub.Name;
            txt_Edit_9.Text = _adminClub.Name;
            GenderSelection(_adminPerson.Gender);
            brdClubPicture.Background = BrushBackground(_adminClub.Name);
            brdEditClubPicture.Background = BrushBackground(_adminClub.Name);
            //brdEditPicture.Background = ByteToBrushBackground(_adminPerson.Picture);
            txt_Edit_1.Text = _adminPerson.FirstName;
            txt_Edit_2.Text = _adminPerson.LastName;
            txt_Edit_8.Text = _adminPerson.EMail;
            txt_Edit_3.Text = _adminPerson.Mobile;
            dpBorn.Text = _adminPerson.BornDate.ToShortDateString();
            int _teller = -1;
            foreach (var item in listNationalities)
            {
                _teller++;
                if (item == _adminPerson.Nationality + $" [{_adminPerson.Iso3166Name}]")
                {
                    cbxNationality.SelectedIndex = _teller;
                }
            }
           
            txt_Edit_4.Text = _adminPerson.NickName;
            txt_Edit_6.Text = _adminClub.StandUserName;
            txt_Edit_7.Text = _adminClub.StandPassWord;
            // **************
            // Subscriptions
            // **************
            // Administrator
            UModule_1.TopLeft = "ADMINISTRATOR";
            if (_adminClub.AdminSubscribtion.ToUniversalTime() >= DateTime.Now.ToUniversalTime())
            {
                UModule_1.TopRight = "1";
            }
            else UModule_1.TopRight = "0";

            UModule_1.BottomLeft = _adminClub.AdminSubscribtion.ToShortDateString();
            UModule_1.BottomRight = "RENEW ->>>";
            UModule_1.Picture = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject("i_Admin")));

            // Website
            UModule_2.TopLeft = "WEBSITE";
            if (_adminClub.AdminSubscribtion.ToUniversalTime() >= DateTime.Now.ToUniversalTime())
            {
                UModule_2.TopRight = "1";
            }
            else UModule_2.TopRight = "0";

            UModule_2.BottomLeft = _adminClub.WebSideSubscribtion.ToShortDateString();
            UModule_2.BottomRight = "RENEW ->>>";
            UModule_2.Picture = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject("i_Website")));

            // Tokens
            UModule_3.TopLeft = "TOKENS";
            UModule_3.TopRight = _adminClub.Tokens.ToString();
            UModule_3.Center = _adminClub.Tokens.ToString();
            UModule_3.BottomRight = "BUY ->>>";
            UModule_3.Picture = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject("i_Tokens")));

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
            UModule_Right_1.TopRight = _adminClub.TournamentCreatorQTY.ToString();
            UModule_Right_1.BottomRight = "VIEW / CREATE TOURNAMENT ->>>";
            UModule_Right_1.Background = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject("a3")));

            UModule_Right_2.TopLeft = "MEMBERS";
            UModule_Right_2.TopRight = _adminClub.PersonRegQTY.ToString();
            UModule_Right_2.BottomRight = "VIEW / CREATE TOURNAMENT ->>>";
            UModule_Right_2.Background = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject("p_Members")));

            UModule_Right_3.TopLeft = "TICKET SALE";
            UModule_Right_3.TopRight = _adminClub.TicketSaleQTY.ToString();
            UModule_Right_3.BottomRight = "SELL TICKETS ->>>";
            UModule_Right_3.Background = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject("p_Ticket")));

            UModule_Right_4.TopLeft = "WALL OF FAME  |  RANK  |  LEAGUE";
            UModule_Right_4.BottomRight = "GO TO SITE ->>>";
            UModule_Right_4.Background = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject("a0")));
        }
        public void UpdateAdminPerson()
        {
            //_adminPerson.ClubName = txt_Login_2.Text.ToString().ToUpper() + " POKER CLUB";
            _adminPerson.FirstName = txt_Login_1.Text.ToString();
            _adminPerson.LastName = txt_Login_2.Text.ToString();
            _adminPerson.EMail = txt_Login_5.Text.ToString();
            _adminPerson.Mobile = txt_Login_4.Text.ToString();
            _adminPerson.UserID = "";
            _adminPerson.PassWord = txt_Login_PW_1.Password.ToString();
            _adminPerson.Picture = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), DataAccess.ToBitmapImage((Bitmap)Properties.Resources.ResourceManager.GetObject("profile")));
           // _adminPerson.ClubPicture = "Avatar_1";
          //  _adminPerson.StandUserName = txt_Login_1.Text.ToString().Substring(0, 1) + txt_Login_2.Text.ToString().Substring(0, 1) + DateTime.Now.Year.ToString();
          //  _adminPerson.StandPassWord = DataAccess.GetVerificationCode();
        }
        public void UpdateAdminPerson(Person person)
        {
            _adminPerson.PlayerID = person.PlayerID;
          //  _adminPerson.ClubID = person.ClubID;
          //  _adminPerson.ClubName = person.ClubName;
            _adminPerson.FirstName = person.FirstName;
            _adminPerson.LastName = person.LastName;
            _adminPerson.EMail = person.EMail;
            _adminPerson.Mobile = person.Mobile;
            _adminPerson.UserID = person.UserID;
            _adminPerson.Picture = person.Picture;
         //   _adminPerson.ClubPicture = person.ClubPicture;
          //  _adminPerson.StandUserName = person.StandUserName;
          //  _adminPerson.StandPassWord = person.StandPassWord;
            UpdateAdminSite();
        }
        // **************************   SAVE
        internal void SaveNewPerson(Person person)
        {
            // Populate admin and save changes
            client.SendObject(UpdateCommunicationPacket(DataAccess.Request.SaveNew, person.ToBytes(), DataAccess.ClassType.Person, ""));
        }
        // **************************   CHECK
        internal void CheckIfPersonExist(Person person)
        {
            // Populate admin and save changes
            client.SendObject(UpdateCommunicationPacket(DataAccess.Request.Get, person.ToBytes(), DataAccess.ClassType.Person, ""));
        }



        //internal void UpdateData(byte[] buffer, string _packet)
        //{
        //    switch (DataAccess.ParseEnum<DataAccess.ClassType>(_packet))
        //    {
        //        case DataAccess.ClassType.Person:
        //            PerformPersonAction(new Person(buffer));
        //            break;
        //        case DataAccess.ClassType.PersonList:
        //            break;
        //        case DataAccess.ClassType.Tournament:
        //            break;
        //        case DataAccess.ClassType.Blinds:
        //            break;
        //        case DataAccess.ClassType.Payouts:
        //            break;
        //        case DataAccess.ClassType.Points:
        //            break;
        //        case DataAccess.ClassType.DataVerify:
        //            break;
        //        case DataAccess.ClassType.Action:
        //            break;
        //        case DataAccess.ClassType.Packet:
        //            break;
        //    }
        //}
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
                    btn_LoggIn_LoggIn.Style = (Style)FindResource("ButtonPushed");
                    btn_LoggIn_LoggIn.Foreground = (SolidColorBrush)FindResource("ActiveText");

                    btn_LoggIn_NewAccount.Style = (Style)FindResource("ButtonUnPushed");
                    btn_LoggIn_NewAccount.Foreground = (SolidColorBrush)FindResource("DeactiveText");

                    stk_LoggIn_1.Visibility = Visibility.Hidden;
                    stk_LoggIn_2.Visibility = Visibility.Visible;
                    stk_LoggIn_3.Visibility = Visibility.Hidden;
                    btn_LogIn.Visibility = Visibility.Visible;
                    btn_NewAccount.Visibility = Visibility.Hidden;
                    break;
                case "btn_loggin_newaccount":
                    btn_LoggIn_NewAccount.Style = (Style)FindResource("ButtonPushed");
                    btn_LoggIn_NewAccount.Foreground = (SolidColorBrush)FindResource("ActiveText");

                    btn_LoggIn_LoggIn.Style = (Style)FindResource("ButtonUnPushed");
                    btn_LoggIn_LoggIn.Foreground = (SolidColorBrush)FindResource("DeactiveText");
                    stk_LoggIn_1.Visibility = Visibility.Visible;
                    stk_LoggIn_2.Visibility = Visibility.Visible;
                    stk_LoggIn_3.Visibility = Visibility.Visible;

                    btn_NewAccount.Visibility = Visibility.Visible;
                    btn_LogIn.Visibility = Visibility.Hidden;
                    break;
            }
        }
        private void LogginRequest_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Name.ToString())
            {
                case "btn_NewAccount":
                    if ((int)txt_Login_1.Tag == 1 && (int)txt_Login_2.Tag == 1 && (int)txt_Login_3.Tag == 1 && (int)txt_Login_4.Tag == 1 && (int)txt_Login_5.Tag == 1 && (int)txt_Login_PW_1.Tag == 1 && (int)txt_Login_PW_2.Tag == 1)
                    {
                        //Registrer Player
                        UpdateAdminPerson();
                        client.SendObject(UpdateCommunicationPacket(DataAccess.Request.NewAccount, _adminPerson.ToBytes(), DataAccess.ClassType.Person, ""));
                    }
                    break;
                case "btn_Verification":
                    if (VerificationLocation == DataAccess.Request.NewAccountVerify)
                    {
                        UpdateAdminPerson();
                        client.SendObject(UpdateCommunicationPacket(DataAccess.Request.VerifyOK, _adminPerson.ToBytes(), DataAccess.ClassType.Person, ""));
                        VerificationCode = "";
                    }
                    else if(VerificationLocation == DataAccess.Request.ResetPasswordVerify)
                    {
                        brd_Verify.Visibility = Visibility.Hidden;
                        brd_ResetPassword.Visibility = Visibility.Visible;
                        brd_ForgotPassword.Visibility = Visibility.Hidden;
                    }
                    break;
                case "btn_LogIn":
                    if (txt_Login_5.Text.Length >= 7 && txt_Login_PW_1.Password.Length >= 8)
                    {
                        //Check if Person exists
                        UpdateAdminPerson();
                        client.SendObject(UpdateCommunicationPacket(DataAccess.Request.LogIn, _adminPerson.ToBytes(), DataAccess.ClassType.Person, ""));
                    }
                    break;
                case "btn_ForgotPassword":
                    brd_ForgotPassword.Visibility = Visibility.Visible;
                    break;
                case "btn_SendResetMail":
                    UpdateAdminPerson();
                    _adminPerson.EMail = txt_ResetPasswordMail.ToString();
                    client.SendObject(UpdateCommunicationPacket(DataAccess.Request.ResetPassword, _adminPerson.ToBytes(), DataAccess.ClassType.Person,""));
                    break;
                case "btn_SendResetPassword":
                    UpdateAdminPerson();
                    _adminPerson.PassWord = txt_Login_PW_3.Password.ToString();
                    client.SendObject(UpdateCommunicationPacket(DataAccess.Request.UpdatePassword, _adminPerson.ToBytes(), DataAccess.ClassType.Person,""));
                    break;
            }
        }
        private byte[] UpdateCommunicationPacket(DataAccess.Request request, byte[] objType, DataAccess.ClassType classType, string info)
        {
            CommunicationManager _cp = new CommunicationManager
            {
                Request = request,
                ObjectType = objType,
                ClassType = classType,
                Info = info,
                Size = objType.Length
            };
            return _cp.ToBytes();
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
 
        private void Btn_ExitAll_Click(object sender, RoutedEventArgs e)
        {
            brd_Verify.Visibility = Visibility.Hidden;
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
                    _adminPerson.Gender = "MALE";
                    btnMale.Style = (Style)FindResource("ButtonPushed");
                    btnMale.Foreground = (SolidColorBrush)FindResource("ActiveText");

                    btnFemale.Style = (Style)FindResource("ButtonUnPushed");
                    btnFemale.Foreground = (SolidColorBrush)FindResource("DeactiveText");
                    break;
                case "btnFemale":
                case "FEMALE":
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
                    TakePicture();
                    break;
            }
        }

        public void TakePicture()
        {
            _adminPerson.Picture = DataAccess.ImageSourceToBytes(new PngBitmapEncoder(), frameHolder.Source);
            brdEditPicture.Background = ByteToBrushBackground(_adminPerson.Picture);
        }

        private void BtnSaveClubChanges_Click(object sender, RoutedEventArgs e)
        {
            // Populate admin and save changes
            client.SendObject(UpdateCommunicationPacket(DataAccess.Request.ClubUpdate, _adminPerson.ToBytes(), DataAccess.ClassType.Person,""));
        }

        private void UModule_Click(object sender, MouseButtonEventArgs e)
        {
            switch (((UserModuleSimple)sender as UserModuleSimple).Name.ToString().ToUpper())
            {
                case "UMODULE_1": // Admin subscription
                    uProductView.Visibility = Visibility.Visible;
                    uAdminCreator.Visibility = Visibility.Hidden;
                    uPersonCreate.Visibility = Visibility.Hidden;
                    uTournamentCreate.Visibility = Visibility.Hidden;
                    break;
                case "UMODULE_2": // Web subscription
                    uPersonCreate.Visibility = Visibility.Visible;
                    uProductView.Visibility = Visibility.Hidden;
                    uAdminCreator.Visibility = Visibility.Hidden;
                    uTournamentCreate.Visibility = Visibility.Hidden;
                    break;
                case "UMODULE_3": // Buy Tokens
                    uAdminCreator.Visibility = Visibility.Visible;
                    uProductView.Visibility = Visibility.Hidden;
                    uPersonCreate.Visibility = Visibility.Hidden;
                    uTournamentCreate.Visibility = Visibility.Hidden;
                    break;
                case "UMODULE_4": // Gadget 1
                    uTournamentCreate.Visibility = Visibility.Visible;
                    uAdminCreator.Visibility = Visibility.Hidden;
                    uProductView.Visibility = Visibility.Hidden;
                    uPersonCreate.Visibility = Visibility.Hidden;
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
            // xxx get selected avatar
            if (lstAvatar.SelectedIndex > -1)
            {
                foreach (var item in listAvatar)
                {
                    if (item == (sender as ListBox).SelectedItem)
                    {
                        _adminClub.Picture = (item as Border).Name;
                        brdEditClubPicture.Background = BrushBackground((item as Border).Name);
                        brdEditClubPicture.Tag = (item as Border).Name;
                        break;
                    }
                }
            }
            lstAvatar.Visibility = Visibility.Hidden;
        }
        //private void TxtVerify_Code_SelectionChanged(object sender, RoutedEventArgs e)
        //{
        //    switch ((sender as TextBox).Name)
        //    {
        //        case "txt_ResetPasswordMail":
        //            if ((sender as TextBox).Text.Contains("@") && (sender as TextBox).Text.Contains(".") && (sender as TextBox).Text.Length >= 7)
        //            {
        //                (sender as TextBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
        //                (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
        //                (sender as TextBox).Tag = 1;
        //                _adminPerson.EMail = (sender as TextBox).Text.ToString();
        //            }
        //            else
        //            {
        //                (sender as TextBox).Background = (SolidColorBrush)FindResource("ErrorBackground");
        //                (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ErrorText");
        //                (sender as TextBox).Tag = 0;
        //            }
        //            try
        //            {
        //                txt_Login_5.Text = (sender as TextBox).Text;
        //            }
        //            catch (Exception)
        //            {
        //            }
        //            break;
        //        case "txt_Verify_Code":
        //            break;
        //    }
        //    if ((sender as TextBox).Text == VerificationCode && VerificationCode.Length > 0)
        //    {
        //        // Code is correct you can now verify
        //        (sender as TextBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
        //        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
        //        btn_Verification.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        // You type the wrong code
        //        (sender as TextBox).Background = (SolidColorBrush)FindResource("ErrorBackground");
        //        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ErrorText");
        //        btn_Verification.Visibility = Visibility.Hidden;
        //    }
        //}
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
            try
            {
                _adminPerson.BornDate = DateTime.Parse(dpBorn.Text.ToString());
            }
            catch (Exception)
            {
            }
        }
        private void Textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            switch ((sender as TextBox).Name)
            {
                case "txt_Verify_Code": // Verification new account
                    if ((sender as TextBox).Text == VerificationCode && VerificationCode.Length > 0)
                    {
                        // Code is correct you can now verify
                        (sender as TextBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
                        (sender as TextBox).Tag = 1;
                        btn_Verification.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        // You type the wrong code
                        (sender as TextBox).Background = (SolidColorBrush)FindResource("ErrorBackground");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ErrorText");
                        (sender as TextBox).Tag = 0;
                        btn_Verification.Visibility = Visibility.Hidden;
                    }
                    break;
                case "txt_Login_1": // FirstName
                case "txt_Edit_1": // FirstName
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
                case "txt_Login_2": // LastName
                case "txt_Edit_2": // LastName
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
                case "txt_Login_3": // EMail
                case "txt_ResetPasswordMail": // EMail
                    if ((sender as TextBox).Text.Contains("@") && (sender as TextBox).Text.Contains(".") && (sender as TextBox).Text.Length >= 7)
                    {
                        (sender as TextBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
                        (sender as TextBox).Tag = 1;
                        _adminPerson.EMail = (sender as TextBox).Text.ToString();
                        try
                        {
                            btn_SendResetMail.Visibility = Visibility.Visible;
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else
                    {
                        (sender as TextBox).Background = (SolidColorBrush)FindResource("ErrorBackground");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ErrorText");
                        (sender as TextBox).Tag = 0;
                        try
                        {
                            btn_SendResetMail.Visibility = Visibility.Hidden;
                        }
                        catch (Exception)
                        {
                        }
                    }
                    try
                    {
                        txt_Login_5.Text = (sender as TextBox).Text;
                    }
                    catch (Exception)
                    {
                    }
                    break;
                case "txt_Login_4": // Mobile
                case "txt_Edit_3": // Mobile
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
                case "txt_Login_5": // UserName
                    if ((sender as TextBox).Text.Length >= 7)
                    {
                        (sender as TextBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
                        (sender as TextBox).Tag = 1;
                        _adminPerson.EMail = (sender as TextBox).Text.ToString();
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
                        _adminPerson.NickName = (sender as TextBox).Text.ToString();
                    }
                    else
                    {
                        (sender as TextBox).Background = (SolidColorBrush)FindResource("ErrorBackground");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ErrorText");
                        (sender as TextBox).Tag = 0;
                    }
                    break;
                case "txt_Edit_5": // Password
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
                case "txt_Edit_6": // Stand Username
                    if ((sender as TextBox).Text.Length >= 0 && (sender as TextBox).Text.Length <= 12)
                    {
                        (sender as TextBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
                        (sender as TextBox).Tag = 1;
                        _adminClub.StandUserName = (sender as TextBox).Text.ToString();
                    }
                    else
                    {
                        (sender as TextBox).Background = (SolidColorBrush)FindResource("ErrorBackground");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ErrorText");
                        (sender as TextBox).Tag = 0;
                    }
                    break;
                case "txt_Edit_7": // Stand Password
                    if ((sender as TextBox).Text.Length >= 0 && (sender as TextBox).Text.Length <= 12)
                    {
                        (sender as TextBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
                        (sender as TextBox).Tag = 1;
                        _adminClub.StandPassWord = (sender as TextBox).Text.ToString();
                    }
                    else
                    {
                        (sender as TextBox).Background = (SolidColorBrush)FindResource("ErrorBackground");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ErrorText");
                        (sender as TextBox).Tag = 0;
                    }
                    break;
                case "txt_Edit_9": // Clubname
                    if ((sender as TextBox).Text.Length >= 0 && (sender as TextBox).Text.Length <= 20)
                    {
                        (sender as TextBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
                        (sender as TextBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
                        (sender as TextBox).Tag = 1;
                        _adminClub.Name = (sender as TextBox).Text.ToString();
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
                case "txt_Login_PW_1": // PAssword
                    if ((sender as PasswordBox).Password.Length >= 8)
                    {
                        (sender as PasswordBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
                        (sender as PasswordBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
                        (sender as PasswordBox).Tag = 1;
                        _adminPerson.PassWord = txt_Login_PW_1.Password.ToString();
                    }
                    else
                    {
                        (sender as PasswordBox).Background = (SolidColorBrush)FindResource("ErrorBackground");
                        (sender as PasswordBox).Foreground = (SolidColorBrush)FindResource("ErrorText");
                        (sender as PasswordBox).Tag = 0;
                    }
                    break;
                case "txt_Login_PW_2": // validated password
                    if ((sender as PasswordBox).Password == txt_Login_PW_1.Password)
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
                case "txt_Login_PW_3": // Reset password
                    if ((sender as PasswordBox).Password.Length >= 8)
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
                case "txt_Login_PW_4": // validated Reset password
                    if ((sender as PasswordBox).Password == txt_Login_PW_3.Password)
                    {
                        (sender as PasswordBox).Background = (LinearGradientBrush)FindResource("ButtonBackgroundPushed");
                        (sender as PasswordBox).Foreground = (SolidColorBrush)FindResource("ActiveText");
                        (sender as PasswordBox).Tag = 1;
                        txt_Login_PW_1.Password = txt_Login_PW_3.Password;
                        btn_SendResetPassword.Visibility = Visibility.Visible;
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
        private void Btn_ExitResetPassword(object sender, RoutedEventArgs e)
        {
            brd_ForgotPassword.Visibility = Visibility.Hidden;
        }

        private void Img_LoggIn_1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //// Populate Administration Creating screen
            //uAdminCreator.Fill();
            //client.SendObject(UpdateCommunicationPacket(DataAccess.Request.GetStartProduct, new Product().ToBytes(), DataAccess.ClassType.Product, ""));
        }

    }
}
