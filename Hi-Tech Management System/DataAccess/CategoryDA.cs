using System.Collections.Generic;
using Hi_Tech_Management_System.Business;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Hi_Tech_Management_System.DataAccess
{
    class CategoryDA
    {
            public static string binPath = @"..\..\Database\CategoryBIN.ser";
            public static void WriteUser(List<Category> listOfCategory)
            {
                FileStream fs = new FileStream(binPath, FileMode.Create, FileAccess.Write);
                BinaryFormatter bin = new BinaryFormatter();
                foreach (Category item in listOfCategory)
                {
                    bin.Serialize(fs, item);
                }
                fs.Close();
            }

            public static List<Category> ReadUsers()
            {
                List<Category> list = new List<Category>();
                if (File.Exists(binPath))
                {
                    FileStream fs = new FileStream(binPath, FileMode.Open, FileAccess.Read);
                    BinaryFormatter bin = new BinaryFormatter();
                    while (fs.Position < fs.Length)
                    {
                        Category unUser = new Category();
                        unUser = (Category)bin.Deserialize(fs);
                        list.Add(unUser);
                    }
                    fs.Close();
                }
                return list;
            }
        }
    
}
