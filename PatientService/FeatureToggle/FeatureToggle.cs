
using FeatureHubSDK;
using IO.FeatureHub.SSE.Model;

namespace PatientService.FeatureToggle
{
    public class FeatureToggle : IFeatureToggle
    {
        EdgeFeatureHubConfig _config = null;
        private string _key = "38239dcf-4380-4e09-b999-93c9027ada96/qAXCqnqLo7GfcG72AUu1kM7XgWEHgxb4LkCuqeVU";

        public FeatureToggle()
        {
            _config = new EdgeFeatureHubConfig("http://featurehub:8085", _key);
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

        public async Task<bool> IsFeatureEnabled()
        {
            var fh = await _config.NewContext().Build();
            if (fh["AddMeasurement"].IsEnabled)
            {
                return true;
            }
            return false;
        }
    }
}
