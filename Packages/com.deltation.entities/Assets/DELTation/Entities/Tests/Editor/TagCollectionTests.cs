using System;
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
			Assert.That(newCount, Is.EqualTo(oldCount + 1));
			Assert.That(_tags.Contains<string>());
		}

		[Test]
		public void GivenCollection_WhenGettingCount_ThenItIsZeroAndDoesNotContain()
		{
			// Arrange

			// Act
			var count = _tags.GetCount<string>();

			// Assert
			Assert.That(count, Is.Zero);
			Assert.That(_tags.Contains<string>(), Is.False);
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
			Assert.That(newCount, Is.EqualTo(oldCount + number));
			Assert.That(_tags.Contains<string>());
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
			Assert.That(newCount, Is.EqualTo(oldCount));
			Assert.That(_tags.Contains<string>(), Is.False);
		}

		[Test]
		public void GivenEmptyCollection_WhenRemovingTag_ThenNothingHappens()
		{
			// Arrange

			// Act
			_tags.Remove<string>();

			// Assert
			Assert.That(_tags.GetCount<string>(), Is.Zero);
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
			Assert.That(newCount, Is.EqualTo(Mathf.Max(initialCount - removal, 0)));
		}

		[Test, TestCase(0), TestCase(1), TestCase(2), TestCase(5), TestCase(10)]
		public void GivenCollection_WhenRemovingAll_ThenNumberIsZeroAndDoesNotContain(int initialCount)
		{
			// Arrange
			_tags.AddMany<string>(initialCount);

			// Act
			_tags.RemoveAll<string>();

			// Assert
			Assert.That(_tags.GetCount<string>(), Is.EqualTo(0));
			Assert.That(_tags.Contains<string>(), Is.False);
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
			Assert.That(_tags.GetCount<string>(), Is.EqualTo(0));
			Assert.That(_tags.GetCount<int>(), Is.EqualTo(0));
			Assert.That(_tags.Contains<string>(), Is.False);
			Assert.That(_tags.Contains<int>(), Is.False);
		}

		[Test, TestCase(-1), TestCase(-2), TestCase(-15)]
		public void GivenCollection_WhenAddingManyButNegativeNumber_ThenThrowsArgumentOutOfRangeException(int number)
		{
			// Arrange

			// Act

			// Assert
			Assert.That(() => _tags.AddMany<string>(number), Throws.InstanceOf<ArgumentOutOfRangeException>());
		}

		[Test, TestCase(-1), TestCase(-2), TestCase(-15)]
		public void GivenCollection_WhenARemovingManyButNegativeNumber_ThenThrowsArgumentOutOfRangeException(int number)
		{
			// Arrange

			// Act

			// Assert
			Assert.That(() => _tags.RemoveMany<string>(number), Throws.InstanceOf<ArgumentOutOfRangeException>());
		}
	}
}