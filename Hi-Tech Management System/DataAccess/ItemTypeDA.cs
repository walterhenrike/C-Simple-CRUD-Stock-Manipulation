using System.Collections.Generic;
using Hi_Tech_Management_System.Business;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Hi_Tech_Management_System.DataAccess
{
    class ItemTypeDA
    {
            public static string binPath = @"..\..\Database\ItemTypeDA.ser";
            public static void WriteUser(List<ItemType> listOfUsers)
            {
                FileStream fs = new FileStream(binPath, FileMode.Create, FileAccess.Write);
                BinaryFormatter bin = new BinaryFormatter();
                foreach (ItemType item in listOfUsers)
                {
                    bin.Serialize(fs, item);
                }
                fs.Close();
            }

            public static List<ItemType> ReadUsers()
            {
                List<ItemType> list = new List<ItemType>();
                if (File.Exists(binPath))
                {
                    FileStream fs = new FileStream(binPath, FileMode.Open, FileAccess.Read);
                    BinaryFormatter bin = new BinaryFormatter();
                    while (fs.Position < fs.Length)
                    {
                        ItemType unUser = new ItemType();
                        unUser = (ItemType)bin.Deserialize(fs);
                        list.Add(unUser);
                    }
                    fs.Close();
                }
                return list;
            }
        }
    
}
