using System;
using System.Collections.Generic;
using System.Linq;
namespace BasicRobotMovement
{
    /// <summary>
    /// Simple Robot. It'll move Robot as well as keep track of all grids robot visited.
    /// </summary>
    public class Robot
    {
        

        //List that stored all grids robot visited
        private List<Square> _robotMovementTracker;
        /// <summary>
        /// Initial grid from where Robot starts moving. it has to read only because, client can't change initial position once robot starts moving
        /// </summary>
        public readonly Square InitialSquareOfRobot;
        /// <summary>
        /// Current grid on which robot is right now. it'll handle internally by class. client can't set current position of robot
        /// </summary>
        public Square CurrentSquareOfRobot { get; private set; }
        /// <summary>
        /// Robot's direction on which he's walking
        /// </summary>
        public Direction CurrentDirection  {   get;private set;  }
        /// <summary>
        /// Robot's previous direction, before current direction
        /// </summary>
        public Direction PreviousDirection {  get;private set;  }

        /// <summary>
        /// Fires whenever robot turns right
        /// </summary>
        public event EventHandler RightHandTurned;
        /// <summary>
        /// Parameter less constructor, in case client wants Robot to start default starting position. it is (0,0)
        /// </summary>
        public Robot()
        {
            _robotMovementTracker = new List<Square>();
            InitialSquareOfRobot = new Square();
            CurrentSquareOfRobot = new Square();
            CurrentDirection = Direction.None;
            PreviousDirection = Direction.None;
           
        }


     
        public void Move(string RobotMovementInput)
        {

            var directionAndSteps = RobotMovementInput.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if (directionAndSteps.All(ds => IsDirectionAndStepValid(ds.Trim())))
            {
                
                foreach (string ds in directionAndSteps)
                {
                    MoveInDrictionForNSteps(GetDirection(ds.Trim()[0]), int.Parse(ds.Trim().Substring(1)));
                }
            }
            else
            {
                throw new ArgumentException("Your input for Robot's movement is invalid.\n Please enter in <direction_letter><numberofsteps> format.");
            }
        }

        /// <summary>
        /// It will call when client wants to move Robot. It'll take particular direction along with number of steps in that direction
        /// </summary>
        /// <param name="RobotDirection">North, East, South, West</param>
        /// <param name="NumberOfSteps">number of steps in particular direction</param>
        private void MoveInDrictionForNSteps(Direction RobotDirection, int NumberOfSteps)
        {
            PreviousDirection = CurrentDirection;
            CurrentDirection = RobotDirection;

            switch (RobotDirection)
            {
                case Direction.North:
                    for (int step = 1; step <= NumberOfSteps; step++)
                    {
                        CoOrdinate bottomLeftForNorthMove = new CoOrdinate(CurrentSquareOfRobot.BottomLeft.X, CurrentSquareOfRobot.BottomLeft.Y + 1);
                        CoOrdinate topRightforNorthMove = new CoOrdinate(CurrentSquareOfRobot.TopRight.X, CurrentSquareOfRobot.TopRight.Y + 1);
                        CurrentSquareOfRobot = new Square(bottomLeftForNorthMove, topRightforNorthMove);
                        _robotMovementTracker.Add(CurrentSquareOfRobot);
                    }
                    break;
                case Direction.East:
                   
                    for (int step = 1; step <= NumberOfSteps; step++)
                    {
                        CoOrdinate bottomLeftForEastMove = new CoOrdinate(CurrentSquareOfRobot.BottomLeft.X + 1, CurrentSquareOfRobot.BottomLeft.Y);
                        CoOrdinate topRightForEastMove = new CoOrdinate(CurrentSquareOfRobot.TopRight.X + 1, CurrentSquareOfRobot.TopRight.Y);
                        CurrentSquareOfRobot = new Square(bottomLeftForEastMove, topRightForEastMove);
                        _robotMovementTracker.Add(CurrentSquareOfRobot);
                    }
                    break;
                case Direction.South:
                    for (int step = 1; step <= NumberOfSteps; step++)
                    {
                        CoOrdinate bottomLeftForSouthMove = new CoOrdinate(CurrentSquareOfRobot.BottomLeft.X, CurrentSquareOfRobot.BottomLeft.Y - 1);
                        CoOrdinate topRightForSouthMove = new CoOrdinate(CurrentSquareOfRobot.TopRight.X, CurrentSquareOfRobot.TopRight.Y - 1);
                        CurrentSquareOfRobot = new Square(bottomLeftForSouthMove, topRightForSouthMove);
                        _robotMovementTracker.Add(CurrentSquareOfRobot);
                    }
                    break;
                case Direction.West:
                    for (int step = 1; step <= NumberOfSteps; step++)
                    {
                        CoOrdinate bottomLeftForWestMove = new CoOrdinate(CurrentSquareOfRobot.BottomLeft.X - 1, CurrentSquareOfRobot.BottomLeft.Y);
                        CoOrdinate topRightForWestMove = new CoOrdinate(CurrentSquareOfRobot.TopRight.X - 1, CurrentSquareOfRobot.TopRight.Y);
                        CurrentSquareOfRobot = new Square(bottomLeftForWestMove, topRightForWestMove);
                        _robotMovementTracker.Add(CurrentSquareOfRobot);
                           
                    }
                    break;
                default:
                    break;
            }
            if (IsRobotTurnedRight())
            {
                OnRightHandTurned(EventArgs.Empty);
            }

            
        }

