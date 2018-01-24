using System;
using System.Collections.Generic;

namespace Hi_Tech_Management_System.Business
{
    [Serializable]
    public partial class GeneralDataManipulation
    {
        public static string userPosition;
        public static int userID;
        public static string userPass;
        public static int userINDEX;

        //List related to the Employee
        public static List<Employee> listOfEmployee = new List<Employee>();

        //List related to the Client
        public static List<Client> listOfClients = new List<Client>();

        //Lists related to the Sales
        public static List<Author> listOfAuthors = new List<Author>();
        public static List<Book> listOfItems = new List<Book>();
        public static List<Publisher> listOfPublishers = new List<Publisher>();
        public static List<Category> listOfCategory = new List<Category>();
        public static List<ItemType> listOfType = new List<ItemType>();

        //Lists related to the Orders
        public static List<Order> listOfOrders = new List<Order>();
    }
}
