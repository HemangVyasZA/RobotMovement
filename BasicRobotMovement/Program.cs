using System;
using System.Linq;
using static System.Console;
namespace BasicRobotMovement
{
    class Program
    {
        private static int NumberOfRightTurns = 0;
        static void Main(string[] args)
        {
            do
            {
                Clear();
                WriteLine("Input Robot's movement in <direction_letter><numberofsteps> format.");
                WriteLine("direction_letter must be N=North, S=South, E=East, W=West.");
                WriteLine("If you want multiple movement enter your multiple instruction seperate by comma");
                WriteLine("For example N4, S3, W5, E1. This is valid input");

                var readInput = ReadLine();

                Robot robot = new Robot();
                NumberOfRightTurns = 0;
                robot.RightHandTurned += Robot_RightHandTurned;
                try
                {
                    robot.Move(readInput);
                    WriteLine($"Total number of unique block Robot Visited: {robot.NumberOfUniqueSquaresRobotVisited}");
                    WriteLine($"Total numer of  times robot made right turn: {NumberOfRightTurns} ");
                }
                catch (Exception ex)
                {

                    WriteLine($"Something went wrong. Exception message {ex.Message}");
                }
               
                WriteLine("Press any key to continue. Press <escape> to quit");
            } while (ReadKey().Key != ConsoleKey.Escape);


        }

        private static void Robot_RightHandTurned(object sender, EventArgs e)
        {
            ++NumberOfRightTurns;
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
;