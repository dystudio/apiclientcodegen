﻿using AutoFixture;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.OpenApi;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Generators.OpenApi
{
    [TestClass]
    [DeploymentItem("Resources/Swagger.json")]
    public class OpenApiCSharpCodeGeneratorTests
    {
        private readonly Mock<IGeneralOptions> optionsMock = new Mock<IGeneralOptions>();
        private readonly Mock<IVsGeneratorProgress> progressMock = new Mock<IVsGeneratorProgress>();

        [TestInitialize]
        public void Init()
            => new OpenApiCSharpCodeGenerator(
                    "Swagger.json",
                    new Fixture().Create<string>(),
                    optionsMock.Object)
                .GenerateCode(progressMock.Object);

        [TestMethod]
        public void Reads_SwaggerCodegenPath()
            => optionsMock.Verify(c => c.OpenApiGeneratorPath);

        [TestMethod]
        public void Updates_Progress()
            => progressMock.Verify(
                c => c.Progress(
                    It.IsAny<uint>(),
                    It.IsAny<uint>()),
                Times.Exactly(5));
    }
}