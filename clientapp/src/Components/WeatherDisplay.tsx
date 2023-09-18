import React from 'react';
import { WeatherData, WeatherDataArray } from '../Models/ForecastModel';

interface WeatherDisplayProps {
  weatherDataArray: WeatherDataArray;
}

export const WeatherDisplay: React.FC<WeatherDisplayProps> = ({ weatherDataArray }) => {
  // Slice the array to display up to 7 objects
  const displayedWeatherData = weatherDataArray.slice(0, 7);

  return (
    <div>
      <h2>Weather Data Display</h2>
      {displayedWeatherData.map((weatherData, index) => (
        <div key={index} className="weather-item">
          <h3>Date: {weatherData.Date}</h3>
          <p>Avg Temperature: {weatherData.TemperatureAverage.toFixed(2)} °C</p>
          {/* Display other weather data properties as needed */}
        </div>
      ))}
    </div>
  );
};
