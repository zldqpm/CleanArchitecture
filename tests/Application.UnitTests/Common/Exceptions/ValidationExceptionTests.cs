using FluentAssertions;
using FluentValidation.Results;
using ValidationException = Application.Common.Exceptions.ValidationException;

namespace Application.UnitTests.Common.Exceptions
{
    public class ValidationExceptionTests
    {
        [Fact]
        public void DefaultConstructorCreatesAnEmptyErrorDictionary()
        {
            var actual = new ValidationException().Errors;

            actual.Keys.Should().BeEquivalentTo(Array.Empty<string>());
        }

        [Fact]
        public void SingleValidationFailureCreatesASingleElementErrorDictionary()
        {
            var failures = new List<ValidationFailure>
            {
                new ("联系方式", "联系方式必填")
            };

            var actual = new ValidationException(failures).Errors;

            actual.Keys.Should().BeEquivalentTo("联系方式");
            actual["联系方式"].Should().BeEquivalentTo("联系方式必填");

        }
    }
}
