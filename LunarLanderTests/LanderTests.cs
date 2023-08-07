namespace LunarLanderTests
{
    using LunarLanderGame;
    using LunarLanderGame.Components;

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
            lander = new Lander(game, null, Vector2.Zero, null);
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
        public void ThrustCalculatedCorrectly_FacingLeft()
        {
            lander.SetThrustMagnitudeInNewtons(1.0f);
            lander.SetRotation(270.0f);
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
        public void ThrustCalculatedCorrectly_FacingDown()
        {
            lander.SetThrustMagnitudeInNewtons(1.0f);
            lander.SetRotation(180.0f);
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
    }
}