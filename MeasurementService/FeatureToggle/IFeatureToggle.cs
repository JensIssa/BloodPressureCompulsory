namespace MeasurementService.FeatureToggle
{
    public interface IFeatureToggle
    {
        public Task<bool> IsFeatureEnabled(string featureName);
    }
}
