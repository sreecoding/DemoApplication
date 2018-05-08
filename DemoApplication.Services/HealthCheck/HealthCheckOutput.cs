using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApplication.Infrastructure.HealthCheck
{
    public class HealthCheckOutput
    {
        public bool IsHealthy { get; set; }

        public string DependencyName { get; set; }
    }
}
