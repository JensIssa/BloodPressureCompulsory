﻿namespace MeasurementService.FeatureToggle
{
    public interface IFeatureToggle
    {
        public Task<bool> IsFeatureEnabled(string featureName);

        public Task<bool> IsCountryAllowed(string country);

    }
}