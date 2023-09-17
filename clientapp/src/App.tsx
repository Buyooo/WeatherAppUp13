import React, { useState } from 'react';
import './App.css';
import { ForecastData } from './Models/ForecastModel';
import headerImage from './Assets/header-image.jpeg';
import Forecast from './Components/Forecast';

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
      <header
        className="App-header"
        style={{
          backgroundImage: `url(${headerImage})`, // Set the background image
          backgroundSize: 'cover', // Adjust the background size as needed
          backgroundRepeat: 'no-repeat', // Prevent image repetition
        }}
      >
        <div>
          {/* The rest of your header content */}
        </div>
      </header>

      <div className="App-body">
        <div>
          <label htmlFor="address">Enter Address:</label>
          <input
            type="text"
            id="address"
            value={address}
            onChange={handleAddressChange}
            className='address-box'
          />
          <button onClick={fetchForecast}>Fetch Forecast</button>
        </div>

        <Forecast forecast={forecast} />
      </div>

      <footer className="App-footer">
        {/* Footer content goes here */}
      </footer>
    </div>
  );
}

export default App;
