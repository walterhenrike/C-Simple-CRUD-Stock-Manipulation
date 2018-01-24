using System.Collections.Generic;
using Hi_Tech_Management_System.Business;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Hi_Tech_Management_System.DataAccess
{
    class EmployeeDA
    {
            public static string binPath = @"..\..\Database\EmployeeBIN.ser";
            public static void WriteUser(List<Employee> listOfUsers)
            {
                FileStream fs = new FileStream(binPath, FileMode.Create, FileAccess.Write);
                BinaryFormatter bin = new BinaryFormatter();
                foreach (Employee item in listOfUsers)
                {
                    bin.Serialize(fs, item);
                }
                fs.Close();
            }

            public static List<Employee> ReadUsers()
            {
                List<Employee> list = new List<Employee>();
                if (File.Exists(binPath))
                {
                    FileStream fs = new FileStream(binPath, FileMode.Open, FileAccess.Read);
                    BinaryFormatter bin = new BinaryFormatter();
                    while (fs.Position < fs.Length)
                    {
                        Employee unUser = new Employee();
                        unUser = (Employee)bin.Deserialize(fs);
                        list.Add(unUser);
                    }
                    fs.Close();
                }
                return list;
            }
        }
    
}
