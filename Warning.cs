namespace LostTech.Stack.WindowManagement
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using LostTech.App;

    public static class WarningExtensions
    {
        static IWarningsService? warningsService;

        public static IWarningsService WarningsService {
            get => warningsService ?? throw new InvalidOperationException($"{nameof(WarningsService)} is not initialized.");
            set {
                if (value is null)
                    throw new ArgumentNullException(nameof(WarningsService));
                if (Interlocked.CompareExchange(ref warningsService, value, null) != null)
                    throw new InvalidOperationException();
            }
        }

        public static void ReportAsWarning(this Exception exception, string prefix = "Warning: ")
            => WarningsService.ReportAsWarning(exception, prefix);
        public static void ReportAsWarning(this Task<Exception> potentiallyFailingTask, string prefix = "Warning: ")
            => WarningsService.ReportAsWarning(potentiallyFailingTask, prefix);
        public static void ReportAsWarning(this Task potentiallyFailingTask, string prefix = "Warning: ")
            => WarningsService.ReportAsWarning(potentiallyFailingTask, prefix);
    }
}
