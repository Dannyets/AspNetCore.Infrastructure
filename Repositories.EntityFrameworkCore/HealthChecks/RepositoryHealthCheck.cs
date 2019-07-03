using AspNetCore.Infrastructure.Repositories.EntityFrameworkCore.Models;
using AspNetCore.Infrastructure.Repositories.EntityFrameworkCore.Models.Interfaces;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCore.Infrastructure.Repositories.EntityFrameworkCore.HealthChecks
{
    public class RepositoryHealthCheck<T> : IHealthCheck where T : BaseDbEntity
    {
        private readonly IEfRepository<T> _repository;

        public RepositoryHealthCheck(IEfRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var isRepositoryHealthy = false;

            try
            {
                isRepositoryHealthy = await _repository.CheckHealth();
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy(ex.Message);
            }

            var checkStatus = isRepositoryHealthy
                ? HealthCheckResult.Healthy()
                : HealthCheckResult.Unhealthy();

            return checkStatus;
        }
    }
}
