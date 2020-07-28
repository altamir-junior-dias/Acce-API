using System;

namespace Acce.Wrappers
{
    public class GarbageCollectorWrapper : IGarbageCollectorWrapper {
        public void SuppressFinalize(Object target) {
            GC.SuppressFinalize(target);
        }
    }
}