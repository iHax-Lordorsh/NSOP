
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NSOP_Tournament_Pro_Library
{
    [Serializable]
    public class DataVerify
    {
        private string _classType = "Person";
        public string ClassType { get => _classType; set => _classType = value; }

        private string _id = "Person";
        public string Id { get => _id; set => _id = value; }

        private bool _IsDataRecieved = false;
        public bool IsDataRecieved { get => _IsDataRecieved; set => _IsDataRecieved = value; }

        // Empty Concstructor
        public DataVerify()
        {
        }

        // Constructor to fill this Class
        public DataVerify(string classType, string id, bool isDataRecieved)
        {
            ClassType = classType;
            Id = id;
            IsDataRecieved = isDataRecieved;
        }

        // Constructor to recieve this Class
        public DataVerify(byte[] packetBytes)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(packetBytes);
            DataVerify p = (DataVerify)bf.Deserialize(ms);
            ms.Close();

            // populate all data
            ClassType = p.ClassType;
            Id = p.Id;
            IsDataRecieved = p.IsDataRecieved;

        }

        // Converts this Class to Byte[]
        public byte[] ToBytes()
        {
            BinaryFormatter _bf = new BinaryFormatter();
            MemoryStream _ms = new MemoryStream();
            _bf.Serialize(_ms, this);
            byte[] bytes = _ms.ToArray();
            _ms.Close();
            return bytes;
        }
    }

}
