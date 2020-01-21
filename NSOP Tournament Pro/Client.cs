using NSOP_Torunament_Pro_Library;
using NSOP_Tournament_Pro_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace NSOP_Tournament_Pro
{
    public class Client
    {
        public static Socket ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //public static string name;
        //public static string id;
        public static string IpAddress = "62.24.34.199";
        public static int IpPort = 5960;
        public static IPEndPoint _ip = new IPEndPoint(IPAddress.Parse(IpAddress), IpPort);
        private static bool _packetRecieved;

        public void SendObject(byte[] byteToSend)
        {
            if (ServerSocket.Connected == false)
            {
                 Connect();
                Thread t = new Thread(() => Data_IN());
                t.Start();
            }
            ServerSocket.SendBufferSize = byteToSend.Length;
            ServerSocket.ReceiveBufferSize = byteToSend.Length;
            int _startTickCount = Environment.TickCount;
            int _sent = 0;  // how many bytes is already sent
            int _startByteTosend = 0;
            do
            {
                try
                {
                    _sent += ServerSocket.Send(byteToSend, _startByteTosend + _sent, byteToSend.Length - _sent, SocketFlags.Partial);
                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode == SocketError.WouldBlock ||
                        ex.SocketErrorCode == SocketError.IOPending ||
                        ex.SocketErrorCode == SocketError.NoBufferSpaceAvailable)
                    {
                        // socket buffer is probably full, wait and try again
                        Thread.Sleep(30);
                    }
                    else
                        throw ex;  // any serious error occurr
                }
            } while (_sent < byteToSend.Length);
        }
        public static void Connect()
        {
        A:
            try
            {
                ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _ip = new IPEndPoint(IPAddress.Parse(IpAddress), IpPort);
                ServerSocket.Connect(_ip);
                //master.SendTimeout = 5000;
                //master.ReceiveTimeout = 5000;
            }
            catch (Exception)
            {
                goto A;
            }
        }

        public static void Data_IN()
        {
            //int _startTickCount = Environment.TickCount;
            int _received = 0;  // how many bytes is already received
            byte[] _bufferTotal = new byte[0];
            byte[] _buffer = new byte[256000]; //buffer recieved
            int _size = 256000; // how many byte recieved this time
            int _offset = 0;
  
            for (; ; )
            {
                try
                {
                    if (ServerSocket.Connected == true)
                    {
                        do
                        {
                            //if (Environment.TickCount > _startTickCount + _timeout)
                            //    throw new Exception("Timeout.");
                            try
                            {
                                _received += ServerSocket.Receive(_buffer, _offset + _received, _size - _received, SocketFlags.Partial);
                                _bufferTotal = DataAccess.ConvertByte(_bufferTotal, _buffer, _received);
                                try
                                {
                                    CommunicationManager _cp = new CommunicationManager(_buffer);
                                    _packetRecieved = true;
                                    RecievedManager(_cp);
                                    break;
                                }
                                catch (Exception e)
                                {
                                    string X =e.ToString();
                                }
                                //if (DataManager(_bufferTotal) != "")
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
                                    throw ex;  // any serious error occurr
                            }
                        } while (_packetRecieved != true);

                        //Console.WriteLine("Done : " + ServerSocket.RemoteEndPoint.ToString());
                        _received = 0;
                        _size = 256000;
                        _bufferTotal = new byte[0];
                        _buffer = new byte[256000];
                       // Thread.CurrentThread.Abort();
                    }
                    else
                    {
                        Connect();
                      //  Thread.CurrentThread.Abort();
                    }
                }
                catch (SocketException)
                {
                }
            }
        }

        private static void RecievedManager(CommunicationManager cp)
        {

            switch (cp.ClassType)
            {
                case DataAccess.ClassType.Product:
                    switch (cp.Request)
                    {
                        case DataAccess.Request.GetStartProduct:
                            Action GetStartProduct = delegate
                            {
                                var mainWnd = Application.Current.MainWindow as MainWindow;
                                mainWnd.ShowAdminCreator(new Product(cp.ObjectType));
                            };
                            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, GetStartProduct); 
                            break;
                        case DataAccess.Request.New:
                            break;
                        case DataAccess.Request.ClubUpdate:
                            break;
                        case DataAccess.Request.PersonUpdate:
                            break;
                        case DataAccess.Request.Delete:
                            break;
                        case DataAccess.Request.Get:
                            break;
                        case DataAccess.Request.Getall:
                            break;
                        case DataAccess.Request.NewAccount:
                            break;
                        case DataAccess.Request.LogIn:
                            break;
                        case DataAccess.Request.LoggInOK:
                            break;
                        case DataAccess.Request.LoggInFailed:
                            break;
                        case DataAccess.Request.PersonExist:
                            break;
                        case DataAccess.Request.PersonCreated:
                            break;
                        case DataAccess.Request.NewAccountVerify:
                            break;
                        case DataAccess.Request.VerifyOK:
                            break;
                        case DataAccess.Request.BadEMail:
                            break;
                        case DataAccess.Request.ResetPassword:
                            break;
                        case DataAccess.Request.UpdatePassword:
                            break;
                        case DataAccess.Request.ResetPasswordVerify:
                            break;
                        case DataAccess.Request.ResetPasswordOK:
                            break;
                        case DataAccess.Request.UpdateFailed:
                            break;
                    }
                    break;
                case DataAccess.ClassType.Person:
                    switch (cp.Request)
                    {
                        // Logg In Request to server
                        case DataAccess.Request.LogIn:
                            break;
                        // Logg In Answer from server
                        case DataAccess.Request.ResetPasswordOK:
                        case DataAccess.Request.LoggInOK:
                            Action LoggInOK = delegate
                            {
                                var mainWnd = Application.Current.MainWindow as MainWindow;
                                mainWnd.ShowAdminScreen(new Person(cp.ObjectType));
                            };
                            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, LoggInOK);
                            break;
                        // Logg In Answer from server
                        case DataAccess.Request.LoggInFailed:
                            Action LoggInFailed = delegate
                            {
                                var mainWnd = Application.Current.MainWindow as MainWindow;
                                mainWnd.LoggInFailed();
                            };
                            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, LoggInFailed);
                            break;
                        // Reset request to server
                        case DataAccess.Request.ResetPassword:
                            break;
                        // Reset verification from server
                        case DataAccess.Request.ResetPasswordVerify:
                            Action ResetVerification = delegate
                            {
                                var mainWnd = Application.Current.MainWindow as MainWindow;
                                mainWnd.ResetVerification(new CommunicationManager(cp.ToBytes()));
                            };
                            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, ResetVerification);
                            break;
                        // Update Password to server
                        case DataAccess.Request.UpdatePassword:
                            Action UpdatePassword = delegate
                            {
                                var mainWnd = Application.Current.MainWindow as MainWindow;
                                mainWnd.ShowAdminScreen(new Person(cp.ObjectType));
                            };
                            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, UpdatePassword);
                            break;

                        case DataAccess.Request.New:
                            break;
                        case DataAccess.Request.ClubUpdate:
                            break;
                        case DataAccess.Request.PersonUpdate:
                            break;
                        case DataAccess.Request.Delete:
                            break;
                        case DataAccess.Request.Get:
                            break;
                        case DataAccess.Request.Getall:
                            break;
                        case DataAccess.Request.NewAccount:
                            break;
                        case DataAccess.Request.PersonExist:
                            Action PersonExist = delegate
                            {
                                var mainWnd = Application.Current.MainWindow as MainWindow;
                                mainWnd.PersonExist();
                            };
                            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, PersonExist);
                            break;
                        case DataAccess.Request.PersonCreated:
                            break;
                        case DataAccess.Request.NewAccountVerify:
                            Action NewAccountVerify = delegate
                            {
                                var mainWnd = Application.Current.MainWindow as MainWindow;
                                mainWnd.NewAccountVerify(cp.Info, cp.Request);
                            };
                            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, NewAccountVerify);

                            break;
                        case DataAccess.Request.VerifyOK:
                            break;
                        case DataAccess.Request.BadEMail:
                            break;
                        case DataAccess.Request.UpdateFailed:
                            break;
                    }
                    break;
                case DataAccess.ClassType.PersonList:
                    break;
                case DataAccess.ClassType.Tournament:
                    break;
                case DataAccess.ClassType.Blinds:
                    break;
                case DataAccess.ClassType.Payouts:
                    break;
                case DataAccess.ClassType.Points:
                    break;
                case DataAccess.ClassType.DataVerify:
                    break;
                case DataAccess.ClassType.Action:
                    break;
                case DataAccess.ClassType.Packet:
                    break;
            }
        }

        //public static string DataManager(byte[] buffer)
        //{
        //string _packet = DataAccess.GetPacket(buffer);
        //    if (_packet !="")
        //    {
        //        switch (DataAccess.ParseEnum<DataAccess.ClassType>(_packet))
        //        {
        //            case DataAccess.ClassType.Person:
        //                // 
        //                Action action = delegate
        //                {
        //                    var mainWnd = Application.Current.MainWindow as MainWindow;
        //                    mainWnd.UpdateData(buffer,_packet);
        //                };
        //                Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, action);
        //                break;
        //            case DataAccess.ClassType.PersonList:
        //                break;
        //            case DataAccess.ClassType.Tournament:
        //                break;
        //            case DataAccess.ClassType.Blinds:
        //                break;
        //            case DataAccess.ClassType.Payouts:
        //                break;
        //            case DataAccess.ClassType.Points:
        //                break;
        //            case DataAccess.ClassType.DataVerify:
        //                break;
        //            case DataAccess.ClassType.Action:
        //                break;
        //            case DataAccess.ClassType.Packet:
        //                break;

        //        }
        //    }
        //    return _packet;
        //}
        //public static void Data_IN(object vSocket)
        //{
        //    Socket _ResieveSocket = (Socket)vSocket;
        //    _ResieveSocket.SendTimeout = 50000;
        //    _ResieveSocket.ReceiveTimeout = 50000;
        //    int _startTickCount = Environment.TickCount;
        //    int _received = 0;  // how many bytes is already received
        //    byte[] _bufferTotal = new byte[0];
        //    byte[] _buffer = new byte[_ResieveSocket.ReceiveBufferSize]; //buffer recieved
        //    int _size = _ResieveSocket.ReceiveBufferSize; // how many byte recieved this time
        //    int s = 0;
        //    bool _firstRecieved = false;
        //    int _offset = 0;
        //    //int _timeout = 5000;
        //    for (; ; )
        //    {
        //        try
        //        {
        //            if (_ResieveSocket.Connected == true)
        //            {
        //                do
        //                {
        //                    //if (Environment.TickCount > _startTickCount + _timeout)
        //                    //    throw new Exception("Timeout.");
        //                    try
        //                    {
        //                        _received += _ResieveSocket.Receive(_buffer, _offset + _received, _size - _received, SocketFlags.Partial);
        //                        if (_firstRecieved)
        //                        {
        //                            _firstRecieved = true;
        //                            s = _ResieveSocket.ReceiveBufferSize;
        //                        }
        //                        _bufferTotal = DataAccess.ConvertByte(_bufferTotal,_buffer,_buffer.Length);
        //                        if (DataManager(_buffer, _ResieveSocket) != "")
        //                        {
        //                            break;
        //                        }
        //                    }
        //                    catch (SocketException ex)
        //                    {
        //                        if (ex.SocketErrorCode == SocketError.WouldBlock ||
        //                            ex.SocketErrorCode == SocketError.IOPending ||
        //                            ex.SocketErrorCode == SocketError.NoBufferSpaceAvailable)
        //                        {
        //                            // socket buffer is probably empty, wait and try again
        //                            Thread.Sleep(30);
        //                        }
        //                        else
        //                            throw ex;  // any serious error occurr
        //                    }
        //                } while (_received < _size);
        //                //Console.WriteLine("Done ");
        //            }
        //        }
        //        catch (SocketException)
        //        {
        //        }
        //    }
        //}
        // All goes trough Datamanager
    }
}
