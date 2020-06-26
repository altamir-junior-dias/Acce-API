using System;

namespace Bootstrapping.Exceptions
{
    public class PropertyNotProvidedException : Exception
    {
        public string PropertyName { get; set; }

        public PropertyNotProvidedException(string propertyName) : base()
        {
            PropertyName = propertyName;
        }
    }
}