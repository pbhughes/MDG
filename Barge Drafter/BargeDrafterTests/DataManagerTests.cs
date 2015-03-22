using MDG.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BargeDrafterTests
{
    
    
    /// <summary>
    ///This is a test class for DataManagerTests and is intended
    ///to contain all DataManagerTests Unit Tests
    ///</summary>
    [TestClass()]
    public class DataManagerTests
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for CalculateStroke
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MDG.BargeModel.dll")]
        public void GenerateXMLFunctionTest()
        {
            BargeModel_Accessor target = new BargeModel_Accessor(); 
            double measuredDepth = 2.83F; 
            double angle = 1.820F; 
            double expected =2.8    ; 
            double length = 0;
            double actual;
            actual = target.CalculateStroke(measuredDepth, angle, length);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CalculateStroke
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MDG.BargeModel.dll")]
        public void GenerateXMLFunctionTest1()
        {
            BargeModel_Accessor target = new BargeModel_Accessor();
            double measuredDepth = 30F;
            double angle = 5F;
            double expected = 30.04F;
            double length = 2400.0F;
            double actual;
            actual = target.CalculateStroke(measuredDepth, angle, length);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
