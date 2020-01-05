using NSOP_Tournament_Pro_Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace NSOP_Tournament_Pro_Library
{
    [Serializable]
    public class CommunicationPacket
    {
        public long Size { get; set; }
        public DataAccess.ClassType ClassType { get; set; }
        public DataAccess.Request Request { get; set; }
        public object ObjectType { get; set; }

        public CommunicationPacket()
        { 
        }
        public CommunicationPacket(byte[] packetBytes)
        {
            BinaryFormatter _bf = new BinaryFormatter();
            MemoryStream _ms = new MemoryStream(packetBytes)
            {
                Position = 0
            };
            CommunicationPacket _cp = (CommunicationPacket)_bf.Deserialize(_ms);
            _ms.Close();

            this.Size = _cp.Size;
            this.ClassType = _cp.ClassType;
            this.Request = _cp.Request;
            this.ObjectType = _cp.ObjectType;
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
    }
}
