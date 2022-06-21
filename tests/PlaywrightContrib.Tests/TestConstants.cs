[assembly: NUnit.Framework.Timeout(PlaywrightContrib.Tests.TestConstants.DefaultTestTimeout)]
[assembly: NUnit.Framework.Parallelizable(NUnit.Framework.ParallelScope.Fixtures)]

namespace PlaywrightContrib.Tests
{
    internal static class TestConstants
    {
        public const int DefaultTestTimeout = 30_000;
    }
}
