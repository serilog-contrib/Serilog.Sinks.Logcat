using System;
using System.IO;
using Java.Lang;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;

namespace Serilog.Sinks.Logcat
{
    internal class LogcatSink: ILogEventSink
    {
        private readonly string _tag;
        private readonly ITextFormatter _formatter;
        private readonly LogEventLevel _restrictedToMinimumLevel;

        public LogcatSink(
            string tag,
            ITextFormatter formatter,
            LogEventLevel restrictedToMinimumLevel)
        {
            _tag = tag ?? throw new ArgumentNullException(nameof(tag));

            _formatter = formatter;
            _restrictedToMinimumLevel = restrictedToMinimumLevel;
        }

        public void Emit(LogEvent logEvent)
        {
            if (logEvent.Level < _restrictedToMinimumLevel)
            {
                return;
            }

            using var writer = new StringWriter();

            _formatter.Format(logEvent, writer);

            var msg = writer.ToString();

            switch (logEvent.Level)
            {
                case LogEventLevel.Verbose:
                    Android.Util.Log.Verbose(_tag, msg);
                    break;
                case LogEventLevel.Debug:
                    Android.Util.Log.Debug(_tag, msg);
                    break;
                case LogEventLevel.Information:
                    Android.Util.Log.Info(_tag, msg);
                    break;
                case LogEventLevel.Warning:
                    Android.Util.Log.Warn(_tag, msg);
                    break;
                case LogEventLevel.Fatal:
                case LogEventLevel.Error:
                    Android.Util.Log.Error(_tag, msg);
                    break;
            }
        }
    }
}
