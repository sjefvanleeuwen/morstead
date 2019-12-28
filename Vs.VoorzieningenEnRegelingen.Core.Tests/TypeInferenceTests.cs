using Xunit;

namespace Vs.VoorzieningenEnRegelingen.Core.Tests
{
    public class TypeInferenceTests
    {
        [Fact]
        public void TypeInference_Test()
        {
           var testData = "10:00/25-12-2008/1097.63/-1/hello world/(1)/(2)/1.0f";
            var result = "";
            foreach (var inference in testData.Split('/'))
            {
                result += TypeInference.Infer(inference).Type.ToString();
            }
        }
    }
}
