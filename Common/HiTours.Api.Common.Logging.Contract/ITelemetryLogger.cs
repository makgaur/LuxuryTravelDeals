using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.Api.Common.Logging.Contract
{
    public interface ITelemetryLogger
    {
        void TrackException(Exception exception, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null);

        void TrackEvent(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null);

        void TrackDependency(string dependencyName, string commandName, DateTimeOffset startTime, TimeSpan duration, bool success, IDictionary<string, string> properties = null);

        void TrackRequest(string requestPath, DateTimeOffset startTime, TimeSpan duration, bool success, int statusCode, IDictionary<string, string> properties = null);
    }
}
