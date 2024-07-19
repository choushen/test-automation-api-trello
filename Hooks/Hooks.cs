using NUnit.Framework;

namespace RestSharpProject.Hooks
{
    [SetUpFixture]
    public class Hooks 
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            // Global setup logic
        }

        [SetUp]
        public void Setup()
        {
            TestContext.WriteLine("Setup");
        }

        [TearDown]
        public void TearDown()
        {
            TestContext.WriteLine("TearDown");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            // Global teardown logic
        }
    }
}
