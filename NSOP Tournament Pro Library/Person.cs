using NSOP_Torunament_cro_Library;
using NSOP_Torunament_Pro_Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace NSOP_Tournament_Pro_Library
{
    [Serializable]
    public class Person
    {
        // ***********
        // PERSONALIA
        // ***********
        private string _PlayerID = "";
        public string PlayerID { get => _PlayerID; set => _PlayerID = value; }

        private string _FirstName = "";
        public string FirstName { get => _FirstName; set => _FirstName = value; }

        private string _LastName = "";
        public string LastName { get => _LastName; set => _LastName = value; }

        private byte[] _Picture;
        public byte[] Picture { get => _Picture; set => _Picture = value; }

        private string _Mobile = "";
        public string Mobile { get => _Mobile; set => _Mobile = value; }

        private string _Email = "";
        public string EMail { get => _Email; set => _Email = value; }

        private string _Gender = "";
        public string Gender { get => _Gender; set => _Gender = value; }

        private string _Nationality = "";
        public string Nationality { get => _Nationality; set => _Nationality = value; }

        private string _Iso3166Name = "";
        public string Iso3166Name { get => _Iso3166Name; set => _Iso3166Name = value; }

        private DateTime _BornDate;// = DateTime.Parse("01.01.1900");
        public DateTime BornDate { get => _BornDate; set => _BornDate = value; }

        private DateTime _RegDate;// = DateTime.Parse("01.01.1900");
        public DateTime RegDate { get => _RegDate; set => _RegDate = value; }

        private string _NickName = "";
        public string NickName { get => _NickName; set => _NickName = value; }

        private string _PassWord = "";
        public string PassWord { get => _PassWord; set => _PassWord = value; }

        private string _UserID = "";
        public string UserID { get => _UserID; set => _UserID = value; }

        private bool _IsPlayerRemoved = false;
        public bool IsPlayerRemoved { get => _IsPlayerRemoved; set => _IsPlayerRemoved = value; }

        private bool _IsLoggedInn = false;
        public bool IsLoggedInn { get => _IsLoggedInn; set => _IsLoggedInn = value; }

        // clubdata

        private Club _ClubData = new Club();
        public Club Clubdata { get => _ClubData; set => _ClubData = value; }

        private List<Club> _ClubDataList = new List<Club>();
        public List<Club> Clubdatalist { get => _ClubDataList; set => _ClubDataList = value; }

        // ***********
        // LIFETIME
        // ***********
        private int _LifetimePlayed = 0;
        public int LifetimePlayed { get => _LifetimePlayed; set => _LifetimePlayed = value; }

        private int _LifetimeWins = 0;
        public int LifetimeWins { get => _LifetimeWins; set => _LifetimeWins = value; }

        private int _LifetimeFinalTables = 0;
        public int LifetimeFinalTables { get => _LifetimeFinalTables; set => _LifetimeFinalTables = value; }

        private int _LifetimeCashed = 0;
        public int LifetimeCashed { get => _LifetimeCashed; set => _LifetimeCashed = value; }

        private int _LifetimeBubbles = 0;
        public int LifetimeBubbles { get => _LifetimeBubbles; set => _LifetimeBubbles = value; }

        private int _LifetimeFirstOuts = 0;
        public int LifetimeFirstOuts { get => _LifetimeFirstOuts; set => _LifetimeFirstOuts = value; }

        private int _LifetimeSevenDeuces = 0;
        public int LifetimeSevenDeuces { get => _LifetimeSevenDeuces; set => _LifetimeSevenDeuces = value; }

        private int _LifetimeBadBeats = 0;
        public int LifetimeBadBeats { get => _LifetimeBadBeats; set => _LifetimeBadBeats = value; }

        private int _LifetimeTakeOuts = 0;
        public int LifetimeTakeOuts { get => _LifetimeTakeOuts; set => _LifetimeTakeOuts = value; }

        private int _LifetimeHunted = 0;
        public int LifetimeHunted { get => _LifetimeHunted; set => _LifetimeHunted = value; }

        private long _LifetimeMoneyEarned = 0;
        public long LifetimeMoneyEarned { get => _LifetimeMoneyEarned; set => _LifetimeMoneyEarned = value; }

        private long _LifetimeMoneySpent = 0;
        public long LifetimeMoneySpent { get => _LifetimeMoneySpent; set => _LifetimeMoneySpent = value; }

        // Check if new day
        private DateTime _1;// = DateTime.Parse("01.01.1900");
        public DateTime LastLogin { get => _1; set => _1 = value; }


        private bool _12 = false;
        public bool IsVerified { get => _12; set => _12 = value; }

        // ***********
        // CONSTRUCTORS
        // ***********

        // Empty Constructor
        public Person()
        {
        }

        // Constructor to send Personalia
        public Person(string playerID, string firstName, string lastName, byte[] picture, string mobile, string email, string gender, string nationality, string iso3166Name, DateTime bornDate, DateTime regDate, string nickName, string passWord, string userID, bool isPlayerRemoved, bool isVerified)
        {
            this.PlayerID = playerID;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Picture = picture;
            this.Mobile = mobile;
            this.EMail = email;
            this.Gender = gender;
            this.Nationality = nationality;
            this.Iso3166Name = iso3166Name;
            this.BornDate = bornDate;
            this.RegDate = regDate;
            this.NickName = nickName;
            this.PassWord = passWord;
            this.UserID = userID;
            this.IsPlayerRemoved = isPlayerRemoved;
            this.IsVerified = isVerified;
        }

        // Constructor to recieve packet
        public Person(byte[] packetBytes)
        {
            BinaryFormatter _bf = new BinaryFormatter();
            MemoryStream _ms = new MemoryStream(packetBytes)
            {
                Position = 0
            };
            Person _person = (Person)_bf.Deserialize(_ms);
            _ms.Close();

            // Populate Personalia
            this.PlayerID = _person.PlayerID;
            this.FirstName = _person.FirstName;
            this.LastName = _person.LastName;
            this.Picture = _person.Picture;
            this.Mobile = _person.Mobile;
            this.EMail = _person.EMail;
            this.Gender = _person.Gender;
            this.Nationality = _person.Nationality;
            this.Iso3166Name = _person.Iso3166Name;
            this.BornDate = _person.BornDate;
            this.RegDate = _person.RegDate;
            this.NickName = _person.NickName;
            this.PassWord = _person.PassWord;
            this.UserID = _person.UserID;
            this.IsPlayerRemoved = _person.IsPlayerRemoved;
            this.IsLoggedInn = _person.IsLoggedInn;

            // Populate Lifetime
            this.LifetimePlayed = _person.LifetimePlayed;
            this.LifetimeWins = _person.LifetimeWins;
            this.LifetimeFinalTables = _person.LifetimeFinalTables;
            this.LifetimeCashed = _person.LifetimeCashed;
            this.LifetimeBubbles = _person.LifetimeBubbles;
            this.LifetimeFirstOuts = _person.LifetimeFirstOuts;
            this.LifetimeSevenDeuces = _person.LifetimeSevenDeuces;
            this.LifetimeBadBeats = _person.LifetimeBadBeats;
            this.LifetimeTakeOuts = _person.LifetimeTakeOuts;
            this.LifetimeHunted = _person.LifetimeHunted;
            this.LifetimeMoneyEarned = _person.LifetimeMoneyEarned;
            this.LifetimeMoneySpent = _person.LifetimeMoneySpent;

            // Populate Modul Access
            this.LastLogin = _person.LastLogin;
            this.IsVerified = _person.IsVerified;

        }
        // Convert Person to Byte[]
        public byte[] ToBytes()
        {
            BinaryFormatter _bf = new BinaryFormatter();
            MemoryStream _ms = new MemoryStream
            {
                Position = 0
            };
            _bf.Serialize(_ms, this);
            byte[] bytes = _ms.ToArray();
            _ms.Close();
            return bytes;
        }
        public bool SaveNew()
        {
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbPerson; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            bool _isOk;
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $" INSERT into tbPerson ";
                cmd.CommandText += $"( ";
                // Personalia
                cmd.CommandText += $"PlayerID, Picture, FirstName, LastName, ";
                cmd.CommandText += $"Mobile, EMail, BornDate, RegDate, PassWord, UserID, IsPlayerRemoved, ";
                cmd.CommandText += $"IsLoggedInn, ";
                // Lifetime
                cmd.CommandText += $"LifetimePlayed, LifetimeWins, LifetimeFinalTables, LifetimeCashed, ";
                cmd.CommandText += $"LifetimeBubbles, LifetimeFirstOuts, LifetimeSevenDeuces, LifetimeBadBeats, ";
                cmd.CommandText += $"LifetimeTakeOuts, LifetimeHunted, LifetimeMoneyEarned, LifetimeMoneySpent, ";
                // Modul Access
                cmd.CommandText += $"LastLogin, ";
                cmd.CommandText += $"IsVerified ";
                // Values
                cmd.CommandText += $") ";
                cmd.CommandText += $"VALUES ";
                cmd.CommandText += $"( ";
                // Personalia
                cmd.CommandText += $"'{PlayerID}', @Picture, '{FirstName}','{LastName}', ";
                cmd.CommandText += $"'{Mobile}','{EMail}','{BornDate.ToShortDateString()}','{RegDate.ToShortDateString()}','{DataAccess.PasswordEncryption(PassWord)}','{Guid.NewGuid()}', 0, ";
                cmd.CommandText += $"1, ";
                // Lifetime
                cmd.CommandText += $"'{LifetimePlayed}','{LifetimeWins}','{LifetimeFinalTables}','{LifetimeCashed}', ";
                cmd.CommandText += $"'{LifetimeBubbles}','{LifetimeFirstOuts}','{LifetimeSevenDeuces}','{LifetimeBadBeats}', ";
                cmd.CommandText += $"'{LifetimeTakeOuts}','{LifetimeHunted}','{LifetimeMoneyEarned}','{LifetimeMoneySpent}', ";
                // Modul Access
                cmd.CommandText += $"'{LastLogin.ToShortDateString()}', ";
                cmd.CommandText += $"'{IsVerified}' ";
                cmd.CommandText += $") ";
                cmd.Parameters.AddWithValue("@Picture", Picture);

                //error = cmd.CommandText;
                cmd.ExecuteNonQuery();

                // Save LifeTime
                _isOk = true;
            }
            catch (Exception e)
            {
                _ = e.ToString();
                _isOk = false;
            }
            con.Close();
            return _isOk;
        }
        public bool Update(Person person)
        {
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbPerson; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            bool _isOk;
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $" UPDATE tbPerson SET ";
                // Personalia
                cmd.CommandText += $"FirstName = '{person.FirstName}', LastName = '{person.LastName}', ";
                cmd.CommandText += $"Picture = @Picture, Mobile = '{person.Mobile}', ";
                cmd.CommandText += $"EMail ='{person.EMail}',Gender ='{person.Gender}', Nationality ='{person.Nationality}', ";
                cmd.CommandText += $"Iso3166Name = '{person.Iso3166Name}', BornDate = '{person.BornDate.ToShortDateString()}', RegDate = '{person.RegDate.ToShortDateString()}', ";
                cmd.CommandText += $"NickName = '{person.NickName}', PassWord = '{person.PassWord}', IsPlayerRemoved = '{person.IsPlayerRemoved}', ";
                cmd.CommandText += $"IsLoggedInn = '{person.IsLoggedInn}', ";
                // Lifetime
                cmd.CommandText += $"LifetimePlayed = '{person.LifetimePlayed}', LifetimeWins = '{person.LifetimeWins}', LifetimeFinalTables = '{person.LifetimeFinalTables}', ";
                cmd.CommandText += $"LifetimeCashed = '{person.LifetimeCashed}', LifetimeBubbles = '{person.LifetimeBubbles}', LifetimeFirstOuts = '{person.LifetimeFirstOuts}', ";
                cmd.CommandText += $"LifetimeSevenDeuces = '{person.LifetimeSevenDeuces}', LifetimeBadBeats = '{person.LifetimeBadBeats}', LifetimeTakeOuts = '{person.LifetimeTakeOuts}', ";
                cmd.CommandText += $"LifetimeHunted = '{person.LifetimeHunted}', LifetimeMoneyEarned = '{person.LifetimeMoneyEarned}', LifetimeMoneySpent = '{person.LifetimeMoneySpent}', ";
                // Modul Access
                cmd.CommandText += $"LastLogin = '{person.LastLogin}' ";

                // Where
                cmd.CommandText += $"WHERE PlayerID = '{person.PlayerID}';";
                cmd.Parameters.AddWithValue("@Picture", person.Picture);


                //error = cmd.CommandText;
                cmd.ExecuteNonQuery();

                // Save LifeTime
                _isOk = true;
            }
            catch (Exception e)
            {
                _ = e.ToString();
                _isOk = false;
            }
            con.Close();
            return _isOk;
        }
        public static bool UpdatePassword(Person person)
        {
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbPerson; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            bool _isOk;
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $" UPDATE tbPerson SET ";
                // Personalia
                cmd.CommandText += $"PassWord = '{DataAccess.PasswordEncryption(person.PassWord)}', ";
                cmd.CommandText += $"IsVerified = 1 ";
                // Where
                cmd.CommandText += $"WHERE EMail = '{person.EMail}';";


                //error = cmd.CommandText;
                cmd.ExecuteNonQuery();

                // Save LifeTime
                _isOk = true;
            }
            catch (Exception e)
            {
                _ = e.ToString();
                _isOk = false;
            }
            con.Close();
            return _isOk;
        }
        public static bool UpdateVerification(string eMail)
        {
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbPerson; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            bool _isOk;
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $" UPDATE tbPerson SET ";
                // Personalia
                cmd.CommandText += $"IsVerified = 1 ";

                // Where
                cmd.CommandText += $"WHERE EMail = '{eMail}';";


                //error = cmd.CommandText;
                cmd.ExecuteNonQuery();

                // Save LifeTime
                _isOk = true;
            }
            catch (Exception e)
            {
                _ = e.ToString();
                _isOk = false;
            }
            con.Close();
            return _isOk;
        }

        public static bool UpdateClub(Person person)
        {
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbPerson; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            bool _isOk;
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $" UPDATE tbPerson SET ";
                // Personalia
                cmd.CommandText += $"FirstName = '{person.FirstName}', LastName = '{person.LastName}', ";
                cmd.CommandText += $"Picture = @Picture, Mobile = '{person.Mobile}', ";
                cmd.CommandText += $"Gender ='{person.Gender}', Nationality ='{person.Nationality}', ";
                cmd.CommandText += $"Iso3166Name = '{person.Iso3166Name}', BornDate = '{person.BornDate.ToShortDateString()}', ";
                cmd.CommandText += $"NickName = '{person.NickName}' ";

                // Where
                cmd.CommandText += $"WHERE EMail = '{person.EMail}';";
                cmd.Parameters.AddWithValue("@Picture", person.Picture);


                //error = cmd.CommandText;
                cmd.ExecuteNonQuery();

                // Save LifeTime
                _isOk = true;
            }
            catch (Exception e)
            {
                _ = e.ToString();
                _isOk = false;
            }
            con.Close();
            return _isOk;
        }
        public bool Delete(Person person)
        {
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbPerson; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            bool _isOk;
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $" DELETE FROM tbPerson WHERE PlayerID = '{person.PlayerID}';";

                //error = cmd.CommandText;
                cmd.ExecuteNonQuery();

                // Save LifeTime
                _isOk = true;
            }
            catch (Exception e)
            {
                _ = e.ToString();
                _isOk = false;
            }
            con.Close();
            return _isOk;
        }
        public static bool IfPersonVerified(string eMail)
        {
            bool _isOK = false;
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbPerson; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {
                SqlDataReader _SqlData = null;
                StringBuilder _s = new StringBuilder();
                _s.Append($"SELECT * FROM dbo.tbPerson WHERE ");
                if (eMail.Length > 0)
                {
                    _s.Append($"EMail = '{eMail}' ");
                }
                _s.Append($" AND IsVerified = {true};");

                SqlCommand _SqlStr = new SqlCommand(_s.ToString(), con);

                _SqlData = _SqlStr.ExecuteReader();
                _SqlStr.Dispose();
                while (_SqlData.Read())
                {
                    _isOK = true;
                }
            }
            catch (Exception e)
            {
                _ = e.ToString();
            }
            con.Close();
            return _isOK;
        }
        public static bool IfPersonExists(string personID, string eMail, string passWord)
        {
            bool _isOK = false;
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbPerson; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {
                SqlDataReader _SqlData = null;
                StringBuilder _s = new StringBuilder();
                _s.Append($"SELECT * FROM dbo.tbPerson WHERE ");
                if (personID.Length > 0)
                {
                    _s.Append($"PlayerID = '{personID}' AND ");
                }
                if (eMail.Length > 0)
                {
                    _s.Append($"EMail = '{eMail}' AND ");
                }
                if (passWord.Length > 0)
                {
                    // 5CEC175B165E3D5E62C9E13CE848EF6FEAC81BFF
                    string c = DataAccess.PasswordEncryption(passWord);
                    _s.Append($"PassWord = '{DataAccess.PasswordEncryption(passWord)}' AND ");
                }
                _s.Remove(_s.Length - 4, 4);
                _s.Append($";");

                SqlCommand _SqlStr = new SqlCommand(_s.ToString(), con);

                _SqlData = _SqlStr.ExecuteReader();
                _SqlStr.Dispose();
                while (_SqlData.Read())
                {
                    _isOK = true;
                }
            }
            catch (Exception e)
            {
                _ = e.ToString();
            }
            con.Close();
            return _isOK;
        }
        public static Person GetPerson(string personID, string eMail, string passWord)
        {
            Person _p = new Person{};
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbPerson; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {
                SqlDataReader _SqlData = null;
                StringBuilder _s = new StringBuilder();
                _s.Append($"SELECT * FROM dbo.tbPerson WHERE ");
                if (personID.Length > 0)
                {
                    _s.Append($"PlayerID = '{personID}' AND ");
                }
                if (eMail.Length > 0)
                {
                    _s.Append($"EMail = '{eMail}' AND ");
                }
                if (passWord.Length > 0)
                {
                    string c = DataAccess.PasswordEncryption(passWord);
                    _s.Append($"PassWord = '{DataAccess.PasswordEncryption(passWord)}' AND ");
                }
                _s.Remove(_s.Length - 4, 4);
                _s.Append($";");

                SqlCommand _SqlStr = new SqlCommand(_s.ToString(), con);

                _SqlData = _SqlStr.ExecuteReader();
                _SqlStr.Dispose();
                while (_SqlData.Read())
                {
                    // Populate Personalia 
                    _p.PlayerID = _SqlData["PlayerID"].ToString();
                    _p.FirstName = _SqlData["FirstName"].ToString();
                    _p.LastName = _SqlData["LastName"].ToString();
                    _p.Picture = (byte[])_SqlData["Picture"];
                    _p.Mobile = _SqlData["Mobile"].ToString();
                    _p.EMail = _SqlData["EMail"].ToString();
                    _p.Gender = _SqlData["Gender"].ToString();
                    _p.Nationality = _SqlData["Nationality"].ToString();
                    _p.Iso3166Name = _SqlData["Iso3166Name"].ToString();

                    string x = _SqlData["BornDate"].ToString();
                    if (_SqlData["BornDate"].ToString() == "")
                    {
                    }
                    else _p.BornDate = DateTime.Parse(_SqlData["BornDate"].ToString());

                    if (_SqlData["RegDate"].ToString() == "")
                    {
                    }
                    else _p.RegDate = DateTime.Parse(_SqlData["RegDate"].ToString());

                    _p.NickName = _SqlData["NickName"].ToString();
                  
                    _p.UserID = _SqlData["UserID"].ToString();
                    _p.IsPlayerRemoved = (Boolean)_SqlData["IsPlayerRemoved"];
                    _p.IsLoggedInn = (Boolean)_SqlData["IsLoggedInn"];

                    // Populate Lifetime
                    _p.LifetimePlayed = (int)_SqlData["LifetimePlayed"];
                    _p.LifetimeWins = (int)_SqlData["LifetimeWins"];
                    _p.LifetimeFinalTables = (int)_SqlData["LifetimeFinalTables"];
                    _p.LifetimeCashed = (int)_SqlData["LifetimeCashed"];
                    _p.LifetimeBubbles = (int)_SqlData["LifetimeBubbles"];
                    _p.LifetimeFirstOuts = (int)_SqlData["LifetimeFirstOuts"];
                    _p.LifetimeSevenDeuces = (int)_SqlData["LifetimeSevenDeuces"];
                    _p.LifetimeBadBeats = (int)_SqlData["LifetimeBadBeats"];
                    _p.LifetimeTakeOuts = (int)_SqlData["LifetimeTakeOuts"];
                    _p.LifetimeHunted = (int)_SqlData["LifetimeHunted"];
                    _p.LifetimeMoneyEarned = (long)_SqlData["LifetimeMoneyEarned"];
                    _p.LifetimeMoneySpent = (long)_SqlData["LifetimeMoneySpent"];
                }
                // Save LifeTime
            }
            catch (Exception e)
            {
                _ = e.ToString();
            }
            con.Close();
            return _p;
        }
        public Person GetPerson(string dataRow, string dataValue)
        {
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbPerson; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {
                SqlDataReader _SqlData = null;
                SqlCommand _SqlStr = new SqlCommand($"SELECT * FROM dbo.tbPerson WHERE '{dataRow}' = '{dataValue}';", con);
                _SqlData = _SqlStr.ExecuteReader();
                _SqlStr.Dispose();
                while (_SqlData.Read())
                {
                    // Populate Personalia
                    this.PlayerID = _SqlData["PlayerID"].ToString();
                    this.FirstName = _SqlData["FirstName"].ToString();
                    this.LastName = _SqlData["LastName"].ToString();
                    this.Picture = (byte[])_SqlData["Picture"];
                    this.Mobile = _SqlData["Mobile"].ToString();
                    this.EMail = _SqlData["EMail"].ToString();
                    this.Gender = _SqlData["Gender"].ToString();
                    this.Nationality = _SqlData["Nationality"].ToString();
                    this.Iso3166Name = _SqlData["Iso3166Name"].ToString();
                    this.BornDate = DateTime.Parse(_SqlData["BornDate"].ToString());
                    this.RegDate = DateTime.Parse(_SqlData["RegDate"].ToString());
                    this.NickName = _SqlData["NickName"].ToString();
                    this.PassWord = _SqlData["PassWord"].ToString();
                    this.UserID = _SqlData["UserID"].ToString();
                    this.IsPlayerRemoved = (Boolean)_SqlData["IsPlayerRemoved"];
                    this.IsLoggedInn = (Boolean)_SqlData["IsLoggedInn"];

                    // Populate Lifetime
                    this.LifetimePlayed = (int)_SqlData["LifetimePlayed"];
                    this.LifetimeWins = (int)_SqlData["LifetimeWins"];
                    this.LifetimeFinalTables = (int)_SqlData["LifetimeFinalTables"];
                    this.LifetimeCashed = (int)_SqlData["LifetimeCashed"];
                    this.LifetimeBubbles = (int)_SqlData["LifetimeBubbles"];
                    this.LifetimeFirstOuts = (int)_SqlData["LifetimeFirstOuts"];
                    this.LifetimeSevenDeuces = (int)_SqlData["LifetimeSevenDeuces"];
                    this.LifetimeBadBeats = (int)_SqlData["LifetimeBadBeats"];
                    this.LifetimeTakeOuts = (int)_SqlData["LifetimeTakeOuts"];
                    this.LifetimeHunted = (int)_SqlData["LifetimeHunted"];
                    this.LifetimeMoneyEarned = (long)_SqlData["LifetimeMoneyEarned"];
                    this.LifetimeMoneySpent = (long)_SqlData["LifetimeMoneySpent"];

                    // Populate Modul Access
                    this.LastLogin = DateTime.Parse(_SqlData["LastLogin"].ToString());
                }

                // Save LifeTime
            }
            catch (Exception e)
            {
                _ = e.ToString();
            }
            con.Close();
            return this;
        }
     }
}
