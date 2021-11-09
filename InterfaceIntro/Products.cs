using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceIntro
{
    class Watermelon : IProduct { public float GetPrice() { return 18.5f; } }

    
    class Toothbrush : IProduct
    {
        public float GetPrice()
        {
            return 12.2f;
        }

        public void Brush(IBrushable brushable)
        {
            // do something
        }
    }
    interface IBrushable
    {

    }

    class Human : IBrushable
    {

    }


}
