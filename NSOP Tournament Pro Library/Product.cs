using System;

namespace NSOP_Torunament_Pro_Library
{
    [Serializable]
    public class UserSalesBox
    {
        private string _1 = "";
        public string ProductName { get => _1; set => _1 = value; }

        private string _2 = "";
        public string ProductInfo { get => _2; set => _2 = value; }

        private double _3 = 0;
        public double Price { get => _3; set => _3 = value; }

        private int _4 = 0;
        public int QTY { get => _4; set => _4 = value; }

        private DateTime _5;
        public DateTime Expire { get => _5; set => _5 = value; }


        // ***********
        // CONSTRUCTORS
        // ***********

        // Empty Constructor
        public UserSalesBox()
        {
        }

        // Filled Constructor
        public UserSalesBox(string productName, string productInfo, double price, int qty, DateTime expire)
        {
            this.ProductName = productName;
            this.ProductInfo = productInfo;
            this.Price = price;
            this.QTY = qty;
            this.Expire = expire;
        }
    }
}
