using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnBoardingTracker.Application.Infrastructure.Exceptions
{
    public class ValidationException : Exception
    {
        public IDictionary<string, string[]> Failures { get; }

        public ValidationException()
            : base("One or more validation failures")
        {
            Failures = new Dictionary<string, string[]>();
        }

        public ValidationException(string message)
            : base(message)
        {
            Failures = new Dictionary<string, string[]>();
        }

        public ValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
            Failures = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            var propertyNames = failures
                .Select(item => item.PropertyName)
                .Distinct();

            foreach (var name in propertyNames)
            {
                var propertyFailures = failures
                    .Where(failure => failure.PropertyName == name)
                    .Select(failure => failure.ErrorMessage)
                    .ToArray();

                Failures.Add(name, propertyFailures);
            }
        }
    }
}
