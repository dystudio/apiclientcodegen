using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Exceptionless;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging
{
    [ExcludeFromCodeCoverage]
    public class ExceptionlessRemoteLogger : IRemoteLogger
    {
        public ExceptionlessRemoteLogger()
        {
            ExceptionlessClient.Default.Startup("6CRkH7zip11qalrUJgxi78lVyi93rxhQkzbYZfK2");
        }

        public void TrackFeatureUsage(string featureName, params string[] tags)
        {
            if (!TestingUtility.IsRunningFromUnitTest && !Debugger.IsAttached)
                ExceptionlessClient.Default
                    .CreateFeatureUsage(featureName)
                    .AddTags(tags)
                    .Submit();
        }

        public void TrackEvent(string message, string source, params string[] tags)
        {
            if (!TestingUtility.IsRunningFromUnitTest && !Debugger.IsAttached)
                ExceptionlessClient.Default
                    .CreateLog(source, message)
                    .AddTags(tags)
                    .Submit();
        }

        public void TrackError(Exception exception)
        {
            if (!TestingUtility.IsRunningFromUnitTest && !Debugger.IsAttached)
                exception.ToExceptionless().Submit();
        }
    }
}