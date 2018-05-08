﻿using System.Collections.Generic;
using DemoApplication.Domain;
using DemoApplication.Repositories;

namespace DemoApplication.Infrastructure.HealthCheck
{
    public interface IHealthCheckFactory
    {
        IList<HealthCheckOutput> GenerateHealthCheckOutputs();
    }

    public class HealthCheckFactory : IHealthCheckFactory
    {
        private readonly IDatabaseCheck _databaseConnection;

        public HealthCheckFactory(IDatabaseCheck databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public IList<HealthCheckOutput> GenerateHealthCheckOutputs()
        {
            var healthCheckOutputs = new List<HealthCheckOutput>
            {
                new HealthCheckOutput() {DependencyName = HealthCheckConstants.SubSystem.SqlDatabase, IsHealthy = _databaseConnection.CheckConnection()}
            };

            return healthCheckOutputs;
        }
    }
}