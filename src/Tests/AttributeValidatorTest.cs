﻿using System;
using System.ComponentModel.DataAnnotations;
using Cloud.Core.Validation;
using Cloud.Core.Exceptions;
using Cloud.Core.Testing;
using FluentAssertions;
using Xunit;

namespace Cloud.Core.Tests
{
    [IsUnit]
    public class AttributeValidatorTest
    {
        /// <summary>Check the validate method returns valid as expected.</summary>
        [Fact]
        public void Test_Validator_ValidObject()
        {
            // Arrange
            var t = new TestClass();

            // Act
            var result = t.Validate();

            // Assert
            result.IsValid.Should().BeTrue();
        }

        /// <summary>Check the validate method returns invalid as expected.</summary>
        [Fact]
        public void Test_Validator_ThrowWhenInvalid()
        {
            // Arrange
            var t = new TestClass()
            {
                // Empty the required field so it fails validation.
                RequiredTest = null
            };

            // Act/Assert
            Assert.Throws<ValidateException>(() => t.ThrowIfInvalid());
        }

        /// <summary>Check the validate method returns invalid as expected.</summary>
        [Fact]
        public void Test_Validator_InvalidRequired()
        {
            // Arrange
            var t = new TestClass() {
                // Empty the required field so it fails validation.
                RequiredTest = null
            };

            // Act
            var result = t.Validate();

            // Assert
            result.IsValid.Should().BeFalse();
        }

        /// <summary>Check the validate method returns invalid as expected.</summary>
        [Fact]
        public void Test_Validator_ValidationException()
        {
            // Arrange
            var t = new TestClass()
            {
                // Empty the required field so it fails validation.
                RequiredTest = null
            };

            // Act/Assert
            Assert.Throws<ValidateException>(() => t.ThrowIfInvalid());
        }

        /// <summary>Check the validate method return invalid due to max length.</summary>
        [Fact]
        public void Test_Validator_InvalidMaxLengthExceeded()
        {
            // Arrange
            var t = new TestClass()
            {
                MaxLengthTest = "Testalongnamethatisinvalid"
            };

            // Act
            var result = t.Validate();

            // Assert
            result.IsValid.Should().BeFalse();
        }

        /// <summary>Check the validate method return invalid due to invalid range.</summary>
        [Fact]
        public void Test_Validator_InvalidRange()
        {
            // Arrange
            var t = new TestClass()
            {
                RangeTest = 100000
            };

            // Act
            var result = t.Validate();

            // Assert
            result.IsValid.Should().BeFalse();
        }

        /// <summary>Check the validate method return invalid due to string length.</summary>
        [Fact]
        public void Test_Validator_InvalidStringLength()
        {
            // Arrange
            var t = new TestClass()
            {
                StringLengthTest = "thisisareallylongstringtest"
            };

            // Act
            var result = t.Validate();

            // Assert
            result.IsValid.Should().BeFalse();
        }

        /// <summary>Check the validate method return invalid due to failing regEx check.</summary>
        [Fact]
        public void Test_Validator_InvalidRegEx()
        {
            // Arrange
            var t = new TestClass()
            {
                RegExTest = "12345abcdeABCDE"
            };

            // Act
            var result = t.Validate();

            // Assert
            result.IsValid.Should().BeFalse();
        }

        private class TestClass : AttributeValidator
        {
            public TestClass()
            {
                SetDefaults();
            }

            [Required]
            public string RequiredTest { get; set; }

            [StringLength(10)]
            public string StringLengthTest { get; set; }

            [Range(1, 100)]
            public int RangeTest { get; set; }

            // This is disabled as its not honoured by validator.
            //[DataType(DataType.EmailAddress)]
            //public string DataTypeTest { get; set; }

            [MaxLength(10)]
            public string MaxLengthTest { get; set; }

            // Alpha characters only (max length 25).
            [RegularExpression(@"^[a-zA-Z]{1,25}$")]
            public string RegExTest { get; set; }

            public void SetDefaults()
            {
                RequiredTest = "Test";
                StringLengthTest = "Test";
                RangeTest = 1;
                MaxLengthTest = "Test";
                RegExTest = "Test";
            }
        }
    }
}
