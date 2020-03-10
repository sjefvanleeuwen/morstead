using Moq;
using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Helpers;
using Vs.VoorzieningenEnRegelingen.Core;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Helpers
{
    public class RechtHelperTests
    {
        [Fact]
        public void ShouldAnswerHasRecht()
        {
            //0 recht
            var moq = new Mock<IExecutionResult>();
            var recht = RechtHelper.HasRecht(moq.Object);
            Assert.True(recht);

            var moqParameter1 = new Mock<IParameter>();
            moqParameter1.Setup(m => m.Name).Returns("recht");
            moqParameter1.Setup(m => m.Value).Returns(true);
            var moqParameter2 = new Mock<IParameter>();
            moqParameter2.Setup(m => m.Name).Returns("recht");
            moqParameter2.Setup(m => m.Value).Returns(true);

            //1 recht: true
            var moqParameterCollection = new Mock<IParametersCollection>();
            moqParameterCollection.Setup(m => m.GetEnumerator()).Returns(new List<IParameter> { moqParameter1.Object }.GetEnumerator());
            moq.Setup(m => m.Parameters).Returns(moqParameterCollection.Object);
            recht = RechtHelper.HasRecht(moq.Object);
            Assert.True(recht);

            //2 recht: true & true
            moqParameterCollection.Setup(m => m.GetEnumerator()).Returns(new List<IParameter> { moqParameter1.Object, moqParameter2.Object }.GetEnumerator());
            moq.Setup(m => m.Parameters).Returns(moqParameterCollection.Object);
            recht = RechtHelper.HasRecht(moq.Object);
            Assert.True(recht);

            //2 recht: true & false
            moqParameter2.Setup(m => m.Value).Returns(false);

            moqParameterCollection.Setup(m => m.GetEnumerator()).Returns(new List<IParameter> { moqParameter1.Object, moqParameter2.Object }.GetEnumerator());
            moq.Setup(m => m.Parameters).Returns(moqParameterCollection.Object);
            recht = RechtHelper.HasRecht(moq.Object);
            Assert.False(recht);
        }
    }
}
