using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Shared.Components.FormElements
{
    public class FileTests : BlazorTestBase
    {
        [Fact]
        public void FileEmpty()
        {
            //todo MPS implement
            //Can not be run since there is no JSInvokeable?
            //var variables = new Dictionary<string, object> { { "Data", new FileFormElementData() } };
            //var component = _host.AddComponent<File>(variables);
            //Assert.Null(component.Find("ul"));
            //Assert.Equal("Bestand uploaden", component.Find("label > span").InnerHtml);
            //Assert.Equal("hint_", component.Find("input").Attr("aria-describedby"));
        }

        [Fact]
        public void FileFilled()
        {
            //todo MPS implement
            //Can not be run since there is no JSInvokeable?
            //set 2 files
            //check that there are 2 files visible
            //check that the correct texts are visible
        }

        [Fact]
        public void ShouldDoTwoWayBinding()
        {
            //todo MPS implement
            //Can not be run since there is no JSInvokeable?
            //try to upload a file
            //check that the file is set
        }

        [Fact]
        public void HasDressingElements()
        {
            //todo MPS implement
            //Can not be run since there is no JSInvokeable?
        }

        [Fact]
        public void ShouldHaveInput()
        {
            var sut = new File();
            Assert.True(sut.HasInput);
        }
    }
}