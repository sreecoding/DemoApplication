using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApplication.Infrastructure.HealthCheck
{
    public class HealthCheckResponse
    {
        public bool IsSystemHealthy { get; set; }

        public IList<HealthCheckOutput> SubSystemHealthCheckOutputs { get; set; }

    }
}