        /// <summary>
        /// Calculate how many unqiue squares robot visited.
        /// </summary>
        public int NumberOfUniqueSquaresRobotVisited
        {
            get
            {
                var numberOfUniqueSquares = _robotMovementTracker.Distinct();
                return numberOfUniqueSquares.Count();
            }
        }

        /// <summary>
        /// Calculate how many  squares robot visited.
        /// </summary>
        public int NumberOfSquaresRobotVisited
        {
            get
            {
                return _robotMovementTracker.Count;
            }
        }
        /// <summary>
        /// Call event handler for Right hand turn
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnRightHandTurned(EventArgs e)
        {
            EventHandler handler = RightHandTurned;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// check whether robot turns right
        /// </summary>
        /// <returns></returns>
        private bool IsRobotTurnedRight()
        {
            if (PreviousDirection != Direction.None)
            {
                return (PreviousDirection == Direction.North && CurrentDirection == Direction.East) ||
                    (PreviousDirection == Direction.South && CurrentDirection == Direction.West) ||
                    (PreviousDirection == Direction.East && CurrentDirection == Direction.South) ||
                    (PreviousDirection == Direction.West && CurrentDirection == Direction.North);
            }
            return false;
        }


        /// <summary>
        /// Hygine check for input entered by user. check whether each part of input [direction_character][number_of_step] format
        /// </summary>
        /// <param name="input">each part of input</param>
        /// <returns>true if format is [direction][number] like N4</returns>
        static bool IsDirectionAndStepValid(string input)
        {
            char[] validCharacters = { 'N', 'E', 'S', 'W' };

            if (!string.IsNullOrEmpty(input) && input.Length >= 2)
            {
                var directionChar = input[0];
                var isDirectionCharValid = validCharacters.Any(c => c.ToString().ToLower() == directionChar.ToString().ToLower());
                var isStepsValid = int.TryParse(input.Substring(1), out int steps);
                return isDirectionCharValid && isStepsValid && steps > 0;

            }
            return false;
        }
        /// <summary>
        /// Get Direction enum from a character of direction
        /// </summary>
        /// <param name="directionCharacter">N or S or E or W</param>
        /// <returns>North or South or East or West</returns>
        static Direction GetDirection(char directionCharacter)
        {
            switch (directionCharacter)
            {
                case 'N':
                case 'n':
                    return Direction.North;
                case 'S':
                case 's':
                    return Direction.South;
                case 'W':
                case 'w':
                    return Direction.West;
                case 'E':
                case 'e':
                    return Direction.East;
                default:
                    throw new ArgumentException("Direction character is not valid");
            }
        }

    }
}
