using System;
using Vs.Rules.Core.Financial;
using Xunit;

namespace Vs.Rules.Core.Tests.FinancialTests
{
    public class BankingFormulaTests
    {

		[Fact]	
		public void CalcAnnualPercentageYield()
        {
            var result = BankingFormulas.CalcAnnualPercentageYield(0.04m, 12);
            Assert.Equal(0.0407415m, Math.Round(result, 7, MidpointRounding.AwayFromZero));
		}

        [Fact]
        public void CalcBalloonLoanPayment()
        {
            var result = BankingFormulas.CalcBalloonLoanPayment(10000, 2000, 0.04m, 10);
            Assert.Equal(1066.33m, Math.Round(result, 2, MidpointRounding.AwayFromZero));
        }

        [Fact]
        public void CalcCompoundInterest()
        {
            var result = BankingFormulas.CalcCompoundInterest(1000, 0.07m, 10);
            Assert.Equal(967.15m, Math.Round(result, 2, MidpointRounding.AwayFromZero));
        }

        [Fact]
        public void CalcContinuousCompounding()
        {
            var result = BankingFormulas.CalcContinuousCompounding(1000, 0.07m, 10);
            Assert.Equal(2013.75m, Math.Round(result, 2, MidpointRounding.AwayFromZero));
        }

        [Fact]
        public void CalcDebtToIncomeRatio()
        {
            var result = BankingFormulas.CalcDebtToIncomeRatio(250, 1000);
            Assert.Equal(0.25m, Math.Round(result, 2, MidpointRounding.AwayFromZero));
        }

        [Fact]
        public void CalcBalloonBalanceOfLoan()
        {
            var result = BankingFormulas.CalcBalloonBalanceOfLoan(100000, 500, 0.04m, 25);
            Assert.Equal(245760.68m, Math.Round(result, 2, MidpointRounding.AwayFromZero));
        }

        [Fact]
        public void CalcLoanPayment()
        {
            var result = BankingFormulas.CalcLoanPayment(1000, 0.04m, 10);
            Assert.Equal(123.29m, Math.Round(result, 2, MidpointRounding.AwayFromZero));
        }

        [Fact]
        public void CalcRemainingBalanceOnLoan()
        {
            var result = BankingFormulas.CalcRemainingBalanceOnLoan(10000, 250, 0.04m, 10);
            Assert.Equal(11800.92m, Math.Round(result, 2, MidpointRounding.AwayFromZero));
        }

        [Fact]
        public void CalcLoanToDepositRatio()
        {
            var result = BankingFormulas.CalcLoanToDepositRatio(10000, 4000);
            Assert.Equal(2.5m, Math.Round(result, 2, MidpointRounding.AwayFromZero));
        }

        [Fact]
        public void CalcLoanToValueRatio()
        {
            var result = BankingFormulas.CalcLoanToValueRatio(150000, 130000);
            Assert.Equal(1.15m, Math.Round(result, 2, MidpointRounding.AwayFromZero));
        }

        [Fact]
        public void CalcSimpleInterest()
        {
            var result = BankingFormulas.CalcSimpleInterest(1000, 0.04m, 10);
            Assert.Equal(400, Math.Round(result, 2, MidpointRounding.AwayFromZero));
        }

        [Fact]
        public void CalcSimpleInterestRate()
        {
            var result = BankingFormulas.CalcSimpleInterestRate(1000, 400, 10);
            Assert.Equal(0.04m, Math.Round(result, 2, MidpointRounding.AwayFromZero));
        }

        [Fact]
        public void CalcSimpleInterestPrincipal()
        {
            var result = BankingFormulas.CalcSimpleInterestPrincipal(400, 0.04m, 10);
            Assert.Equal(1000, Math.Round(result, 2, MidpointRounding.AwayFromZero));
        }

        [Fact]
        public void CalcSimpleInterestTime()
        {
            var result = BankingFormulas.CalcSimpleInterestTime(1000, 400, 0.04m);
            Assert.Equal(10, Math.Round(result, 2, MidpointRounding.AwayFromZero));
        }
    }
}