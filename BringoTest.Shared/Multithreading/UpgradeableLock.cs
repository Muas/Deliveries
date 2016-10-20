using System;
using System.Threading;

namespace BringoTest.Shared.Multithreading
{
    public class UpgradeableLock : IDisposable
    {
        public UpgradeableLock(ReaderWriterLockSlim locker)
		{
			if (locker == null)
			{
				throw new ArgumentNullException(nameof(locker));
			}

			_locker = locker;
            try {} finally
            {
                _locker.EnterUpgradeableReadLock();
            }
        }

        private readonly ReaderWriterLockSlim _locker;

        public void ToWrite()
        {
            if (!_locker.IsUpgradeableReadLockHeld || _locker.IsReadLockHeld || _locker.IsWriteLockHeld) return;

            //prevent thread abort while acquiring lock
            try {} finally
            {
                _locker.EnterWriteLock();
            }
        }

        public void ToRead()
        {
            if (!_locker.IsUpgradeableReadLockHeld || _locker.IsReadLockHeld || _locker.IsWriteLockHeld) return;

            //prevent thread abort while acquiring lock
            try {} finally
            {
                _locker.EnterReadLock();
            }
            _locker.ExitUpgradeableReadLock();
        }

        public void ToUpgradeable()
        {
            if (_locker.IsWriteLockHeld)
            {
                _locker.ExitWriteLock();
            }
            else if (_locker.IsReadLockHeld)
            {
                _locker.ExitReadLock();
            }

            if (!_locker.IsUpgradeableReadLockHeld)
            {
                //prevent thread abort while acquiring lock
                try {} finally
                {
                    _locker.EnterUpgradeableReadLock();
                }
            }
        }

        public void Dispose()
        {
            if (_locker.IsWriteLockHeld)
            {
                _locker.ExitWriteLock();
            }
            if (_locker.IsReadLockHeld)
            {
                _locker.ExitReadLock();
            }
            if (_locker.IsUpgradeableReadLockHeld)
            {
                _locker.ExitUpgradeableReadLock();
            }
        }
    }
}
