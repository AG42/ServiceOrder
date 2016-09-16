using System.Configuration;
using Configuration = Order.Common.Configuration;


namespace Order.DataLayer
{
    class ConfigReader
    {
        private const string BASEURI_KEY = "BaseUri";
        private const string DENODO_USERNAME_KEY = "DenodoUsername";
        private const string DENODO_PASSWORD_KEY = "DenodoPassword";
        private const string Order_VIEWURI_KEY = "OrderViewUri";
        private string ServiceName { get; set; }
        private string Environment { get; set; }
        public string ConfigurationDbConnectionString { get; set; }
        public string BaseUri { get; set; }
        public string DenodoUsername { get; private set; }
        public string DenodoPassword { get; private set; }
        public string OrderViewUri { get; private set; }
        public ConfigReader(bool readFromDatabase)
        {   
            ServiceName = ReadConfig("ServiceName");
            Environment = ReadConfig("Environment");
            if (readFromDatabase)
                InitializeFromDatabase();
            else
                InitializeFromConfig();
        }
        private string ReadConfig(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        private void InitializeFromConfig()
        {
            BaseUri = ReadConfig(BASEURI_KEY);
            DenodoUsername = ReadConfig(DENODO_USERNAME_KEY);
            DenodoPassword = ReadConfig(DENODO_PASSWORD_KEY);
            OrderViewUri = ReadConfig(Order_VIEWURI_KEY);
        }
        private void InitializeFromDatabase()
        {
            string configurationDbConnectionString = ReadConfig("ConfigurationDbConnectionString");
            var configuration = new Configuration(configurationDbConnectionString);
            var configurationDictionary = configuration.GetConfiguration(ServiceName, Environment);
            BaseUri = configurationDictionary[BASEURI_KEY];
            DenodoUsername = configurationDictionary[DENODO_USERNAME_KEY];
            DenodoPassword = configurationDictionary[DENODO_PASSWORD_KEY];
            OrderViewUri = configurationDictionary[Order_VIEWURI_KEY];
        }

    }
}
