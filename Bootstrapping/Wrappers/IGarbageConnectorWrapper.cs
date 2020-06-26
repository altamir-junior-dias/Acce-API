using System;

namespace Bootstrapping.Wrappers
{
    public interface IGarbageCollectorWrapper {
        void SuppressFinalize(Object target);
    }
}