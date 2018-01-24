using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hi_Tech_Management_System.Business
{
    [Serializable]
    public class Order 
    {
        private int orderID;
        private int clientID;
        private int itemID;
        private string itemName;
        private decimal unitPrice;
        private int quantity;
        private string orderedBy;
        private string orderDate;
        private string shipDate;
        private decimal orderTotal;
        private string placedBy;

        public Order() {
            this.OrderID = 0;
            this.clientID = 0;
            this.itemName = "Default";
            this.unitPrice = 0;
            this.quantity = 0;
            this.orderedBy = "Default";
            this.orderDate = "Default";
            this.shipDate = "Default";
            this.orderTotal = 0;
            this.placedBy = "Default";
            this.itemID = 0;
        }

        public Order(int orderID, int clientID, int itemID, string itemName, decimal unitPrice, int quantity, string orderedBy, string orderDate, string shipDate, decimal orderTotal, string placedBy)
        {
            this.orderID = orderID;
            this.clientID = clientID;
            this.itemID = itemID;
            this.itemName = itemName;
            this.unitPrice = unitPrice;
            this.quantity = quantity;
            this.orderedBy = orderedBy;
            this.orderDate = orderDate;
            this.shipDate = shipDate;
            this.orderTotal = orderTotal;
            this.placedBy = placedBy;
        }

        public int ClientID
        {
            get
            {
                return clientID;
            }

            set
            {
                clientID = value;
            }
        }

        public string ItemName
        {
            get
            {
                return itemName;
            }

            set
            {
                itemName = value;
            }
        }

        public decimal UnitPrice
        {
            get
            {
                return unitPrice;
            }

            set
            {
                unitPrice = value;
            }
        }

        public int Quantity
        {
            get
            {
                return quantity;
            }

            set
            {
                quantity = value;
            }
        }

        public string OrderedBy
        {
            get
            {
                return orderedBy;
            }

            set
            {
                orderedBy = value;
            }
        }

        public string OrderDate
        {
            get
            {
                return orderDate;
            }

            set
            {
                orderDate = value;
            }
        }

        public decimal OrderTotal
        {
            get
            {
                return orderTotal;
            }

            set
            {
                orderTotal = value;
            }
        }

        public string PlacedBy
        {
            get
            {
                return placedBy;
            }

            set
            {
                placedBy = value;
            }
        }

        public int ItemID
        {
            get
            {
                return itemID;
            }

            set
            {
                itemID = value;
            }
        }

        public int OrderID
        {
            get
            {
                return orderID;
            }

            set
            {
                orderID = value;
            }
        }

        public string ShipDate
        {
            get
            {
                return shipDate;
            }

            set
            {
                shipDate = value;
            }
        }
    }
}
