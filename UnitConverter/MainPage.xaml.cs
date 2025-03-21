using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace UnitConverter
{
    public partial class MainPage : ContentPage
    {
        Dictionary<string, List<string>> unitOptions = new Dictionary<string, List<string>>
        {
            { "Speed", new List<string> { "Miles per Hour (MPH)", "Kilometers per Hour (KPH)", "Meters per Second (m/s)", "Knots" } },
            { "Distance", new List<string> { "Miles", "Kilometers", "Meters", "Yards" } }
        };

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnConversionTypeChanged(object sender, EventArgs e)
        {
            if (conversionTypePicker.SelectedIndex == -1) return;

            string selectedType = conversionTypePicker.SelectedItem.ToString();
            fromUnitPicker.ItemsSource = unitOptions[selectedType];
            toUnitPicker.ItemsSource = unitOptions[selectedType];
        }

        private void OnConvertClicked(object sender, EventArgs e)
        {
            if (fromUnitPicker.SelectedIndex == -1 || toUnitPicker.SelectedIndex == -1 || string.IsNullOrWhiteSpace(valueEntry.Text))
            {
                resultLabel.Text = "Please select units and enter a value.";
                return;
            }

            double value;
            if (!double.TryParse(valueEntry.Text, out value))
            {
                resultLabel.Text = "Invalid number.";
                return;
            }

            string fromUnit = fromUnitPicker.SelectedItem.ToString();
            string toUnit = toUnitPicker.SelectedItem.ToString();
            double convertedValue = ConvertUnits(value, fromUnit, toUnit);

            resultLabel.Text = $"{value} {fromUnit} = {convertedValue:F2} {toUnit}";

            ResetValues();

        }

        private double ConvertUnits(double value, string fromUnit, string toUnit)
        {
            Dictionary<string, double> speedConversion = new Dictionary<string, double>
            {
                { "Miles per Hour (MPH)", 1 },
                { "Kilometers per Hour (KPH)", 1.60934 },
                { "Meters per Second (m/s)", 0.44704 },
                { "Knots", 0.868976 }
            };

            Dictionary<string, double> distanceConversion = new Dictionary<string, double>
            {
                { "Miles", 1 },
                { "Kilometers", 1.60934 },
                { "Meters", 1609.34 },
                { "Yards", 1760 }
            };

            if (speedConversion.ContainsKey(fromUnit) && speedConversion.ContainsKey(toUnit))
            {
                return value * (speedConversion[toUnit] / speedConversion[fromUnit]);
            }
            else if (distanceConversion.ContainsKey(fromUnit) && distanceConversion.ContainsKey(toUnit))
            {
                return value * (distanceConversion[toUnit] / distanceConversion[fromUnit]);
            }

            return 0;

           


        }

        private void ResetValues()
        {
            // Reset the Entry and Pickers to default values
            valueEntry.Text = string.Empty;
            conversionTypePicker.SelectedIndex = -1;
            fromUnitPicker.SelectedIndex = -1;
            toUnitPicker.SelectedIndex = -1;

            //// Set result label back to a default message
            //resultLabel.Text = "Enter value to convert.";
        }
    }


}

