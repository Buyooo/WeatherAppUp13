import React from 'react';
import { ForecastData, Temperature, ValueItem } from '../Models/ForecastModel';

interface ForecastProps {
    forecast: ForecastData | null;
}

const Forecast: React.FC<ForecastProps> = ({ forecast }) => {
    if (!forecast) {
        return null; // If there's no forecast data, don't render anything
    }

    // Helper function to group temperature values by day
    const groupTemperaturesByDay = (temperatures: ValueItem[]) => {
        const groupedTemperatures: { [day: string]: ValueItem[] } = {};

        temperatures.forEach((item) => {
            const day = item.ValidTime.split('T')[0]; // Extract the date part as the day
            if (!groupedTemperatures[day]) {
                groupedTemperatures[day] = [];
            }
            groupedTemperatures[day].push(item);
        });

        return groupedTemperatures;
    };

    // Helper function to calculate the average temperature for a day
    const calculateAverageTemperature = (temperatures: ValueItem[]) => {
        if (temperatures.length === 0) {
            return 0;
        }

        const total = temperatures.reduce((sum, temp) => sum + temp.Value, 0);
        return total / temperatures.length;
    };

    // Group temperature values by day
    const groupedTemperatures = groupTemperaturesByDay(
        forecast.Properties.Temperature.Values
    );

    return (
        <div>
            <h2>Forecast Data</h2>
            <p>UpdateTime: {forecast.Properties.UpdateTime}</p>

            <h3>Temperature</h3>
            <div className="temperature-grid">
                {Object.entries(groupedTemperatures).map(([day, temperatures], index) => (
                    <div key={index} className="temperature-panel">
                        <h4>Day: {day}</h4>
                        <p>Avg Temperature: {calculateAverageTemperature(temperatures).toFixed(2)} °C</p>
                    </div>
                ))}
            </div>
        </div>
    );
}

export default Forecast;
