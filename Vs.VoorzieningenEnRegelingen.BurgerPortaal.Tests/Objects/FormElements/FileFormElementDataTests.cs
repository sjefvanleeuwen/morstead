using BlazorInputFile;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Objects.FormElements
{
    public class FileFormElementDataTests
    {
        [Fact]
        public void CheckValidEmptyFromParent()
        {
            var sut = new FileFormElementData();
            sut.Validate();
            Assert.Empty(sut.Value);
            Assert.False(sut.IsValid);
            Assert.Equal("Er zijn geen bestanden geupload.", sut.ErrorText);
        }

        [Fact]
        public void CheckValidHasFile()
        {
            var sut = new FileFormElementData
            {
                Files = new List<IFileListEntry> {
                    InitMockFile("Test1").Object
                }
            };
            sut.Validate();
            Assert.Empty(sut.Value);
            Assert.True(sut.IsValid);
        }

        [Fact]
        public void ShouldRemoveFile()
        {
            var sut = new FileFormElementData
            {
                MaximumNumberOfFiles = 3,
                Files = new List<IFileListEntry> {
                    InitMockFile("Test2").Object,
                    InitMockFile("Test1").Object,
                    InitMockFile("Test3").Object
                }
            };
            Assert.Equal(3, sut.Files.Count());
            sut.RemoveFile("Test1");
            Assert.Equal(2, sut.Files.Count());
            Assert.Equal("Test2", sut.Files.ToList()[0].Name);
            Assert.Equal("Test3", sut.Files.ToList()[1].Name);
        }

        [Fact]
        public void ShouldMakeRoomForNewFile()
        {
            var sut = new FileFormElementData
            {
                MaximumNumberOfFiles = 3,
                Files = new List<IFileListEntry> {
                    InitMockFile("Test2").Object,
                    InitMockFile("Test1").Object,
                    InitMockFile("Test3").Object
                }
            };
            Assert.Equal(3, sut.Files.Count());
            sut.MakeRoomForNewFile();
            Assert.Equal(2, sut.Files.Count());
            Assert.Equal("Test1", sut.Files.ToList()[0].Name);
            Assert.Equal("Test3", sut.Files.ToList()[1].Name);
        }

        [Fact]
        public void ShouldValidateUploadedFile()
        {
            var sut = new FileFormElementData();
            sut.AllowedExtensions = new List<string> { ".pdf" };
            var moq = InitMockFile("uploadedFile.not");
            sut.ValidateUploadedFile(moq.Object);
            Assert.False(sut.IsValid);
            Assert.Equal("Het bestand heeft niet de juiste extensie (.not); PDF, Word documenten en afbeeldingen zijn toegestaan. Probeer het opnieuw.", sut.ErrorText);

            sut.AllowedExtensions = new List<string> { ".pDf" };
            sut.MaximumFileSize = 1234567;
            moq = InitMockFile("uploadedFile.pDf");
            moq.Setup(m => m.Size).Returns(1234567 + 1);
            sut.ValidateUploadedFile(moq.Object);
            Assert.False(sut.IsValid);
            Assert.Equal("Het bestand is groter dan de maximaal toegestane grootte (1,18MB). Probeer het opnieuw.", sut.ErrorText);

            moq.Setup(m => m.Size).Returns(1234567);
            sut.ValidateUploadedFile(moq.Object);
            Assert.True(sut.IsValid);
            Assert.Empty(sut.ErrorText);
        }

        private Mock<IFileListEntry> InitMockFile(string name)
        {
            var result = new Mock<IFileListEntry>();
            result.Setup(m => m.Name).Returns(name);
            return result;
        }
    }
}
