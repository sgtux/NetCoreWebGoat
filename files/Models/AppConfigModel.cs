using NetCoreWebGoat.Config;

namespace NetCoreWebGoat.Models
{
    public class AppConfigModel
    {
        public string CspHttpHeader { get; set; }

        public AppConfigModel() { }

        public AppConfigModel(AppConfig config)
        {
            CspHttpHeader = config.CspHttpHeader;
        }
    }
}
