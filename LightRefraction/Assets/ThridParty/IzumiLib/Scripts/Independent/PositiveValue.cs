using UnityEngine;

namespace IzumiLib
{
    /// <summary>
    /// 0-x ranged value, for supports of ratio calcuration.
    /// </summary>
    [System.Serializable]
    public class PositiveValue
    {
        //inspector
        [Min(0)]
        float _maxValue;

        //init
        public PositiveValue(float maxValue = 1)
        {
            MaxValue = maxValue;
        }
        //data
        protected float _value;

        public float MaxValue
        {
            get => _maxValue;
            set => _maxValue = value > 0 ? value : 0;
        }

        public float Value
        {
            get => _value;
            set
            {
                _value = Mathf.Clamp(_value + value, 0, _maxValue);
            }
        }
        public float Ratio
        {
            get => _value / _maxValue;
            set => Value = Mathf.Clamp01(value) * _maxValue;
        }
        public bool IsMax => _value == _maxValue;
        public bool IsMin => _value == 0;

        public void Add(float value)
        {
            Value += value;
        }
        public virtual void ToMin()
        {
            _value = 0;
        }
        public void ToMax()
        {
            _value = _maxValue;
        }
        public void RatioKeepingMaxValueSet(float maxValue)
        {
            float ratio = Ratio;
            MaxValue = maxValue;
            Ratio = ratio;
        }
        public void RatioKeepingMaxValueAdd(float maxValueAdd)
        {
            RatioKeepingMaxValueSet(_maxValue + maxValueAdd);
        }
    }
}