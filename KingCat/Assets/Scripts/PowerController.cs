using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class PowerController : MonoBehaviour
{
    [Serializable]
    public class PowerDuration
    {
        public float speedBoostDuration;
        public float stunBoostDuration;
        public float hairballBoostDuration;
        public float stunImmunityDuration;
        public float laserPointerDuration;
        public float bootsDuration;
    }
    
    [Serializable]
    public class PowerPercent
    {
        public float speedBoostPercent;
        public float stunBoostPercent;
        public float hairballBoostPercent;
    }

    public class PowerTimer
    {
        private Powers powers;
        private List<(FieldInfo, string)> timerFields;

        public PowerTimer(Powers p)
        {
            powers = p;
            timerFields = new List<(FieldInfo, string)>();

            FieldInfo[] fields = powers.GetType().GetFields();
            for(int i=0;i<fields.Length;i++)
            {
                string fieldName = fields[i].Name;
                if(fieldName.Contains("Timer")) //if powers has field with word 'Timer', keep it in a local list
                {
                    var powerField = fields[i];
                    timerFields.Add((powerField, fieldName.Substring(0, fieldName.IndexOf("Timer"))));
                    powerField.SetValue(powers, 0); //initialize to 0
                }
            }
        }

        public void decrementPowerTimers()
        {
            for(int i=0;i<timerFields.Count;i++) //check that local list for nonzero timers, decrement them, change state if required.
            {
                FieldInfo field = timerFields[i].Item1;
                string powerName = timerFields[i].Item2;
                float timerVal = (float)field.GetValue(powers);
                if(timerVal > 0)
                {
                    if(timerVal <= Time.deltaTime) //timer ran out
                    {
                        powers.GetType().GetField(powerName).SetValue(powers, false); //toggle power bool
                        field.SetValue(powers, 0); //set timer to 0
                    }
                    else
                    {
                        field.SetValue(powers, timerVal-Time.deltaTime);
                    }
                }
            }
        }
    }
    
    public class Powers
    { //Requires following naming convention (X, XTimer, XDuration, XPercent) in any ordering
        public bool speedBoost;
        public float speedBoostPercent;
        public float speedBoostDuration;
        public float speedBoostTimer;

        public bool stunBoost;
        public float stunBoostPercent;
        public float stunBoostDuration;
        public float stunBoostTimer;
        
        public bool hairballBoost;
        public float hairballBoostPercent;
        public float hairballBoostDuration;
        public float hairballBoostTimer;

        public bool stunImmunity;
        public float stunImmunityDuration;
        public float stunImmunityTimer;

        public bool whistle;

        public bool laserPointer;
        public float laserPointerDuration;
        public float laserPointerTimer;

        public bool boots;
        public float bootsDuration;
        public float bootsTimer;

        private PowerTimer timers;
        public Powers(PowerDuration durations, PowerPercent percents)
        {
            timers = new PowerTimer(this);
            speedBoost = false;
            hairballBoost = false;
            stunBoost = false;
            stunImmunity = false;
            whistle = false;
            laserPointer = false;
            boots = false;

            speedBoostDuration    = durations.speedBoostDuration;
            stunBoostDuration     = durations.stunBoostDuration;
            hairballBoostDuration = durations.hairballBoostDuration;
            stunImmunityDuration  = durations.stunImmunityDuration;
            laserPointerDuration  = durations.laserPointerDuration;
            bootsDuration         = durations.bootsDuration;

            speedBoostPercent     = percents.speedBoostPercent;
            stunBoostPercent      = percents.stunBoostPercent;
            hairballBoostPercent  = percents.hairballBoostPercent;

        }
        public void decrementPowerTimers()
        {
            timers.decrementPowerTimers();
        }

        public void setActive(string power)
        {
            var powerProperty = this.GetType().GetField(power);
            if(powerProperty == null)
            {
                Debug.Log("tried to set a power that doesn't exist: " + power);
                return;
            }
            else
            {
                powerProperty.SetValue(this, true);
            }

            var durationProperty= this.GetType().GetField(power+"Duration");
            var timerProperty = this.GetType().GetField(power+"Timer"); /////////////////////////
            if(durationProperty != null && timerProperty != null)
                timerProperty.SetValue(this, durationProperty.GetValue(this));
        }
    }

    public PowerDuration powerDurations = new PowerDuration();
    public PowerPercent powerPercents = new PowerPercent();
    public Powers powers;

    // Start is called before the first frame update
    void Start()
    {
        powers = new Powers(powerDurations, powerPercents);
        
    }

    // Update is called once per frame
    void Update()
    {
        powers.decrementPowerTimers();
        
    }
}
