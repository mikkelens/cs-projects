using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesOOP
{
    class Chair : IFurniture
    {
        private string _name;

        private List<ChairPart> _allParts;

        private ChairPart legs = new ChairPart(ChairPart.Type.Legs);
        private ChairPart seat = new ChairPart(ChairPart.Type.Seat);
        private ChairPart back = new ChairPart(ChairPart.Type.Back);

        public Chair(string name = "Unnamed chair", IPart.Material material = IPart.Material.Unset)
        {
            _name = name;
            _allParts = new List<ChairPart>()
            {
                legs,
                seat,
                back
            };
            SetAllMaterials(material);
        }

        public void SetAllMaterials(IPart.Material material)
        {
            foreach (ChairPart part in _allParts)
            {
                part.SetMaterial(material);   
            }
        }

        public List<IPart.Material> GetAllMaterials()
        {
            List<IPart.Material> materials = new List<IPart.Material>();
            foreach (ChairPart part in _allParts)
            {
                materials.Add(part.GetMaterial());   
            }

            return materials;
        }

        public string GetPartNames()
        {
            string partNames = "";
            for (int i = 0; i < _allParts.Count; i++)
            {
                ChairPart part = _allParts[i];
                if (i == 0) // no space
                {
                    partNames += part.GetPartType();
                }
                else if (i == _allParts.Count - 1) // " and "
                {
                    partNames += ", and " + part.GetPartType();
                }
                else // with space
                {
                    partNames += ", " + part.GetPartType();
                }
            }

            return partNames;
        }
    }
}