﻿using System;

namespace Common.Core.Common
{
    public class ValueGetter<T>
    {
        private readonly Func<T> _getter;
        private readonly object _getterLock = new object();
        private readonly Action<T> _onSet;
        private volatile bool _exec;
        private T _value;

        public ValueGetter(Func<T> getter, Action<T> onSet = null)
        {
            _getter = getter;
            _onSet = onSet;
        }

        public T Get(bool recreate = false)
        {
            lock (_getterLock)
            {
                if (_exec && !recreate) return _value;

                if (_getter != null)
                    _value = _getter();

                _exec = true;

                return _value;
            }
        }

        public void Set(T value)
        {
            lock (_getterLock)
            {
                _exec = true;
                _onSet?.Invoke(value);
                _value = value;
            }
        }
    }
}