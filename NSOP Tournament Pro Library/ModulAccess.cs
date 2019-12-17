using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NSOP_Torunament_Pro_Library
{
    [Serializable]
    public class ModulAccess
    {
        private string _classType = "ModulAccess";
        public string ClassType { get => _classType; set => _classType = value; }

        private string _ActionType = "";
        public string ActionType { get => _ActionType; set => _ActionType = value; }

        private string _PlayerID = "0000 0000 0000 000P";
        public string PlayerID { get => _PlayerID; set => _PlayerID = value; }

        private string _ClubID = "0000C";
        public string ClubID { get => _ClubID; set => _ClubID = value; }

        private DateTime _AdminSubscribtion = DateTime.Parse("01.01.1900");
        public DateTime AdminSubscribtion { get => _AdminSubscribtion; set => _AdminSubscribtion = value; }

        private DateTime _WebSideSubscribtion = DateTime.Parse("01.01.1900");
        public DateTime WebSideSubscribtion { get => _WebSideSubscribtion; set => _WebSideSubscribtion = value; }

        // Check if new day
        private DateTime _1 = DateTime.Parse("01.01.1900");
        public DateTime LastLogin { get => _1; set => _1 = value; }

        private int _Tokens = 0;
        public int Tokens { get => _Tokens; set => _Tokens = value; }

        private int _2 = 0;
        public int TournamentCreatorQTY { get => _2; set => _2 = value; }

        private int _3 = 0;
        public int TournamentPersonQTY { get => _3; set => _3 = value; }

        private int _4 = 0;
        public int TournamentTicketQTY { get => _4; set => _4 = value; }

        private int _5 = 0;
        public int TournamentBlindQTY { get => _5; set => _5 = value; }

        private int _6 = 0;
        public int TournamentPayoutQTY { get => _6; set => _6 = value; }

        private int _7 = 0;
        public int TournamentPointQTY { get => _7; set => _7 = value; }

        private int _8 = 0;
        public int AdventModuleQTY { get => _8; set => _8 = value; }

        private int _9 = 0;
        public int TableManagerQTY { get => _9; set => _9 = value; }

        // Pre-Generated Username every login per day
        private string _10 = "";
        public string StandUserName { get => _10; set => _10 = value; }

        // Pre-Generated Password every login per day
        private string _11 = "";
        public string StandPassWord { get => _11; set => _11 = value; }

        // empty Constructor
        public ModulAccess()
        {
        }

        // Constructor to send ModulAccess
        public ModulAccess(string actionType, string playerID, string clubID)
        {
            this.ClassType = "ModulAccess";
            this.ActionType = actionType;
            this.PlayerID = playerID;
            this.ClubID = clubID;
        }
        // Constructor to recieve ModulAccess
        public ModulAccess(byte[] packetBytes)
        {
            BinaryFormatter _bf = new BinaryFormatter();
            MemoryStream _ms = new MemoryStream(packetBytes);
            _ms.Position = 0;
            ModulAccess _modulAccess = (ModulAccess)_bf.Deserialize(_ms);
            _ms.Close();

            // populate all data
            this.ClassType = "ModulAccess";
            this.ActionType = _modulAccess.ActionType;
            this.PlayerID = _modulAccess.PlayerID;
            this.ClubID = _modulAccess.ClubID;
            this.AdminSubscribtion = _modulAccess.AdminSubscribtion;
            this.AdventModuleQTY = _modulAccess.AdventModuleQTY;
            this.TableManagerQTY = _modulAccess.TableManagerQTY;
            this.Tokens = _modulAccess.Tokens;
            this.TournamentBlindQTY = _modulAccess.TournamentBlindQTY;
            this.TournamentCreatorQTY = _modulAccess.TournamentCreatorQTY;
            this.TournamentPayoutQTY = _modulAccess.TournamentPayoutQTY;
            this.TournamentPersonQTY = _modulAccess.TournamentPointQTY;
            this.WebSideSubscribtion = _modulAccess.WebSideSubscribtion;
        }
        // Convert ModulAccess to Byte[]
        public byte[] ToBytes()
        {
            BinaryFormatter _bf = new BinaryFormatter();
            MemoryStream _ms = new MemoryStream();
            _ms.Position = 0;
            _bf.Serialize(_ms, this);
            byte[] bytes = _ms.ToArray();
            _ms.Close();
            return bytes;
        }
        public bool SaveNew()
        {
            bool _isOk = false;
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbPerson; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $" INSERT into tbModulAccess ";
                cmd.CommandText += $"( ";
                cmd.CommandText += $"ClassType, ActionType, PlayerID, ClubID, AdminSubscribtion, WebSideSubscribtion, Tokens, ";
                cmd.CommandText += $"TournamentCreatorQTY, TournamentPersonQTY, ";
                cmd.CommandText += $"TournamentTicketQTY, TournamentBlindQTY, ";
                cmd.CommandText += $"TournamentPayoutQTY, TournamentPointQTY, ";
                cmd.CommandText += $"AdventModuleQTY, TableManagerQTY ";
                cmd.CommandText += $") ";
                cmd.CommandText += $"VALUES ";
                cmd.CommandText += $"( ";
                cmd.CommandText += $"'{ClassType}','{ActionType}','{PlayerID}','{ClubID}',@AdminSubscribtion,@WebSideSubscribtion,{Tokens}, ";
                cmd.CommandText += $"{TournamentCreatorQTY},{TournamentPersonQTY},{TournamentTicketQTY},{TournamentBlindQTY}, ";
                cmd.CommandText += $"{TournamentPayoutQTY},{TournamentPointQTY},{AdventModuleQTY},{TableManagerQTY} ";
                cmd.Parameters.AddWithValue("@AdminSubscribtion", AdminSubscribtion.ToShortDateString()); ;
                cmd.Parameters.AddWithValue("@WebSideSubscribtion", WebSideSubscribtion.ToShortDateString());
                cmd.CommandText += $")";
                cmd.ExecuteNonQuery();
                _isOk = true;
            }
            catch (Exception e)
            {
                string xError = e.ToString();
                _isOk = false;
            }
            con.Close();
            return _isOk;
        }
        public bool Update(ModulAccess modulAccess)
        {
            bool _isOk = false;
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbPerson; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $" UPDATE tbModulAccess SET ";
                cmd.CommandText += $"ClassType = '{modulAccess.ClassType}', ActionType = '{modulAccess.ActionType}', PlayerID = '{modulAccess.PlayerID}', ClubID ,'{modulAccess.ClubID}' ";
                cmd.CommandText += $"AdminSubscribtion = '{modulAccess.AdminSubscribtion.ToShortDateString()}', WebSideSubscribtion = '{modulAccess.WebSideSubscribtion.ToShortDateString()}', PlayerID = '{modulAccess.Tokens}', ";
                cmd.CommandText += $"TournamentCreatorQTY = '{modulAccess.TournamentCreatorQTY}', ";
                cmd.CommandText += $"TournamentPersonQTY = '{modulAccess.TournamentPersonQTY}', ";
                cmd.CommandText += $"TournamentTicketQTY = '{modulAccess.TournamentTicketQTY}', ";
                cmd.CommandText += $"TournamentPayoutQTY = '{modulAccess.TournamentPayoutQTY}', ";
                cmd.CommandText += $"TournamentPointQTY = '{modulAccess.TournamentPointQTY}',  ";
                cmd.CommandText += $"AdventModuleQTY = '{modulAccess.AdventModuleQTY}', ";
                cmd.CommandText += $"TableManagerQTY = '{modulAccess.TableManagerQTY}'  ";
                cmd.CommandText += $"WHERE PlayerID = '{modulAccess.ClubID}';";

                //error = cmd.CommandText;
                cmd.ExecuteNonQuery();

                // Save LifeTime
                _isOk = true;
            }
            catch (Exception e)
            {
                string xError = e.ToString();
                _isOk = false;
            }
            con.Close();
            return _isOk;
        }
        public bool Delete(ModulAccess modulAccess)
        {
            bool _isOk = false;
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbPerson; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $" DELETE FROM tbModulAccess WHERE ClubID = '{modulAccess.ClubID}';";
                cmd.ExecuteNonQuery();
                _isOk = true;
            }
            catch (Exception e)
            {
                string xError = e.ToString();
                _isOk = false;
            }
            con.Close();
            return _isOk;
        }
        public ModulAccess GetModulAccess(string modulAccessID)
        {
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbPerson; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {
                SqlDataReader _SqlData = null;
                SqlCommand _SqlStr = new SqlCommand($"SELECT * FROM dbo.tbModulAccess WHERE PlayerID = '{modulAccessID}';", con);
                _SqlData = _SqlStr.ExecuteReader();
                while (_SqlData.Read())
                {
                    this.ClassType = _SqlData["ClassType"].ToString();
                    this.ActionType = _SqlData["ActionType"].ToString();
                    this.PlayerID = _SqlData["PlayerID"].ToString();
                    this.ClubID = _SqlData["ClubID"].ToString();
                }
            }
            catch (Exception e)
            {
                string xError = e.ToString();
            }
            con.Close();
            return this;
        }
        public bool CheckModulAccess(string userName, string passWord)
        {
            bool _isOk = false;
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbPerson; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {
                SqlDataReader _SqlData = null;
                SqlCommand _SqlStr = new SqlCommand($"SELECT * FROM dbo.tbModulAccess WHERE ClubID = '{ClubID}' AND PlayerID = '{PlayerID}';", con);
                _SqlData = _SqlStr.ExecuteReader();
                while (_SqlData.Read())
                {
                }
                _isOk = true;
                // Save LifeTime
            }
            catch (Exception e)
            {
                string xError = e.ToString();
                _isOk = false;
            }
            con.Close();
            return _isOk;
        }
    }
}
