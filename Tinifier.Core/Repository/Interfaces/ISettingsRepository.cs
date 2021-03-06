﻿namespace Tinifier.Core.Repository.Interfaces
{
    // Repository for settings with custom methods
    public interface ISettingsRepository<TSetting>
    {
        TSetting GetSettings();

        void Update(int currentMonthRequests);
    }
}
