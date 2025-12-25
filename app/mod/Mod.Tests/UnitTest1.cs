
using mod;

namespace Mod.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void EndSum_Correct()
        {
            var program = new Program();
            program.money = new Program.Money
            {
                age = 5,
                procent = 12,
                beginSum = 100000,
                type = 1
            };
            double resProgr = program.CalculateEndSum();
            double resTest = 100000 * Math.Pow(1 + (12 * 0.01) / 12, 12 * 5);
            Assert.Equal(resTest, resProgr, 2);
        }
        [Fact]
        public void EndSum_Correct_Year()
        {
            var program = new Program();
            program.money = new Program.Money
            {
                age = 5,
                procent = 12,
                beginSum = 100000,
                type = 3
            };
            double resProgr = program.CalculateEndSum();
            double resTest = 100000 * Math.Pow(1 + (12 * 0.01) / 1, 1 * 5);
            Assert.Equal(resTest, resProgr, 2);
        }
    }
}