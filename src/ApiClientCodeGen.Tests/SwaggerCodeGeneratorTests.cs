﻿using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.Swagger;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests
{
    [TestClass]
    //[Ignore("Integration Tests")]
    [DeploymentItem("Resources/Swagger.json")]
    public class SwaggerCodeGeneratorTests
    {
        [TestMethod]
        public void Can_Generate_Code_Using_Swagger()
            => new SwaggerCSharpCodeGenerator(
                    Path.GetFullPath("Swagger.json"), 
                    GetType().Namespace)
                .GenerateCode()
                .Should()
                .NotBeNullOrWhiteSpace();
    }
}
