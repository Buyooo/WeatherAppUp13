import React from 'react';
import { ForecastData } from '../Models/ForecastModel';

interface ForecastProps {
  forecast: ForecastData | null;
}

const Forecast: React.FC<ForecastProps> = ({ forecast }) => {
  if (!forecast) {
    return null; // If there's no forecast data, don't render anything
  }

  return (
    <div>
      <h2>Forecast Data</h2>
      <p>UpdateTime: {forecast.Properties.UpdateTime}</p>
      <p>ValidTimes: {forecast.Properties.ValidTimes}</p>

      {/* Display other properties as needed */}

      {/* Example for Temperature */}
      <h3>Temperature</h3>
      <p>UOM: {forecast.Properties.Temperature.uom}</p>
      <ul>
        {forecast.Properties.Temperature.Values.map((item, index) => (
          <li key={index}>
            ValidTime: {item.ValidTime}, Value: {item.Value}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default Forecast;
