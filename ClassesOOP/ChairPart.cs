using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesOOP
{
    class ChairPart : IPart
    {
        // data

        private IPart.Material _material;

        public enum Type { Legs, Seat, Back }
        private Type _type;

        public ChairPart(Type type, IPart.Material material = IPart.Material.Unset)
        {
            _type = type;
            _material = material;
        }

        // SET/GET PART
        public void SetPartType(Type newType)
        {
            _type = newType;
        }
        public Type GetPartType()
        {
            return _type;
        }

        // SET/GET MATERIAL
        public void SetMaterial(IPart.Material newMaterial)
        {
            _material = newMaterial;
        }
        public IPart.Material GetMaterial()
        {
            return _material;
        }
    }
}
