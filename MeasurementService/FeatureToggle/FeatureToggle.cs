using FeatureHubSDK;
using IO.FeatureHub.SSE.Model;

namespace MeasurementService.FeatureToggle
{
    public class FeatureToggle : IFeatureToggle
    {
        EdgeFeatureHubConfig _config = null;
        private string _key = "fcd6d2d4-5a6c-49f0-8da1-39578d1a4863/kAnYPNi5rfN7i4y8GK6UV5pmd116xPw4fcpc3pCT";

        public FeatureToggle()
        {
            FeatureLogging.DebugLogger += (sender, s) => Console.WriteLine("DEBUG: " + s + "\n");
            FeatureLogging.TraceLogger += (sender, s) => Console.WriteLine("TRACE: " + s + "\n");
            FeatureLogging.InfoLogger += (sender, s) => Console.WriteLine("INFO: " + s + "\n");
            FeatureLogging.ErrorLogger += (sender, s) => Console.WriteLine("ERROR: " + s + "\n");
            _config = new EdgeFeatureHubConfig("http://featurehub:8085", _key);
        }

        public async Task<bool> IsFeatureEnabled(string featureName)
        {
            var fh = await _config.NewContext().Build();
            if (fh[featureName].IsEnabled)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> IsCountryAllowed(string country)
        {
            StrategyAttributeCountryName SACM;
            var couldParse = Enum.TryParse(country, true, out SACM);
            if (couldParse)
            {
                var fh = await _config.NewContext().Country(SACM).Build();
                if (fh["DanishAccess"].IsEnabled)
                {
                    return true;
                }
            }
            return false;
        }
    }
}