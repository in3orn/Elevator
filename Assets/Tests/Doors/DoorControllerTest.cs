using NUnit.Framework;

namespace Krk.Doors
{
    public class DoorControllerTest
    {
        DoorController controller;

        [SetUp]
        public void Init()
        {
            controller = new DoorController();
        }

        [Test]
        public void WhenInitializedShouldBeUnlockedAndClosed()
        {
            controller.Init();

            Assert.IsFalse(controller.State.locked);
            Assert.IsFalse(controller.State.open);
        }

        [Test]
        public void WhenReinitializedShouldBeUnlockedAndClosed()
        {
            controller.SetLocked(true);
            controller.Open();

            controller.Init();

            Assert.IsFalse(controller.State.locked);
            Assert.IsFalse(controller.State.open);
        }
    }
}