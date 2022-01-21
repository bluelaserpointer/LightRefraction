using UnityEngine;

namespace IzumiLib
{
    /// <summary>
    /// Manual charge cooldown system.<br/>
    /// <br/>
    /// Basic usage: <br/>
    /// public Cooldown cd = new Cooldown(30); // every 30 charge amount<br/>
    /// <br/>
    /// void Update() {<br/>
    ///     if(cd.AddDeltaTimeAndConsumeReady())<br/> 
    ///         Something(); // invoked every 30 seconds<br/>
    /// }<br/>
    /// </summary>
    [System.Serializable]
    public class Cooldown
    {
        //inspector
        [SerializeField]
        PositiveValue _chargeValue;

        //init
        public Cooldown(float maxValue = 1)
        {
            _chargeValue = new PositiveValue(maxValue);
        }

        //data
        public float MaxValue
        {
            get => _chargeValue.MaxValue;
            set => _chargeValue.MaxValue = value;
        }
        public float Value
        {
            get => _chargeValue.Value;
            set => _chargeValue.Value = value;
        }
        public float Ratio
        {
            get => _chargeValue.Ratio;
            set => _chargeValue.Ratio = value; 
        }
        public bool IsReady => _chargeValue.IsMax;
        public bool AddDeltaTimeAndConsumeReady(bool useFixedDeltaTime = false)
        {
            AddDeltaTime(useFixedDeltaTime);
            return ConsumeReady();
        }
        public bool AddAndConsumeReady(float time)
        {
            Add(time);
            return ConsumeReady();
        }
        /// <summary>
        /// If ready, reset it and returns true.
        /// </summary>
        /// <returns></returns>
        public bool ConsumeReady()
        {
            if (IsReady)
            {
                Reset();
                return true;
            }
            return false;
        }
        public void Reset()
        {
            _chargeValue.ToMin();
        }
        public void FullCharge()
        {
            _chargeValue.ToMax();
        }
        public void Add(float time)
        {
            _chargeValue.Add(time);
        }
        public void AddDeltaTime(bool useFixedDeltaTime = false)
        {
            Add(useFixedDeltaTime ? Time.fixedDeltaTime : Time.deltaTime);
        }
    }
}