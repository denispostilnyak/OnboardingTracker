using System;
using System.Collections.Generic;
using System.Text;

namespace OnBoardingTracker.Infrastructure.CurrentUser
{
    public interface IUserContext
    {
         int Id { get; set; }

         string FirstName { get; set; }

         string LastName { get; set; }

         string Email { get; set; }
    }
}
