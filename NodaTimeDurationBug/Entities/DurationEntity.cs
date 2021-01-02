using System;
using NodaTime;

namespace NodaTimeDurationBug.Entities
{
	public record DurationEntity
	{
		public Guid Id { get; set; }

		public Duration? Value { get; set; }
	}
}
