using System.Collections.Specialized;
using System.Configuration;

namespace Avengers.Mvc.Services
{
    public class AppSettingsWrapper
    {
        public virtual NameValueCollection GetAppSettings()
        {
            return ConfigurationManager.AppSettings;
        }
    }
}