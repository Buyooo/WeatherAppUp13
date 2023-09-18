export interface WeatherData {
    DewpointAverage: number;
    MaxTemperatureAverage: number;
    MinTemperatureAverage: number;
    RelativeHumidityAverage: number;
    HeatIndexAverage: number;
    WindChillAverage: number;
    SkyCoverAverage: number;
    WindDirectionAverage: number;
    WindSpeedAverage: number;
    WindGustAverage: number;
    ProbabilityOfPrecipitationAverage: number;
    Date: string;
    TemperatureAverage: number;
  }
  
  // Define an array type using the WeatherData interface
  export type WeatherDataArray = WeatherData[];