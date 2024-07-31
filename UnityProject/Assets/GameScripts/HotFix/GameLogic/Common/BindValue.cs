using System;

namespace GameLogic
{
    public class BindValue<T>
    {
        private Action<T> OnValueChange;
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                if (value.Equals(_value)) return;
                _value = value;
                OnValueChange?.Invoke(_value);
            }
        }

        public void InitValue(T value)
        {
            _value = value;
        }
        
        public void AddListener(Action<T> onValueChange)
        {
            OnValueChange = onValueChange;
        }
    }
}