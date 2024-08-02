using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace GameLogic
{
    [Serializable]
    public class AttributeDictionary
    {
        [ShowInInspector]
        [DictionaryDrawerSettings(KeyLabel = "Key", ValueLabel = "Value")]
        protected Dictionary<EAttributeType, float> _skillAttributeDict = new Dictionary<EAttributeType, float>();
        
        public float GetValue(EAttributeType key)
        {
            float value;
            if (_skillAttributeDict.TryGetValue(key, out value) == false)
            {
                value = 0;
            }

            return value;
        }

        public void SetValue(EAttributeType key, float value)
        {
            _skillAttributeDict[key] = value;
        }

        public void AddValue(EAttributeType key, float changedValue)
        {
            float value;
            if (_skillAttributeDict.TryGetValue(key, out value) == false)
            {
                value = 0;
            }

            _skillAttributeDict[key] = value + changedValue;
        }

        public void Clear()
        {
            _skillAttributeDict.Clear();
        }
    }
}