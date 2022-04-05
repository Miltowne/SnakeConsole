using Microsoft.VisualStudio.TestTools.UnitTesting;
using Snake;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Tests
{
    [TestClass()]
    public class GameWorldTests
    {
        GameWorld world = new GameWorld();
        [TestMethod()]
        public void GameOverTest()
        {
            Assert.IsTrue(world.GameOver()); // testar om gameover skickar true (skickar false om det är gameover) utan att ha spelat spelet
        }

        [TestMethod()]
        public void CreateObjectsTest() // testar om vi kan välja om Snake/Ai ska skapas med hjälp av int 0 lr int 1
        {
            world.CreateObjects(0);
            Assert.IsNotNull(world.Snake);
            world.CreateObjects(1);
            Assert.IsNotNull(world.Ai);
        }

        [TestMethod()]
        public void CreateTailTest()
        {
            world.CreateObjects(0);
            world.CreateTail();
            Assert.AreEqual(1,world.Snake.Tail.Count); // testar om Tail skapas till List<Tail> om vi kör CreateTail() metoden
        }

        [TestMethod()]
        public void UpdateTest()
        {
            world.CreateObjects(0);
            world.CreateFood();
            Assert.IsTrue(world.Collection.Contains(world.Food)); // kollar om food finns med i listan efter det skapats
        }
    }
}