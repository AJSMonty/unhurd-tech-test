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

        var firestoreCredential = GetGoogleCredential(firebaseSettings);

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

    private static GoogleCredential GetGoogleCredential(FirebaseSettings settings)
    {
        var credentialPath = settings.CredentialPath;

        if (!File.Exists(credentialPath))
            throw new FileNotFoundException("Firebase service account JSON not found", credentialPath);

        return GoogleCredential.FromFile(credentialPath);
    }
}
