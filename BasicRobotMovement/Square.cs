using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicRobotMovement
{
    public enum Direction
    {
        
        North,
        East,
        South,
        West,
        None
    }
    /// <summary>
    /// This class represents square or grid on the floor. When robot moves he'll visit squares in particular direction.
    /// </summary>
    public class Square : IEquatable<Square>
    {
        public CoOrdinate BottomLeft
        {
            get; set;
        }
        public CoOrdinate TopRight
        {
            get; set;
        }
        public Square()
        {
            BottomLeft = new CoOrdinate(0, 0);
            TopRight = new CoOrdinate(1, 1);
        }
        public Square(CoOrdinate bottomLeft, CoOrdinate topRight)
        {
            BottomLeft = bottomLeft;
            TopRight = topRight;
        }
        public override string ToString()
        {
            return $"BottomLeft: {BottomLeft.ToString()}, TopRight: {TopRight.ToString()}";
        }

        public bool Equals(Square other)
        {
            //Check whether the compared object is null. 
            if (ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data. 
            if (ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal. 
            return BottomLeft.Equals(other.BottomLeft) && TopRight.Equals(other.TopRight);

        }
        public override int GetHashCode()
        {
            return BottomLeft.GetHashCode() ^ TopRight.GetHashCode();
        }
    }

 


}
