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
        private string _classType = "Person";
        public string ClassType { get => _classType; set => _classType = value; }

        private string _ActionType = "";
        public string ActionType { get => _ActionType; set => _ActionType = value; }

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

        private string _UserName = "";
        public string UserName { get => _UserName; set => _UserName = value; }

        private string _PassWord = "";
        public string PassWord { get => _PassWord; set => _PassWord = value; }

        private string _UserID = "";
        public string UserID { get => _UserID; set => _UserID = value; }

        private bool _IsPlayerRemoved = false;
        public bool IsPlayerRemoved { get => _IsPlayerRemoved; set => _IsPlayerRemoved = value; }

        private bool _IsLoggedInn = false;
        public bool IsLoggedInn { get => _IsLoggedInn; set => _IsLoggedInn = value; }

        private string _ClubID = "";
        public string ClubID { get => _ClubID; set => _ClubID = value; }

        private string _CPicture;
        public string ClubPicture { get => _CPicture; set => _CPicture = value; }

        private string _ClubName = "";
        public string ClubName { get => _ClubName; set => _ClubName = value; }
        // ***********
        // RANK
        // ***********
        // Club rank
        private int _cClubRank = 0;
        public int ClubRank { get => _cClubRank; set => _cClubRank = value; }

        private long _cClubPoints = 0;
        public long ClubPoints { get => _cClubPoints; set => _cClubPoints = value; }

        private int _cClubMembership = 0;
        public int ClubMembership { get => _cClubMembership; set => _cClubMembership = value; }

        private DateTime _cClubMembershipExpires;// = DateTime.Parse("01.01.1900");
        public DateTime ClubMembershipExpires { get => _cClubMembershipExpires; set => _cClubMembershipExpires = value; }

        private DateTime _cClubRegDate;// = DateTime.Parse("01.01.1900");
        public DateTime ClubRegDate { get => _cClubRegDate; set => _cClubRegDate = value; }

        // NSOP rank
        private string _NSOPID = "";
        public string NSOPID { get => _NSOPID; set => _NSOPID = value; }

        private int _cNSOPRank = 0;
        public int NSOPRank { get => _cNSOPRank; set => _cNSOPRank = value; }

        private long _cNSOPPoints = 0;
        public long NSOPPoints { get => _cNSOPPoints; set => _cNSOPPoints = value; }

        private int _cNSOPMembership = 0;
        public int NSOPMembership { get => _cNSOPMembership; set => _cNSOPMembership = value; }

        private DateTime _cNSOPMembershipExpires;// = DateTime.Parse("01.01.1900");
        public DateTime NSOPMembershipExpires { get => _cNSOPMembershipExpires; set => _cNSOPMembershipExpires = value; }

        private DateTime _cNSOPRegDate;// = DateTime.Parse("01.01.1900");
        public DateTime NSOPRegDate { get => _cNSOPRegDate; set => _cNSOPRegDate = value; }

        // National rank
        private string _nationalID = "";
        public string NationalID { get => _nationalID; set => _nationalID = value; }

        private int _cNationalRank = 0;
        public int NationalRank { get => _cNationalRank; set => _cNationalRank = value; }

        private long _cNationalPoints = 0;
        public long NationalPoints { get => _cNationalPoints; set => _cNationalPoints = value; }

        private int _cNationalMembership = 0;
        public int NationalMembership { get => _cNationalMembership; set => _cNationalMembership = value; }

        private DateTime _cNationalMembershipExpires;// = DateTime.Parse("01.01.1900");
        public DateTime NationalMembershipExpires { get => _cNationalMembershipExpires; set => _cNationalMembershipExpires = value; }

        private DateTime _cNationalRegDate;// = DateTime.Parse("01.01.1900");
        public DateTime NationalRegDate { get => _cNationalRegDate; set => _cNationalRegDate = value; }

        //World Rank
        private string _worldID = "000";
        public string WorldID { get => _worldID; set => _worldID = value; }

        private int _cWorldRank = 0;
        public int WorldRank { get => _cWorldRank; set => _cWorldRank = value; }

        private long _cWorldPoints = 0;
        public long WorldPoints { get => _cWorldPoints; set => _cWorldPoints = value; }

        private int _cWorldMembership = 0;
        public int WorldMembership { get => _cWorldMembership; set => _cWorldMembership = value; }

        private DateTime _cWorldMembershipExpires;// = DateTime.Parse("01.01.1900");
        public DateTime WorldMembershipExpires { get => _cWorldMembershipExpires; set => _cWorldMembershipExpires = value; }

        private DateTime _cWorldRegDate;// = DateTime.Parse("01.01.1900");
        public DateTime WorldRegDate { get => _cWorldRegDate; set => _cWorldRegDate = value; }

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

        // ***********
        // MODUL ACCESS
        // ***********
        private DateTime _AdminSubscribtion;// = DateTime.Parse("01.01.1900");
        public DateTime AdminSubscribtion { get => _AdminSubscribtion; set => _AdminSubscribtion = value; }

        private DateTime _WebSideSubscribtion;// = DateTime.Parse("01.01.1900");
        public DateTime WebSideSubscribtion { get => _WebSideSubscribtion; set => _WebSideSubscribtion = value; }

        // Check if new day
        private DateTime _1;// = DateTime.Parse("01.01.1900");
        public DateTime LastLogin { get => _1; set => _1 = value; }

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

        // ***********
        // CONSTRUCTORS
        // ***********

        // Empty Constructor
        public Person()
        {
        }

        // Constructor to send Personalia
        public Person(string playerID, string clubID, string actionType, string firstName, string lastName, byte[] picture, string mobile, string email, string gender, string nationality, string iso3166Name, DateTime bornDate, DateTime regDate, string nickName, string userName, string passWord, string userID, bool isPlayerRemoved)
        {
            this.ClassType = "Person";
            this.ActionType = actionType;
            this.PlayerID = playerID;
            this.ClubID = clubID;
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
            this.UserName = userName;//XXX
            this.PassWord = passWord;
            this.UserID = userID;
            this.IsPlayerRemoved = isPlayerRemoved;
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
            this.ClassType = "Person";
            this.ActionType = _person.ActionType;
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
            this.UserName = _person.UserName;
            this.PassWord = _person.PassWord;
            this.UserID = _person.UserID;
            this.IsPlayerRemoved = _person.IsPlayerRemoved;
            this.IsLoggedInn = _person.IsLoggedInn;
            this.ClubID = _person.ClubID;
            this.ClubName = _person.ClubName;
            this.ClubPicture = _person.ClubPicture;

            // Populate Rank
            this.ClubRank = _person.ClubRank;
            this.ClubPoints = _person.ClubPoints;
            this.ClubMembership = _person.ClubMembership;
            this.ClubMembershipExpires = _person.ClubMembershipExpires;
            this.ClubRegDate = _person.ClubRegDate;
            this.NSOPID = _person.NSOPID;
            this.NSOPRank = _person.NSOPRank;
            this.NSOPPoints = _person.NSOPPoints;
            this.NSOPMembership = _person.NSOPMembership;
            this.NSOPMembershipExpires = _person.NSOPMembershipExpires;
            this.NSOPRegDate = _person.NSOPRegDate;
            this.NationalID = _person.NationalID;
            this.NationalRank = _person.NationalRank;
            this.NationalPoints = _person.NationalPoints;
            this.NationalMembership = _person.NationalMembership;
            this.NationalMembershipExpires = _person.NationalMembershipExpires;
            this.NationalRegDate = _person.NationalRegDate;
            this.WorldID = _person.WorldID;
            this.WorldRank = _person.WorldRank;
            this.WorldPoints = _person.WorldPoints;
            this.WorldMembership = _person.WorldMembership;
            this.WorldMembershipExpires = _person.WorldMembershipExpires;
            this.WorldRegDate = _person.WorldRegDate;

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
            this.AdminSubscribtion = _person.AdminSubscribtion;
            this.WebSideSubscribtion = _person.WebSideSubscribtion;
            this.LastLogin = _person.LastLogin;
            this.Tokens = _person.Tokens;
            this.TournamentCreatorQTY = _person.TournamentCreatorQTY;
            this.PersonRegQTY = _person.PersonRegQTY;
            this.TicketSaleQTY = _person.TicketSaleQTY;
            this.BlindStructureQTY = _person.BlindStructureQTY;
            this.PayoutStructureQTY = _person.PayoutStructureQTY;
            this.PointStructureQTY = _person.PointStructureQTY;
            this.AdvertiseModuleQTY = _person.AdvertiseModuleQTY;
            this.TableManagerQTY = _person.TableManagerQTY;
            this.StandUserName = _person.StandUserName;
            this.StandPassWord = _person.StandPassWord;

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
                cmd.CommandText += $"ClassType, PlayerID, Picture, FirstName, LastName, ";
                cmd.CommandText += $"Mobile, EMail, BornDate, RegDate, UserName, PassWord, UserID, IsPlayerRemoved, ";
                cmd.CommandText += $"IsLoggedInn, ClubID, ClubName, ClubPicture, ";
                // Rank
                cmd.CommandText += $"ClubRank, ClubPoints, ClubMembership, ClubMembershipExpires, ClubRegDate, ";
                cmd.CommandText += $"NSOPID, NSOPRank, NSOPPoints, NSOPMembership, NSOPMembershipExpires, NSOPRegDate, ";
                cmd.CommandText += $"NationalID, NationalRank, NationalPoints, NationalMembership, NationalMembershipExpires, NationalRegDate, ";
                cmd.CommandText += $"WorldID, WorldRank, WorldPoints, WorldMembership, WorldMembershipExpires, WorldRegDate, ";
                // Lifetime
                cmd.CommandText += $"LifetimePlayed, LifetimeWins, LifetimeFinalTables, LifetimeCashed, ";
                cmd.CommandText += $"LifetimeBubbles, LifetimeFirstOuts, LifetimeSevenDeuces, LifetimeBadBeats, ";
                cmd.CommandText += $"LifetimeTakeOuts, LifetimeHunted, LifetimeMoneyEarned, LifetimeMoneySpent, ";
                // Modul Access
                cmd.CommandText += $"AdminSubscribtion, WebSideSubscribtion, LastLogin, Tokens, ";
                cmd.CommandText += $"TournamentCreatorQTY, PersonRegQTY, TicketSaleQTY, BlindStructureQTY, ";
                cmd.CommandText += $"PayoutStructureQTY, PointStructureQTY, AdvertiseModuleQTY, TableManagerQTY, ";
                cmd.CommandText += $"StandUserName, StandPassWord ";
                // Values
                cmd.CommandText += $") ";
                cmd.CommandText += $"VALUES ";
                cmd.CommandText += $"( ";
                // Personalia
                cmd.CommandText += $"'{ClassType}','{PlayerID}', @Picture, '{FirstName}','{LastName}', ";
                cmd.CommandText += $"'{Mobile}','{EMail}','{BornDate.ToShortDateString()}','{RegDate.ToShortDateString()}','{UserName}','{DataAccess.PasswordEncryption(PassWord)}','{Guid.NewGuid()}', 0, ";
                cmd.CommandText += $"1, '{ClubID}','{ClubName}', '{ClubPicture}', ";
                // Rank
                cmd.CommandText += $"'{ClubRank}','{ClubPoints}','{ClubMembership}','{ClubMembershipExpires.ToShortDateString()}','{ClubRegDate.ToShortDateString()}', ";
                cmd.CommandText += $"'{NSOPID}','{NSOPRank}','{NSOPPoints}','{NSOPMembership}','{NSOPMembershipExpires.ToShortDateString()}','{NSOPRegDate.ToShortDateString()}', ";
                cmd.CommandText += $"'{NationalID}','{NationalRank}','{NationalPoints}','{NationalMembership}','{NationalMembershipExpires.ToShortDateString()}','{NationalRegDate.ToShortDateString()}', ";
                cmd.CommandText += $"'{WorldID}','{WorldRank}','{WorldPoints}','{WorldMembership}','{WorldMembershipExpires.ToShortDateString()}','{WorldRegDate.ToShortDateString()}', ";
                // Lifetime
                cmd.CommandText += $"'{LifetimePlayed}','{LifetimeWins}','{LifetimeFinalTables}','{LifetimeCashed}', ";
                cmd.CommandText += $"'{LifetimeBubbles}','{LifetimeFirstOuts}','{LifetimeSevenDeuces}','{LifetimeBadBeats}', ";
                cmd.CommandText += $"'{LifetimeTakeOuts}','{LifetimeHunted}','{LifetimeMoneyEarned}','{LifetimeMoneySpent}', ";
                // Modul Access
                cmd.CommandText += $"'{AdminSubscribtion.ToShortDateString()}','{WebSideSubscribtion.ToShortDateString()}','{LastLogin.ToShortDateString()}','{Tokens}', ";
                cmd.CommandText += $"'{TournamentCreatorQTY}','{PersonRegQTY}','{TicketSaleQTY}','{BlindStructureQTY}', ";
                cmd.CommandText += $"'{PayoutStructureQTY}','{PointStructureQTY}','{AdvertiseModuleQTY}','{TableManagerQTY}', ";
                cmd.CommandText += $"'{StandUserName}','{StandPassWord}' ";
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
                cmd.CommandText += $"ClassType = '{person.ClassType}', ";
                cmd.CommandText += $"FirstName = '{person.FirstName}', LastName = '{person.LastName}', ";
                cmd.CommandText += $"Picture = @Picture, Mobile = '{person.Mobile}', ";
                cmd.CommandText += $"EMail ='{person.EMail}',Gender ='{person.Gender}', Nationality ='{person.Nationality}', ";
                cmd.CommandText += $"Iso3166Name = '{person.Iso3166Name}', BornDate = '{person.BornDate.ToShortDateString()}', RegDate = '{person.RegDate.ToShortDateString()}', ";
                cmd.CommandText += $"NickName = '{person.NickName}', UserName = '{person.UserName}', PassWord = '{person.PassWord}', IsPlayerRemoved = '{person.IsPlayerRemoved}', ";
                cmd.CommandText += $"IsLoggedInn = '{person.IsLoggedInn}', ClubID = '{person.ClubID}', ClubName = '{person.ClubName}', ClubPicture = '{person.ClubPicture}', ";
                // Rank
                cmd.CommandText += $"ClubRank = '{person.ClubRank}', ClubPoints = '{person.ClubPoints}', ";
                cmd.CommandText += $"ClubMembership = '{person.ClubMembership}', ClubMembershipExpires = '{person.ClubMembershipExpires.ToShortDateString()}', ClubRegDate = '{person.ClubRegDate.ToShortDateString()}', ";
                cmd.CommandText += $"NSOPID = '{person.NSOPID}', NSOPRank = '{person.NSOPRank}', NSOPPoints = '{person.NSOPPoints}', ";
                cmd.CommandText += $"NSOPMembership = '{person.NSOPMembership}', NSOPMembershipExpires = '{person.NSOPMembershipExpires.ToShortDateString()}', NSOPRegDate = '{person.NSOPRegDate.ToShortDateString()}', ";
                cmd.CommandText += $"NationalID = '{person.NationalID}', NationalRank = '{person.NationalRank}', NationalPoints = '{person.NationalPoints}', ";
                cmd.CommandText += $"NationalMembership = '{person.NationalMembership}', NationalMembershipExpires = '{person.NationalMembershipExpires.ToShortDateString()}', NationalRegDate = '{person.NationalRegDate.ToShortDateString()}', ";
                cmd.CommandText += $"WorldID = '{person.WorldID}', WorldRank = '{person.WorldRank}', WorldPoints = '{person.WorldPoints}', ";
                cmd.CommandText += $"WorldMembership = '{person.WorldMembership}', WorldMembershipExpires = '{person.WorldMembershipExpires.ToShortDateString()}', WorldRegDate = '{person.WorldRegDate.ToShortDateString()}', ";
                // Lifetime
                cmd.CommandText += $"LifetimePlayed = '{person.LifetimePlayed}', LifetimeWins = '{person.LifetimeWins}', LifetimeFinalTables = '{person.LifetimeFinalTables}', ";
                cmd.CommandText += $"LifetimeCashed = '{person.LifetimeCashed}', LifetimeBubbles = '{person.LifetimeBubbles}', LifetimeFirstOuts = '{person.LifetimeFirstOuts}', ";
                cmd.CommandText += $"LifetimeSevenDeuces = '{person.LifetimeSevenDeuces}', LifetimeBadBeats = '{person.LifetimeBadBeats}', LifetimeTakeOuts = '{person.LifetimeTakeOuts}', ";
                cmd.CommandText += $"LifetimeHunted = '{person.LifetimeHunted}', LifetimeMoneyEarned = '{person.LifetimeMoneyEarned}', LifetimeMoneySpent = '{person.LifetimeMoneySpent}', ";
                // Modul Access
                cmd.CommandText += $"AdminSubscribtion = '{person.AdminSubscribtion}', WebSideSubscribtion = '{person.WebSideSubscribtion}', LastLogin = '{person.LastLogin}', ";
                cmd.CommandText += $"Tokens = '{person.Tokens}', TournamentCreatorQTY = '{person.TournamentCreatorQTY}', PersonRegQTY = '{person.PersonRegQTY}', ";
                cmd.CommandText += $"TicketSaleQTY = '{person.TicketSaleQTY}', BlindStructureQTY = '{person.BlindStructureQTY}', PayoutStructureQTY = '{person.PayoutStructureQTY}', ";
                cmd.CommandText += $"PointStructureQTY = '{person.PointStructureQTY}', AdvertiseModuleQTY = '{person.AdvertiseModuleQTY}', TableManagerQTY = '{person.TableManagerQTY}', ";
                cmd.CommandText += $"StandUserName = '{person.StandUserName}', StandPassWord = '{person.StandPassWord}' ";

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
                cmd.CommandText += $"PassWord = '{DataAccess.PasswordEncryption(person.PassWord)}' ";

                // Where
                cmd.CommandText += $"WHERE PlayerID = '{person.PlayerID}';";


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
        public bool UpdateClub(Person person)
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
                cmd.CommandText += $"ClassType = '{person.ClassType}', ";
                cmd.CommandText += $"FirstName = '{person.FirstName}', LastName = '{person.LastName}', ";
                cmd.CommandText += $"Picture = @Picture, Mobile = '{person.Mobile}', ";
                cmd.CommandText += $"Gender ='{person.Gender}', Nationality ='{person.Nationality}', ";
                cmd.CommandText += $"Iso3166Name = '{person.Iso3166Name}', BornDate = '{person.BornDate.ToShortDateString()}', ";
                cmd.CommandText += $"NickName = '{person.NickName}', ";
                cmd.CommandText += $"ClubName = '{person.ClubName}', ClubPicture = '{person.ClubPicture}', ";
                cmd.CommandText += $"StandUserName = '{person.StandUserName}', StandPassWord = '{person.StandPassWord}' ";

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
        public Person GetPerson(string personID)
        {
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbPerson; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {
                SqlDataReader _SqlData = null;
                SqlCommand _SqlStr = new SqlCommand($"SELECT * FROM dbo.tbPerson WHERE PlayerID = '{personID}';", con);
                _SqlData = _SqlStr.ExecuteReader();
                _SqlStr.Dispose();
                while (_SqlData.Read())
                {
                    // Populate Personalia
                    this.ClassType = _SqlData["ClassType"].ToString();
                    this.ActionType = _SqlData["ActionType"].ToString();
                    this.PlayerID = personID;
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
                    this.UserName = _SqlData["UserName"].ToString();
                    this.PassWord = _SqlData["PassWord"].ToString();
                    this.UserID = _SqlData["UserID"].ToString();
                    this.IsPlayerRemoved = (Boolean)_SqlData["IsPlayerRemoved"];
                    this.IsLoggedInn = (Boolean)_SqlData["IsLoggedInn"];
                    this.ClubID = _SqlData["ClubID"].ToString();
                    this.ClubName = _SqlData["ClubName"].ToString();
                    this.ClubPicture = _SqlData["ClubPicture"].ToString();

                    // Populate Rank
                    this.ClubRank = (int)_SqlData["ClubRank"];
                    this.ClubPoints = (long)_SqlData["ClubPoints"];
                    this.ClubMembership = (int)_SqlData["ClubMembership"];
                    this.ClubMembershipExpires = DateTime.Parse(_SqlData["ClubMembershipExpires"].ToString());
                    this.ClubRegDate = DateTime.Parse(_SqlData["ClubRegDate"].ToString());

                    this.NSOPID = _SqlData["NSOPID"].ToString();
                    this.NSOPRank = (int)_SqlData["NSOPRank"];
                    this.NSOPPoints = (long)_SqlData["NSOPPoints"];
                    this.NSOPMembership = (int)_SqlData["NSOPMembership"];
                    this.NSOPMembershipExpires = DateTime.Parse(_SqlData["NSOPMembershipExpires"].ToString());
                    this.NSOPRegDate = DateTime.Parse(_SqlData["NSOPRegDate"].ToString());

                    this.NationalID = _SqlData["NationalID"].ToString();
                    this.NationalRank = (int)_SqlData["NationalRank"];
                    this.NationalPoints = (long)_SqlData["NationalPoints"];
                    this.NationalMembership = (int)_SqlData["NationalMembership"];
                    this.NationalMembershipExpires = DateTime.Parse(_SqlData["NationalMembershipExpires"].ToString());
                    this.NationalRegDate = DateTime.Parse(_SqlData["NationalRegDate"].ToString());

                    this.WorldID = _SqlData["WorldID"].ToString();
                    this.WorldRank = (int)_SqlData["WorldRank"];
                    this.WorldPoints = (long)_SqlData["WorldPoints"];
                    this.WorldMembership = (int)_SqlData["WorldMembership"];
                    this.WorldMembershipExpires = DateTime.Parse(_SqlData["WorldMembershipExpires"].ToString());
                    this.WorldRegDate = DateTime.Parse(_SqlData["WorldRegDate"].ToString());

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
                    this.AdminSubscribtion = DateTime.Parse(_SqlData["AdminSubscribtion"].ToString());
                    this.WebSideSubscribtion = DateTime.Parse(_SqlData["WebSideSubscribtion"].ToString());
                    this.LastLogin = DateTime.Parse(_SqlData["LastLogin"].ToString());
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
        public static Person CheckPerson(string userName, string passWord)
        {
            Person _p = new Person
            {
                PlayerID = "" // test if player excist
            };
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbPerson; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {
                SqlDataReader _SqlData = null;
                StringBuilder _s = new StringBuilder();
                _s.Append($"SELECT * FROM dbo.tbPerson WHERE UserName = '{userName}'");
                if (passWord.Length>0)
                {
                    _s.Append($"AND PassWord = '{DataAccess.PasswordEncryption(passWord)}'");
                }
                _s.Append($";");
                SqlCommand _SqlStr = new SqlCommand(_s.ToString(), con);

                _SqlData = _SqlStr.ExecuteReader();
                _SqlStr.Dispose();
                while (_SqlData.Read())
                {
                    // Populate Personalia
                    _p.ClassType = _SqlData["ClassType"].ToString();

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
                    _p.UserName = userName;
                    _p.UserID = _SqlData["UserID"].ToString();
                    _p.IsPlayerRemoved = (Boolean)_SqlData["IsPlayerRemoved"];
                    _p.IsLoggedInn = (Boolean)_SqlData["IsLoggedInn"];
                    _p.ClubID = _SqlData["ClubID"].ToString();
                    _p.ClubName = _SqlData["ClubName"].ToString();
                    _p.ClubPicture = _SqlData["ClubPicture"].ToString();

                    // Populate Rank
                    _p.ClubRank = (int)_SqlData["ClubRank"];
                    _p.ClubPoints = (long)_SqlData["ClubPoints"];
                    _p.ClubMembership = (int)_SqlData["ClubMembership"];

                    if (_SqlData["ClubMembershipExpires"].ToString() == "")
                    {
                    }
                    else _p.ClubMembershipExpires = DateTime.Parse(_SqlData["ClubMembershipExpires"].ToString());

                    if (_SqlData["ClubRegDate"].ToString() == "")
                    {
                    }
                    else _p.ClubRegDate = DateTime.Parse(_SqlData["ClubRegDate"].ToString());

                    _p.NSOPID = _SqlData["NSOPID"].ToString();
                    _p.NSOPRank = (int)_SqlData["NSOPRank"];
                    _p.NSOPPoints = (long)_SqlData["NSOPPoints"];
                    _p.NSOPMembership = (int)_SqlData["NSOPMembership"];

                    if (_SqlData["NSOPMembershipExpires"].ToString() == "")
                    {
                    }
                    else _p.NSOPMembershipExpires = DateTime.Parse(_SqlData["NSOPMembershipExpires"].ToString());

                    if (_SqlData["NSOPRegDate"].ToString() == "")
                    {
                    }
                    else _p.NSOPRegDate = DateTime.Parse(_SqlData["NSOPRegDate"].ToString());

                    _p.NationalID = _SqlData["NationalID"].ToString();
                    _p.NationalRank = (int)_SqlData["NationalRank"];
                    _p.NationalPoints = (long)_SqlData["NationalPoints"];
                    _p.NationalMembership = (int)_SqlData["NationalMembership"];

                    if (_SqlData["NationalMembershipExpires"].ToString() == "")
                    {
                    }
                    else _p.NationalMembershipExpires = DateTime.Parse(_SqlData["NationalMembershipExpires"].ToString());

                    if (_SqlData["NationalRegDate"].ToString() == "")
                    {
                    }
                    else _p.NationalRegDate = DateTime.Parse(_SqlData["NationalRegDate"].ToString());

                    _p.WorldID = _SqlData["WorldID"].ToString();
                    _p.WorldRank = (int)_SqlData["WorldRank"];
                    _p.WorldPoints = (long)_SqlData["WorldPoints"];
                    _p.WorldMembership = (int)_SqlData["WorldMembership"];

                    if (_SqlData["WorldMembershipExpires"].ToString() == "")
                    {
                    }
                    else _p.WorldMembershipExpires = DateTime.Parse(_SqlData["WorldMembershipExpires"].ToString());

                    if (_SqlData["WorldRegDate"].ToString() == "")
                    {
                    }
                    else _p.WorldRegDate = DateTime.Parse(_SqlData["WorldRegDate"].ToString());

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

                    // Populate Modul Access
                    if (_SqlData["AdminSubscribtion"].ToString() == "")
                    {
                    }
                    else _p.AdminSubscribtion = DateTime.Parse(_SqlData["AdminSubscribtion"].ToString());

                    if (_SqlData["WebSideSubscribtion"].ToString() == "")
                    {
                    }
                    else _p.WebSideSubscribtion = DateTime.Parse(_SqlData["WebSideSubscribtion"].ToString());

                    if (_SqlData["LastLogin"].ToString() == "")
                    {
                    }
                    else _p.LastLogin = DateTime.Parse(_SqlData["LastLogin"].ToString());

                    _p.Tokens = (int)_SqlData["Tokens"];
                    _p.TournamentCreatorQTY = (int)_SqlData["TournamentCreatorQTY"];
                    _p.PersonRegQTY = (int)_SqlData["PersonRegQTY"];
                    _p.TicketSaleQTY = (int)_SqlData["TicketSaleQTY"];
                    _p.BlindStructureQTY = (int)_SqlData["BlindStructureQTY"];
                    _p.PayoutStructureQTY = (int)_SqlData["PayoutStructureQTY"];
                    _p.PointStructureQTY = (int)_SqlData["PointStructureQTY"];
                    _p.AdvertiseModuleQTY = (int)_SqlData["AdvertiseModuleQTY"];
                    _p.TableManagerQTY = (int)_SqlData["TableManagerQTY"];
                    _p.StandUserName = _SqlData["StandUserName"].ToString();
                    _p.StandPassWord = _SqlData["StandPassWord"].ToString();
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
        // Do all action with person
        public static Person ProsessPerson(Person person, DataAccess.Request request)
        {
            // FillThisPerson(person);
            switch (request)
            {
                case DataAccess.Request.ResetPassword:
                    // xxx 
                    // Check if Person already exists
                    Person _p0 = CheckPerson(person.UserName, "");
                    // Person exist if PlayerID all other than ""
                    if (_p0.PlayerID != "")
                    {
                        person.ActionType = DataAccess.Request.ResetPassword.ToString();
                        person.ClubID = DataAccess.GetVerificationCode();
                    }
                    else
                    {
                        // player dont exist
                    }
                    break;
                case DataAccess.Request.UpdatePassword:
                    // XXX MAKE A PASSWORD DATABASE CHANGER
                    UpdatePassword(person);
                    break;
                case DataAccess.Request.VerifyOK:
                    person.ActionType = DataAccess.Request.PersonCreated.ToString();
                    _ = person.SaveNew();
                    break;
                case DataAccess.Request.New:
                    // Saving new Person
                    person.PlayerID = DataAccess.FillID(DataAccess.IdType.Person);
                    _ = person.SaveNew();
                    // xx check if players is saved ok
                    break;
                case DataAccess.Request.ClubUpdate:

                    _ = person.UpdateClub(person);
                    break;
                case DataAccess.Request.Delete:
                    _ = person.Delete(person);
                    break;
                case DataAccess.Request.Get:
                    person = person.GetPerson(person.PlayerID);
                    break;
                case DataAccess.Request.Getall:

                    break;
                case DataAccess.Request.Registrer:
                    // Check if Person already exists
                    Person _p1 = CheckPerson(person.UserName, person.PassWord);
                    // Person exist if PlayerID all other than ""
                    if (_p1.PlayerID != "")
                    {
                        person.ActionType = DataAccess.Request.PersonExist.ToString();
                    }
                    else
                    {
                        // Need verification
                        person.ActionType = DataAccess.Request.Verify.ToString();
                        person.ClubID = DataAccess.GetVerificationCode();

                        // Add a new person to Person Dataabase

                    }
                    break;
                case DataAccess.Request.LoggIn:
                    person = CheckPerson(person.UserName, person.PassWord);
                    if (person.PlayerID != "")
                    {
                        // person found
                        //person.GetPerson(person.PlayerID);
                        person.ActionType = DataAccess.Request.LoggInOK.ToString();
                    }
                    else
                    {
                        //person not found
                        person.ActionType = DataAccess.Request.LoggInFailed.ToString();
                    }
                    break;
                case DataAccess.Request.LoggInOK:
                    break;
                case DataAccess.Request.LoggInFailed:
                    break;
                case DataAccess.Request.PersonExist:
                    person.ActionType = DataAccess.Request.PersonExist.ToString();
                    break;
                case DataAccess.Request.PersonCreated:
                    person.ActionType = DataAccess.Request.PersonCreated.ToString();
                    break;
                case DataAccess.Request.PersonUpdate:
                    break;
                case DataAccess.Request.Verify:
                    break;
                case DataAccess.Request.BadEMail:
                    break;
            }
            return person;
        }
    }
}
