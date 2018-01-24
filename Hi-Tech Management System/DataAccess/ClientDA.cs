using System.Collections.Generic;
using Hi_Tech_Management_System.Business;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Hi_Tech_Management_System.DataAccess
{
    class ClientDA
    {
            public static string binPath = @"..\..\Database\ClientBIN.ser";
            public static void WriteUser(List<Client> listOfClients)
            {
                FileStream fs = new FileStream(binPath, FileMode.Create, FileAccess.Write);
                BinaryFormatter bin = new BinaryFormatter();
                foreach (Client item in listOfClients)
                {
                    bin.Serialize(fs, item);
                }
                fs.Close();
            }

            public static List<Client> ReadUsers()
            {
                List<Client> list = new List<Client>();
                if (File.Exists(binPath))
                {
                    FileStream fs = new FileStream(binPath, FileMode.Open, FileAccess.Read);
                    BinaryFormatter bin = new BinaryFormatter();
                    while (fs.Position < fs.Length)
                    {
                        Client unUser = new Client();
                        unUser = (Client)bin.Deserialize(fs);
                        list.Add(unUser);
                    }
                    fs.Close();
                }
                return list;
            }
        }
    
}
