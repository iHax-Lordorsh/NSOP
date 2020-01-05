using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NSOP_Tournament_Pro_Library
{
    [Serializable]
    public class Tournament
    {
        private string _classType = "Tournament";
        public string ClassType { get => _classType; set => _classType = value; }

        private string _TournamentID = "0000 0000 0000 000P";
        public string TournamentID { get => _TournamentID; set => _TournamentID = value; }

        private string _TournamentName = "NSOP";
        public string TournamentName { get => _TournamentName; set => _TournamentName = value; }

        private byte[] _Picture = null;
        public byte[] Picture { get => _Picture; set => _Picture = value; }

        private string _GameType = "Tournament";
        public string GameType { get => _GameType; set => _GameType = value; }

        private string _GameStyle = "Texas Holdem";
        public string GameStyle { get => _GameStyle; set => _GameStyle = value; }

        private string _GameLimits = "No Limits";
        public string GameLimits { get => _GameLimits; set => _GameLimits = value; }

        private string _GameVariant = "Rebuy";
        public string GameVariant { get => _GameVariant; set => _GameVariant = value; }

        private string _Gender = "Any";
        public string Gender { get => _Gender; set => _Gender = value; }

        private byte[] _Nationality = null;
        public byte[] Nationality { get => _Nationality; set => _Nationality = value; }

        private string _Iso3166Name = "NO";
        public string Iso3166Name { get => _Iso3166Name; set => _Iso3166Name = value; }

        private bool _ClubMembership = false;
        public bool ClubMembership { get => _ClubMembership; set => _ClubMembership = value; }

        private bool _NSOPMembership = false;
        public bool NSOPMembership { get => _NSOPMembership; set => _NSOPMembership = value; }

        private bool _NationalMembership = false;
        public bool NationalMembership { get => _NationalMembership; set => _NationalMembership = value; }

        private bool _WorldMembership = false;
        public bool WorldMembership { get => _WorldMembership; set => _WorldMembership = value; }

        private int _EntryCost = 0;
        public int EntryCost { get => _EntryCost; set => _EntryCost = value; }

        private int _EntryFee = 0;
        public int EntryFee { get => _EntryFee; set => _EntryFee = value; }

        private int _StartingChips = 0;
        public int StartingChips { get => _StartingChips; set => _StartingChips = value; }

        private int _EarlyRegistrered = 0;
        public int EarlyRegistrered { get => _EarlyRegistrered; set => _EarlyRegistrered = value; }

        private int _EarlieAttending = 0;
        public int EarlieAttending { get => _EarlieAttending; set => _EarlieAttending = value; }

        private short _LateRegLevel = 0;
        public short LateRegLevel { get => _LateRegLevel; set => _LateRegLevel = value; }

        private short _RebuyQty = 0;
        public short RebuyQty { get => _RebuyQty; set => _RebuyQty = value; }

        private int _RebuyCost = 0;
        public int RebuyCost { get => _RebuyCost; set => _RebuyCost = value; }

        private int _RebuyFee = 0;
        public int RebuyFee { get => _RebuyFee; set => _RebuyFee = value; }

        private int _RebuyChips = 0;
        public int RebuyChips { get => _RebuyChips; set => _RebuyChips = value; }

        private short _RebuyPoints = 0;
        public short RebuyPoints { get => _RebuyPoints; set => _RebuyPoints = value; }

        private short _AddonQty = 0;
        public short AddonQty { get => _AddonQty; set => _AddonQty = value; }

        private int _AddonCost = 0;
        public int AddonCost { get => _AddonCost; set => _AddonCost = value; }

        private int _AddonFee = 0;
        public int AddonFee { get => _AddonFee; set => _AddonFee = value; }

        private int _AddonChips = 0;
        public int AddonChips { get => _AddonChips; set => _AddonChips = value; }

        private short _AddonPoints = 0;
        public short AddonPoints { get => _AddonPoints; set => _AddonPoints = value; }

        private short _TableSize = 0;
        public short TableSize { get => _TableSize; set => _TableSize = value; }

        private short _MaxTables = 0;
        public short MaxTables { get => _MaxTables; set => _MaxTables = value; }

        private int _MaxEntries = 0;
        public int MaxEntries { get => _MaxEntries; set => _MaxEntries = value; }

        private int _RegPlayers = 0;
        public int RegPlayers { get => _RegPlayers; set => _RegPlayers = value; }

        private int _PayoutValue = 0;
        public int PayoutValue { get => _PayoutValue; set => _PayoutValue = value; }

        private string _PayoutStructure = "0000 0000 0000 000P";
        public string PayoutStructure { get => _PayoutStructure; set => _PayoutStructure = value; }

        private short _StartTable = 0;
        public short StartTable { get => _StartTable; set => _StartTable = value; }

        private string _RemoveTableHighLow = "HIGH";
        public string RemoveTableHighLow { get => _RemoveTableHighLow; set => _RemoveTableHighLow = value; }

        private DateTime _StartDate = DateTime.Now;
        public DateTime StartDate { get => _StartDate; set => _StartDate = value; }

        private short _WaitingList = 0;
        public short WaitingList { get => _WaitingList; set => _WaitingList = value; }

        private short _WaitingMax = 0;
        public short WaitingMax { get => _WaitingMax; set => _WaitingMax = value; }

        private short _ExcludedSeats = 0;
        public short ExcludedSeats { get => _ExcludedSeats; set => _ExcludedSeats = value; }

        private short _TotalRounds = 0;
        public short TotalRounds { get => _TotalRounds; set => _TotalRounds = value; }

        private short _ValidRounds = 0;
        public short ValidRounds { get => _ValidRounds; set => _ValidRounds = value; }

        private string _FinalID = "0000 0000 0000 000T";
        public string FinalID { get => _FinalID; set => _FinalID = value; }

        private int _FinalFee = 0;
        public int FinalFee { get => _FinalFee; set => _FinalFee = value; }

        private short _FinalMinRounds = 0;
        public short FinalMinRounds { get => _FinalMinRounds; set => _FinalMinRounds = value; }

        private short _FinalMinPoints = 0;
        public short FinalMinPoints { get => _FinalMinPoints; set => _FinalMinPoints = value; }

        private short _FinalDirect = 0;
        public short FinalDirect { get => _FinalDirect; set => _FinalDirect = value; }

        private int _FinalPayout = 0;
        public int FinalPayout { get => _FinalPayout; set => _FinalPayout = value; }

        private string _FinalBlindStructure = "0000 0000 0000 000T";
        public string FinalBlindStructure { get => _FinalBlindStructure; set => _FinalBlindStructure = value; }

        private string _SemiID = "0000 0000 0000 000B";
        public string SemiID { get => _SemiID; set => _SemiID = value; }

        private int _SemiFee = 0;
        public int SemiFee { get => _SemiFee; set => _SemiFee = value; }

        private short _SemiMinRounds = 0;
        public short SemiMinRounds { get => _SemiMinRounds; set => _SemiMinRounds = value; }

        private short _SemiMinPoints = 0;
        public short SemiMinPoints { get => _SemiMinPoints; set => _SemiMinPoints = value; }

        private short _SemiDirect = 0;
        public short SemiDirect { get => _SemiDirect; set => _SemiDirect = value; }

        private int _SemiPayout = 0;
        public int SemiPayout { get => _SemiPayout; set => _SemiPayout = value; }

        private short _SemiToFinalTicket = 0;
        public short SemiToFinalTickets { get => _SemiToFinalTicket; set => _SemiToFinalTicket = value; }

        private string _SemiBlindStructure = "0000 0000 0000 000B";
        public string SemiBlindStructure { get => _SemiBlindStructure; set => _SemiBlindStructure = value; }

        private bool _TournamentWinToSemi = false;
        public bool TournamentWinToSemi { get => _TournamentWinToSemi; set => _TournamentWinToSemi = value; }

        private bool _TournamentWinToFinal = false;
        public bool TournamentWinToFinal { get => _TournamentWinToFinal; set => _TournamentWinToFinal = value; }

        private bool _IsPlayed = false;
        public bool IsPlayed { get => _IsPlayed; set => _IsPlayed = value; }

        // empty Constructor
        public Tournament()
        {
        }

        // Constructor to send Tournament
        public Tournament(string TournamentID, string TournamentName, byte[] Picture, string GameType, string GameStyle, string GameLimits, string GameVariant,
            string Gender, byte[] Nationality, string Iso3166Name, bool ClubMembership, bool NSOPMembership, bool NationalMembership, bool WorldMembership)
        {
            this.ClassType = "Tournament";
            this.TournamentID = TournamentID;
            this.TournamentName = TournamentName;
            this.Picture = Picture;
            this.GameType = GameType;
            this.GameStyle = GameStyle;
            this.GameLimits = GameLimits;
            this.GameVariant = GameVariant;
            this.Gender = Gender;
            this.Nationality = Nationality;
            this.Iso3166Name = Iso3166Name;
            this.ClubMembership = ClubMembership;
            this.NSOPMembership = NSOPMembership;
            this.NationalMembership = NationalMembership;
            this.WorldMembership = WorldMembership;
        }

        // Constructor to recieve Tournament
        public Tournament(byte[] packetBytes)
        {
            BinaryFormatter _bf = new BinaryFormatter();
            MemoryStream _ms = new MemoryStream(packetBytes);
            _ms.Position = 0;
            Tournament _Tournament = (Tournament)_bf.Deserialize(_ms);
            _ms.Close();

            // populate all data
            this.ClassType = "Tournament";
            this.TournamentID = _Tournament.TournamentID;
            this.TournamentName = _Tournament.TournamentName;
            this.Picture = _Tournament.Picture;
            this.GameType = _Tournament.GameType;
            this.GameStyle = _Tournament.GameStyle;
            this.GameLimits = _Tournament.GameLimits;
            this.GameVariant = _Tournament.GameVariant;
            this.Gender = _Tournament.Gender;
            this.Nationality = _Tournament.Nationality;
            this.Iso3166Name = _Tournament.Iso3166Name;
            this.ClubMembership = _Tournament.ClubMembership;
            this.NSOPMembership = _Tournament.NSOPMembership;
            this.NationalMembership = _Tournament.NationalMembership;
            this.WorldMembership = _Tournament.WorldMembership;
        }

        // Convert Tournament to Byte[]
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
        public bool Save()
        {
            bool _isOk = false;
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbTournament; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $" INSERT into tbTournament ";
                cmd.CommandText += $"(";
                cmd.CommandText += $"ClassType, TournamentID, TournamentName, Picture, GameType, ";
                cmd.CommandText += $"GameStyle, GameLimits, GameVariant,Gender, Nationality, ";
                cmd.CommandText += $"Iso3166Name, ClubMembership, NSOPMembership, NationalMembership, WorldMembership, ";
                cmd.CommandText += $"EntryCost, EntryFee, StartingChips, EarlyRegistrered, EarlyAttending, LateRegLevel, ";
                cmd.CommandText += $"RebuyQty, RebuyCost, RebuyFee, RebuyChips, RebuyPoints, ";
                cmd.CommandText += $"AddonQty, AddonCost, AddonFee, AddonChips, AddonPoints, ";
                cmd.CommandText += $"TableSize, MaxTables, MaxEntries, RegPlayers, PayoutValue, ";
                cmd.CommandText += $"PayoutStructure, StartTable, RemoveTableHighLow, StartDate, WaitingList, ";
                cmd.CommandText += $"WaitingMax, ExcludedSeats, TotalRounds, ValidRounds, FinalID, ";
                cmd.CommandText += $"FinalFee, FinalMinRounds, FinalMinPoints, FinalDirect, FinalPayout, ";
                cmd.CommandText += $"FinalBlindStructure, SemiID, SemiFee, SemiMinRounds, SemiMinPoints, ";
                cmd.CommandText += $"SemiDirect, SemiPayout, SemiToFinalTickets, SemiBlindStructure, TournamentWinToSemi, TournamentWinToFinal, IsPlayed ";
                cmd.CommandText += $") VALUES ( ";
                cmd.CommandText += $"'{ClassType}','{TournamentID}','{TournamentName}', @Picture,'{GameType}', ";
                cmd.CommandText += $"'{GameStyle}','{GameLimits}','{GameVariant}','{Gender}','{Nationality}', ";
                cmd.CommandText += $"'{Iso3166Name}','{ClubMembership.ToString()}','{NSOPMembership.ToString()}','{NationalMembership.ToString()}','{WorldMembership.ToString()}', ";
                cmd.CommandText += $"'{EntryCost.ToString()}','{EntryFee.ToString()}','{StartingChips.ToString()}','{EarlyRegistrered.ToString()}','{EarlieAttending.ToString()}','{LateRegLevel.ToString()}', ";
                cmd.CommandText += $"'{RebuyQty.ToString()}','{RebuyCost.ToString()}','{RebuyFee.ToString()}','{RebuyChips.ToString()}','{RebuyPoints.ToString()}', ";
                cmd.CommandText += $"'{AddonQty.ToString()}','{AddonCost.ToString()}','{AddonFee.ToString()}','{AddonChips.ToString()}','{AddonPoints.ToString()}', ";
                cmd.CommandText += $"'{TableSize.ToString()}','{MaxTables.ToString()}','{MaxEntries.ToString()}','{RegPlayers.ToString()}','{PayoutValue.ToString()}', ";
                cmd.CommandText += $"'{PayoutStructure}','{StartTable.ToString()}','{RemoveTableHighLow.ToString()}','{StartDate.ToString()}','{WaitingList.ToString()}', ";
                cmd.CommandText += $"'{WaitingMax.ToString()}','{ExcludedSeats.ToString()}','{TotalRounds.ToString()}','{ValidRounds.ToString()}','{FinalID}', ";
                cmd.CommandText += $"'{FinalFee.ToString()}','{FinalMinRounds.ToString()}','{FinalMinPoints.ToString()}','{FinalDirect.ToString()}','{FinalPayout.ToString()}', ";
                cmd.CommandText += $"'{FinalBlindStructure}','{SemiID}','{SemiFee.ToString()}','{SemiMinRounds.ToString()}','{SemiMinPoints.ToString()}', ";
                cmd.CommandText += $"'{SemiDirect.ToString()}','{SemiPayout.ToString()}','{SemiToFinalTickets.ToString()}','{SemiBlindStructure}','{TournamentWinToSemi}','{TournamentWinToFinal}','{IsPlayed}' ";
                cmd.CommandText += $")";
                cmd.Parameters.AddWithValue("@Picture", Picture);

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
        public bool Update()
        {
            bool _isOk = false;
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbTournament; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $" UPDATE tbTournament SET ";
                cmd.CommandText += $"ClassType = '{ClassType}', TournamentID ='{TournamentID}', ";
                cmd.CommandText += $"TournamentName = '{TournamentName}', Picture = '{Picture}', GameType = '{GameType}', ";
                cmd.CommandText += $"GameStyle = '{GameStyle}', GameLimits = '{GameLimits}', ";
                cmd.CommandText += $"GameVariant ='{GameVariant}',Gender ='{Gender}', Nationality ='{Nationality}', ";
                cmd.CommandText += $"Iso3166Name = '{Iso3166Name}', ClubMembership = '{ClubMembership.ToString()}', NSOPMembership = '{NSOPMembership.ToString()}', ";
                cmd.CommandText += $"NationalMembership = '{NationalMembership.ToString()}', WorldMembership = '{WorldMembership.ToString()}' ";
                cmd.CommandText += $"WHERE TournamentID = '{TournamentID}';";

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
        public bool Delete()
        {
            bool _isOk = false;
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbTournament; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $" DELETE FROM tbTournament WHERE TournamentID = '{TournamentID}';";

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
        public Tournament Select()
        {
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbTournament; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {
                SqlDataReader _SqlData = null;
                SqlCommand _SqlStr = new SqlCommand($"SELECT * FROM tbTournament WHERE TournamentID >= @prm1 AND cTournamentName <= @vToTournamentName AND cStillPlaying = 1", con);
                SqlParameter _prm1 = new SqlParameter("@prm1", TournamentID.ToString());
                _SqlStr.Parameters.Add(_prm1);
                _SqlData = _SqlStr.ExecuteReader();
                while (_SqlData.Read())
                {
                    this.ClassType = _SqlData["ClassType"].ToString();
                    this.TournamentName = _SqlData["TournamentName"].ToString();
                    this.Picture = (byte[])_SqlData["Picture"];
                    this.GameType = _SqlData["GameType"].ToString();
                    this.GameStyle = _SqlData["GameStyle"].ToString();
                    this.GameLimits = _SqlData["GameLimits"].ToString();
                    this.GameVariant = _SqlData["GameVariant"].ToString();
                    this.Gender = _SqlData["Gender"].ToString();
                    this.Nationality = (byte[])_SqlData["Nationality"];
                    this.Iso3166Name = _SqlData["Iso3166Name"].ToString();
                    this.ClubMembership = (bool)_SqlData["ClubMembership"];
                    this.NSOPMembership = (bool)_SqlData["NSOPMembership"];
                    this.NationalMembership = (bool)_SqlData["NationalMembership"];
                    this.WorldMembership = (bool)_SqlData["WorldMembership"];
                }

                // Save LifeTime
            }
            catch (Exception e)
            {
                string xError = e.ToString();
            }
            con.Close();
            return this;
        }
    }
}
