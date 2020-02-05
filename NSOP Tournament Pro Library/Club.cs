
using NSOP_Tournament_Pro_Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace NSOP_Torunament_cro_Library
{
    [Serializable]
    public class Club
    {
        // ***********
        // RANK
        // ***********

        private string _ID = "";
        public string ID { get => _ID; set => _ID = value; }

        private string _CPicture;
        public string Picture { get => _CPicture; set => _CPicture = value; }

        private string _Name = "";
        public string Name { get => _Name; set => _Name = value; }

        private string _Owner = "";
        public string Owner { get => _Owner; set => _Owner = value; }

        private string _Mobile = "";
        public string Mobile { get => _Mobile; set => _Mobile = value; }

        private string _EMail = "";
        public string EMail { get => _EMail; set => _EMail = value; }

        private string _cassWord = "";
        public string PassWord { get => _cassWord; set => _cassWord = value; }

        private int _cRank = 0;
        public int Rank { get => _cRank; set => _cRank = value; }

        private long _cPoints = 0;
        public long Points { get => _cPoints; set => _cPoints = value; }

        private DateTime _cRegDate;// = DateTime.Parse("01.01.1900");
        public DateTime RegDate { get => _cRegDate; set => _cRegDate = value; }

        // ***********
        // MODUL ACCESS
        // ***********
        private DateTime _AdminSubscribtion;// = DateTime.Parse("01.01.1900");
        public DateTime AdminSubscribtion { get => _AdminSubscribtion; set => _AdminSubscribtion = value; }

        private DateTime _WebSideSubscribtion;// = DateTime.Parse("01.01.1900");
        public DateTime WebSideSubscribtion { get => _WebSideSubscribtion; set => _WebSideSubscribtion = value; }

        private int _Tokens = 0;
        public int Tokens { get => _Tokens; set => _Tokens = value; }

        private int _2 = 0;
        public int TournamentCreatorQTY { get => _2; set => _2 = value; }

        private int _3 = 0;
        public int PersonRegQTY { get => _3; set => _3 = value; }

        private int _4 = 0;
        public int TicketSaleQTY { get => _4; set => _4 = value; }

        private int _5 = 0;
        public int BlindStructureQTY { get => _5; set => _5 = value; }

        private int _6 = 0;
        public int PayoutStructureQTY { get => _6; set => _6 = value; }

        private int _7 = 0;
        public int PointStructureQTY { get => _7; set => _7 = value; }

        private int _8 = 0;
        public int AdvertiseModuleQTY { get => _8; set => _8 = value; }

        private int _9 = 0;
        public int TableManagerQTY { get => _9; set => _9 = value; }

        // Pre-Generated Username every login per day
        private string _10 = "";
        public string StandUserName { get => _10; set => _10 = value; }

        // Pre-Generated Password every login per day
        private string _11 = "";
        public string StandPassWord { get => _11; set => _11 = value; }

        private bool _12 = false;
        public bool IsVerified { get => _12; set => _12 = value; }

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
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbClub; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            bool _isOk;
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $" INSERT into tbClub ";
                cmd.CommandText += $"( ";
                // Personalia
                cmd.CommandText += $"ID, Name, Picture, Owner, Mobile, EMail, PassWord ";
                // Rank
                cmd.CommandText += $"Rank, Points, AdminSubscribtion, WebSideSubscribtion, LastLogin, Tokens, ";
                cmd.CommandText += $"TournamentCreatorQTY, PersonRegQTY, TicketSaleQTY, BlindStructureQTY, ";
                cmd.CommandText += $"PayoutStructureQTY, PointStructureQTY, AdvertiseModuleQTY, TableManagerQTY, ";
                cmd.CommandText += $"StandUserName, StandPassWord, IsVerified ";
                // Values
                cmd.CommandText += $") ";
                cmd.CommandText += $"VALUES ";
                cmd.CommandText += $"( ";
                // Personalia
                cmd.CommandText += $"'{ID}', @Picture, '{Name}','{Owner}', '{Mobile}', '{EMail}','{PassWord}', ";
                cmd.CommandText += $"'{Rank}','{Points}','{AdminSubscribtion.ToShortDateString()}','{WebSideSubscribtion.ToShortDateString()}', '{Tokens}', ";
                cmd.CommandText += $"'{TournamentCreatorQTY}','{PersonRegQTY}','{TicketSaleQTY}','{BlindStructureQTY}', ";
                cmd.CommandText += $"'{PayoutStructureQTY}','{PointStructureQTY}','{AdvertiseModuleQTY}','{TableManagerQTY}', ";
                cmd.CommandText += $"'{StandUserName}','{StandPassWord}','{IsVerified}' ";
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
        public static bool UpdatePassword(Club club)
        {
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbClub; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            bool _isOk;
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $" UPDATE tbClub SET ";
                // Personalia
                cmd.CommandText += $"PassWord = '{DataAccess.PasswordEncryption(club.PassWord)}', ";
                cmd.CommandText += $"IsVerified = 1 ";
                // Where
                cmd.CommandText += $"WHERE EMail = '{club.EMail}';";


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
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbClub; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            bool _isOk;
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $" UPDATE tbClub SET ";
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

        public bool Update(Club club)
        {
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbClub; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            bool _isOk;
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $" UPDATE tbclub SET ";
                // clubalia
                cmd.CommandText += $"ID = '{club.ID}', Name = '{club.Name}', Picture = @Picture, Owner = '{club.Owner}', Mobile = '{club.Mobile}',EMail = '{club.EMail}', ";
                cmd.CommandText += $"PassWord = '{PassWord}', Rank = '{club.Rank}', Points = '{club.Points}', ";
                // Modul Access
                cmd.CommandText += $"AdminSubscribtion = '{club.AdminSubscribtion}', WebSideSubscribtion = '{club.WebSideSubscribtion}', ";
                cmd.CommandText += $"Tokens = '{club.Tokens}', TournamentCreatorQTY = '{club.TournamentCreatorQTY}', ";
                cmd.CommandText += $"TicketSaleQTY = '{club.TicketSaleQTY}', BlindStructureQTY = '{club.BlindStructureQTY}', PayoutStructureQTY = '{club.PayoutStructureQTY}', ";
                cmd.CommandText += $"PointStructureQTY = '{club.PointStructureQTY}', AdvertiseModuleQTY = '{club.AdvertiseModuleQTY}', TableManagerQTY = '{club.TableManagerQTY}', ";
                cmd.CommandText += $"StandUserName = '{club.StandUserName}', StandPassWord = '{club.StandPassWord}' ";

                // Where
                cmd.CommandText += $"WHERE PlayerID = '{club.ID}';";
                cmd.Parameters.AddWithValue("@Picture", club.Picture);


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
        public static bool UpdateClub(Club club)
        {
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbClub; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            bool _isOk;
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $" UPDATE tbClub SET ";
                // clubalia
                cmd.CommandText += $"ID = '{club.ID}', Name = '{club.Name}', Picture = @Picture, Owner = '{club.Owner}', Mobile = '{club.Mobile}',EMail = '{club.EMail}', ";
                cmd.CommandText += $"Rank = '{club.Rank}', Points = '{club.Points}', ";
                // Modul Access
                cmd.CommandText += $"AdminSubscribtion = '{club.AdminSubscribtion}', WebSideSubscribtion = '{club.WebSideSubscribtion}', ";
                cmd.CommandText += $"Tokens = '{club.Tokens}', TournamentCreatorQTY = '{club.TournamentCreatorQTY}', ";
                cmd.CommandText += $"TicketSaleQTY = '{club.TicketSaleQTY}', BlindStructureQTY = '{club.BlindStructureQTY}', PayoutStructureQTY = '{club.PayoutStructureQTY}', ";
                cmd.CommandText += $"PointStructureQTY = '{club.PointStructureQTY}', AdvertiseModuleQTY = '{club.AdvertiseModuleQTY}', TableManagerQTY = '{club.TableManagerQTY}', ";
                cmd.CommandText += $"StandUserName = '{club.StandUserName}', StandPassWord = '{club.StandPassWord}' ";

                // Where
                cmd.CommandText += $"WHERE PlayerID = '{club.ID}';";
                cmd.Parameters.AddWithValue("@Picture", club.Picture);


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
        public bool Delete(Club club)
        {
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbClub; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            bool _isOk;
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $" DELETE FROM tbClub WHERE ID = '{club.ID}';";

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
        public static bool IfClubVerified(string eMail)
        {
            bool _isOK = false;
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbClub; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {
                SqlDataReader _SqlData = null;
                StringBuilder _s = new StringBuilder();
                _s.Append($"SELECT * FROM dbo.tbClub WHERE ");
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
        public static bool IfClubExists(string ID, string eMail)
        {
            bool _isOK = false;
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbClub; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {
                SqlDataReader _SqlData = null;
                StringBuilder _s = new StringBuilder();
                _s.Append($"SELECT * FROM dbo.tbClub WHERE ");
                if (ID.Length > 0)
                {
                    _s.Append($"ID = '{ID}' AND ");
                }
                if (eMail.Length > 0)
                {
                    _s.Append($"EMail = '{eMail}' AND ");
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
        public static Club GetClub(string ID, string eMail)
        {
            Club _c = new Club { };
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbClub; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {
                SqlDataReader _SqlData = null;
                StringBuilder _s = new StringBuilder();
                _s.Append($"SELECT * FROM dbo.tbClub WHERE ");
                if (ID.Length > 0)
                {
                    _s.Append($"ID = '{ID}' AND ");
                }
                if (eMail.Length > 0)
                {
                    _s.Append($"EMail = '{eMail}' AND ");
                }
                _s.Remove(_s.Length - 4, 4);
                _s.Append($";");

                SqlCommand _SqlStr = new SqlCommand(_s.ToString(), con);

                _SqlData = _SqlStr.ExecuteReader();
                _SqlStr.Dispose();
                while (_SqlData.Read())
                {
                    // Populate Personalia 
                    _c.ID = _SqlData["PlayerID"].ToString();
                    _c.Name = _SqlData["FirstName"].ToString();
                    _c.Picture = _SqlData["Picture"].ToString();
                    _c.Mobile = _SqlData["Mobile"].ToString();
                    _c.EMail = _SqlData["EMail"].ToString();

                    if (_SqlData["RegDate"].ToString() == "")
                    {
                    }
                    else _c.RegDate = DateTime.Parse(_SqlData["RegDate"].ToString());


                    // Populate Modul Access
                    if (_SqlData["AdminSubscribtion"].ToString() == "")
                    {
                    }
                    else _c.AdminSubscribtion = DateTime.Parse(_SqlData["AdminSubscribtion"].ToString());

                    if (_SqlData["WebSideSubscribtion"].ToString() == "")
                    {
                    }
                    else _c.WebSideSubscribtion = DateTime.Parse(_SqlData["WebSideSubscribtion"].ToString());

                    _c.Tokens = (int)_SqlData["Tokens"];
                    _c.TournamentCreatorQTY = (int)_SqlData["TournamentCreatorQTY"];
                    _c.PersonRegQTY = (int)_SqlData["PersonRegQTY"];
                    _c.TicketSaleQTY = (int)_SqlData["TicketSaleQTY"];
                    _c.BlindStructureQTY = (int)_SqlData["BlindStructureQTY"];
                    _c.PayoutStructureQTY = (int)_SqlData["PayoutStructureQTY"];
                    _c.PointStructureQTY = (int)_SqlData["PointStructureQTY"];
                    _c.AdvertiseModuleQTY = (int)_SqlData["AdvertiseModuleQTY"];
                    _c.TableManagerQTY = (int)_SqlData["TableManagerQTY"];
                    _c.StandUserName = _SqlData["StandUserName"].ToString();
                    _c.StandPassWord = _SqlData["StandPassWord"].ToString();
                }
                // Save LifeTime
            }
            catch (Exception e)
            {
                _ = e.ToString();
            }
            con.Close();
            return _c;
        }
        public Club GetClub(string dataRow1, string dataValue1, string dataRow2, string dataValue2)
        {
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbClub; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {
                SqlDataReader _SqlData = null;
                SqlCommand _SqlStr;
                if (dataRow2.Length >0)
                {
                     _SqlStr = new SqlCommand($"SELECT * FROM dbo.tbPerson WHERE {dataRow1} = '{dataValue1}' AND {dataRow2} = '{dataValue2}';", con);
                } else  _SqlStr = new SqlCommand($"SELECT * FROM dbo.tbPerson WHERE {dataRow1} = '{dataValue1}';", con);
                _SqlData = _SqlStr.ExecuteReader();
                _SqlStr.Dispose();
                while (_SqlData.Read())
                {
                    // Populate Personalia 
                    this.ID = _SqlData["PlayerID"].ToString();
                    this.Name = _SqlData["FirstName"].ToString();
                    this.Picture = _SqlData["Picture"].ToString();
                    this.Mobile = _SqlData["Mobile"].ToString();
                    this.EMail = _SqlData["EMail"].ToString();

                    if (_SqlData["RegDate"].ToString() == "")
                    {
                    }
                    else this.RegDate = DateTime.Parse(_SqlData["RegDate"].ToString());


                    // Populate Modul Access
                    if (_SqlData["AdminSubscribtion"].ToString() == "")
                    {
                    }
                    else this.AdminSubscribtion = DateTime.Parse(_SqlData["AdminSubscribtion"].ToString());

                    if (_SqlData["WebSideSubscribtion"].ToString() == "")
                    {
                    }
                    else this.WebSideSubscribtion = DateTime.Parse(_SqlData["WebSideSubscribtion"].ToString());

                    this.Tokens = (int)_SqlData["Tokens"];
                    this.TournamentCreatorQTY = (int)_SqlData["TournamentCreatorQTY"];
                    this.PersonRegQTY = (int)_SqlData["PersonRegQTY"];
                    this.TicketSaleQTY = (int)_SqlData["TicketSaleQTY"];
                    this.BlindStructureQTY = (int)_SqlData["BlindStructureQTY"];
                    this.PayoutStructureQTY = (int)_SqlData["PayoutStructureQTY"];
                    this.PointStructureQTY = (int)_SqlData["PointStructureQTY"];
                    this.AdvertiseModuleQTY = (int)_SqlData["AdvertiseModuleQTY"];
                    this.TableManagerQTY = (int)_SqlData["TableManagerQTY"];
                    this.StandUserName = _SqlData["StandUserName"].ToString();
                    this.StandPassWord = _SqlData["StandPassWord"].ToString();
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
