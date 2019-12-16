using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace NSOP_Torunament_Pro_Library
{
    [Serializable]
    public class Rank
    {
        private string _classType = "Rank";
        public string ClassType { get => _classType; set => _classType = value; }
        private string _cPlayerID = "0000 0000 0000 000P";
        public string PlayerID { get => _cPlayerID; set => _cPlayerID = value; }

        // Club rank
        private string _clubID = "0000 0000 0000 000P";
        public string ClubID { get => _clubID; set => _clubID = value; }

        private int _cClubRank = 0;
        public int ClubRank { get => _cClubRank; set => _cClubRank = value; }

        private long _cClubPoints = 0;
        public long ClubPoints { get => _cClubPoints; set => _cClubPoints = value; }

        private int _cClubMembership = 0;
        public int ClubMembership { get => _cClubMembership; set => _cClubMembership = value; }

        private DateTime _cClubMembershipExpires = new DateTime(1990, 1, 1);
        public DateTime ClubMembershipExpires { get => _cClubMembershipExpires; set => _cClubMembershipExpires = value; }

        private DateTime _cClubRegDate = new DateTime(1990, 1, 1);
        public DateTime ClubRegDate { get => _cClubRegDate; set => _cClubRegDate = value; }


        // NSOP rank
        private string _NSOPID = "0000 0000 0000 000P";
        public string NSOPID { get => _NSOPID; set => _NSOPID = value; }

        private int _cNSOPRank = 0;
        public int NSOPRank { get => _cNSOPRank; set => _cNSOPRank = value; }

        private long _cNSOPPoints = 0;
        public long NSOPPoints { get => _cNSOPPoints; set => _cNSOPPoints = value; }

        private int _cNSOPMembership = 0;
        public int NSOPMembership { get => _cNSOPMembership; set => _cNSOPMembership = value; }

        private DateTime _cNSOPMembershipExpires = new DateTime(1990, 1, 1);
        public DateTime NSOPMembershipExpires { get => _cNSOPMembershipExpires; set => _cNSOPMembershipExpires = value; }

        private DateTime _cNSOPRegDate = new DateTime(1990, 1, 1);
        public DateTime NSOPRegDate { get => _cNSOPRegDate; set => _cNSOPRegDate = value; }


        // National rank
        private string _nationalID = "0000 0000 0000 000P";
        public string NationalID { get => _nationalID; set => _nationalID = value; }

        private int _cNationalRank = 0;
        public int NationalRank { get => _cNationalRank; set => _cNationalRank = value; }

        private long _cNationalPoints = 0;
        public long NationalPoints { get => _cNationalPoints; set => _cNationalPoints = value; }

        private int _cNationalMembership = 0;
        public int NationalMembership { get => _cNationalMembership; set => _cNationalMembership = value; }

        private DateTime _cNationalMembershipExpires = new DateTime(1990, 1, 1);
        public DateTime NationalMembershipExpires { get => _cNationalMembershipExpires; set => _cNationalMembershipExpires = value; }

        private DateTime _cNationalRegDate = new DateTime(1990, 1, 1);
        public DateTime NationalRegDate { get => _cNationalRegDate; set => _cNationalRegDate = value; }

        //World Rank
        private string _worldID = "0000 0000 0000 000P";
        public string WorldID { get => _worldID; set => _worldID = value; }

        private int _cWorldRank = 0;
        public int WorldRank { get => _cWorldRank; set => _cWorldRank = value; }

        private long _cWorldPoints = 0;
        public long WorldPoints { get => _cWorldPoints; set => _cWorldPoints = value; }

        private int _cWorldMembership = 0;
        public int WorldMembership { get => _cWorldMembership; set => _cWorldMembership = value; }

        private DateTime _cWorldMembershipExpires = new DateTime(1990, 1, 1);
        public DateTime WorldMembershipExpires { get => _cWorldMembershipExpires; set => _cWorldMembershipExpires = value; }

        private DateTime _cWorldRegDate = new DateTime(1990, 1, 1);
        public DateTime WorldRegDate { get => _cWorldRegDate; set => _cWorldRegDate = value; }


        // Empty Constructor
        public Rank()
        {
        }
        // Constructor for playerId
        public Rank(string playerId)
        {
            this.PlayerID = playerId;
        }

        //Conventerer Parson til Byte[]
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

        // Constructor complete
        public Rank(string classType, string playerID)
        {
            // populate all data xxx
            this.ClassType = classType;
            this.PlayerID = playerID;
        }

        // Constructore packing to byte
        public Rank(byte[] packetBytes)
        {
            BinaryFormatter _bf = new BinaryFormatter();
            MemoryStream _ms = new MemoryStream(packetBytes);
            _ms.Position = 0;
            Rank _rank = (Rank)_bf.Deserialize(_ms);
            _ms.Close();

            // populate all data XXX
            ClassType = _rank.ClassType;
        }

        public bool Save()
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
                cmd.CommandText = $" insert into tbRank ";
                cmd.CommandText += $"(ClassType, PlayerID, ";
                cmd.CommandText += $"ClubID, ClubRank, ClubPoints, ClubMembership, ClubMembershipExpires, ClubRegDate, ";
                cmd.CommandText += $"NSOPID, NSOPRank, NSOPPoints, NSOPMembership, NSOPMembershipExpires, NSOPRegDate, ";
                cmd.CommandText += $"NationalID, NationalRank, NationalPoints, NationalMembership, NationalMembershipExpires, NationalRegDate, ";
                cmd.CommandText += $"WorldID, WorldRank, WorldPoints, WorldMembership, WorldMembershipExpires, WorldRegDate) ";

                cmd.CommandText += $"VALUES ( ";
                cmd.CommandText += $"'{ClassType}','{PlayerID}', ";
                cmd.CommandText += $"'{ClubID}','{ClubRank.ToString()}','{ClubPoints.ToString()}','{ClubMembership.ToString()}','{ClubMembershipExpires.ToString()}','{ClubRegDate.ToString()}',";
                cmd.CommandText += $"'{NSOPID}','{NSOPRank.ToString()}','{NSOPPoints.ToString()}','{NSOPMembership.ToString()}','{NSOPMembershipExpires.ToString()}','{NSOPRegDate.ToString()}',";
                cmd.CommandText += $"'{NationalID}','{NationalRank.ToString()}','{NationalPoints.ToString()}','{NationalMembership.ToString()}','{NationalMembershipExpires.ToString()}','{NationalRegDate.ToString()}',";
                cmd.CommandText += $"'{WorldID}','{WorldRank.ToString()}','{WorldPoints.ToString()}','{WorldMembership.ToString()}','{WorldMembershipExpires.ToString()}','{WorldRegDate.ToString()}'";
                cmd.CommandText += $")";

                //error = cmd.CommandText;
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
        public bool Update()
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
                cmd.CommandText = $" UPDATE tbRank SET ";
                cmd.CommandText += $"ClassType = '{ClassType}', PlayerID ='{PlayerID}', ";

                cmd.CommandText += $"ClubID = '{ClubID}', ClubRank = '{ClubRank}', ClubPoints = '{ClubPoints}', ";
                cmd.CommandText += $"ClubID = '{ClubMembership}', ClubMembershipExpires = '{ClubMembershipExpires}', ClubRegDate = '{ClubRegDate}', ";

                cmd.CommandText += $"NSOPID = '{NSOPID}', NSOPRank = '{NSOPRank}', NSOPPoints = '{NSOPPoints}', ";
                cmd.CommandText += $"NSOPID = '{NSOPMembership}', NSOPMembershipExpires = '{NSOPMembershipExpires}', NSOPRegDate = '{NSOPRegDate}', ";

                cmd.CommandText += $"NationalID = '{NationalID}', NationalRank = '{NationalRank}', NationalPoints = '{NationalPoints}', ";
                cmd.CommandText += $"NationalID = '{NationalMembership}', NationalMembershipExpires = '{NationalMembershipExpires}', NationalRegDate = '{NationalRegDate}', ";

                cmd.CommandText += $"WorldID = '{WorldID}', WorldRank = '{WorldRank}', WorldPoints = '{WorldPoints}', ";
                cmd.CommandText += $"WorldID = '{WorldMembership}', WorldMembershipExpires = '{WorldMembershipExpires}', WorldRegDate = '{WorldRegDate}' ";
                cmd.CommandText += $"WHERE PlayerID = '{PlayerID}';";
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
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbPerson; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $" DELETE FROM tbRank WHERE PlayerID = '{PlayerID}';";

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
        public Rank Select()
        {
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbPerson; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {
                SqlDataReader _SqlData = null;
                SqlCommand _SqlStr = new SqlCommand($"SELECT * FROM tbRank WHERE PlayerID >= @prm1", con);
                SqlParameter _prm1 = new SqlParameter("@prm1", PlayerID.ToString());
                _SqlStr.Parameters.Add(_prm1);
                _SqlData = _SqlStr.ExecuteReader();
                while (_SqlData.Read())
                {
                    this.ClassType = _SqlData["ClassType"].ToString();
                    this.PlayerID = _SqlData["PlayerID"].ToString();

                    this.ClubID = _SqlData["ClubID"].ToString();
                    this.ClubMembership = (int)_SqlData["ClubMembership"];
                    this.ClubMembershipExpires = (DateTime)_SqlData["ClubMembershipExpires"];
                    this.ClubPoints = (long)_SqlData["ClubPoints"];
                    this.ClubRank = (int)_SqlData["ClubRank"];
                    this.ClubRegDate = (DateTime)_SqlData["ClubRegDate"];

                    this.NSOPID = _SqlData["NSOPID"].ToString();
                    this.NSOPMembership = (int)_SqlData["NSOPMembership"];
                    this.NSOPMembershipExpires = (DateTime)_SqlData["NSOPMembershipExpires"];
                    this.NSOPPoints = (long)_SqlData["NSOPPoints"];
                    this.NSOPRank = (int)_SqlData["NSOPRank"];
                    this.NSOPRegDate = (DateTime)_SqlData["NSOPRegDate"];

                    this.NationalID = _SqlData["NationalID"].ToString();
                    this.NationalMembership = (int)_SqlData["NationalMembership"];
                    this.NationalMembershipExpires = (DateTime)_SqlData["NationalMembershipExpires"];
                    this.NationalPoints = (long)_SqlData["NationalPoints"];
                    this.NationalRank = (int)_SqlData["NationalRank"];
                    this.NationalRegDate = (DateTime)_SqlData["NationalRegDate"];

                    this.WorldID = _SqlData["WorldID"].ToString();
                    this.WorldMembership = (int)_SqlData["WorldMembership"];
                    this.WorldMembershipExpires = (DateTime)_SqlData["WorldMembershipExpires"];
                    this.WorldPoints = (long)_SqlData["WorldPoints"];
                    this.WorldRank = (int)_SqlData["WorldRank"];
                    this.WorldRegDate = (DateTime)_SqlData["WorldRegDate"];
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
