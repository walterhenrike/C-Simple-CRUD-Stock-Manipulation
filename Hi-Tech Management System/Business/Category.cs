using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hi_Tech_Management_System.Business
{
    [Serializable]
    public class Category
    {
        private int catID;
        private string catName;

        public Category(int catID, string catName)
        {
            this.catID = catID;
            this.catName = catName;
        }

        public Category()
        {
            this.catID = 0;
            this.catName = "Default";
        }

        public int CatID
        {
            get
            {
                return catID;
            }

            set
            {
                catID = value;
            }
        }

        public string CatName
        {
            get
            {
                return catName;
            }

            set
            {
                catName = value;
            }
        }
    }
}
