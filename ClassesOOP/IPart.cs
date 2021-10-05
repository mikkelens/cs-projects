using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesOOP
{
    interface IPart
    {
        public enum Material
        {
            Unset,
            Wood,
            Metal,
            Concrete,
            Brick,
            Cloth
        }

        void SetMaterial(Material newMaterial);
        Material GetMaterial();
    }

}