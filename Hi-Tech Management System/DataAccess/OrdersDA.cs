using System.Collections.Generic;
using Hi_Tech_Management_System.Business;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Hi_Tech_Management_System.DataAccess
{
    class OrdersDA
    {
            public static string binPath = @"..\..\Database\OrdersBIN.ser";
            public static void WriteUser(List<Order> listOfOrders)
            {
                FileStream fs = new FileStream(binPath, FileMode.Create, FileAccess.Write);
                BinaryFormatter bin = new BinaryFormatter();
                foreach (Order item in listOfOrders)
                {
                    bin.Serialize(fs, item);
                }
                fs.Close();
            }

            public static List<Order> ReadUsers()
            {
                List<Order> list = new List<Order>();
                if (File.Exists(binPath))
                {
                    FileStream fs = new FileStream(binPath, FileMode.Open, FileAccess.Read);
                    BinaryFormatter bin = new BinaryFormatter();
                    while (fs.Position < fs.Length)
                    {
                        Order unUser = new Order();
                        unUser = (Order)bin.Deserialize(fs);
                        list.Add(unUser);
                    }
                    fs.Close();
                }
                return list;
            }
        }
    
}
