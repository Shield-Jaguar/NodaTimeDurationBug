using System;
using NodaTime;

namespace NodaTimeDurationBug
{
	public record Property
	{
		public Guid Id { get; set; }

		public Duration? StartDuration { get; set; }

		public Duration? EndDuration { get; set; }

	}
}
