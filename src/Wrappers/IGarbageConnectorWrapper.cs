using System;

namespace Acce.Wrappers
{
    public interface IGarbageCollectorWrapper {
        void SuppressFinalize(Object target);
    }
}