import React from 'react';
import { WeatherDataArray } from '../Models/ForecastModel';

interface WeatherDisplayProps {
  weatherDataArray: WeatherDataArray;
}

export const WeatherDisplay: React.FC<WeatherDisplayProps> = ({ weatherDataArray }) => {
  // Helper function to render a property if it's a non-zero number
  const renderProperty = (key: string, value: number | string) => {
    // Map property keys to more readable names, categories, and units
    const propertyMap: { [key: string]: { category: string; displayName: string; unit: string } } = {
      DewpointAverage: {
        category: 'Dewpoint',
        displayName: 'Dewpoint Avg',
        unit: '°C', // Add the unit for Dewpoint
      },
      MaxTemperatureAverage: {
        category: 'Temperature',
        displayName: 'Max Temp Avg',
        unit: '°C', // Add the unit for Max Temperature
      },
      MinTemperatureAverage: {
        category: 'Temperature',
        displayName: 'Min Temp Avg',
        unit: '°C', // Add the unit for Min Temperature
      },
      RelativeHumidityAverage: {
        category: 'Humidity',
        displayName: 'Relative Humidity Avg',
        unit: '%', // Add the unit for Relative Humidity
      },
      HeatIndexAverage: {
        category: 'Temperature',
        displayName: 'Heat Index Avg',
        unit: '°C', // Add the unit for Heat Index
      },
      SkyCoverAverage: {
        category: 'Sky',
        displayName: 'Sky Cover Avg',
        unit: '%', // Add the unit for Sky Cover
      },
      WindDirectionAverage: {
        category: 'Wind',
        displayName: 'Wind Direction Avg',
        unit: '°', // Add the unit for Wind Direction
      },
      WindSpeedAverage: {
        category: 'Wind',
        displayName: 'Wind Speed Avg',
        unit: 'm/s', // Add the unit for Wind Speed
      },
      WindGustAverage: {
        category: 'Wind',
        displayName: 'Wind Gust Avg',
        unit: 'm/s', // Add the unit for Wind Gust
      },
      TemperatureAverage: {
        category: 'Temperature',
        displayName: 'Temp Avg',
        unit: '°C', // Add the unit for Temperature
      },
      ProbabilityOfPrecipitationAverage: {
        category: 'Precipitation',
        displayName: 'Precipitation Avg',
        unit: '%', // Add the unit for Probability Of Precipitation
      },
    };

    const { displayName, unit } = propertyMap[key] || { category: 'Other', displayName: key, unit: '' };

    if (typeof value === 'number' && value !== 0) {
      return (
        <div className="property-item" key={key}>
          <div className="property-header">{displayName}:</div>
          <div className="property-value">
            {value.toFixed(2)} {unit}
          </div>
        </div>
      );
    }
    return null;
  };

  // Helper function to format the date to a human-readable format
  const formatReadableDate = (dateString: string) => {
    const date = new Date(dateString);
    const options: Intl.DateTimeFormatOptions = {
      weekday: 'long',
      month: 'long',
      day: 'numeric',
    };
    return date.toLocaleDateString('en-US', options);
  };

  return (
    <div className="weather-display">
      {weatherDataArray.map((weatherData, index) => (
        <div key={index} className="weather-item">
          <h3>Date: {formatReadableDate(weatherData.Date)}</h3>
          <div className="weather-properties">
            {Object.entries(weatherData).map(([key, value]) => (
              <React.Fragment key={key}>{renderProperty(key, value)}</React.Fragment>
            ))}
          </div>
        </div>
      ))}
    </div>
  );
};
