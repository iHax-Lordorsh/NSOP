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
            MemoryStream _ms = new MemoryStream(packetBytes)
            {
                Position = 0
            };
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
            MemoryStream _ms = new MemoryStream
            {
                Position = 0
            };
            _bf.Serialize(_ms, this);
            byte[] bytes = _ms.ToArray();
            _ms.Close();
            return bytes;
        }
    }
}
