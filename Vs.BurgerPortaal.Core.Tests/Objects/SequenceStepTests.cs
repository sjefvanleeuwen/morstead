using Moq;
using System.Collections.Generic;
using Vs.Rules.Core.Model;
using Vs.BurgerPortaal.Core.Objects;
using Xunit;

namespace Vs.BurgerPortaal.Core.Tests.Objects
{
    public class SequenceStepTests
    {
        private List<string> _names = new List<string> { "param1", "param2" };
        private List<string> _names2 = new List<string> { "param1", "param2", "woonland" };
        private string _name = "param3";
        private string _name2 = "woonland";

        [Fact]
        public void HasMultipleValidParameterNamesTest()
        {
            var sut = new SequenceStep() { ValidParameterNames = _names };

            Assert.True(sut.HasMultipleValidParameterNames());
        }

        [Fact]
        public void IsMatchTest()
        {
            var parameter = InitMoqParameter();

            var sut1 = new SequenceStep() { ValidParameterNames = _names };
            Assert.False(sut1.IsMatch(parameter));

            var sut2 = new SequenceStep() { ValidParameterNames = _names2 };
            Assert.True(sut2.IsMatch(parameter));

            var sut3 = new SequenceStep() { ParameterName = _name };
            Assert.False(sut3.IsMatch(parameter));

            var sut4 = new SequenceStep() { ParameterName = _name2 };
            Assert.True(sut4.IsMatch(parameter));
        }

        private IParameter InitMoqParameter()
        {
            var moq = new Mock<IParameter>();
            moq.Setup(m => m.Name).Returns("woonland");
            return moq.Object;
        }
    }
}
