using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesOOP
{
    interface IFurniture
    {
        void SetAllMaterials(IPart.Material newMaterial);
        List<IPart.Material> GetAllMaterials();
    }

}
