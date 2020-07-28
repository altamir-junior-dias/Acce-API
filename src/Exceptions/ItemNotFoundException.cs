using System;

namespace Acce.Exceptions
{
    public class ItemNotFoundException : Exception
    {
        public string ContextName { get; set; }

        public ItemNotFoundException() : this(null)
        {
        }

        public ItemNotFoundException(string contextName) : base()
        {
            ContextName = contextName;
        }
    }
}