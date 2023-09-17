import React, { useState } from 'react';
import './App.css';
import { ForecastData } from './Models/ForecastModel';

function App() {
  // State variables
  const [address, setAddress] = useState<string>('');
  const [forecast, setForecast] = useState<ForecastData | null>(null);

  // Function to handle address input changes
  const handleAddressChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setAddress(event.target.value);
  };
  
  // Function to fetch forecast data from the API
  const fetchForecast = async () => {
    try {
      const response = await fetch(`http://localhost:5200/Geocoding/Get7DayForecast?address=${encodeURIComponent(address)}`);
      if (response.ok) {
        const data = await response.json();
        setForecast(data as ForecastData);
      } else {
        // Handle error response
        console.error('Error fetching forecast data');
      }
    } catch (error) {
      console.error('Error fetching forecast data', error);
    }
  };

  return (
    <div className="App">
      <header className="App-header">
        <div>
          <label htmlFor="address">Enter Address:</label>
          <input
            type="text"
            id="address"
            value={address}
            onChange={handleAddressChange}
          />
          <button onClick={fetchForecast}>Fetch Forecast</button>
        </div>
      </header>

      <div className="App-body">
        {forecast && (
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
        )}
      </div>

      <footer className="App-footer">
        {/* Footer content goes here */}
      </footer>
    </div>
  );
}

export default App;
