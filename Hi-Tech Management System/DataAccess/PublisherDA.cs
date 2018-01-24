using System.Collections.Generic;
using Hi_Tech_Management_System.Business;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Hi_Tech_Management_System.DataAccess
{
    public class PublisherDA
    {
        public static string binPath = @"..\..\Database\PublisherBIN.ser";
        public static void WriteUser(List<Publisher> listOfPublisher)
        {
            FileStream fs = new FileStream(binPath, FileMode.Create, FileAccess.Write);
            BinaryFormatter bin = new BinaryFormatter();
            foreach (Publisher item in listOfPublisher)
            {
                bin.Serialize(fs, item);
            }
            fs.Close();
        }

        public static List<Publisher> ReadUsers()
        {
            List<Publisher> list = new List<Publisher>();
            if (File.Exists(binPath))
            {
                FileStream fs = new FileStream(binPath, FileMode.Open, FileAccess.Read);
                BinaryFormatter bin = new BinaryFormatter();
                while (fs.Position < fs.Length)
                {
                    Publisher unUser = new Publisher();
                    unUser = (Publisher)bin.Deserialize(fs);
                    list.Add(unUser);
                }
                fs.Close();
            }
            return list;
        }
    }    
}
