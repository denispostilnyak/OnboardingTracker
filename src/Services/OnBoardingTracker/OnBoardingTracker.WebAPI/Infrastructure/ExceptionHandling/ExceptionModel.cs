using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnBoardingTracker.WebApi.Infrastructure.ExceptionHandling
{
    public class ExceptionModel
    {
        public string Message { get; set; }

        public IDictionary<string, string[]> ModelState { get; set; }

        public string StackTrace { get; set; }
    }
}
