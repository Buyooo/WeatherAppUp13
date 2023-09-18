export interface WeatherData {
    Date: string;
    MaxTemperatureAverage: number;
    TemperatureAverage: number;
    MinTemperatureAverage: number;
    DewpointAverage: number;    
    RelativeHumidityAverage: number;
    HeatIndexAverage: number;
    WindChillAverage: number;
    SkyCoverAverage: number;
    WindDirectionAverage: number;
    WindSpeedAverage: number;
    WindGustAverage: number;
    ProbabilityOfPrecipitationAverage: number;
  }
  
  // Define an array type using the WeatherData interface
  export type WeatherDataArray = WeatherData[];