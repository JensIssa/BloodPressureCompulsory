﻿
using FeatureHubSDK;

namespace MeasurementService.FeatureToggle
{
    public class FeatureToggle : IFeatureToggle
    {
        EdgeFeatureHubConfig _config = null;
        private string _key = "38239dcf-4380-4e09-b999-93c9027ada96/qAXCqnqLo7GfcG72AUu1kM7XgWEHgxb4LkCuqeVU";

        public FeatureToggle()
        {
            FeatureLogging.DebugLogger += (sender, s) => Console.WriteLine("DEBUG: " + s + "\n");
            FeatureLogging.TraceLogger += (sender, s) => Console.WriteLine("TRACE: " + s + "\n");
            FeatureLogging.InfoLogger += (sender, s) => Console.WriteLine("INFO: " + s + "\n");
            FeatureLogging.ErrorLogger += (sender, s) => Console.WriteLine("ERROR: " + s + "\n");
            _config = new EdgeFeatureHubConfig("http://featurehub:8085", _key);
        }

        public Task<bool> IsFeatureEnabled(string featureName)
        {
            throw new NotImplementedException();
        }
    }
}
