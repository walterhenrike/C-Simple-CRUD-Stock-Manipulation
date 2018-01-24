using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hi_Tech_Management_System.Business
{
    [Serializable]
    public class ItemType
    {
        private int typeId;
        private string typeName;

        public ItemType(int typeId, string typeName)
        {
            this.typeId = typeId;
            this.typeName = typeName;
        }

        public ItemType()
        {
            this.typeId = 0;
            this.typeName = "Default";
        }

        public int TypeId
        {
            get
            {
                return typeId;
            }

            set
            {
                typeId = value;
            }
        }

        public string TypeName
        {
            get
            {
                return typeName;
            }

            set
            {
                typeName = value;
            }
        }
    }
}
