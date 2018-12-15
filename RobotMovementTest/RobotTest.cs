using System;
using BasicRobotMovement;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RobotMovementTest
{
    /// <summary>
    /// This class test core features of Robot class which are Counting number of steps after Robot moves in number of sequences
    /// It also test total right hand turns robot makes at the end of the sequence.
    /// </summary>
    [TestClass]
    public class RobotTest
    {
        int totalNumbersOfRightTurn = 0;

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Throw_ArgumentException_If_User_Enter_Invalid_Robot_Movement_Input()
        {
            //Arrange
            string inputString = "N4,edhd2,E2,S2";//invalid input
            var robot = new Robot();

            //Act
            robot.Move(inputString);

            //assert handle by ExpectedException.

        }

        [TestMethod]
        public void Total_Number_Of_Block_Must_Be_12_After_Robot_Moves_In_N4_E2_S2_W4_Sequence()
        {
            //Arrange
            string stepsInSequence = "N4, E2, S2, W4";
            var robot = new Robot();
            //Act
            robot.Move(stepsInSequence);

            Assert.AreEqual(12, robot.NumberOfSquaresRobotVisited);
        }

        [TestMethod]
        public void Total_Number_Of_Unique_Block_Must_Be_11_After_Robot_Moves_In_N4_E2_S2_W4_Sequence()
        {
            //Arrange
            string stepsInSequence = "N4,E2,S2,W4";
            var robot = new Robot();
            //Act
            robot.Move(stepsInSequence);

            Assert.AreEqual(11, robot.NumberOfUniqueSquaresRobotVisited);
        }

        [TestMethod]
        public void Total_Number_Of_Right_Hand_Turns_Must_Be_3_After_Robot_Moves_In_N4_E2_S2_W4_Sequence()
        {
            //Arrange
            string stepsInSequence = "N4,E2,S2,W4";
            var robot = new Robot();
            robot.RightHandTurned += Robot_RightHandTurned;
            //Act
            robot.Move(stepsInSequence);

            Assert.AreEqual(3, totalNumbersOfRightTurn);
        }

        private void Robot_RightHandTurned(object sender, EventArgs e)
        {
            totalNumbersOfRightTurn++;
        }

        //some extra Tests

        [TestMethod]
        public void Total_Number_Of_Block_Must_Be_24_After_Robot_Moves_In_E4_N1_W1_S2_E1_S1_W2_N4_W1_S3_W3_S1_Sequence()
        {
            //Arrange
            string stepsInSequence = "E4,n1,w1,s2,e1,s1,w2,n4,w1,s3,w3,s1";
            var robot = new Robot();
            //Act
            robot.Move(stepsInSequence);

            Assert.AreEqual(24, robot.NumberOfSquaresRobotVisited);
        }

        [TestMethod]
        public void Total_Number_Of_Unique_Block_Must_Be_21_After_Robot_Moves_In_E4_N1_W1_S2_E1_S1_W2_N4_W1_S3_W3_S1_Sequence()
        {
            //Arrange
            string stepsInSequence = "E4,n1,w1,s2,e1,s1,w2,n4,w1,s3,w3,s1";
            var robot = new Robot();
            //Act
            robot.Move(stepsInSequence);

            Assert.AreEqual(21, robot.NumberOfUniqueSquaresRobotVisited);
        }
        [TestMethod]
        public void Total_Number_Of_Right_Hand_Turns_Must_Be_4_After_Robot_Moves_In_E4_N1_W1_S2_E1_S1_W2_N4_W1_S3_W3_S1_Sequence()
        {
            //Arrange
            string stepsInSequence = "E4,n1,w1,s2,e1,s1,w2,n4,w1,s3,w3,s1";
            var robot = new Robot();
            robot.RightHandTurned += Robot_RightHandTurned;
            //Act
            robot.Move(stepsInSequence);

            Assert.AreEqual(4, totalNumbersOfRightTurn);
        }
    }
}
