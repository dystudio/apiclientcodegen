﻿using System;
using ApiClientCodeGen.Tests.Common.Build;
using ApiClientCodeGen.Tests.Common.Fixtures.Yaml;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using FluentAssertions;
using Moq;
using Xunit;

namespace ApiClientCodeGen.Core.IntegrationTests.Generators.Yaml
{
    [Trait("Category", "SkipWhenLiveUnitTesting")]
    public class SwaggerCodeGeneratorYamlTests : IClassFixture<SwaggerCodeGeneratorFixture>
    {
        private readonly SwaggerCodeGeneratorFixture fixture;

        public SwaggerCodeGeneratorYamlTests(SwaggerCodeGeneratorFixture fixture)
        {
            this.fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        }

        [SkippableFact(typeof(NotSupportedException))]
        public void Swagger_Generated_Code_NotNullOrWhitespace()
            => fixture.Code.Should().NotBeNullOrWhiteSpace();

        [SkippableFact(typeof(NotSupportedException))]
        public void Swagger_Reports_Progres()
            => fixture.ProgressReporterMock.Verify(
                c => c.Progress(It.IsAny<uint>(), It.IsAny<uint>()),
                Times.AtLeastOnce);

        [SkippableFact(typeof(NotSupportedException))]
        public void Reads_JavaPath_From_Options()
            => fixture.OptionsMock.Verify(c => c.JavaPath);

        [SkippableFact(typeof(NotSupportedException))]
        public void GeneratedCode_Can_Build_In_NetCoreApp()
            => BuildHelper.BuildCSharp(ProjectTypes.DotNetCoreApp, fixture.Code, SupportedCodeGenerator.Swagger);

        [SkippableFact(typeof(NotSupportedException))]
        public void GeneratedCode_Can_Build_In_NetStandardLibrary()
            => BuildHelper.BuildCSharp(ProjectTypes.DotNetStandardLibrary, fixture.Code, SupportedCodeGenerator.Swagger);

        // [SkippableFact(typeof(NotSupportedException))]
        // public void GeneratedCode_Can_Build_In_NetFrameworkApp()
        //     => BuildHelper.BuildCSharp(ProjectTypes.DotNetFramework, fixture.Code, SupportedCodeGenerator.Swagger);
    }
}