using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicRobotMovement
{
    /// <summary>
    /// This class represent Co-Ordinate. This'll useful to calculate Bottom Left point and Top Right point of a square.
    /// </summary>
   public  class CoOrdinate : IEquatable<CoOrdinate>
    {
        public int X
        {
            get; set;
        }
        public int Y
        {
            get; set;
        }
        public CoOrdinate()
        {
            X = 0;
            Y = 0;
        }
        public CoOrdinate(int x, int y)
        {
            X = x;
            Y = y;
        }
        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public bool Equals(CoOrdinate other)
        {
            //Check whether the compared object is null. 
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data. 
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal. 
            return X.Equals(other.X) && Y.Equals(other.Y);

        }
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }
    }
  
}
