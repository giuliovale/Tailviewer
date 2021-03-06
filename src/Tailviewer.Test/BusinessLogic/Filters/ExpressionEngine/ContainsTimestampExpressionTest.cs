﻿using System;
using FluentAssertions;
using NUnit.Framework;
using Tailviewer.Core.Filters.ExpressionEngine;
using Tailviewer.Core.Settings;

namespace Tailviewer.Test.BusinessLogic.Filters.ExpressionEngine
{
	[TestFixture]
	public sealed class ContainsTimestampExpressionTest
	{
		[Test]
		public void TestEvaluateNullTimestamp()
		{
			var expression = new ContainsTimestampExpression(new DateTimeLiteral(null),
			                                                 new DateTimeIntervalLiteral(SpecialDateTimeInterval.Today));
			expression.Evaluate(null).Should().BeFalse("because a null timestamp is not part of any interval");
		}

		[Test]
		public void TestEvaluateNullInterval()
		{
			var expression = new ContainsTimestampExpression(new DateTimeLiteral(DateTime.Now),
			                                                 new DateTimeInterval(null, null));
			expression.Evaluate(null).Should().BeFalse("because nothing can be part of a null interval");
		}

		[Test]
		public void TestEvaluateOnlyStart1()
		{
			var start = new DateTime(2018, 8, 1, 0, 0, 0);
			var expression = new ContainsTimestampExpression(new DateTimeLiteral(start), new DateTimeInterval(start, null));
			expression.Evaluate(null).Should().BeTrue("because the specified timestamp is equal to start");
		}

		[Test]
		public void TestEvaluateOnlyStart2()
		{
			var start = new DateTime(2018, 8, 1, 0, 0, 0);
			var expression = new ContainsTimestampExpression(new DateTimeLiteral(start - TimeSpan.FromTicks(1)), new DateTimeInterval(start, null));
			expression.Evaluate(null).Should().BeFalse("because the specified timestamp is less than start");
		}

		[Test]
		public void TestEvaluateOnlyStart3()
		{
			var start = new DateTime(2018, 8, 1, 0, 0, 0);
			var expression = new ContainsTimestampExpression(new DateTimeLiteral(start + TimeSpan.FromTicks(1)), new DateTimeInterval(start, null));
			expression.Evaluate(null).Should().BeTrue("because the specified timestamp is greater than start");
		}

		[Test]
		public void TestEvaluateOnlyEnd1()
		{
			var end = new DateTime(2018, 8, 1, 0, 0, 0);
			var expression = new ContainsTimestampExpression(new DateTimeLiteral(end), new DateTimeInterval(null, end));
			expression.Evaluate(null).Should().BeTrue("because the specified timestamp is equal to end");
		}

		[Test]
		public void TestEvaluateOnlyEnd2()
		{
			var end = new DateTime(2018, 8, 1, 0, 0, 0);
			var expression = new ContainsTimestampExpression(new DateTimeLiteral(end - TimeSpan.FromTicks(1)), new DateTimeInterval(null, end));
			expression.Evaluate(null).Should().BeTrue("because the specified timestamp is less than end");
		}

		[Test]
		public void TestEvaluateOnlyEnd3()
		{
			var end = new DateTime(2018, 8, 1, 0, 0, 0);
			var expression = new ContainsTimestampExpression(new DateTimeLiteral(end + TimeSpan.FromTicks(1)), new DateTimeInterval(null, end));
			expression.Evaluate(null).Should().BeFalse("because the specified timestamp is greater than end");
		}
	}
}
