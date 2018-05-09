namespace DemoApplication.Repositories.Tests
{
    public class RepositoryTestConstants
    {
        public const string InsertCountry = "INSERT INTO [dbo].[Country]\r\n          " +
                                             " ([CountryId]\r\n" +
                                             ",[CountryName]\r\n " +
                                             ",[CountryCode])\r\n " +
                                             "VALUES\r\n  " +
                                             "('99999',\r\n" +
                                             "'Test Country',\r\n" +
                                             "@CountryCode)";

        public const string InsertTaxRateForCountry = "INSERT INTO [dbo].[GiftAidTaxRate]\r\n           " +
                                                       "([TaxRateId]\r\n" +
                                                       ",[CountryId]\r\n" +
                                                       ",[TaxRate])\r\n" +
                                                       "VALUES\r\n" +
                                                       "(9999,99999,15)";

        public const string DeleteCountry = "delete from [dbo].[Country]\r\n" +
                                             "where CountryId = 99999 " +
                                             "and CountryCode = @CountryCode";

        public const string DeleteCountryTaxRate = "delete from [dbo].[GiftAidTaxRate]\r\n" +
                                                    "where CountryId = 99999 " +
                                                    "and TaxRateId = 9999";
    }
}