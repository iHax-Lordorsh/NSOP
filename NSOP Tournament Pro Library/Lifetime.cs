using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NSOP_Torunament_Pro_Library
{
    [Serializable]
    public class Lifetime
    {
        private string _lassType = "Lifetime";
        public string ClassType { get => _lassType; set => _lassType = value; }

        private string _PlayerID = "0000 0000 0000 000P";
        public string PlayerID { get => _PlayerID; set => _PlayerID = value; }

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

        // Pakker data som skal sendes
        public Lifetime()
        {
        }
        // Pakker data som skal sendes
        public Lifetime(string playerID)
        {
            this.PlayerID = playerID;
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

        // Pakker data som skal sendes
        public Lifetime(string classType, string playerID)
        {
            this.ClassType = classType;
            this.PlayerID = playerID;
        }

        // Pakker opp data som er mottatt
        public Lifetime(byte[] packetBytes)
        {
            BinaryFormatter _bf = new BinaryFormatter();
            MemoryStream _ms = new MemoryStream(packetBytes);
            _ms.Position = 0;
            Lifetime _lifetime = (Lifetime)_bf.Deserialize(_ms);
            _ms.Close();

            // populate all data
            ClassType = _lifetime.ClassType;
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
                cmd.CommandText = $" insert into tbLifetime ";
                cmd.CommandText += $"( ";
                cmd.CommandText += $"ClassType, PlayerID, ";
                cmd.CommandText += $"LifetimePlayed, LifetimeWins, LifetimeFinalTables, LifetimeCashed, LifetimeBubbles, LifetimeFirstOuts, ";
                cmd.CommandText += $"LifetimeSevenDeuces, LifetimeBadBeats, LifetimeTakeOuts, LifetimeHunted, LifetimeMoneyEarned, LifetimeMoneySpent) ";
                cmd.CommandText += $"VALUES ( ";
                cmd.CommandText += $"'{ClassType}','{PlayerID}', ";
                cmd.CommandText += $"{LifetimePlayed.ToString()},{LifetimeWins.ToString()},{LifetimeFinalTables.ToString()},{LifetimeCashed.ToString()},{LifetimeBubbles.ToString()},{LifetimeFirstOuts.ToString()},";
                cmd.CommandText += $"{LifetimeSevenDeuces.ToString()},{LifetimeBadBeats.ToString()},{LifetimeTakeOuts.ToString()},{LifetimeHunted.ToString()},{LifetimeMoneyEarned.ToString()},{LifetimeMoneySpent.ToString()} ";
                cmd.CommandText += $") ";

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
                cmd.CommandText = $" UPDATE tbLifetime SET ";
                cmd.CommandText += $"ClassType = '{ClassType}', PlayerID ='{PlayerID}', ";
                cmd.CommandText += $"LifetimePlayed = '{LifetimePlayed.ToString()}', LifetimeWins = '{LifetimeWins.ToString()}', LifetimeFinalTables = '{LifetimeFinalTables.ToString()}', ";
                cmd.CommandText += $"LifetimeCashed = '{LifetimeCashed.ToString()}', LifetimeBubbles = '{LifetimeBubbles.ToString()}', ";
                cmd.CommandText += $"LifetimeFirstOuts ='{LifetimeFirstOuts.ToString()}',LifetimeSevenDeuces ='{LifetimeSevenDeuces.ToString()}', ";
                cmd.CommandText += $"LifetimeBadBeats = '{LifetimeBadBeats.ToString()}', LifetimeTakeOuts = '{LifetimeTakeOuts.ToString()}', ";
                cmd.CommandText += $"LifetimeHunted = '{LifetimeHunted.ToString()}', LifetimeMoneyEarned = '{LifetimeMoneyEarned.ToString()}', ";
                cmd.CommandText += $"LifetimeMoneySpent = '{LifetimeMoneySpent.ToString()}' ";
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
                cmd.CommandText = $" DELETE FROM tbLifetime WHERE PlayerID = '{PlayerID}';";

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
        public Lifetime Select()
        {
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbPerson; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {
                SqlDataReader _SqlData = null;
                SqlCommand _SqlStr = new SqlCommand($"SELECT * FROM tbLifetime WHERE PlayerID >= @prm1 ", con);
                SqlParameter _prm1 = new SqlParameter("@prm1", PlayerID.ToString());
                _SqlStr.Parameters.Add(_prm1);
                _SqlData = _SqlStr.ExecuteReader();
                while (_SqlData.Read())
                {
                    this.ClassType = _SqlData["ClassType"].ToString();
                    this.PlayerID = _SqlData["PlayerID"].ToString();
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
                    this.LifetimeMoneyEarned = (int)_SqlData["LifetimeMoneyEarned"];
                    this.LifetimeMoneySpent = (int)_SqlData["LifetimeMoneySpent"];
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
