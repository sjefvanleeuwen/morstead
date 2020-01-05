using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Vs.VoorzieningenEnRegelingen.Service
{
    public class CustomTextInputFormatter : TextInputFormatter
    {
        // Required to use text/html or foo/bar in ConsumesAttribute, see ConsumesController and ConsumesTests

        public CustomTextInputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/html"));
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("foo/bar"));

            SupportedEncodings.Add(Encoding.UTF8);
        }

        public override Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            return Task.FromResult(InputFormatterResult.Success(null));
        }
    }
}
