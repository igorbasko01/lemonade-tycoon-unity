namespace baskorp.Weather.Runtime {
    public class WeatherForecast {
        public override bool Equals(object obj) {
            return obj is WeatherForecast;
        }

        public override int GetHashCode() {
            return 0;
        }

    }
}