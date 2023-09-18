import React from 'react';
import { WeatherData, WeatherDataArray } from '../Models/ForecastModel';

interface WeatherDisplayProps {
  weatherDataArray: WeatherDataArray;
}

export const WeatherDisplay: React.FC<WeatherDisplayProps> = ({ weatherDataArray }) => {
  // Helper function to render a property if it's a non-zero number
  const renderProperty = (key: string, value: number | string) => {
    if (typeof value === 'number' && value !== 0) {
      return (
        <p key={key}>
          {key}: {value.toFixed(2)}
        </p>
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
    <div>
      <h2>Weather Data Display</h2>
      {weatherDataArray.map((weatherData, index) => (
        <div key={index} className="weather-item">
          <h3>Date: {formatReadableDate(weatherData.Date)}</h3>
          {Object.entries(weatherData).map(([key, value]) => (
            <React.Fragment key={key}>{renderProperty(key, value)}</React.Fragment>
          ))}
        </div>
      ))}
    </div>
  );
};
