using System;
using System.IO;
using CsvHelper;
using CSVHelperRun;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSVHelperRunTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [DeploymentItem("Files", "Files")]
        public void TestMethod1()
        {
            using (var sr = new StreamReader(@"Files\csvwitherrors.csv"))
            {
                var processor = new CSVProcessor<CSVRecord>();
                processor.Validate(sr);
            }
        }
    }
}
