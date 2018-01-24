using System.Collections.Generic;
using Hi_Tech_Management_System.Business;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Hi_Tech_Management_System.DataAccess
{
    class AuthorDA : ItemInterface
    {
            public static string binPath = @"..\..\Database\AuthorBIN.ser";
            public static void WriteUser(List<Author> listOfAuthors)
            {
                FileStream fs = new FileStream(binPath, FileMode.Create, FileAccess.Write);
                BinaryFormatter bin = new BinaryFormatter();
                foreach (Author item in listOfAuthors)
                {
                    bin.Serialize(fs, item);
                }
                fs.Close();
            }

            public List<Author> ReadUsers()
            {
                List<Author> list = new List<Author>();
                if (File.Exists(binPath))
                {
                    FileStream fs = new FileStream(binPath, FileMode.Open, FileAccess.Read);
                    BinaryFormatter bin = new BinaryFormatter();
                    while (fs.Position < fs.Length)
                    {
                        Author unUser = new Author();
                        unUser = (Author)bin.Deserialize(fs);
                        list.Add(unUser);
                    }
                    fs.Close();
                }
                return list;
            }


        }    
}
