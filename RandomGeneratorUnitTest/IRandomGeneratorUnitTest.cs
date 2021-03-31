using DevelopmentStatusUpdater;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace RandomGeneratorUnitTest
{
    public class IRandomGeneratorUnitTest
    {
        [Fact]
        public void TestRandomGenerateAppNumber()
        {
            string[] appNames = new[] { "App-A", "App-B", "App-C", "App-D", "App-E" };
            double[] appRatio = new[] { 0.5, 0.2, 0.1, 0.1, 0.1 };

            IRandomGenerator randomGenerator = new EntryGenerator("whatever", appNames, appRatio);

            int total = 10000;
            int[] counts = new int[appNames.Length];
            for (int i = 0; i < total; i++)
            {
                int numOfApp = randomGenerator.RandomGenerateAppNumber(appNames.Length);
                counts[numOfApp - 1]++;
            }

            int totalRun = counts.Sum();

            Assert.True(totalRun == total);
            for (int i = 0; i < appNames.Length - 1; i++)
            {
                double a = (double)counts[i] / counts[i + 1];
                double b = 1 / EntryGenerator.APP_NUMBER_RATIO;
                Assert.True(Math.Abs(a - b) < 0.2);
            }
        }

        [Fact]
        public void TestRandomGenerateApplicationName()
        {
            string[] appNames = new[] { "App-A", "App-B", "App-C", "App-D", "App-E" };
            double[] appRatio = new[] { 0.5, 0.2, 0.1, 0.1, 0.1 };

            EntryGenerator randomGenerator = new EntryGenerator("whatever", appNames, appRatio);

            int total = 10000;
            Dictionary<string, int> counts = new Dictionary<string, int>();
            foreach (string name in appNames) counts[name] = 0;
            for (int i = 0; i < total; i++)
            {
                string name = randomGenerator.RandomGenerateApplicationName(appNames, randomGenerator.ApplicationMap);
                counts[name]++;
            }

            Assert.True(counts.Values.Sum() == total);

            for (int i = 0; i < appNames.Length - 1; i++)
            {
                double a = (double)counts[appNames[i]] / counts[appNames[i + 1]];
                double b = appRatio[i] / appRatio[i + 1];
                Assert.True(Math.Abs(a - b) < 0.2);
            }
        }
    }
}
