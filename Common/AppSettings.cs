using Microsoft.Extensions.Configuration;
using System;

namespace HR.WebApi
{
    public static class AppSettings
    {
        static AppSettings()
        {
            IConfigurationRoot objConfig = new ConfigurationBuilder()
                                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                    .AddJsonFile("appsettings.json")
                                    .Build();

            DBType = objConfig.GetValue<string>("DataBase:ConnectionType");
            RecordLimit = Convert.ToInt32(objConfig.GetValue<string>("DataBase:RecordLimit"));

            TokenValidIssuer = objConfig.GetValue<string>("JwtAuth:IssuedBy");
            TokenValidIssuer = objConfig.GetValue<string>("JwtAuth:Audience");
            TokenIssuerSigningKey = objConfig.GetValue<string>("JwtAuth:Secret");

            EmailDefaultPassword = objConfig.GetValue<string>("User:EmailDefaultPassword");
            PasswordExpiryDays = objConfig.GetValue<int>("User:PasswordExpiryDays");
            PasswordExpiryTime = objConfig.GetValue<int>("User:PasswordExpiryTime");
            PasswordResetLink = objConfig.GetValue<string>("User:PasswordResetLink");

            DocumentPath = objConfig.GetValue<string>("User:DocumentPath");
            DocumentPathBackup = objConfig.GetValue<string>("User:DocumentPathBackup");
        }

        public static string DBType { get; }
        public static int RecordLimit { get; }

        public static string TokenValidIssuer { get; }
        public static string TokenValidAudience { get; }
        public static string TokenIssuerSigningKey { get; }

        public static int PasswordExpiryDays { get; }
        public static int PasswordExpiryTime { get; }
        public static string PasswordResetLink { get; }
        public static string EmailDefaultPassword { get; }

        public static string DocumentPath { get; }

        public static string DocumentPathBackup { get; }
    }
}
