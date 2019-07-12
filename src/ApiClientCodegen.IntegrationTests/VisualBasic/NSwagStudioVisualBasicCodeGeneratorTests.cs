﻿using System.IO;
using System.Threading.Tasks;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options;
using FluentAssertions;
using ICSharpCode.CodeConverter;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.VisualBasic
{
    [TestClass]
    [TestCategory("SkipWhenLiveUnitTesting")]
    [DeploymentItem("Resources/Swagger.nswag")]
    [DeploymentItem("Resources/Swagger.json")]
    public class NSwagStudioVisualBasicCodeGeneratorTests
    {
        private Mock<IGeneralOptions> optionsMock;
        private IGeneralOptions options;

        [TestInitialize]
        public void Init()
        {
            optionsMock = new Mock<IGeneralOptions>();
            optionsMock.Setup(c => c.NSwagPath).Returns(PathProvider.GetNSwagPath());
            options = optionsMock.Object;
        }

        [TestMethod]
        public async Task NSwagStudio_Generate_Code_Using_NSwagStudio()
        {
            var contents = await NSwagStudioFileHelper.CreateNSwagStudioFileAsync(
                File.ReadAllText("Swagger.json"),
                "https://petstore.swagger.io/v2/swagger.json");

            File.WriteAllText("Petstore.nswag", contents);
            new NSwagStudioCodeGenerator(Path.GetFullPath("Petstore.nswag"), options)
                .GenerateCode(new Mock<IVsGeneratorProgress>().Object)
                .Should()
                .BeNull();

            var outputFile = Path.GetFullPath("PetstoreClient.cs");
            File.Exists(outputFile)
                .Should()
                .BeTrue();

            var csharp = File.ReadAllText(outputFile);
            var result = await CodeConverter.Convert(new CodeWithOptions(csharp));

            result.ConvertedCode
                .Should()
                .NotBeNullOrWhiteSpace();
        }
    }
}