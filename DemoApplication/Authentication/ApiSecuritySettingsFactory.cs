using System;
using System.Collections.Generic;
using System.Linq;
using Asos.Identity.Core.Api.Authentication.Configuration;
using static DemoApplication.AppSettingsReader;

namespace DemoApplication
{
    public class ApiSecuritySettingsFactory
    {
        private const string XSiteOrigin = "Api.XSiteOrigin";
        private const string WhiteListKey = "Api.WhitelistedServiceIdentifiers";
        private const string AuthenticationEnabled = "Api.AuthorizationLockdownEnabled";
        private const string SslRequired = "Api.RequireSsl";
        private const string SendDetailedErrorMessages = "Api.SendDetailedErrorMessages";
        private const string IdentityTokenissuerIssuerUris = "Identity.TokenIssuer.IssuerUris";

        public ApiSecuritySettings Create()
        {
            var securitySettings = new ApiSecuritySettings
            {
                AuthorizationLockdownEnabled = GetBooleanFromConfig(AuthenticationEnabled),
                RequireSsl = GetBooleanFromConfig(SslRequired),
                SendDetailedErrorMessages = GetBooleanFromConfig(SendDetailedErrorMessages),
                SiteOrigin = ReadAppSetting(XSiteOrigin)
            };

            securitySettings.IssuerUris.AddRange(ReadCommaSepararated(IdentityTokenissuerIssuerUris));
            securitySettings.Audiences.AddRange(ReadCommaSepararated("Identity.TokenIssuer.Audiences"));
            securitySettings.JsonWebKeysDiscoveryEndpoints.AddRange(GetJsonWebKeysDiscoveryEndpoints());

            AppendWhiteListedObjectIdServices(securitySettings);

            return securitySettings;
        }

        private static IEnumerable<Uri> GetJsonWebKeysDiscoveryEndpoints()
        {
            return ReadCommaSepararated("Identity.JsonWebKeysDiscoveryEndpoints").Select(uri => new Uri(uri));
        }

        private static void AppendWhiteListedObjectIdServices(ApiSecuritySettings securitySettings)
        {
            foreach (var identifier in ReadCommaSepararated(WhiteListKey))
            {
                securitySettings.WhitelistedServiceIdentifiers.Add(identifier);
            }
        }
    }
}