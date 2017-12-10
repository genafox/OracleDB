using System;

namespace CourseApp.DataAccess.Exceptions
{
    public class UniqueNameViolationException : Exception
    {
        public UniqueNameViolationException(string message) : base(message)
        {
        }
    }
}
