namespace baskorp.Weather.Runtime {
    public class WeatherForecast {
        public SkyType SkyType { get; private set; }
        public TemperatureType TemperatureType { get; private set; }

        public WeatherForecast(SkyType skyType, TemperatureType temperatureType) {
            SkyType = skyType;
            TemperatureType = temperatureType;
        }

        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType()) {
                return false;
            }

            var other = (WeatherForecast) obj;
            return SkyType == other.SkyType && TemperatureType == other.TemperatureType;
        }

        public override int GetHashCode() {
            return SkyType.GetHashCode() ^ TemperatureType.GetHashCode();
        }

    }

    public enum SkyType {
        Clear,
        Rainy
    }

    public enum TemperatureType {
        Hot,
        Cold,
        Mild
    }
}