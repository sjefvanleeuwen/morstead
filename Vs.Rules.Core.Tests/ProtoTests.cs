using Scriban;
using Vs.Rules.Core.Protogen;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.Core.Tests
{
    public class ProtoTests
    {
        [Fact]
        public void Proto_Global_Proto_Version_Correct()
        {
            Assert.True(Global.Syntax == "proto3");
        }

        [Fact]
        public void Proto_Template_Engine_Hello_World()
        {
            var template = Template.Parse("Hello {{name}}!");
            var result = template.Render(new { Name = "World" }); // => "Hello World!" 
            Assert.True(result == "Hello World!");
        }

        [Fact]
        public void Proto_Create_Message_Type()
        {
            var template = Template.Parse("syntax = {{protoversion}}");
            var result = template.Render(new { protoversion = "proto3" }); // => "Hello World!" 
            Assert.True(result == "syntax = proto3");
        }


    }
}


