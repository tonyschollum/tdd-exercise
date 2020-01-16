using System;
using Xunit;

namespace Tests
{
    public class StringCalculatorTests
    {
        [Fact]
        public void Can_Add_Two_Numbers()
        {
            const string numbers = "1,2";

            var calculator = new Tdd.Calculator.Calculator();

            var result = calculator.AddNumbersFromString(numbers);

            Assert.Equal(3, result);
        }

        [Fact]
        public void When_No_Numbers_Return_Value_As_0()
        {
            const string numbers = "";

            var calculator = new Tdd.Calculator.Calculator();

            var result = calculator.AddNumbersFromString(numbers);

            Assert.Equal(0, result);
        }

        [Fact]
        public void Can_Add_Single_Number()
        {
            const string numbers = "1";

            var calculator = new Tdd.Calculator.Calculator();

            var result = calculator.AddNumbersFromString(numbers);

            Assert.Equal(1, result);
        }

        //[Fact]
        //public void Cannot_Add_Three_Numbers()
        //{
        //    const string numbers = "1,2,3";
        //    var calculator = new Tdd.Calculator.Calculator();

        //    var exception = Record.Exception(() => calculator.AddNumbersFromString(numbers));
            
        //    Assert.NotNull(exception);
        //    Assert.IsType<ArgumentException>(exception);
        //}

        [Fact]
        public void Can_Add_Unknown_Number_Of_Numbers()
        {
            const string numbers = "1,2,3,4,5";

            var calculator = new Tdd.Calculator.Calculator();

            var result = calculator.AddNumbersFromString(numbers);

            Assert.Equal(1+2+3+4+5, result);
        }

        [Fact]
        public void Can_Add_Numbers_Separated_By_Line_Breaks()
        {
            var numbers = "1\n2\n3";

            var calculator = new Tdd.Calculator.Calculator();

            var result = calculator.AddNumbersFromString(numbers);

            Assert.Equal(6, result);
        }

        [Fact]
        public void Can_Add_Numbers_Separated_By_Commas_And_Line_Breaks()
        {
            var numbers = "1,2\n3";

            var calculator = new Tdd.Calculator.Calculator();

            var result = calculator.AddNumbersFromString(numbers);

            Assert.Equal(6, result);
        }

        [Fact]
        public void Can_First_Line_Define_Additional_Delimiter()
        {
            var numbers = "//[;]\n1;2";

            var calculator = new Tdd.Calculator.Calculator();

            var result = calculator.AddNumbersFromString(numbers);

            Assert.Equal(3, result);
        }

        [Fact]
        public void Can_Add_Numbers_Separated_By_Any_Delimiter()
        {
            var numbers = "//[;]\n1;2,3\n4";

            var calculator = new Tdd.Calculator.Calculator();

            var result = calculator.AddNumbersFromString(numbers);

            Assert.Equal(10, result);
        }

        [Fact]
        public void Expection_Thrown_When_A_Negative_Number_Is_Supplied()
        {
            const string numbers = "-1";

            var calculator = new Tdd.Calculator.Calculator();

            var exception = Record.Exception(() => calculator.AddNumbersFromString(numbers));

            Assert.NotNull(exception);
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public void Exception_Message_Contains_NegativesNotAllowedMessage_When_A_Negative_Number_Is_Supplied()
        {
            const string numbers = "-1";

            var calculator = new Tdd.Calculator.Calculator();

            var exception = Record.Exception(() => calculator.AddNumbersFromString(numbers));
            
            Assert.Contains("negatives not allowed", exception.Message, StringComparison.CurrentCultureIgnoreCase);
        }

        [Fact]
        public void Exception_Message_Contains_Negative_Number_When_A_Single_Negative_Number_Is_Supplied()
        {
            const string numbers = "-1";

            var calculator = new Tdd.Calculator.Calculator();

            var exception = Record.Exception(() => calculator.AddNumbersFromString(numbers));

            Assert.Contains("-1", exception.Message);
        }

        [Fact]
        public void Exception_Message_Contains_All_Negative_Numbers_When_Negative_Numbers_Are_Supplied()
        {
            const string numbers = "-1,2,-3";

            var calculator = new Tdd.Calculator.Calculator();

            var exception = Record.Exception(() => calculator.AddNumbersFromString(numbers));

            Assert.Contains("-1", exception.Message);
            Assert.Contains("-3", exception.Message);
        }

        [Fact]
        public void Numbers_Greater_Than_1000_Are_Ignored()
        {
            const string numbers = "1,1000,10001";

            var calculator = new Tdd.Calculator.Calculator();

            var result = calculator.AddNumbersFromString(numbers);

            Assert.Equal(1001, result);
        }

        [Fact]
        public void Delimiters_Can_Be_Any_Length()
        {
            const string numbers = "//[--]\n1--2--3";

            var calculator = new Tdd.Calculator.Calculator();

            var result = calculator.AddNumbersFromString(numbers);

            Assert.Equal(6, result);
        }

        [Fact]
        public void Can_First_Line_Define_Multiple_Additional_Delimiters()
        {
            var numbers = "//[-][%]\n1-2%3";

            var calculator = new Tdd.Calculator.Calculator();

            var result = calculator.AddNumbersFromString(numbers);

            Assert.Equal(6, result);
        }

        [Fact]
        public void Can_First_Line_Define_Multiple_Additional_Delimiters_Of_Any_Length()
        {
            var numbers = "//[--][-][%]\n1-2%3--3";

            var calculator = new Tdd.Calculator.Calculator();

            var result = calculator.AddNumbersFromString(numbers);

            Assert.Equal(9, result);
        }

        [Fact]
        public void Expection_Thrown_When_A_Value_Is_Not_A_Number()
        {
            const string numbers = "1,3,abc";

            var calculator = new Tdd.Calculator.Calculator();

            var exception = Record.Exception(() => calculator.AddNumbersFromString(numbers));

            Assert.NotNull(exception);
            Assert.IsType<FormatException>(exception);
        }
    }
}
