using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace AirMonitor.Airly
{
    [Serializable]
    public class Measurements
    {
        public Measurement current;
        public Measurement[] history;
        public Measurement[] forecast;
    }

    [Serializable]
    public class Measurement
    {
        public string fromDateTime;
        public string tillDateTime;
        public MeasuredValue[] values;
        public Index[] indexes;
        public Standard[] standards;
    }

    [Serializable]
    public class Standard
    {
        public string name;
        public string pollutant;
        public double? limit;
        public double? percent;
        public string averaging;
    }

    [Serializable]
    public class Index
    {
        public string name;
        public double? value;
        public string level;
        public string description;
        public string advice;
        public string color;
    }

    [Serializable]
    public class MeasuredValue
    {
        public string name;
        public double? value;
    }
}
