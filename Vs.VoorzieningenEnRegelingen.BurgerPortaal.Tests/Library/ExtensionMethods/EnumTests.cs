using System.ComponentModel;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Library.ExtensionMethods;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Library.ExtensionMethods
{
    public class EnumTests
    {
        private enum _testEnum1
        {
            [Description("_the_test_1_")]
            Test1_1,
            [Description("_the_test_2_")]
            Test1_2
        }

        private enum _testEnum2
        {
            Test2_1,
            Test2_2
        }

        [Fact]
        public void ShouldGetNullValueFromNonEnum()
        {
            var text = string.Empty;
            Assert.Null(Enum.GetDescription(text));

            var number = int.MinValue;
            Assert.Null(Enum.GetDescription(number));
        }

        [Fact]
        public void ShouldGetNullValueWhenNoDescriptionProvided()
        {
            var test1 = _testEnum2.Test2_1;
            Assert.Null(test1.GetDescription());

            var test2 = _testEnum2.Test2_2;
            Assert.Null(test2.GetDescription());
        }


        [Fact]
        public void ShouldGetDescription()
        {
            var test1 = _testEnum1.Test1_1;
            Assert.NotNull(test1.GetDescription());
            Assert.Equal("_the_test_1_", test1.GetDescription());

            var test2 = _testEnum1.Test1_2;
            Assert.NotNull(test2.GetDescription());
            Assert.Equal("_the_test_2_", test2.GetDescription());
        }
    }
}
