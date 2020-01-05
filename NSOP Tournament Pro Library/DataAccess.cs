using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows;
using System.Resources;
using System.Reflection;

namespace NSOP_Tournament_Pro_Library
{
    public static class DataAccess
    {
       
        #region Windows API

        private delegate bool EnumLocalesProcExDelegate(
           [MarshalAs(UnmanagedType.LPWStr)]String lpLocaleString,
           LocaleType dwFlags, int lParam);

        [DllImport(@"kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool EnumSystemLocalesEx(EnumLocalesProcExDelegate pEnumProcEx,
           LocaleType dwFlags, int lParam, IntPtr lpReserved);

        private enum LocaleType : uint
        {
            LocaleAll = 0x00000000,             // Enumerate all named based locales
            LocaleWindows = 0x00000001,         // Shipped locales and/or replacements for them
            LocaleSupplemental = 0x00000002,    // Supplemental locales only
            LocaleAlternateSorts = 0x00000004,  // Alternate sort locales
            LocaleNeutralData = 0x00000010,     // Locales that are "neutral" (language only, region data is default)
            LocaleSpecificData = 0x00000020,    // Locales that contain language and region data
        }

        #endregion

        //public enum CultureTypes : uint
        //{
        //    SpecificCultures = LocaleType.LocaleSpecificData,
        //    NeutralCultures = LocaleType.LocaleNeutralData,
        //    AllCultures = LocaleType.LocaleWindows
        //}
        /// <summary>
        /// Gets the list of countries based on ISO 3166-1
        /// </summary>
        /// <returns>Returns the list of countries based on ISO 3166-1</returns>
        public static List<RegionInfo> GetCountriesByIso3166()
        {
            List<RegionInfo> countries = new List<RegionInfo>();
            foreach (CultureInfo culture in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                RegionInfo region = new RegionInfo(culture.LCID);
                //RegionInfo name = new RegionInfo(culture.EnglishName);
                if (countries.Where(p => p.Name == region.Name).Count() == 0)
                    countries.Add(region);
            }
            return countries.OrderBy(p => p.EnglishName).ToList();
        }
        public static T[] Concat<T>(this T[] bufferTotal, T[] buffer, int readByte)
        {
            if (bufferTotal == null) throw new ArgumentNullException("x");
            if (buffer == null) throw new ArgumentNullException("y");
            int oldLen = bufferTotal.Length;
            Array.Resize<T>(ref bufferTotal, bufferTotal.Length + readByte);
            Array.Copy(buffer, 0, bufferTotal, oldLen, readByte);
            return bufferTotal;
        }

      //  public static readonly ResourceManager RM = new ResourceManager("NSOP_Tournament_Pro_Library.Properties.Resources", Assembly.GetExecutingAssembly());

        public static byte[] ConvertByte(byte[] bufferTotal, byte[] buffer, int readByte)
        {
            byte[] _xValue = bufferTotal.Concat(buffer, readByte);
            return _xValue;
        }

        public static List<string> GetCountries()
        {
            List<string> countries = new List<string>();

            foreach (CultureInfo culture in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                RegionInfo region = new RegionInfo(culture.LCID);

                if (!(countries.Contains(region.EnglishName + " [" + region + "]")))
                {
                    countries.Add(region.EnglishName + " [" + region + "]");
                }
            }
            countries.Sort();

            return countries;
        }
        public static CultureInfo GetCountriesISO(string culture)
        {
            CultureInfo _Culture = new CultureInfo(culture);

            foreach (CultureInfo c in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                RegionInfo _Region = new RegionInfo(c.LCID);

                if (_Region.ToString() == culture)
                {
                    //_Culture = _Region.ToString();
                }
            }
            //countries.Sort();

            return _Culture;
        }
        public static List<string> GetCountriesISO()
        {
            List<string> countries = new List<string>();

            foreach (CultureInfo culture in CultureInfo.GetCultures(System.Globalization.CultureTypes.SpecificCultures))
            {
                RegionInfo region = new RegionInfo(culture.LCID);

                if (!(countries.Contains(region.ToString())))
                {
                    countries.Add(region.ToString());
                }
            }
            //countries.Sort();

            return countries;
        }

        /// <summary>
        /// idType is either Person, Tournament, Blindstructure, Payoutsturcture, Pointstructure
        /// </summary>
        /// <param name="idType"></param>
        /// <returns></returns>
        public static string FillID(IdType idType)
        {
            string _H0 = "";
            string _xV = "";
            switch (idType)
            {
                case IdType.Person:
                    _H0 = "P";
                    _xV = DateTime.Now.Year.ToString();
                    break;
                case IdType.Tournament:
                    _H0 = "T";
                    _xV = DateTime.Now.Year.ToString();
                    break;
                case IdType.Blinds:
                    _H0 = "B";
                    _xV = DateTime.Now.Year.ToString();
                    break;
                case IdType.Payouts:
                    _H0 = "P";
                    _xV = DateTime.Now.Year.ToString();
                    break;
                case IdType.Points:
                    _H0 = "X";
                    _xV = DateTime.Now.Year.ToString();
                    break;
                case IdType.Club:
                    _H0 = "C";
                    _xV = "CLUB";
                    break;
            }
            string _ID;
            string _xD = DateTime.Now.Day.ToString("00");
            string _xM = DateTime.Now.Month.ToString("00");
            string _xY = DateTime.Now.Year.ToString("0000");
            int _xHo = DateTime.Now.Hour;
            int _xMi = DateTime.Now.Minute;
            int _xSe = DateTime.Now.Second;
            int _xMs = DateTime.Now.Millisecond;
            string _xT = ((_xMs + 1) * (_xSe + 1) * (_xMi + 1) * (_xHo + 1)).ToString("00000000");
            _ID = $"{_xV} ";
            _ID += $"{_xY.Substring(0, 1)}{_xT.Substring(0, 1)}{_xM.Substring(0, 1)}{_xT.Substring(1, 1)} ";
            _ID += $"{_xD.Substring(0, 1)}{_xT.Substring(2, 1)}{_xD.Substring(1, 1)}{_xT.Substring(3, 1)} ";
            _ID += $"{_xM.Substring(1, 1)}{_xT.Substring(4, 1)}{_xY.Substring(3, 1)}{_H0}";
            return _ID;
        }

        /// <summary>
        /// vFormat : Upper, Lower og Normal
        /// </summary>
        internal static string FormaterText(string vText, string vFormat)
        {
            string xValue = vText;
            switch (vFormat.ToUpper())
            {
                case "UPPER":
                    xValue = xValue.ToUpper();
                    break;
                case "LOWER":
                    xValue = xValue.ToLower();
                    break;
                case "NORMAL":
                    string xCapitol = vText.Substring(0, 1);
                    string xRest = vText.Substring(1, vText.Length - 1);
                    xValue = xCapitol.ToUpper() + xRest.ToLower(); ;
                    break;
            }
            return xValue;
        }

        public static Bitmap ToWinFormsBitmap(this BitmapSource bitmapsource)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapsource));
                enc.Save(stream);

