using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Vs.VoorzieningenEnRegelingen.Service
{
    public class CustomTextOutputFormatter : TextOutputFormatter
    {
        // Required to use text/html or foo/bar in ProducesAttribute, see ProducesController and ProducesTests

        public CustomTextOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/html"));
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("foo/bar"));

            SupportedEncodings.Add(Encoding.UTF8);
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            return Task.CompletedTask;
        }
    }
}
