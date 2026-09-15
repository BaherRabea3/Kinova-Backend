using Kinova.Domain.Entities.Sessions;

namespace Kinova.Domain.Entities.SessionTelemetryBatchs
{
    public class SessionTelemetryBatch
    {
        public long Id { get; private set; }

        public Guid SessionId { get; private set; }

        public long SequenceNumber { get; private set; }

        public DateTime ReceivedAt { get; private set; }

        public Session Session { get; private set; } = null!;
    }
}
