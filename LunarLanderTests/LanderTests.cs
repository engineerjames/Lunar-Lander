namespace LunarLanderTests
{
    using LunarLanderGame;
    using LunarLanderGame.Components;
    using LunarLanderGame.Logging;

    using Microsoft.Xna.Framework;


    [TestClass]
    public class LanderTests
    {
        Lander lander;

        static float TOLERANCE = 1e-4f;

        [TestInitialize]
        public void Initialize()
        {
            Game1 game = new Game1();
            ILogger logger = new ConsoleLogger();
            lander = new Lander(game, null, Vector2.Zero, logger);
        }

        [TestMethod]
        public void ThrustCalculatedCorrectly_FacingRight()
        {
            lander.SetThrustMagnitudeInNewtons(1.0f);
            lander.SetRotation(90.0f);
            Vector2 calculatedThrust = lander.CalculateVectorizedThrust();

            Assert.AreEqual(0.0f, calculatedThrust.Y, TOLERANCE);
            Assert.AreEqual(1.0f, calculatedThrust.X, TOLERANCE);
        }

        [TestMethod]
        public void ThrustCalculatedCorrectly_FacingRight_Plus_360()
        {
            lander.SetThrustMagnitudeInNewtons(1.0f);
            lander.SetRotation(90.0f + 360.0f);
            Vector2 calculatedThrust = lander.CalculateVectorizedThrust();

            Assert.AreEqual(0.0f, calculatedThrust.Y, TOLERANCE);
            Assert.AreEqual(1.0f, calculatedThrust.X, TOLERANCE);
        }

        [TestMethod]
        public void ThrustCalculatedCorrectly_FacingRight_Minus_360()
        {
            lander.SetThrustMagnitudeInNewtons(1.0f);
            lander.SetRotation(90.0f - 360.0f);
            Vector2 calculatedThrust = lander.CalculateVectorizedThrust();

            Assert.AreEqual(0.0f, calculatedThrust.Y, TOLERANCE);
            Assert.AreEqual(1.0f, calculatedThrust.X, TOLERANCE);
        }

        [TestMethod]
        public void ThrustCalculatedCorrectly_FacingLeft()
        {
            lander.SetThrustMagnitudeInNewtons(1.0f);
            lander.SetRotation(270.0f);
            Vector2 calculatedThrust = lander.CalculateVectorizedThrust();

            Assert.AreEqual(0.0f, calculatedThrust.Y, TOLERANCE);
            Assert.AreEqual(-1.0f, calculatedThrust.X, TOLERANCE);
        }

        [TestMethod]
        public void ThrustCalculatedCorrectly_FacingLeft_Plus_360()
        {
            lander.SetThrustMagnitudeInNewtons(1.0f);
            lander.SetRotation(270.0f + 360.0f);
            Vector2 calculatedThrust = lander.CalculateVectorizedThrust();

            Assert.AreEqual(0.0f, calculatedThrust.Y, TOLERANCE);
            Assert.AreEqual(-1.0f, calculatedThrust.X, TOLERANCE);
        }

        [TestMethod]
        public void ThrustCalculatedCorrectly_FacingLeft_Minus_360()
        {
            lander.SetThrustMagnitudeInNewtons(1.0f);
            lander.SetRotation(270.0f - 360.0f);
            Vector2 calculatedThrust = lander.CalculateVectorizedThrust();

            Assert.AreEqual(0.0f, calculatedThrust.Y, TOLERANCE);
            Assert.AreEqual(-1.0f, calculatedThrust.X, TOLERANCE);
        }

        [TestMethod]
        public void ThrustCalculatedCorrectly_FacingUp()
        {
            lander.SetThrustMagnitudeInNewtons(1.0f);
            lander.SetRotation(0.0f);
            Vector2 calculatedThrust = lander.CalculateVectorizedThrust();

            Assert.AreEqual(-1.0f, calculatedThrust.Y, TOLERANCE);
            Assert.AreEqual(0.0f, calculatedThrust.X, TOLERANCE);
        }

        [TestMethod]
        public void ThrustCalculatedCorrectly_FacingUp_Plus_360()
        {
            lander.SetThrustMagnitudeInNewtons(1.0f);
            lander.SetRotation(0.0f + 360.0f);
            Vector2 calculatedThrust = lander.CalculateVectorizedThrust();

            Assert.AreEqual(-1.0f, calculatedThrust.Y, TOLERANCE);
            Assert.AreEqual(0.0f, calculatedThrust.X, TOLERANCE);
        }

        [TestMethod]
        public void ThrustCalculatedCorrectly_FacingUp_Minus_360()
        {
            lander.SetThrustMagnitudeInNewtons(1.0f);
            lander.SetRotation(0.0f - 360.0f);
            Vector2 calculatedThrust = lander.CalculateVectorizedThrust();

            Assert.AreEqual(-1.0f, calculatedThrust.Y, TOLERANCE);
            Assert.AreEqual(0.0f, calculatedThrust.X, TOLERANCE);
        }

        [TestMethod]
        public void ThrustCalculatedCorrectly_FacingDown()
        {
            lander.SetThrustMagnitudeInNewtons(1.0f);
            lander.SetRotation(180.0f);
            Vector2 calculatedThrust = lander.CalculateVectorizedThrust();

            Assert.AreEqual(1.0f, calculatedThrust.Y, TOLERANCE);
            Assert.AreEqual(0.0f, calculatedThrust.X, TOLERANCE);
        }

        [TestMethod]
        public void ThrustCalculatedCorrectly_FacingDown_Plus_360()
        {
            lander.SetThrustMagnitudeInNewtons(1.0f);
            lander.SetRotation(180.0f + 360.0f);
            Vector2 calculatedThrust = lander.CalculateVectorizedThrust();

            Assert.AreEqual(1.0f, calculatedThrust.Y, TOLERANCE);
            Assert.AreEqual(0.0f, calculatedThrust.X, TOLERANCE);
        }

        [TestMethod]
        public void ThrustCalculatedCorrectly_FacingDown_Minus_360()
        {
            lander.SetThrustMagnitudeInNewtons(1.0f);
            lander.SetRotation(180.0f - 360.0f);
            Vector2 calculatedThrust = lander.CalculateVectorizedThrust();

            Assert.AreEqual(1.0f, calculatedThrust.Y, TOLERANCE);
            Assert.AreEqual(0.0f, calculatedThrust.X, TOLERANCE);
        }

        [TestMethod]
        public void ThrustCalculatedCorrectly_45_Degrees()
        {
            lander.SetThrustMagnitudeInNewtons(1.0f);
            lander.SetRotation(45.0f);
            Vector2 calculatedThrust = lander.CalculateVectorizedThrust();

            Assert.AreEqual(-Math.Sqrt(2.0) / 2.0f, calculatedThrust.Y, TOLERANCE);
            Assert.AreEqual(Math.Sqrt(2.0) / 2.0f, calculatedThrust.X, TOLERANCE);
        }

        [TestMethod]
        public void ThrustCalculatedCorrectly_45_Degrees_Plus_360()
        {
            lander.SetThrustMagnitudeInNewtons(1.0f);
            lander.SetRotation(45.0f + 360.0f);
            Vector2 calculatedThrust = lander.CalculateVectorizedThrust();

            Assert.AreEqual(-Math.Sqrt(2.0) / 2.0f, calculatedThrust.Y, TOLERANCE);
            Assert.AreEqual(Math.Sqrt(2.0) / 2.0f, calculatedThrust.X, TOLERANCE);
        }

        [TestMethod]
        public void ThrustCalculatedCorrectly_45_Degrees_Minus_360()
        {
            lander.SetThrustMagnitudeInNewtons(1.0f);
            lander.SetRotation(45.0f - 360.0f);
            Vector2 calculatedThrust = lander.CalculateVectorizedThrust();

            Assert.AreEqual(-Math.Sqrt(2.0) / 2.0f, calculatedThrust.Y, TOLERANCE);
            Assert.AreEqual(Math.Sqrt(2.0) / 2.0f, calculatedThrust.X, TOLERANCE);
        }
    }
}