                using (var tempBitmap = new Bitmap(stream))
                {
                    // According to MSDN, one "must keep the stream open for the lifetime of the Bitmap."
                    // So we return a copy of the new bitmap, allowing us to dispose both the bitmap and the stream.
                    return new Bitmap(tempBitmap);
                }
            }
        }
        private delegate double RoundingFunction(double value);
        public static BitmapSource ToWpfBitmap(this System.Drawing.Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Bmp);

                stream.Position = 0;
                BitmapImage result = new BitmapImage();
                result.BeginInit();
                // According to MSDN, "The default OnDemand cache option retains access to the stream until the image is needed."
                // Force the bitmap to load right now so we can dispose the stream.
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.StreamSource = stream;
                result.EndInit();
                result.Freeze();
                return result;
            }
        }
        public static Bitmap BitmapSourceToBitmap(BitmapSource srs)
        {
            int width = srs.PixelWidth;
            int height = srs.PixelHeight;
            int stride = width * ((srs.Format.BitsPerPixel + 7) / 8);
            IntPtr ptr = IntPtr.Zero;
            try
            {
                ptr = Marshal.AllocHGlobal(height * stride);
                srs.CopyPixels(new System.Windows.Int32Rect(0, 0, width, height), ptr, height * stride, stride);
                using (var btm = new Bitmap(width, height, stride, System.Drawing.Imaging.PixelFormat.Format1bppIndexed, ptr))
                {
                    // Clone the bitmap so that we can dispose it and
                    // release the unmanaged memory at ptr
                    return new Bitmap(btm);
                }
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                    Marshal.FreeHGlobal(ptr);
            }
        }
        public static BitmapImage ToBitmapImage(this Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }
        public static byte[] ImageSourceToBytes(BitmapEncoder encoder, ImageSource imageSource)
        {
            byte[] bytes = null;

            if (imageSource is BitmapSource bitmapSource)
            {
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    bytes = stream.ToArray();
                }
            }

            return bytes;
        }
        public static BitmapImage ConvertByteArrayToBitMapImage(byte[] imageByteArray)
        {
            if (imageByteArray == null || imageByteArray.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageByteArray))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
        public static string GetIP4Address()
        {
            IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());

            foreach (IPAddress i in ips)
            {
                if (i.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    return i.ToString();
            }
            return "62.24.34.199";
        }
        public enum IdType
        {
            Person,
            Tournament,
            Blinds,
            Payouts,
            Points,
            Club
        }
        public enum ClassType
        {
            Person,
            PersonList,
            Tournament,
            Blinds,
            Payouts,
            Points,
            DataVerify,
            Action,
            Packet
        }
        public enum Request
        {
            New,
            ClubUpdate,
            PersonUpdate,
            Delete,
            Get,
            Getall,
            Registrer,
            LoggIn,
            LoggInOK,
            LoggInFailed,
            PersonExist,
            PersonCreated,
            Verify,
            VerifyOK,
            BadEMail,
            ResetPassword,
            UpdatePassword,
            ResetVerification,
            ResetPasswordOK
        }
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
        public static string GetPacket(byte[] buffer)
        {
            string _Packet = "";
            try // Person
            {
                Person _obj = new Person(buffer);
                _Packet = _obj.ClassType.ToString();
            }
            catch (Exception)
            {
                try // Tournament
                {
                    Tournament _obj = new Tournament(buffer);
                    _Packet = _obj.ClassType.ToString();
                }
                catch (Exception)
                {
                    try // Playerlist
                    {
                        PersonList _obj = new PersonList(buffer);
                        _Packet = _obj.ClassType.ToString();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return _Packet;
        }
        public static string GetAction(byte[] buffer)
        {
            string _Action = "";
            try // Person
            {
                Person _obj = new Person(buffer);
                _Action = _obj.ActionType.ToString();
            }
            catch (Exception)
            {
                try // Tournament
                {
                    Tournament _obj = new Tournament(buffer);
                   // _Action = _obj.ActionType.ToString();
                }
                catch (Exception)
                {
                    try // Playerlist
                    {
                        PersonList _obj = new PersonList(buffer);
                        _Action = _obj.ActionType.ToString();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return _Action;
        }
        public static string PasswordEncryption(string value)
        {
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var inputBytes = Encoding.ASCII.GetBytes(value);
            var hash = sha1.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            sha1.Dispose();
            return sb.ToString();
        }

        public static string GetVerificationCode()
        {

            var _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var _stringChars = new char[8];
            var _random = new Random();

            for (int i = 0; i < _stringChars.Length; i++)
            {
                _stringChars[i] = _chars[_random.Next(_chars.Length)];
            }
            return new String(_stringChars);
        }
    }
}
