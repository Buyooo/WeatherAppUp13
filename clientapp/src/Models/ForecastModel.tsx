export interface ValueItem {
    ValidTime: string;
    Value: number;
}

export interface Temperature {
    uom: string;
    Values: ValueItem[];
}

export interface Dewpoint {
    uom: string;
    Values: ValueItem[];
}

export interface MaxTemperature {
    uom: string;
    Values: ValueItem[];
}

export interface MinTemperature {
    uom: string;
    Values: ValueItem[];
}

export interface RelativeHumidity {
    uom: string;
    Values: ValueItem[];
}

export interface HeatIndex {
    uom: string;
    Values: {
        ValidTime: string;
        Value?: number;
    }[];
}

export interface WindChill {
    uom: string;
    Values: {
        ValidTime: string;
        Value?: number;
    }[];
}

export interface SkyCover {
    uom: string;
    Values: ValueItem[];
}

export interface WindDirection {
    uom: string;
    Values: ValueItem[];
}

export interface WindSpeed {
    uom: string;
    Values: ValueItem[];
}

export interface WindGust {
    uom: string;
    Values: ValueItem[];
}

export interface ProbabilityOfPrecipitation {
    uom: string;
    Values: ValueItem[];
}

export interface Properties {
    UpdateTime: string;
    ValidTimes: string;
    Temperature: Temperature;
    Dewpoint: Dewpoint;
    MaxTemperature: MaxTemperature;
    MinTemperature: MinTemperature;
    RelativeHumidity: RelativeHumidity;
    HeatIndex: HeatIndex;
    WindChill: WindChill;
    SkyCover: SkyCover;
    WindDirection: WindDirection;
    WindSpeed: WindSpeed;
    WindGust: WindGust;
    ProbabilityOfPrecipitation: ProbabilityOfPrecipitation;
}

export interface ForecastData {
    Properties: Properties;
}
