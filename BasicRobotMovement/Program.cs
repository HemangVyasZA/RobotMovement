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
                WriteLine("If you want multiple movement enter your multiple instruction separate by comma");
                WriteLine("For example N4, S3, W5, E1. This is valid input");

                var readInput = ReadLine();

                Robot robot = new Robot();
                NumberOfRightTurns = 0;
                robot.RightHandTurned += Robot_RightHandTurned;
                try
                {
                    robot.Move(readInput);
                    WriteLine($"Total number of unique block Robot Visited: {robot.NumberOfUniqueSquaresRobotVisited}");
                    WriteLine($"Total number of  times robot made right turn: {NumberOfRightTurns} ");
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

    }
}
