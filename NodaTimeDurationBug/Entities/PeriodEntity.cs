using System;
using System.Text;
using NodaTime;

namespace NodaTimeDurationBug.Entities
{
	public record PeriodEntity
	{
		public Guid Id { get; set; }

		public Period? Value { get; set; }
	}
}