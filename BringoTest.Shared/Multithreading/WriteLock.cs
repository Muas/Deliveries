using System;
using System.Threading;

namespace BringoTest.Shared.Multithreading
{
    public class WriteLock : IDisposable
    {
        public WriteLock(ReaderWriterLockSlim locker)
		{
			if (locker == null)
			{
				throw new ArgumentNullException(nameof(locker));
			}

			_locker = locker;
            try {} finally
            {
                _locker.EnterWriteLock();
            }
        }

        private readonly ReaderWriterLockSlim _locker;

        public void Dispose()
        {
            if (_locker.IsWriteLockHeld)
            {
                _locker.ExitWriteLock();
            }
        }
    }
}
