namespace PatientService.FeatureToggle
{
    public interface IFeatureToggle
    {
        public Task<bool> IsCountryAllowed(string country);
        public Task<bool> IsFeatureEnabled();
    }
}
