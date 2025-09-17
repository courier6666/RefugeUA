namespace RefugeUA.WebApp.Server.Options
{
    public class JwtSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether the issuer signing key should be validated.
        /// </summary>
        public bool ValidateIssuerSigningKey { get; set; } = true;

        /// <summary>
        /// Gets or sets the secret or RSA key used to sign the JWT.
        /// </summary>
        public string IssuerSigningKey { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the token issuer should be validated.
        /// </summary>
        public bool ValidateIssuer { get; set; } = true;

        /// <summary>
        /// Gets or sets the expected issuer of the token.
        /// </summary>
        public string ValidIssuer { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the token audience should be validated.
        /// </summary>
        public bool ValidateAudience { get; set; } = true;

        /// <summary>
        /// Gets or sets the expected audience of the token.
        /// </summary>
        public string ValidAudience { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the expiration time of the token is required.
        /// </summary>
        public bool RequireExpirationTime { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether the token's lifetime should be validated.
        /// </summary>
        public bool ValidateLifetime { get; set; } = true;
    }
}
