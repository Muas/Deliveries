using System.Threading;
using BringoTest.Shared.Multithreading;

namespace BringoTest.Shared.Extensions
{
    public static class ReaderWriterLockSlimExtensions
    {
        public static ReadLock UseReadLock(this ReaderWriterLockSlim locker)
        {
            return new ReadLock(locker);
        }

        public static WriteLock UseWriteLock(this ReaderWriterLockSlim locker)
        {
            return new WriteLock(locker);
        }

        public static UpgradeableLock UseUpgradeableLock(this ReaderWriterLockSlim locker)
        {
            return new UpgradeableLock(locker);
        }
    }
}
