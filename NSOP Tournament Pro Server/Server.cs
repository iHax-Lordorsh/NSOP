using NSOP_Tournament_Pro_Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace NSOP_Tournament_Pro_Server
{
    class Server
    {
        static Socket _listenerSocket;
        static List<ClientData> _clients;

        static void Main()
        {

            Console.WriteLine("Starting server on " + DataAccess.GetIP4Address());

            _listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clients = new List<ClientData>();

            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(DataAccess.GetIP4Address()), 5960);
            _listenerSocket.Bind(ip);

            Thread listenThread = new Thread(ListenThread);
            listenThread.Start();
        }

        //listner = listens for clients trying to connect
        static void ListenThread()
        {
            for (; ; )
            {
                _listenerSocket.Listen(0);
                _clients.Add(new ClientData(_listenerSocket.Accept()));
            }
        }
       
        // clientdata thread - receives data from each client individually
        public static void Data_IN(object vSocket)
        {
            Socket clientSocket = (Socket)vSocket;
           // int _startTickCount = Environment.TickCount;
            int _received = 0;  // how many bytes is already received
            _ = new byte[0];
            byte[] _buffer = new byte[clientSocket.ReceiveBufferSize]; //buffer recieved
            int _size = clientSocket.ReceiveBufferSize; // how many byte recieved this time
            int _offset = 0;
            bool _packetRecieved = false;
            for (; ; )
            {
                try
                {
                    if (clientSocket.Connected == true)
                    {
                        do
                        {
                            //if (Environment.TickCount > _startTickCount + _timeout)
                            //    throw new Exception("Timeout.");
                            try
                            {
                                _received += clientSocket.Receive(_buffer, _offset + _received, _size - _received, SocketFlags.Partial);
                                try
                                {
                                    CommunicationManager _cp = new CommunicationManager(_buffer);
                                    _packetRecieved = true;
                                    Console.WriteLine("Communication Package Found: Request = " + _cp.Request.ToString() + "Packet Recieved = "+ _packetRecieved.ToString());
                                    clientSocket.Send(_cp.ManagePacket().ToBytes());
                                    //  break;
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Packet Recieved: " + _received.ToString() + " Continue Recieving ...");
                                }
                                //if (_packetRecieved)
                                //{
                                //    break;
                                //}
                                //if (DataManager(_buffer, clientSocket) != "")
                                //{
                                //    break;
                                //}
                            }
                            catch (SocketException ex)
                            {
                                if (ex.SocketErrorCode == SocketError.WouldBlock ||
                                    ex.SocketErrorCode == SocketError.IOPending ||
                                    ex.SocketErrorCode == SocketError.NoBufferSpaceAvailable)
                                {
                                    // socket buffer is probably empty, wait and try again
                                    Thread.Sleep(30);
                                }
                                else
                                {
                                    // throw ex;  // any serious error occurr
                                    // xxx her mister vi connection
                                    // 
                                    //Action action = delegate
                                    //{
                                    //    RemoveClient(clientSocket);
                                    //};

                                    int _clientTeller = -1;
                                    Console.WriteLine(clientSocket.RemoteEndPoint.ToString() + " - Clients: " + _clients.Count.ToString());
                                    // search for client who is disconnectet
                                    foreach (ClientData _c in _clients)
                                    {
                                        _clientTeller++;
                                        if (_c._clientSocket.RemoteEndPoint == clientSocket.RemoteEndPoint)
                                        {
                                            _clients.RemoveAt(_clientTeller);
                                            _c.EndThread();
                                            break;
                                        }
                                    }
                                }
                            }
                        } while (_packetRecieved != true);

                        Console.WriteLine("Done : " + clientSocket.RemoteEndPoint.ToString());
                        _packetRecieved = false;
                        _received = 0;
                        _size = clientSocket.ReceiveBufferSize;
                        byte[] _bufferTotal = new byte[0];
                        _buffer = new byte[clientSocket.ReceiveBufferSize];
                    }
                }
                catch (SocketException)
                {
                }
            }
        }

        //data manager
        //public static string DataManager(byte[] buffer, Socket clientSocket)
        //{
        //    // Comaper buffer to all classes
        //    string _Packet = DataAccess.GetPacket(buffer);
        //    if (_Packet != "")
        //    {
        //        switch (DataAccess.ParseEnum<DataAccess.ClassType>(_Packet))
        //        {
        //            case DataAccess.ClassType.Person:
        //                // converting bytes to Peron
        //                Person _person = new Person(buffer);
        //                _person = Person.ProsessPerson(_person);
        //                if (DataAccess.ParseEnum<DataAccess.Request>(_person.ActionType) == DataAccess.Request.Verify)
        //                {
        //                    if (Send_Verification(_person))
        //                    { }
        //                    else
        //                    {
        //                        _person.ActionType = DataAccess.Request.BadEMail.ToString();
        //                    }
        //                } else if (DataAccess.ParseEnum<DataAccess.Request>(_person.ActionType) == DataAccess.Request.ResetPassword)
        //                {
        //                    if (Send_ResetPassword(_person))
        //                    { }
        //                    else
        //                    {
        //                        _person.ActionType = DataAccess.Request.BadEMail.ToString();
        //                    }
        //                }
        //                Console.WriteLine(_person.PlayerID + "   " + _person.FirstName);

        //                // Sending back if its ok or not
        //                Replay(_person.ToBytes(), _person.ClassType, clientSocket);
        //                break;
        //            case DataAccess.ClassType.Tournament:
        //                Tournament _tournament = new Tournament(buffer);
        //                Console.WriteLine(_tournament.TournamentID + "   " + _tournament.TournamentName);
        //                // Save and Reply
        //                _ = _tournament.Save();
        //                Replay(_tournament.ToBytes(), _tournament.ClassType, clientSocket);
        //                break;
        //            case DataAccess.ClassType.Action:
        //                break;
        //            case DataAccess.ClassType.Packet:
        //                break;
        //            case DataAccess.ClassType.Blinds:
        //                break;
        //            case DataAccess.ClassType.Payouts:
        //                break;
        //            case DataAccess.ClassType.Points:
        //                break;
        //            case DataAccess.ClassType.DataVerify:
        //                break;
        //            case DataAccess.ClassType.PersonList:
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Unknown PacketType [ " + buffer.Length.ToString() + " ] " + _Packet + " / Client Connected : " + _clients.Count.ToString());
        //    }
        //    return _Packet;
        //}
        //private static void Replay(byte[] obj, string packet, Socket clientSocket)
        //{
        //    foreach (ClientData _c in _clients)
        //    {
        //        if (_c._clientSocket.RemoteEndPoint == clientSocket.RemoteEndPoint)
        //        {
        //            // create return data
        //            switch (DataAccess.ParseEnum<DataAccess.ClassType>(packet))
        //            {
        //                case DataAccess.ClassType.Person:
        //                    Person _p = new Person(obj);
        //                    _c._clientSocket.Send(_p.ToBytes());

        //                    break;
        //                case DataAccess.ClassType.Tournament:
        //                    break;
        //                case DataAccess.ClassType.Blinds:
        //                    break;
        //                case DataAccess.ClassType.Payouts:
        //                    break;
        //                case DataAccess.ClassType.Points:
        //                    break;
        //                case DataAccess.ClassType.DataVerify:
        //                    break;
        //                case DataAccess.ClassType.Action:
        //                    break;
        //                case DataAccess.ClassType.Packet:
        //                    break;
        //                case DataAccess.ClassType.PersonList:
        //                    break;
        //            }
        //            break;
        //        }
        //    }
        //}
        //public static void Receive(Socket socket, byte[] buffer, int offset, int size)
        //{
        //    //int startTickCount = Environment.TickCount;
        //    int received = 0;  // how many bytes is already received
        //    do
        //    {
        //        //if (Environment.TickCount > startTickCount + timeout)
        //        //    throw new Exception("Timeout.");
        //        try
        //        {
        //            received += socket.Receive(buffer, offset + received, size - received, SocketFlags.None);
        //            Console.WriteLine("_readBytes : " + received.ToString() + " _BufferTotal : " + buffer.Length.ToString());
        //            DataManager(buffer, socket);
        //        }
        //        catch (SocketException ex)
        //        {
        //            if (ex.SocketErrorCode == SocketError.WouldBlock ||
        //                ex.SocketErrorCode == SocketError.IOPending ||
        //                ex.SocketErrorCode == SocketError.NoBufferSpaceAvailable)
        //            {
        //                // socket buffer is probably empty, wait and try again
        //                Thread.Sleep(30);
        //                Console.WriteLine(ex.ToString());
        //            }
        //            else
        //                throw ex;  // any serious error occurr
        //            //xxx her mister vi connection

        //        }
        //    } while (received < size);
        //}
 
        //private void SEND_MAIL_LEAGUE_TABLE()
        //{
        //    try
        //    {
        //        MailMessage mail = new MailMessage();
        //        mail.IsBodyHtml = true;
        //        //put your SMTP address and port here.
        //        SmtpClient SmtpServer = new SmtpClient(dbSmtpMail, dbSmtpPortMail);// smtp-mail.outlook.com
        //        //Put the email address
        //        mail.From = new MailAddress(dbNavnMail);
        //        //Put the email where you want to send.
        //        foreach (DataRow dRow in dSpillerLise.Rows)
        //        {
        //            string spillerNavn = (string)dRow[2];
        //            if (spillerNavn != "AAdresser")
        //            {
        //                mail.To.Add(new MailAddress(HENT_BASE_VERDI(dbSpillere, "AAdresser", "Navn", spillerNavn, "", "", "Email").ToString()));
        //            }
        //        }

        //        mail.Subject = "LIGATABELL : " + visSpråk[2].Content.ToString();
        //        StringBuilder sb = new StringBuilder();
        //        string tab = "\t";
        //        string xNavn = "";
        //        string xSpilt = "";
        //        string xPoeng = "";
        //        int Teller = 0;

        //        sb.AppendLine("<html>");
        //        sb.AppendLine(tab + "<body>");
        //        sb.AppendLine(tab + tab + "<table width='400'>");
        //        // headers.
        //        //sb.Append(tab + tab + tab + "<tr>");

        //        foreach (object o in lSortLigePlass)
        //        {
        //            sb.Append(tab + tab + tab + "<tr>");
        //            Teller++;
        //            xNavn = (o as LigaPlassTemplateClass).lNavn;
        //            xSpilt = (o as LigaPlassTemplateClass).lSpilt.ToString();
        //            xPoeng = (o as LigaPlassTemplateClass).lPoints.ToString();
        //            sb.AppendFormat("<td width='50' align='right' style='font-size:16px' style='font-weight:bold'>{0}.</td><td width='200' style='font-size:16px'>{1}</td><td width='50' align='right' style='font-size:12px'>({2})</td><td width='100' align='right'style='font-size:16px' style='font-weight:bold'>{3}</td>", Teller.ToString(), xNavn.ToString(), xSpilt.ToString(), xPoeng.ToString());
        //            sb.AppendLine("</tr>");
        //        }
        //        sb.AppendLine(tab + tab + "</table>");
        //        sb.AppendLine(tab + "</body>");
        //        sb.AppendLine("</html>");

        //        mail.Body = sb.ToString();

        //        //Your log file path
        //        //System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(@"C:\Logs\CheckoutPOS.log");
        //        //mail.Attachments.Add(attachment);

        //        SmtpServer.Credentials = new System.Net.NetworkCredential(dbBrukerNavnMail, dbPassordMail);
        //        //Set Smtp Server port
        //        SmtpServer.Port = dbSmtpServerPortMail;
        //        SmtpServer.EnableSsl = dbSmtpEnableMail;

        //        SmtpServer.Send(mail);
        //        //MessageBox.Show("The exception has been sent! :)");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }
        //}
    }


    class ClientData
    {
        public Socket _clientSocket;
        public Thread _clientThread;
        public string _id;

        public ClientData()
        {
            _id = Guid.NewGuid().ToString();
            _clientThread = new Thread(Server.Data_IN);
            _clientThread.Start(_clientSocket);
            //EndThread();
        }
        public ClientData(Socket clientSocket)
        {
            this._clientSocket = clientSocket;
            _id = Guid.NewGuid().ToString();
            _clientThread = new Thread(Server.Data_IN);
            _clientThread.Start(_clientSocket);
        }
        public void EndThread()
        {
            _clientSocket.Dispose();
            _clientThread.Abort();
        }
    }
}
