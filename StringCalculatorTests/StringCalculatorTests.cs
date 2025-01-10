using StringCalculatorNS;

namespace StringCalculatorTests
{
    [TestClass]
    public class StringCalculatorTests
    {
        StringCalculator Calculator = new StringCalculator();

        [TestMethod]
        public void Add_EmptyString_ZeroReturned()
        {
            int result = Calculator.Add("");

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Add_SingleValue_ValueReturned()
        {
            int result = Calculator.Add("1");

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Add_SimpleCommaSeparatedSequence_SumReturned()
        {
            int result = Calculator.Add("1,2");

            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void Add_LongCommaSeparatedSequence_SumReturned()
        {
            int result = Calculator.Add("43,7,2,9,12,11,23,101");

            Assert.AreEqual(208, result);
        }

        [TestMethod]
        public void Add_WithCommaAndNewlineDelimiters_SumReturned()
        {
            int result = Calculator.Add("1\n2,3");

            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void Add_DelimitersWithNoValueBetween_ZeroReturned()
        {
            int result = Calculator.Add("1,\n");

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Add_DelimiterListSupplied_SumReturned()
        {
            int result = Calculator.Add("//;\n1;2");

            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void Add_DelimitersCaseInsensitve_SumReturned()
        {
            int result = Calculator.Add("//abcd\n1a2A3b4B5c6C7d8D9");

            Assert.AreEqual(45, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_NegativeNumbersThrowException_DoesNotReturn()
        {
            int result = Calculator.Add("1,2,-3,4,-5,-6");

            // Assert - Expects exception
        }

        [TestMethod]
        public void Add_NumberTooLarge_SumReturnedLargeNumberIgnored()
        {
            int result = Calculator.Add("2,1001,13");

            Assert.AreEqual(15, result);
        }

        [TestMethod]
        public void Add_NumberEqualToMaximum_SumReturned()
        {
            int result = Calculator.Add("2,1000,13");

            Assert.AreEqual(1015, result);
        }

        [TestMethod]
        public void Add_SeriesIncludesZero_SumReturned()
        {
            int result = Calculator.Add("2,0,13");

            Assert.AreEqual(15, result);
        }

        [TestMethod]
        public void Add_SeriesPassInMultipleDelimiters_SumReturned()
        {
            int result = Calculator.Add("//*%\n1*2%3");

            Assert.AreEqual(6, result);
        }
    }
}