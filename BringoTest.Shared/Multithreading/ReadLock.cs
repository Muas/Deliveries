using System;
using System.Threading;

namespace BringoTest.Shared.Multithreading
{
    public class ReadLock : IDisposable
    {
        public ReadLock(ReaderWriterLockSlim locker)
        {
	        if (locker == null)
	        {
		        throw new ArgumentNullException(nameof(locker));
	        }

            _locker = locker;
            try {} finally
            {
                _locker.EnterReadLock();
            }
        }

        private readonly ReaderWriterLockSlim _locker;

        public void Dispose()
        {
            if (_locker.IsReadLockHeld)
            {
                _locker.ExitReadLock();
            }
        }
    }
}
