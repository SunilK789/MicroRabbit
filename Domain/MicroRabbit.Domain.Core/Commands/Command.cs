namespace MicroRabbit.Domain.Core.Commands
{
    using System;
    public abstract class Command : Message
    {
        public DateTime Timestamp {get; protected set;}

        protected Command()
        {
            Timestamp = DateTime.Now();
        }
    }
}