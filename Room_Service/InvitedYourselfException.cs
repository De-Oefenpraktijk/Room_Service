using System;
namespace Room_Service
{
    public class InvitedYourselfException : Exception
    {
        public InvitedYourselfException()
        {
        }
        public InvitedYourselfException(string message)
            : base(message)
        {
        }
        public InvitedYourselfException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

