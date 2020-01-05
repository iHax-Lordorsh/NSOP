using NSOP_Tournament_Pro_Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NSOP_Tournament_Pro_Library
{
    [Serializable]
    public class PersonList
    {
        private string _classType = "Person";
        public string ClassType { get => _classType; set => _classType = value; }

        private string _ActionType = "GetAll";
        public string ActionType { get => _ActionType; set => _ActionType = value; }

        private List<Person> _list = new List<Person>();
        public List<Person> List { get => _list; set => _list = value; }

        // Constructor to send Person
        public PersonList()
        {
        }

        // Constructor to recieve Person
        public PersonList(byte[] packetBytes)
        {
            BinaryFormatter _bf = new BinaryFormatter();
            MemoryStream _ms = new MemoryStream(packetBytes);
            _ms.Position = 0;
            PersonList _personList = (PersonList)_bf.Deserialize(_ms);
            _ms.Close();

            // populate all data
            foreach (var item in _personList.List)
            {

                List.Add(item);
            }

        }
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
        public List<Person> GetPersons(string clubType, string clubID)
        {
            List.Clear();
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbPerson; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {
                SqlDataReader _SqlData = null;
                SqlCommand _SqlStr = new SqlCommand($"SELECT * FROM dbo.tbPerson ", con);// WHERE '{clubType}' = @prm1", con);
                //SqlParameter _prm1 = new SqlParameter("@prm1", clubID);
                //_SqlStr.Parameters.Add(_prm1);

                _SqlData = _SqlStr.ExecuteReader();
                int x = _SqlData.FieldCount;
                while (_SqlData.Read())
                {
                    Person _p = new Person();
                    _p.ClassType = _SqlData["ClassType"].ToString();
                    _p.ClubID = _SqlData["ClubID"].ToString();
                    _p.FirstName = _SqlData["FirstName"].ToString();
                    _p.LastName = _SqlData["LastName"].ToString();
                    _p.Picture = (byte[])_SqlData["Picture"];
                    _p.Mobile = _SqlData["Mobile"].ToString();
                    _p.EMail = _SqlData["EMail"].ToString();
                    _p.Gender = _SqlData["Gender"].ToString();
                    _p.Nationality = _SqlData["Nationality"].ToString();
                    _p.Iso3166Name = _SqlData["Iso3166Name"].ToString();
                    _p.BornDate = DateTime.Parse(_SqlData["BornDate"].ToString());
                    _p.RegDate = DateTime.Parse(_SqlData["RegDate"].ToString());
                    _p.NickName = _SqlData["NickName"].ToString();
                    _p.IsPlayerRemoved = (Boolean)_SqlData["IsPlayerRemoved"];
                    List.Add(_p);
                }

                // Save LifeTime
            }
            catch (Exception e)
            {
                string xError = e.ToString();
            }
            con.Close();
            return List;
        }
    }
}
