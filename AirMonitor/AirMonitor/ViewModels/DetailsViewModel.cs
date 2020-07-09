using AirMonitor.Airly;
using Android.Locations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AirMonitor.ViewModels
{
    public class DetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public DetailsViewModel() => GetMeasurementAndUpdateAsync();

        async void GetMeasurementAndUpdateAsync()
        {
            Measurement measurement = await AirlyManager.GetNearestMeasurement();
            Index caqi = measurement.indexes.Where(index => index.name == "AIRLY_CAQI").FirstOrDefault();
            MeasuredValue valuePm25 = measurement.values.Where(value => value.name == "PM25").FirstOrDefault();
            MeasuredValue valuePm10 = measurement.values.Where(value => value.name == "PM10").FirstOrDefault();
            MeasuredValue valueHumidity = measurement.values.Where(value => value.name == "HUMIDITY").FirstOrDefault();
            MeasuredValue valuePressure = measurement.values.Where(value => value.name == "PRESSURE").FirstOrDefault();
            Standard standardPm10 = measurement.standards.Where(standard => standard.pollutant == "PM10").FirstOrDefault();
            Standard standardPm25 = measurement.standards.Where(standard => standard.pollutant == "PM25").FirstOrDefault();

            if (caqi != null)
            {
                CaqiValue = caqi.value.HasValue ? (int)caqi.value : 0;
                CaqiTitle = caqi.description;
                CaqiDescription = caqi.advice;
                CaquiColor = caqi.color;
            }

            if (valuePm25 != null) Pm25Value = valuePm25.value.HasValue ? (int)valuePm25.value : 0;
            if (valuePm10 != null) Pm10Value = valuePm10.value.HasValue ? (int)valuePm10.value : 0;
            if (valueHumidity != null) HumidityValue = valueHumidity.value.HasValue ? ((double)valueHumidity.value / 100d) : 0;
            if (valuePressure != null) PressureValue = valuePressure.value.HasValue ? (int)valuePressure.value : 1000;
            if (standardPm25 != null) Pm25Percent = standardPm25.percent.HasValue ? (int)standardPm25.percent : 0;
            if (standardPm10 != null) Pm10Percent = standardPm10.percent.HasValue ? (int)standardPm10.percent : 0;

        }

        private int _caqiValue = 0;
        public int CaqiValue
        {
            get => _caqiValue;
            set => SetProperty(ref _caqiValue, value);
        }

        private string _caqiTitle = "Loading...";
        public string CaqiTitle
        {
            get => _caqiTitle;
            set => SetProperty(ref _caqiTitle, value);
        }

        private string _caqiDescription = "Loading...";
        public string CaqiDescription
        {
            get => _caqiDescription;
            set => SetProperty(ref _caqiDescription, value);
        }

        private int _pm25Value = 0;
        public int Pm25Value
        {
            get => _pm25Value;
            set => SetProperty(ref _pm25Value, value);
        }

        private int _pm25Percent = 0;
        public int Pm25Percent
        {
            get => _pm25Percent;
            set => SetProperty(ref _pm25Percent, value);
        }

        private int _pm10Value = 0;
        public int Pm10Value
        {
            get => _pm10Value;
            set => SetProperty(ref _pm10Value, value);
        }

        private int _pm10Percent = 0;
        public int Pm10Percent
        {
            get => _pm10Percent;
            set => SetProperty(ref _pm10Percent, value);
        }

        private double _humidityValue = 0;
        public double HumidityValue
        {
            get => _humidityValue;
            set => SetProperty(ref _humidityValue, value);
        }

        private int _pressureValue = 1000;
        public int PressureValue
        {
            get => _pressureValue;
            set => SetProperty(ref _pressureValue, value);
        }

        private string _caquiColor = "#FFFFFF";
        public string CaquiColor
        {
            get => _caquiColor;
            set => SetProperty(ref _caquiColor, value);
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;

            field = value;

            RaisePropertyChanged(propertyName);

            return true;
        }
    }
}
