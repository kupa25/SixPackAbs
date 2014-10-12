using System;
using System.Windows;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class SportsIPractiseDataSource : DataSourceBase<SportsIPractiseSchema>
    {
        private const string _appId = "4c1a6d42-5cf2-4aa9-b4b7-a04baeaa5eb9";
        private const string _dataSourceName = "5c305a69-c25c-422f-8d08-ed6a74250b69";

        protected override string CacheKey
        {
            get { return "SportsIPractiseDataSource"; }
        }

        public override bool HasStaticData
        {
            get { return false; }
        }

        public async override Task<IEnumerable<SportsIPractiseSchema>> LoadDataAsync()
        {
            try
            {
                var serviceDataProvider = new ServiceDataProvider(_appId, _dataSourceName);
                return await serviceDataProvider.Load<SportsIPractiseSchema>();
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("SportsIPractiseDataSource.LoadData", ex.ToString());
                return new SportsIPractiseSchema[0];
            }
        }
    }
}
