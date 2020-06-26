using System;

namespace Bootstrapping.Wrappers
{
    public class GarbageCollectorWrapper : IGarbageCollectorWrapper {
        public void SuppressFinalize(Object target) {
            GC.SuppressFinalize(target);
        }
    }
}