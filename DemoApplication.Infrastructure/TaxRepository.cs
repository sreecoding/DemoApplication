using System;
using System.Collections.Generic;

namespace DemoApplication.Services
{
    public interface ITaxRepository
    {
        IEnumerable<TaxData> GetTaxRate(string uk);
    }

    public class TaxRepository : ITaxRepository
    {
        public IEnumerable<TaxData> GetTaxRate(string uk)
        {
            return null;

        }
    }

    public class TaxData
    {
        public string Country;
        public decimal TaxRate;
    }
}