namespace RefugeUA.WebApp.Server.Options
{
    /// <summary>
    /// Represents the configuration settings for email functionality.
    /// </summary>
    public class EmailSettings
    {
        /// <summary>
        /// Gets or sets the email address that will be used as the sender.
        /// </summary>
        public string FromEmail { get; set; } = default!;

        /// <summary>
        /// Gets or sets the password associated with the sender email address.
        /// </summary>
        public string Password { get; set; } = default!;
    }
}
