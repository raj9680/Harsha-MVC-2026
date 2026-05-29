using ServiceContracts;

namespace Services
{
    public class CitiesService: ICitiesService
    {
        private List<string> cities;

        #region Get Accessor
        private Guid _serviceInstanceId { get; }

        public Guid ServiceInstanceId
        {
            get
            {
                return _serviceInstanceId;
            }
        }
        // Or Shorter
        // public Guid ServiceInstanceId => _serviceInstanceId;
        #endregion

        public CitiesService()
        {
            _serviceInstanceId = Guid.NewGuid();
            cities = new List<string>()
            {
                "New York",
                "Mumbai",
                "Denmark",
                "Germany",
                "Tokyo"
            };
        }

        public List<string> GetCities()
        {
            return cities;
        }
    }
}
