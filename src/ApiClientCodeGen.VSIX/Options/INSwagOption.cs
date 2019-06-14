﻿using NJsonSchema.CodeGeneration.CSharp;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options
{
    public interface INSwagOptions
    {
        bool InjectHttpClient { get; }
        bool GenerateClientInterfaces { get; }
        bool GenerateDtoTypes { get; }
        bool UseBaseUrl { get; }
        CSharpClassStyle ClassStyle { get; }
    }
}