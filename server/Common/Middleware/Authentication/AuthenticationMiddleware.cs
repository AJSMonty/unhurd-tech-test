using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
public static class FirebaseAuthenticationExtensions
{
    public static void AddFirebaseAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var firebaseSettings = configuration
                                   .GetSection("Firebase")
                                   .Get<FirebaseSettings>()
                               ?? throw new ArgumentException(nameof(FirebaseSettings));

        var firestoreCredential = GetGoogleCredential(firebaseSettings, configuration);

        var tokenAuthority = $"https://securetoken.google.com/{firebaseSettings.ProjectId}";

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = tokenAuthority;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = tokenAuthority,
                    ValidateAudience = true,
                    ValidAudience = firebaseSettings.ProjectId,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        return Task.CompletedTask;
                    }
                };
            });
    }

    private static GoogleCredential GetGoogleCredential(FirebaseSettings settings, IConfiguration configuration)
    {
        var credentialPath = settings.CredentialPath;

        if (!string.IsNullOrWhiteSpace(credentialPath) && File.Exists(credentialPath))
        {
            return GoogleCredential.FromFile(credentialPath);
        }

        var firebaseJson = configuration["FirebaseCredentials"];

        if (string.IsNullOrWhiteSpace(firebaseJson))
        {
            throw new Exception("FirebaseCredentials not found in configuration or local path.");
        }

        var jsonStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(firebaseJson));
        return GoogleCredential.FromStream(jsonStream);
    }

}
