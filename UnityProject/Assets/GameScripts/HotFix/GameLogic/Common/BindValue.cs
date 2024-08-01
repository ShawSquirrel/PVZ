using System;

namespace GameLogic
{
    public class BindValue<T>
    {
        private Action<T, T> OnValueChange;
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                if (value.Equals(_value)) return;
                OnValueChange?.Invoke(_value, value);
                _value = value;
            }
        }

        public void InitValue(T value)
        {
            _value = value;
        }
        
        public void AddListener(Action<T, T> onValueChange)
        {
            OnValueChange = onValueChange;
        }
    }
}