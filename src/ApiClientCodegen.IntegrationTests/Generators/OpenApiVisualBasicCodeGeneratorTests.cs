﻿using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.OpenApi;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Utility;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options;
using FluentAssertions;
using ICSharpCode.CodeConverter;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Generators
{
    [TestClass]
    [TestCategory("SkipWhenLiveUnitTesting")]
    [DeploymentItem("Resources/Swagger.json")]
    public class OpenApiVisualBasicCodeGeneratorTests
    {
        private static readonly Mock<IVsGeneratorProgress> mock = new Mock<IVsGeneratorProgress>();
        private static Mock<IGeneralOptions> optionsMock;
        private static string code = null;

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            optionsMock = new Mock<IGeneralOptions>();
            optionsMock.Setup(c => c.NSwagPath).Returns(PathProvider.GetJavaPath());

            var codeGenerator = new OpenApiCSharpCodeGenerator(
                Path.GetFullPath("Swagger.json"),
                typeof(OpenApiVisualBasicCodeGeneratorTests).Namespace,
                optionsMock.Object);

            var options = new CodeWithOptions(codeGenerator.GenerateCode(mock.Object));
            var result = CodeConverter
                .Convert(options)
                .GetAwaiter()
                .GetResult();

            code = result.ConvertedCode;
        }

        [ClassCleanup]
        public static void CleanUp()
            => DependencyUninstaller.UninstallOpenApiGenerator();

        [TestMethod]
        public void OpenApi_Generated_Code_NotNullOrWhitespace()
            => code.Should().NotBeNullOrWhiteSpace();

        [TestMethod]
        public void OpenApi_Reports_Progres()
            => mock.Verify(
                c => c.Progress(It.IsAny<uint>(), It.IsAny<uint>()), 
                Times.AtLeastOnce);

        [TestMethod]
        public void Reads_JavaPath_From_Options() 
            => optionsMock.Verify(c => c.JavaPath);
    }
}
