﻿using System;
using System.Collections.Generic;

namespace Open.Memory
{
	public class MemoryComparer<T> : IComparer<ReadOnlyMemory<T>>
		where T : IComparable<T>
	{
		MemoryComparer(int sign = +1)
		{
			Sign = sign;
		}
		public int Sign { get; }
		public int Compare(ReadOnlyMemory<T> x, ReadOnlyMemory<T> y)
			=> Sign * MemoryComparer.Compare(x, y);

		// ReSharper disable once RedundantArgumentDefaultValue
		public static IComparer<ReadOnlyMemory<T>> Ascending { get; } = new MemoryComparer<T>(+1);
		public static IComparer<ReadOnlyMemory<T>> Descending { get; } = new MemoryComparer<T>(-1);
	}

	class MemoryFloatComparer : IComparer<ReadOnlyMemory<float>>
	{
		internal MemoryFloatComparer(int sign = +1)
		{
			Sign = sign;
		}
		public int Sign { get; }

		public int Compare(ReadOnlyMemory<float> x, ReadOnlyMemory<float> y)
			=> Sign * MemoryComparer.Compare(x, y);
	}

	class MemoryDoubleComparer : IComparer<ReadOnlyMemory<double>>
	{
		internal MemoryDoubleComparer(int sign = +1)
		{
			Sign = sign;
		}
		public int Sign { get; }

		public int Compare(ReadOnlyMemory<double> x, ReadOnlyMemory<double> y)
			=> Sign * MemoryComparer.Compare(x, y);
	}

	[System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Ok as a subclass.")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "Ok as a subclass.")]
	public static class MemoryComparer
	{
		public static int Compare<T>(in ReadOnlyMemory<T> target, in ReadOnlyMemory<T> other)
			where T : IComparable<T>
			=> SpanComparer.Compare(target.Span, other.Span);

		public static class Float
		{
			public static IComparer<ReadOnlyMemory<float>> Ascending { get; } = new MemoryFloatComparer();
			public static IComparer<ReadOnlyMemory<float>> Descending { get; } = new MemoryFloatComparer(-1);
			public static int Compare(in ReadOnlyMemory<float> target, in ReadOnlyMemory<float> other)
				=> SpanComparer.Float.Compare(target.Span, other.Span);
		}

		public static class Double
		{
			public static IComparer<ReadOnlyMemory<double>> Ascending { get; } = new MemoryDoubleComparer();
			public static IComparer<ReadOnlyMemory<double>> Descending { get; } = new MemoryDoubleComparer(-1);
			public static int Compare(in ReadOnlyMemory<double> target, in ReadOnlyMemory<double> other)
				=> SpanComparer.Double.Compare(target.Span, other.Span);
		}
	}

}
