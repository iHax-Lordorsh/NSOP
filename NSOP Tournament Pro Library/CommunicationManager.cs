using NSOP_Tournament_Pro_Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace NSOP_Tournament_Pro_Library
{
    [Serializable]
    public class CommunicationManager
    {
        public long Size { get; set; }
        public DataAccess.ClassType ClassType { get; set; }
        public DataAccess.Request Request { get; set; }
        public string Info { get; set; }
        public byte[] ObjectType { get; set; }

        public CommunicationManager()
        { 
        }
        public CommunicationManager(byte[] packetBytes)
        {
            BinaryFormatter _bf = new BinaryFormatter();
            MemoryStream _ms = new MemoryStream(packetBytes)
            {
                Position = 0
            };
            CommunicationManager _cp = (CommunicationManager)_bf.Deserialize(_ms);
            _ms.Close();

            this.Size = _cp.Size;
            this.ClassType = _cp.ClassType;
            this.Request = _cp.Request;
            this.Info = _cp.Info;
            this.ObjectType = _cp.ObjectType;
        }

        //public static CommunicationManager ProcessPacket(Person person, DataAccess.Request request)
        //{
        //    // FillThisPerson(person);
        //    switch (request)
        //    {
        //        case DataAccess.Request.ResetPassword:
        //            // xxx 
        //            // Check if Person already exists
        //            Person _p0 = CheckPerson(person.UserName, "");
        //            // Person exist if PlayerID all other than ""
        //            if (_p0.PlayerID != "")
        //            {
        //                person.ActionType = DataAccess.Request.ResetPassword.ToString();
        //                person.ClubID = DataAccess.GetVerificationCode();
        //            }
        //            else
        //            {
        //                // player dont exist
        //            }
        //            break;
        //        case DataAccess.Request.UpdatePassword:
        //            // XXX MAKE A PASSWORD DATABASE CHANGER
        //            UpdatePassword(person);
        //            break;
        //        case DataAccess.Request.VerifyOK:
        //            person.ActionType = DataAccess.Request.PersonCreated.ToString();
        //            _ = person.SaveNew();
        //            break;
        //        case DataAccess.Request.New:
        //            // Saving new Person
        //            person.PlayerID = DataAccess.FillID(DataAccess.IdType.Person);
        //            _ = person.SaveNew();
        //            // xx check if players is saved ok

        //            break;
        //        case DataAccess.Request.ClubUpdate:

        //            _ = person.UpdateClub(person);
        //            break;
        //        case DataAccess.Request.Delete:
        //            _ = person.Delete(person);
        //            break;
        //        case DataAccess.Request.Get:
        //            person = person.GetPerson(person.PlayerID);
        //            break;
        //        case DataAccess.Request.Getall:

        //            break;
        //        case DataAccess.Request.Registrer:
        //            // Check if Person already exists
        //            Person _p1 = CheckPerson(person.UserName, person.PassWord);
        //            // Person exist if PlayerID all other than ""
        //            if (_p1.PlayerID != "")
        //            {
        //                person.ActionType = DataAccess.Request.PersonExist.ToString();
        //            }
        //            else
        //            {
        //                // Need verification
        //                person.ActionType = DataAccess.Request.Verify.ToString();
        //                person.ClubID = DataAccess.GetVerificationCode();

        //                // Add a new person to Person Dataabase

        //            }
        //            break;
        //        case DataAccess.Request.LoggIn:
        //            person = CheckPerson(person.UserName, person.PassWord);
        //            if (person.PlayerID != "")
        //            {
        //                // person found
        //                //person.GetPerson(person.PlayerID);
        //                person.ActionType = DataAccess.Request.LoggInOK.ToString();
        //            }
        //            else
        //            {
        //                //person not found
        //                person.ActionType = DataAccess.Request.LoggInFailed.ToString();
        //            }
        //            break;
        //        case DataAccess.Request.LoggInOK:
        //            break;
        //        case DataAccess.Request.LoggInFailed:
        //            break;
        //        case DataAccess.Request.PersonExist:
        //            person.ActionType = DataAccess.Request.PersonExist.ToString();
        //            break;
        //        case DataAccess.Request.PersonCreated:
        //            person.ActionType = DataAccess.Request.PersonCreated.ToString();
        //            break;
        //        case DataAccess.Request.PersonUpdate:
        //            break;
        //        case DataAccess.Request.Verify:
        //            break;
        //        case DataAccess.Request.BadEMail:
        //            break;
        //    }
        //    return person;
        //}

        public CommunicationManager ManagePacket()
        {
            switch (this.ClassType)
            {
                case DataAccess.ClassType.Person:
                    // converting bytes to Peron
                    Person _person = new Person(this.ObjectType);
                    switch (this.Request)
                    {
                        // Logg In Request to server
                        case DataAccess.Request.LoggIn:
                            // Check if person exists                           
                            if (Person.IfPersonExists("", _person.EMail, _person.PassWord))
                            {
                                // person excists returning perosn
                                _person = Person.GetPerson("", _person.EMail, _person.PassWord);
                                this.Request = DataAccess.Request.LoggInOK;
                            }
                            else
                            {
                                //person not found
                                this.Request = DataAccess.Request.LoggInFailed;
                            }
                            break;
                        // Logg In Answer from Server
                        case DataAccess.Request.LoggInOK:

                            break;
                        // Logg In Answer from Server
                        case DataAccess.Request.LoggInFailed:
                            break;

                        case DataAccess.Request.New:
                            break;
                        case DataAccess.Request.ClubUpdate:
                            //Check if Person exists
                            if (Person.IfPersonExists("", _person.EMail, _person.PassWord))
                            {
                                // person found updating person
                                if (Person.UpdateClub(_person))
                                {
                                    this.Request = DataAccess.Request.LoggInOK;
                                }
                                else
                                {
                                    this.Request = DataAccess.Request.UpdateFailed;
                                }
                            }
                            else
                            {
                                //person not found
                                this.Request = DataAccess.Request.LoggInFailed;
                            }
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
                            // Check if Person already exists
                            if (Person.IfPersonExists("", _person.EMail, ""))
                            {
                                // Player already exist
                                this.Request = DataAccess.Request.PersonExist;
                            }
                            else
                            {
                                // Need verification
                                this.Info = DataAccess.GetVerificationCode();
                                _person.IsVerified = false;
                                if (Send_Verification("","","",_person.EMail,"NSOP New account verification.", this.Info) && _person.SaveNew())
                                {
                                    this.Request = DataAccess.Request.NewAccountVerify;
                                }
                                else
                                {
                                    this.Request = DataAccess.Request.BadEMail;
                                }

                            }
                            break;
                        case DataAccess.Request.PersonExist:
                            break;
                        case DataAccess.Request.PersonCreated:
                            break;
                        case DataAccess.Request.NewAccountVerify:
                            break;
                        case DataAccess.Request.VerifyOK:
                            // logging granted
                            if (Person.UpdateVerification(_person.EMail) == true)
                            {
                                _person.IsVerified = true;
                                this.Request = DataAccess.Request.LoggInOK;
                            }
                            else
                            {
                                this.Request = DataAccess.Request.BadEMail;
                            }
                            break;
                        case DataAccess.Request.BadEMail:
                            break;
                        case DataAccess.Request.ResetPassword:
                            this.Info = DataAccess.GetVerificationCode();
                            if (Send_Verification("", "", "", _person.EMail, "NSOP Reset Password verification.", this.Info))
                            {
                                this.Request = DataAccess.Request.ResetPasswordVerify;
                            }
                            else
                            {
                                this.Request = DataAccess.Request.BadEMail;
                            }
                            break;
                        case DataAccess.Request.UpdatePassword:
                            if (Person.UpdatePassword(_person) == true)
                            {
                                this.Request = DataAccess.Request.ResetPasswordOK;
                            }
                            else
                            {
                                this.Request = DataAccess.Request.BadEMail;
                            }
                            break;
                        case DataAccess.Request.ResetPasswordVerify:
                            break;
                        case DataAccess.Request.ResetPasswordOK:
                            break;
                    }
                    this.ObjectType = _person.ToBytes();

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
            return this;
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

        private static bool Send_Verification(string smtp, string emailFrom, string passWord, string eMail, string subject, string verificationCode)
        {
            bool _IsSendt;
            try
            {
                string _smtpClient = "smtp-mail.outlook.com";
                //string _mailFrom = "post.nsop@outlook.com";
                //string _mailPassword = "62N24s34o199p";
                string _mailFrom = "ovehauge@hotmail.no";
                string _mailPassword = "OSilverO1967O";
                int _smtpPort = 25;

                MailMessage _mail = new MailMessage();
                //put your SMTP address and port here.
                SmtpClient _SmtpServer = new SmtpClient(_smtpClient, _smtpPort);// smtp-mail.outlook.com
                //Put the email address
                _mail.From = new MailAddress(_mailFrom);
                //Put the email where you want to send.
                _mail.To.Add(eMail);

                _mail.Subject = subject;

                StringBuilder _sb = new StringBuilder();
                _sb.AppendLine("Your verification code is " + verificationCode);

                _mail.Body = _sb.ToString();

                //Your log file path
                //System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(@"C:\Logs\CheckoutPOS.log");
                //mail.Attachments.Add(attachment);

                //Your username and password!

                //SmtpServer.Credentials = new System.Net.NetworkCredential("ovehauge@hotmail.no", "Silver1967X");
                _SmtpServer.Credentials = new System.Net.NetworkCredential(_mailFrom, _mailPassword);
                //Set Smtp Server port
                //      iVerdi = Convert.ToInt16(HENT_BASE_VERDI(dbOppsett, "DB_Mail", "Navn", dbMail, "", "", "SmtpServerPort"));
                _SmtpServer.Port = 25;
                //  bool xBool =Convert.ToBoolean(HENT_BASE_VERDI(dbOppsett, "DB_Mail", "Navn", dbMail, "", "", "SmtpEnable"));
                _SmtpServer.EnableSsl = true;

                _SmtpServer.Send(_mail);
                _SmtpServer.Dispose();
                _IsSendt = true;
                //MessageBox.Show("The exception has been sent! :)");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                _IsSendt = false;
            }
            return _IsSendt;
        }

        //private static bool Send_ResetVerification(string eMail, string verificationCode)
        //{
        //    bool _IsSendt;
        //    try
        //    {
        //        string _smtpClient = "smtp-mail.outlook.com";
        //        //string _mailFrom = "post.nsop@outlook.com";
        //        //string _mailPassword = "62N24s34o199p";
        //        string _mailFrom = "ovehauge@hotmail.no";
        //        string _mailPassword = "OSilverO1967O";
        //        int _smtpPort = 25;

        //        MailMessage _mail = new MailMessage();
        //        //put your SMTP address and port here.
        //        SmtpClient _SmtpServer = new SmtpClient(_smtpClient, _smtpPort);// smtp-mail.outlook.com
        //        //Put the email address
        //        _mail.From = new MailAddress(_mailFrom);
        //        //Put the email where you want to send.
        //        _mail.To.Add(eMail);

        //        _mail.Subject = "NSOP Reset password verification";

        //        StringBuilder _sb = new StringBuilder();
        //        _sb.AppendLine("Your verification code is " + verificationCode);

        //        _mail.Body = _sb.ToString();

        //        //Your log file path
        //        //System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(@"C:\Logs\CheckoutPOS.log");
        //        //mail.Attachments.Add(attachment);

        //        //Your username and password!

        //        //SmtpServer.Credentials = new System.Net.NetworkCredential("ovehauge@hotmail.no", "Silver1967X");
        //        _SmtpServer.Credentials = new System.Net.NetworkCredential(_mailFrom, _mailPassword);
        //        //Set Smtp Server port
        //        //      iVerdi = Convert.ToInt16(HENT_BASE_VERDI(dbOppsett, "DB_Mail", "Navn", dbMail, "", "", "SmtpServerPort"));
        //        _SmtpServer.Port = 25;
        //        //  bool xBool =Convert.ToBoolean(HENT_BASE_VERDI(dbOppsett, "DB_Mail", "Navn", dbMail, "", "", "SmtpEnable"));
        //        _SmtpServer.EnableSsl = true;

        //        _SmtpServer.Send(_mail);
        //        _SmtpServer.Dispose();
        //        _IsSendt = true;
        //        //MessageBox.Show("The exception has been sent! :)");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //        _IsSendt = false;
        //    }
        //    return _IsSendt;
        //}
        //private static bool Send_NewAccountVerification(string eMail, string verificationCode)
        //{
        //    bool _IsSendt;
        //    try
        //    {
        //        string _smtpClient = "smtp-mail.outlook.com";
        //        //string _mailFrom = "post.nsop@outlook.com";
        //        //string _mailPassword = "62N24s34o199p";
        //        string _mailFrom = "ovehauge@hotmail.no";
        //        string _mailPassword = "OSilverO1967O";
        //        int _smtpPort = 25;

        //        MailMessage _mail = new MailMessage();
        //        //put your SMTP address and port here.
        //        SmtpClient _SmtpServer = new SmtpClient(_smtpClient, _smtpPort);// smtp-mail.outlook.com
        //        //Put the email address
        //        _mail.From = new MailAddress(_mailFrom);
        //        //Put the email where you want to send.
        //        _mail.To.Add(eMail);

        //        _mail.Subject = "NSOP Verification";

        //        StringBuilder _sb = new StringBuilder();
        //        _sb.AppendLine("Your verification code is " + verificationCode);

        //        _mail.Body = _sb.ToString();

        //        //Your log file path
        //        //System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(@"C:\Logs\CheckoutPOS.log");
        //        //mail.Attachments.Add(attachment);

        //        //Your username and password!

        //        //SmtpServer.Credentials = new System.Net.NetworkCredential("ovehauge@hotmail.no", "Silver1967X");
        //        _SmtpServer.Credentials = new System.Net.NetworkCredential(_mailFrom, _mailPassword);
        //        //Set Smtp Server port
        //        //      iVerdi = Convert.ToInt16(HENT_BASE_VERDI(dbOppsett, "DB_Mail", "Navn", dbMail, "", "", "SmtpServerPort"));
        //        _SmtpServer.Port = 25;
        //        //  bool xBool =Convert.ToBoolean(HENT_BASE_VERDI(dbOppsett, "DB_Mail", "Navn", dbMail, "", "", "SmtpEnable"));
        //        _SmtpServer.EnableSsl = true;

        //        _SmtpServer.Send(_mail);
        //        _SmtpServer.Dispose();
        //        _IsSendt = true;
        //        //MessageBox.Show("The exception has been sent! :)");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //        _IsSendt = false;
        //    }
        //    return _IsSendt;
        //}
    }
}
