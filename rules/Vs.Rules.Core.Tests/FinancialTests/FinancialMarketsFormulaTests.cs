using Vs.Rules.Core.Financial;
using Xunit;

namespace Vs.Rules.Core.Tests.FinancialTests
{
    public class FinancialMarketsFormulaTests
    {

        [Fact]
        public void CalcRateOfInflation()
        {
            var result = FinancialMarketsFormulas.CalcRateOfInflation(1000, 2000);
            Assert.Equal(1, result);
        }

        [Fact]
        public void CalcRealRateOfReturn()
        {
            var result = FinancialMarketsFormulas.CalcRealRateOfReturn(0.04m, 0.02m);
            Assert.Equal(0.0196078431372549019607843137m, result);
        }
    }
}