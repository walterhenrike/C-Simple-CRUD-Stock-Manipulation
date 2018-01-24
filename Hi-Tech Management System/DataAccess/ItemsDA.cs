using System.Collections.Generic;
using Hi_Tech_Management_System.Business;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Hi_Tech_Management_System.DataAccess
{
    class ItemsDA
    {
            public static string binPath = @"..\..\Database\ItemBIN.ser";
            public static void WriteUser(List<Book> listOfItems)
            {
                FileStream fs = new FileStream(binPath, FileMode.Create, FileAccess.Write);
                BinaryFormatter bin = new BinaryFormatter();
                foreach (Book item in listOfItems)
                {
                    bin.Serialize(fs, item);
                }
                fs.Close();
            }

            public static List<Book> ReadUsers()
            {
                List<Book> list = new List<Book>();
                if (File.Exists(binPath))
                {
                    FileStream fs = new FileStream(binPath, FileMode.Open, FileAccess.Read);
                    BinaryFormatter bin = new BinaryFormatter();
                    while (fs.Position < fs.Length)
                    {
                        Book unUser = new Book();
                        unUser = (Book)bin.Deserialize(fs);
                        list.Add(unUser);
                    }
                    fs.Close();
                }
                return list;
            }
        }
    
}
