using System;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;

namespace DELTation.Entities.Tests.Editor
{
	public class TagCollectionTests
	{
		private TagCollection _tags;

		[SetUp]
		public void SetUp()
		{
			_tags = new TagCollection();
		}

		[Test]
		public void GivenCollection_WhenAddingTag_ThenCountIsIncrementedAndContains()
		{
			// Arrange
			var oldCount = _tags.GetCount<string>();

			// Act
			_tags.Add<string>();
			var newCount = _tags.GetCount<string>();

			// Assert
			newCount.Should().Be(oldCount + 1);
			_tags.Contains<string>().Should().BeTrue();
		}

		[Test]
		public void GivenCollection_WhenGettingCount_ThenItIsZeroAndDoesNotContain()
		{
			// Arrange

			// Act
			var count = _tags.GetCount<string>();

			// Assert
			count.Should().Be(0);
			_tags.Contains<string>().Should().BeFalse();
		}

		[Test, TestCase(1), TestCase(2), TestCase(5)]
		public void GivenCollection_WhenAddingManyTags_ThenCountIsIncreasedByThatNumberAndContains(int number)
		{
			// Arrange
			var oldCount = _tags.GetCount<string>();

			// Act
			_tags.AddMany<string>(number);
			var newCount = _tags.GetCount<string>();

			// Assert
			newCount.Should().Be(oldCount + number);
			_tags.Contains<string>().Should().BeTrue();
		}

		[Test]
		public void GivenCollection_WhenAddingManyTagsButZero_ThenCountIsNotIncreasedAndDoesNotContain()
		{
			// Arrange
			var oldCount = _tags.GetCount<string>();

			// Act
			_tags.AddMany<string>(0);
			var newCount = _tags.GetCount<string>();

			// Assert
			newCount.Should().Be(oldCount);
			_tags.Contains<string>().Should().BeFalse();
		}

		[Test]
		public void GivenEmptyCollection_WhenRemovingTag_ThenNothingHappens()
		{
			// Arrange

			// Act
			_tags.Remove<string>();

			// Assert
			_tags.GetCount<string>().Should().Be(0);
		}

		[Test, TestCase(0), TestCase(1), TestCase(2), TestCase(5), TestCase(10)]
		public void GivenCollection_WhenRemovingMany_ThenNumberIsDecremented(int removal)
		{
			// Arrange
			const int initialCount = 5;
			_tags.AddMany<string>(initialCount);

			// Act
			_tags.RemoveMany<string>(removal);
			var newCount = _tags.GetCount<string>();

			// Assert
			newCount.Should().Be(Mathf.Max(initialCount - removal, 0));
		}

		[Test, TestCase(0), TestCase(1), TestCase(2), TestCase(5), TestCase(10)]
		public void GivenCollection_WhenRemovingAll_ThenNumberIsZeroAndDoesNotContain(int initialCount)
		{
			// Arrange
			_tags.AddMany<string>(initialCount);

			// Act
			_tags.RemoveAll<string>();

			// Assert
			_tags.GetCount<string>().Should().Be(0);
			_tags.Contains<string>().Should().BeFalse();
		}

		[Test]
		public void GivenCollection_WhenClearing_ThenAllAreZeroAndDoesNotAnyOfThem()
		{
			// Arrange
			_tags.AddMany<string>(10);
			_tags.AddMany<int>(5);

			// Act
			_tags.Clear();

			// Assert
			_tags.GetCount<string>().Should().Be(0);
			_tags.GetCount<int>().Should().Be(0);
			_tags.Contains<string>().Should().BeFalse();
			_tags.Contains<int>().Should().BeFalse();
		}

		[Test, TestCase(-1), TestCase(-2), TestCase(-15)]
		public void GivenCollection_WhenAddingManyButNegativeNumber_ThenThrowsArgumentOutOfRangeException(int number)
		{
			// Arrange

			// Act

			// Assert
			_tags.Invoking(t => t.AddMany<string>(number))
				.Should()
				.Throw<ArgumentOutOfRangeException>();
		}

		[Test, TestCase(-1), TestCase(-2), TestCase(-15)]
		public void GivenCollection_WhenARemovingManyButNegativeNumber_ThenThrowsArgumentOutOfRangeException(int number)
		{
			// Arrange

			// Act

			// Assert
			_tags.Invoking(t => t.RemoveMany<string>(number))
				.Should()
				.Throw<ArgumentOutOfRangeException>();
		}
	}
}