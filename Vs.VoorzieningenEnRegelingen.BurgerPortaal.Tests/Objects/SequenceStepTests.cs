using Moq;
using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects;
using Vs.VoorzieningenEnRegelingen.Core;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Objects
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
            var sut = new SequeceStep() { ValidParameterNames = _names };

            Assert.True(sut.HasMultipleValidParameterNames());
        }

        [Fact]
        public void IsMatchTest()
        {
            var parameter = InitMoqParameter();

            var sut1 = new SequeceStep() { ValidParameterNames = _names };
            Assert.False(sut1.IsMatch(parameter));

            var sut2 = new SequeceStep() { ValidParameterNames = _names2 };
            Assert.True(sut2.IsMatch(parameter));

            var sut3 = new SequeceStep() { ParameterName = _name };
            Assert.False(sut3.IsMatch(parameter));

            var sut4 = new SequeceStep() { ParameterName = _name2 };
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
