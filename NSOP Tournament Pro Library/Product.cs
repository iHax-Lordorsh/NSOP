using NSOP_Tournament_Pro_Library;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NSOP_Torunament_Pro_Library
{
    [Serializable]
    public class Product
    {
        private string _Id = "";
        private string _Name = "";
        private string _Picture;
        private string _Info = "";
        private string _Description = "";
        private decimal _Price = 0;
        private int _Discount = 0;
        private int _Quantity = 0;

        private DateTime _StartDate;
        private DateTime _EndDate;
        private string _Expire;
        private string _ProductType;

        private string _Id_1 = "";
        private string _Id_2 = "";
        private string _Id_3 = "";
        private string _Id_4 = "";
        private string _Id_5 = "";
        private string _Id_6 = "";

        private int _Qty_1 = 0;
        private int _Qty_2 = 0;
        private int _Qty_3 = 0;
        private int _Qty_4 = 0;
        private int _Qty_5 = 0;
        private int _Qty_6 = 0;

        public string ID { get => _Id; set { _Id = value; } }
        public string ProductName { get => _Name; set { _Name = value; } }
        public string Picture { get => _Picture; set { _Picture = value; } }
        public string Information { get => _Info; set { _Info = value; } }

        public string Description { get => _Description; set { _Description = value; } }
        public decimal Price { get => _Price; set { _Price = value; } }
        public int Discount { get => _Discount; set { _Discount = value; } }
        public int Quantity { get => _Quantity; set { _Quantity = value; } }

        public string Expires { get => _Expire; set { _Expire = value; } }
        public DateTime StartDate { get => _StartDate; set { _StartDate = value; } }
        public DateTime EndDate { get => _EndDate; set { _EndDate = value; } }

        public string ProductType { get => _ProductType; set { _ProductType = value; } }

        public string ID_1 { get => _Id_1; set { _Id_1 = value; } }
        public string ID_2 { get => _Id_2; set { _Id_2 = value; } }
        public string ID_3 { get => _Id_3; set { _Id_3 = value; } }
        public string ID_4 { get => _Id_4; set { _Id_4 = value; } }
        public string ID_5 { get => _Id_5; set { _Id_5 = value; } }
        public string ID_6 { get => _Id_6; set { _Id_6 = value; } }

        public int Qty_1 { get => _Qty_1; set { _Qty_1 = value; } }
        public int Qty_2 { get => _Qty_2; set { _Qty_2 = value; } }
        public int Qty_3 { get => _Qty_3; set { _Qty_3 = value; } }
        public int Qty_4 { get => _Qty_4; set { _Qty_4 = value; } }
        public int Qty_5 { get => _Qty_5; set { _Qty_5 = value; } }
        public int Qty_6 { get => _Qty_6; set { _Qty_6 = value; } }

        private List<Product> _list = new List<Product>();
        public List<Product> StartProductsList { get => _list; set => _list = value; }

        // ***********
        // CONSTRUCTORS
        // ***********

        // Empty Constructor
        public Product()
        {
        }
        // Constructor to recieve packet
        public Product(byte[] packetBytes)
        {
            BinaryFormatter _bf = new BinaryFormatter();
            MemoryStream _ms = new MemoryStream(packetBytes)
            {
                Position = 0
            };
            Product _p = (Product)_bf.Deserialize(_ms);
            _ms.Close();

            // Populate Personalia
            this.ID = _p.ID;
            this.ProductName = _p.ProductName;
            this.Picture = _p.Picture;
            this.Information = _p.Information;
            this.Description = _p.Description;
            this.Price = _p.Price;
            this.Discount = _p.Discount;
            this.Quantity = _p.Quantity;
            this.Expires = _p.Expires;
            this.StartDate = _p.StartDate;
            this.EndDate = _p.EndDate;
            this.ID_1 = _p.ID_1;
            this.Qty_1 = _p.Qty_1;
            this.ID_2 = _p.ID_2;
            this.Qty_2 = _p.Qty_2;
            this.ID_3 = _p.ID_3;
            this.Qty_3 = _p.Qty_3;
            this.ID_4 = _p.ID_4;
            this.Qty_4 = _p.Qty_4;
            this.ID_5 = _p.ID_5;
            this.Qty_5 = _p.Qty_5;
            this.ID_6 = _p.ID_6;
            this.Qty_6 = _p.Qty_6;
            this.StartProductsList = _p.StartProductsList;
            this.ProductType = _p.ProductType;

        }
        //Constructor to send Personalia
        //public Product(string playerID, string clubID, string firstName, string lastName, byte[] picture, string mobile, string email, string gender, string nationality, string iso3166Name, DateTime bornDate, DateTime regDate, string nickName, string passWord, string userID, bool isPlayerRemoved, bool isVerified)
        //{
        //    this.PlayerID = playerID;
        //    this.ClubID = clubID;
        //    this.FirstName = firstName;
        //    this.LastName = lastName;
        //    this.Picture = picture;
        //    this.Mobile = mobile;
        //    this.EMail = email;
        //    this.Gender = gender;
        //    this.Nationality = nationality;
        //    this.Iso3166Name = iso3166Name;
        //    this.BornDate = bornDate;
        //    this.RegDate = regDate;
        //    this.NickName = nickName;
        //    this.PassWord = passWord;
        //    this.UserID = userID;
        //    this.IsPlayerRemoved = isPlayerRemoved;
        //    this.IsVerified = isVerified;
        //}
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
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbOption; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            bool _isOk;
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $" INSERT into tbProduct ";
                cmd.CommandText += $"( ";
                //
                cmd.CommandText += $"ID, Name, Picture, Info, ";
                cmd.CommandText += $"Description, Price, Discount, Quantity, ";
                cmd.CommandText += $"Expires, StartDate, EndDate, ";
                cmd.CommandText += $"ID_1, Qty_1, ";
                cmd.CommandText += $"ID_2, Qty_2, ";
                cmd.CommandText += $"ID_3, Qty_3, ";
                cmd.CommandText += $"ID_4, Qty_4, ";
                cmd.CommandText += $"ID_5, Qty_5, ";
                cmd.CommandText += $"ID_6, Qty_6, ProductType ";
                //Values
                cmd.CommandText += $") ";
                cmd.CommandText += $"VALUES ";
                cmd.CommandText += $"( ";
                //Personalia
                cmd.CommandText += $"'{ID}', '{ProductName}','{Picture}', '{Information}', ";
                cmd.CommandText += $"'{Description}',@Price, {Discount}, {Quantity}, ";
                cmd.CommandText += $"'{Expires}', '{StartDate.ToShortDateString()}', '{EndDate.ToShortDateString()}', ";
                cmd.CommandText += $"'{ID_1}', {Qty_1}, ";
                cmd.CommandText += $"'{ID_2}', {Qty_2}, ";
                cmd.CommandText += $"'{ID_3}', {Qty_3}, ";
                cmd.CommandText += $"'{ID_4}', {Qty_4}, ";
                cmd.CommandText += $"'{ID_5}', {Qty_5}, ";
                cmd.CommandText += $"'{ID_6}', {Qty_6}, '{ProductType}' ";

                cmd.CommandText += $") ";
                cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(Price.ToString()));
                cmd.ExecuteNonQuery();

                //Save LifeTime
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
        //public bool Update(Product product)
        //{
        //    SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbPerson; Trusted_Connection = True; Asynchronous Processing=True; ");
        //    if (con.State == ConnectionState.Closed)
        //    {
        //        con.Open();
        //    }
        //    bool _isOk;
        //    try
        //    {
        //        SqlCommand cmd = con.CreateCommand();
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = $" UPDATE tbPerson SET ";
        //        //Personalia
        //        cmd.CommandText += $"FirstName = '{product.FirstName}', LastName = '{product.LastName}', ";
        //        cmd.CommandText += $"Picture = @Picture, Mobile = '{product.Mobile}', ";
        //        cmd.CommandText += $"EMail ='{product.EMail}',Gender ='{product.Gender}', Nationality ='{product.Nationality}', ";
        //        cmd.CommandText += $"Iso3166Name = '{product.Iso3166Name}', BornDate = '{product.BornDate.ToShortDateString()}', RegDate = '{product.RegDate.ToShortDateString()}', ";
        //        cmd.CommandText += $"NickName = '{product.NickName}', PassWord = '{product.PassWord}', IsPlayerRemoved = '{product.IsPlayerRemoved}', ";
        //        cmd.CommandText += $"IsLoggedInn = '{product.IsLoggedInn}', ClubID = '{product.ClubID}', ClubName = '{product.ClubName}', ClubPicture = '{product.ClubPicture}', ";
        //        //Rank
        //        cmd.CommandText += $"ClubRank = '{product.ClubRank}', ClubPoints = '{product.ClubPoints}', ";
        //        cmd.CommandText += $"ClubMembership = '{product.ClubMembership}', ClubMembershipExpires = '{product.ClubMembershipExpires.ToShortDateString()}', ClubRegDate = '{product.ClubRegDate.ToShortDateString()}', ";
        //        cmd.CommandText += $"NSOPID = '{product.NSOPID}', NSOPRank = '{product.NSOPRank}', NSOPPoints = '{product.NSOPPoints}', ";
        //        cmd.CommandText += $"NSOPMembership = '{product.NSOPMembership}', NSOPMembershipExpires = '{product.NSOPMembershipExpires.ToShortDateString()}', NSOPRegDate = '{product.NSOPRegDate.ToShortDateString()}', ";
        //        cmd.CommandText += $"NationalID = '{product.NationalID}', NationalRank = '{product.NationalRank}', NationalPoints = '{product.NationalPoints}', ";
        //        cmd.CommandText += $"NationalMembership = '{product.NationalMembership}', NationalMembershipExpires = '{product.NationalMembershipExpires.ToShortDateString()}', NationalRegDate = '{product.NationalRegDate.ToShortDateString()}', ";
        //        cmd.CommandText += $"WorldID = '{product.WorldID}', WorldRank = '{product.WorldRank}', WorldPoints = '{product.WorldPoints}', ";
        //        cmd.CommandText += $"WorldMembership = '{product.WorldMembership}', WorldMembershipExpires = '{product.WorldMembershipExpires.ToShortDateString()}', WorldRegDate = '{product.WorldRegDate.ToShortDateString()}', ";
        //        //Lifetime
        //        cmd.CommandText += $"LifetimePlayed = '{product.LifetimePlayed}', LifetimeWins = '{product.LifetimeWins}', LifetimeFinalTables = '{product.LifetimeFinalTables}', ";
        //        cmd.CommandText += $"LifetimeCashed = '{product.LifetimeCashed}', LifetimeBubbles = '{product.LifetimeBubbles}', LifetimeFirstOuts = '{product.LifetimeFirstOuts}', ";
        //        cmd.CommandText += $"LifetimeSevenDeuces = '{product.LifetimeSevenDeuces}', LifetimeBadBeats = '{product.LifetimeBadBeats}', LifetimeTakeOuts = '{product.LifetimeTakeOuts}', ";
        //        cmd.CommandText += $"LifetimeHunted = '{product.LifetimeHunted}', LifetimeMoneyEarned = '{product.LifetimeMoneyEarned}', LifetimeMoneySpent = '{product.LifetimeMoneySpent}', ";
        //        //Modul Access
        //        cmd.CommandText += $"AdminSubscribtion = '{product.AdminSubscribtion}', WebSideSubscribtion = '{product.WebSideSubscribtion}', LastLogin = '{product.LastLogin}', ";
        //        cmd.CommandText += $"Tokens = '{product.Tokens}', TournamentCreatorQTY = '{product.TournamentCreatorQTY}', PersonRegQTY = '{product.PersonRegQTY}', ";
        //        cmd.CommandText += $"TicketSaleQTY = '{product.TicketSaleQTY}', BlindStructureQTY = '{product.BlindStructureQTY}', PayoutStructureQTY = '{product.PayoutStructureQTY}', ";
        //        cmd.CommandText += $"PointStructureQTY = '{product.PointStructureQTY}', AdvertiseModuleQTY = '{product.AdvertiseModuleQTY}', TableManagerQTY = '{product.TableManagerQTY}', ";
        //        cmd.CommandText += $"StandUserName = '{product.StandUserName}', StandPassWord = '{product.StandPassWord}' ";

        //        //Where
        //        cmd.CommandText += $"WHERE PlayerID = '{product.PlayerID}';";
        //        cmd.Parameters.AddWithValue("@Picture", product.Picture);


        //        //error = cmd.CommandText;
        //        cmd.ExecuteNonQuery();

        //       // Save LifeTime
        //        _isOk = true;
        //    }
        //    catch (Exception e)
        //    {
        //        _ = e.ToString();
        //        _isOk = false;
        //    }
        //    con.Close();
        //    return _isOk;
        //}

        //public static bool UpdateClub(Product product)
        //{
        //    SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbPerson; Trusted_Connection = True; Asynchronous Processing=True; ");
        //    if (con.State == ConnectionState.Closed)
        //    {
        //        con.Open();
        //    }
        //    bool _isOk;
        //    try
        //    {
        //        SqlCommand cmd = con.CreateCommand();
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = $" UPDATE tbPerson SET ";
        //        //Personalia
        //        cmd.CommandText += $"FirstName = '{product.FirstName}', LastName = '{product.LastName}', ";
        //        cmd.CommandText += $"Picture = @Picture, Mobile = '{product.Mobile}', ";
        //        cmd.CommandText += $"Gender ='{product.Gender}', Nationality ='{product.Nationality}', ";
        //        cmd.CommandText += $"Iso3166Name = '{product.Iso3166Name}', BornDate = '{product.BornDate.ToShortDateString()}', ";
        //        cmd.CommandText += $"NickName = '{product.NickName}', ";
        //        cmd.CommandText += $"ClubName = '{product.ClubName}', ClubPicture = '{product.ClubPicture}', ";
        //        cmd.CommandText += $"StandUserName = '{product.StandUserName}', StandPassWord = '{product.StandPassWord}' ";

        //        //Where
        //        cmd.CommandText += $"WHERE EMail = '{product.EMail}';";
        //        cmd.Parameters.AddWithValue("@Picture", product.Picture);


        //        //error = cmd.CommandText;
        //        cmd.ExecuteNonQuery();

        //        //Save LifeTime
        //        _isOk = true;
        //    }
        //    catch (Exception e)
        //    {
        //        _ = e.ToString();
        //        _isOk = false;
        //    }
        //    con.Close();
        //    return _isOk;
        //}
        public bool Delete(Product product)
        {
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbOption; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            bool _isOk;
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $" DELETE FROM tbPerson WHERE PlayerID = '{product.ID}';";

                //error = cmd.CommandText;
                cmd.ExecuteNonQuery();

                //Save LifeTime
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

        public static Product GetProduct(string ID)
        {
            Product _p = new Product();
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbOption; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {
                SqlDataReader _SqlData = null;
                StringBuilder _s = new StringBuilder();
                _s.Append($"SELECT * FROM dbo.tbProduct ");
                if (ID.Length > 0)
                {
                    _s.Append($"PlayerID = '{ID}' ");
                }
                _s.Append($";");

                SqlCommand _SqlStr = new SqlCommand(_s.ToString(), con);

                _SqlData = _SqlStr.ExecuteReader();
                _SqlStr.Dispose();
                while (_SqlData.Read())
                {
                    //Populate Product
                    _p.ID = _SqlData["ID"].ToString();
                    _p.ProductName = _SqlData["Name"].ToString();
                    _p.Picture = _SqlData["Picture"].ToString();
                    _p.Information = _SqlData["Info"].ToString();
                    _p.Description = _SqlData["Description"].ToString();
                    _p.Price = (int)_SqlData["Price"];
                    _p.Discount = (int)_SqlData["Discount"];
                    _p.Expires = _SqlData["Expires"].ToString();
                    _p.StartDate = DateTime.Parse(_SqlData["StartDate"].ToString());
                    _p.EndDate = DateTime.Parse(_SqlData["EndDate"].ToString());

                    _p.ID_1 = _SqlData["ID_1"].ToString();
                    _p.Qty_1 = (int)_SqlData["Qty_1"];
                    _p.ID_2 = _SqlData["ID_2"].ToString();
                    _p.Qty_2 = (int)_SqlData["Qty_2"];
                    _p.ID_3 = _SqlData["ID_3"].ToString();
                    _p.Qty_3 = (int)_SqlData["Qty_3"];
                    _p.ID_4 = _SqlData["ID_4"].ToString();
                    _p.Qty_4 = (int)_SqlData["Qty_4"];
                    _p.ID_5 = _SqlData["ID_5"].ToString();
                    _p.Qty_5 = (int)_SqlData["Qty_5"];
                    _p.ID_6 = _SqlData["ID_6"].ToString();
                    _p.Qty_6 = (int)_SqlData["Qty_6"];

                }
                //Save LifeTime
            }
            catch (Exception e)
            {
                _ = e.ToString();
            }
            con.Close();
            return _p;
        }
        public Product GetProductList(string ID)
        {
            Product _p = new Product();
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbOption; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {
                SqlDataReader _SqlData = null;
                StringBuilder _s = new StringBuilder();
                _s.Append($"SELECT * FROM dbo.tbProduct WHERE ");
                if (ID.Length > 0)
                {
                    _s.Append($"ID = '{ID}'");
                }
                _s.Append($";");

                SqlCommand _SqlStr = new SqlCommand(_s.ToString(), con);

                _SqlData = _SqlStr.ExecuteReader();
                _SqlStr.Dispose();
                while (_SqlData.Read())
                {
                    Product _product = new Product
                    {
                        //Populate Product
                        ID = _SqlData["ID"].ToString(),
                        ProductName = _SqlData["Name"].ToString(),
                        Picture = _SqlData["Picture"].ToString(),
                        Information = _SqlData["Info"].ToString(),
                        Description = _SqlData["Description"].ToString(),
                        Price = (decimal)_SqlData["Price"],
                        Discount = (int)_SqlData["Discount"],
                        Expires = _SqlData["Expires"].ToString(),
                        StartDate = DateTime.Parse(_SqlData["StartDate"].ToString()),
                        EndDate = DateTime.Parse(_SqlData["EndDate"].ToString()),

                        ID_1 = _SqlData["ID_1"].ToString(),
                        Qty_1 = (int)_SqlData["Qty_1"],
                        ID_2 = _SqlData["ID_2"].ToString(),
                        Qty_2 = (int)_SqlData["Qty_2"],
                        ID_3 = _SqlData["ID_3"].ToString(),
                        Qty_3 = (int)_SqlData["Qty_3"],
                        ID_4 = _SqlData["ID_4"].ToString(),
                        Qty_4 = (int)_SqlData["Qty_4"],
                        ID_5 = _SqlData["ID_5"].ToString(),
                        Qty_5 = (int)_SqlData["Qty_5"],
                        ID_6 = _SqlData["ID_6"].ToString(),
                        Qty_6 = (int)_SqlData["Qty_6"]
                    };
                    StartProductsList.Add(_product);

                }
                //Save LifeTime
            }
            catch (Exception e)
            {
                _ = e.ToString();
            }
            con.Close();
            return _p;
        }
        public List<Product> GetStartUpProductList()
        {
            List<Product> _pList = new List<Product>();
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbOption; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {
                SqlDataReader _SqlData = null;
                StringBuilder _s = new StringBuilder();
                _s.Append($"SELECT * FROM dbo.tbProduct WHERE ");
                _s.Append($"ID LIKE '%0000 0000%'  ");
                _s.Append($";");

                SqlCommand _SqlStr = new SqlCommand(_s.ToString(), con);

                _SqlData = _SqlStr.ExecuteReader();
                _SqlStr.Dispose();
                while (_SqlData.Read())
                {
                    Product _product = new Product
                    {
                        //Populate Product
                        ID = _SqlData["ID"].ToString(),
                        ProductName = _SqlData["Name"].ToString(),
                        Picture = _SqlData["Picture"].ToString(),
                        Information = _SqlData["Info"].ToString(),
                        Description = _SqlData["Description"].ToString(),
                        Price = (decimal)_SqlData["Price"],
                        Discount = (int)_SqlData["Discount"]
                    };

                    if (_SqlData["Expires"].ToString() == "")
                    {
                    }
                    else _product.Expires = _SqlData["Expires"].ToString();

                    if (_SqlData["StartDate"].ToString() == "")
                    {
                    }
                    else _product.StartDate = DateTime.Parse(_SqlData["StartDate"].ToString());

                    if (_SqlData["EndDate"].ToString() == "")
                    {
                    }
                    else _product.EndDate = DateTime.Parse(_SqlData["EndDate"].ToString());

                    if (_SqlData["Qty_1"].ToString() == "")
                    {
                    }
                    else _product.Qty_1 = (int)_SqlData["Qty_1"];

                    if (_SqlData["Qty_2"].ToString() == "")
                    {
                    }
                    else _product.Qty_2 = (int)_SqlData["Qty_2"];

                    if (_SqlData["Qty_3"].ToString() == "")
                    {
                    }
                    else _product.Qty_3 = (int)_SqlData["Qty_3"];

                    if (_SqlData["Qty_4"].ToString() == "")
                    {
                    }
                    else _product.Qty_4 = (int)_SqlData["Qty_4"];

                    if (_SqlData["Qty_5"].ToString() == "")
                    {
                    }
                    else _product.Qty_5 = (int)_SqlData["Qty_5"];

                    if (_SqlData["Qty_6"].ToString() == "")
                    {
                    }
                    else _product.Qty_6 = (int)_SqlData["Qty_6"];

                    if (_SqlData["ID_1"].ToString() == "")
                    {
                    }
                    else _product.ID_1 = _SqlData["ID_1"].ToString();

                    if (_SqlData["ID_2"].ToString() == "")
                    {
                    }
                    else _product.ID_2 = _SqlData["ID_2"].ToString();

                    if (_SqlData["ID_3"].ToString() == "")
                    {
                    }
                    else _product.ID_3 = _SqlData["ID_3"].ToString();

                    if (_SqlData["ID_4"].ToString() == "")
                    {
                    }
                    else _product.ID_4 = _SqlData["ID_4"].ToString();

                    if (_SqlData["ID_5"].ToString() == "")
                    {
                    }
                    else _product.ID_5 = _SqlData["ID_5"].ToString();

                    if (_SqlData["ID_6"].ToString() == "")
                    {
                    }
                    else _product.ID_6 = _SqlData["ID_6"].ToString();

                    if (_SqlData["ProductType"].ToString() == "")
                    {
                    }
                    else _product.ProductType = _SqlData["ProductType"].ToString();

                    _pList.Add(_product);
                }
                //Save LifeTime
            }
            catch (Exception e)
            {
                _ = e.ToString();
            }
            con.Close();
            return _pList;
        }
        public List<Product> GetAllProductList(string productType)
        {
            List<Product> _pList = new List<Product>();
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbOption; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {
                SqlDataReader _SqlData = null;
                StringBuilder _s = new StringBuilder();
                _s.Append($"SELECT * FROM dbo.tbProduct WHERE ");
                _s.Append($"ID NOT LIKE 'ITEM 0000 0000 %%%I' ");
                switch (DataAccess.ParseEnum<DataAccess.Request>(productType))
                {
                    case DataAccess.Request.GetProductsAll:
                        break;
                    case DataAccess.Request.GetProductSubscription:
                        _s.Append($"AND ProductType = 'Subscription' ");
                        break;
                    case DataAccess.Request.GetProductToken:
                        _s.Append($"AND ProductType = 'Token' ");
                        break;
                    default:
                        //  AND ProductType = '{productType}' 
                        break;
                }
                _s.Append($";");

                SqlCommand _SqlStr = new SqlCommand(_s.ToString(), con);

                _SqlData = _SqlStr.ExecuteReader();
                _SqlStr.Dispose();
                while (_SqlData.Read())
                {
                    Product _product = new Product
                    {
                        //Populate Product
                        ID = _SqlData["ID"].ToString(),
                        ProductName = _SqlData["Name"].ToString(),
                        Picture = _SqlData["Picture"].ToString(),
                        Information = _SqlData["Info"].ToString(),
                        Description = _SqlData["Description"].ToString(),
                        Price = (decimal)_SqlData["Price"],
                        Discount = (int)_SqlData["Discount"]
                    };

                    if (_SqlData["Expires"].ToString() == "")
                    {
                    }
                    else _product.Expires = _SqlData["Expires"].ToString();

                    if (_SqlData["StartDate"].ToString() == "")
                    {
                    }
                    else _product.StartDate = DateTime.Parse(_SqlData["StartDate"].ToString());

                    if (_SqlData["EndDate"].ToString() == "")
                    {
                    }
                    else _product.EndDate = DateTime.Parse(_SqlData["EndDate"].ToString());

                    if (_SqlData["Qty_1"].ToString() == "")
                    {
                    }
                    else _product.Qty_1 = (int)_SqlData["Qty_1"];

                    if (_SqlData["Qty_2"].ToString() == "")
                    {
                    }
                    else _product.Qty_2 = (int)_SqlData["Qty_2"];

                    if (_SqlData["Qty_3"].ToString() == "")
                    {
                    }
                    else _product.Qty_3 = (int)_SqlData["Qty_3"];

                    if (_SqlData["Qty_4"].ToString() == "")
                    {
                    }
                    else _product.Qty_4 = (int)_SqlData["Qty_4"];

                    if (_SqlData["Qty_5"].ToString() == "")
                    {
                    }
                    else _product.Qty_5 = (int)_SqlData["Qty_5"];

                    if (_SqlData["Qty_6"].ToString() == "")
                    {
                    }
                    else _product.Qty_6 = (int)_SqlData["Qty_6"];

                    if (_SqlData["ID_1"].ToString() == "")
                    {
                    }
                    else _product.ID_1 = _SqlData["ID_1"].ToString();

                    if (_SqlData["ID_2"].ToString() == "")
                    {
                    }
                    else _product.ID_2 = _SqlData["ID_2"].ToString();

                    if (_SqlData["ID_3"].ToString() == "")
                    {
                    }
                    else _product.ID_3 = _SqlData["ID_3"].ToString();

                    if (_SqlData["ID_4"].ToString() == "")
                    {
                    }
                    else _product.ID_4 = _SqlData["ID_4"].ToString();

                    if (_SqlData["ID_5"].ToString() == "")
                    {
                    }
                    else _product.ID_5 = _SqlData["ID_5"].ToString();

                    if (_SqlData["ID_6"].ToString() == "")
                    {
                    }
                    else _product.ID_6 = _SqlData["ID_6"].ToString();

                    if (_SqlData["ProductType"].ToString() == "")
                    {
                    }
                    else _product.ProductType = _SqlData["ProductType"].ToString();

                    _pList.Add(_product);
                }
                //Save LifeTime
            }
            catch (Exception e)
            {
                _ = e.ToString();
            }
            con.Close();
            return _pList;
        }
        public List<Product> GetProductList(string startId, string endId)
        {
            List<Product> _pList = new List<Product>();
            SqlConnection con = new SqlConnection("Data Source = NSOP\\POKER; Initial Catalog = dbOption; Trusted_Connection = True; Asynchronous Processing=True; ");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {
                SqlDataReader _SqlData = null;
                StringBuilder _s = new StringBuilder();
                _s.Append($"SELECT * FROM dbo.tbProduct WHERE ");
                if (startId.Length > 0)
                {
                    _s.Append($"ID >= '{startId}' AND ");
                }
                if (endId.Length > 0)
                {
                    _s.Append($"ID <= '{endId}'");
                }
                _s.Append($";");

                SqlCommand _SqlStr = new SqlCommand(_s.ToString(), con);

                _SqlData = _SqlStr.ExecuteReader();
                _SqlStr.Dispose();
                while (_SqlData.Read())
                {
                    Product _product = new Product
                    {
                        //Populate Product
                        ID = _SqlData["ID"].ToString(),
                        ProductName = _SqlData["Name"].ToString(),
                        Picture = _SqlData["Picture"].ToString(),
                        Information = _SqlData["Info"].ToString(),
                        Description = _SqlData["Description"].ToString(),
                        Price = (decimal)_SqlData["Price"],
                        Discount = (int)_SqlData["Discount"]
                    };

                    if (_SqlData["Expires"].ToString() == "")
                    {
                    }
                    else _product.Expires = _SqlData["Expires"].ToString();

                    if (_SqlData["StartDate"].ToString() == "")
                    {
                    }
                    else _product.StartDate = DateTime.Parse(_SqlData["StartDate"].ToString());

                    if (_SqlData["EndDate"].ToString() == "")
                    {
                    }
                    else _product.EndDate = DateTime.Parse(_SqlData["EndDate"].ToString());

                    if (_SqlData["Qty_1"].ToString() == "")
                    {
                    }
                    else _product.Qty_1 = (int)_SqlData["Qty_1"];

                    if (_SqlData["Qty_2"].ToString() == "")
                    {
                    }
                    else _product.Qty_2 = (int)_SqlData["Qty_2"];

                    if (_SqlData["Qty_3"].ToString() == "")
                    {
                    }
                    else _product.Qty_3 = (int)_SqlData["Qty_3"];

                    if (_SqlData["Qty_4"].ToString() == "")
                    {
                    }
                    else _product.Qty_4 = (int)_SqlData["Qty_4"];

                    if (_SqlData["Qty_5"].ToString() == "")
                    {
                    }
                    else _product.Qty_5 = (int)_SqlData["Qty_5"];

                    if (_SqlData["Qty_6"].ToString() == "")
                    {
                    }
                    else _product.Qty_6 = (int)_SqlData["Qty_6"];

                    if (_SqlData["ID_1"].ToString() == "")
                    {
                    }
                    else _product.ID_1 = _SqlData["ID_1"].ToString();

                    if (_SqlData["ID_2"].ToString() == "")
                    {
                    }
                    else _product.ID_2 = _SqlData["ID_2"].ToString();

                    if (_SqlData["ID_3"].ToString() == "")
                    {
                    }
                    else _product.ID_3 = _SqlData["ID_3"].ToString();

                    if (_SqlData["ID_4"].ToString() == "")
                    {
                    }
                    else _product.ID_4 = _SqlData["ID_4"].ToString();

                    if (_SqlData["ID_5"].ToString() == "")
                    {
                    }
                    else _product.ID_5 = _SqlData["ID_5"].ToString();

                    if (_SqlData["ID_6"].ToString() == "")
                    {
                    }
                    else _product.ID_6 = _SqlData["ID_6"].ToString();

                    if (_SqlData["ProductType"].ToString() == "")
                    {
                    }
                    else _product.ProductType = _SqlData["ProductType"].ToString();

                    _pList.Add(_product);
                }
                //Save LifeTime
            }
            catch (Exception e)
            {
                _ = e.ToString();
            }
            con.Close();
            return _pList;
        }
    }
}
