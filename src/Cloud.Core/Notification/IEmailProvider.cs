﻿namespace Cloud.Core.Notification
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>Interface for Email notification providers.</summary>
    public interface IEmailProvider : INamedInstance
    {
        /// <summary>Sent an email synchronously.</summary>
        /// <param name="email">The email to send.</param>
        /// <returns><c>True</c> if sent successfully, <c>false</c> otherwise.</returns>
        /// <exception cref="Exceptions.RequestFailedException{T}">Error during processing.</exception>
        bool Send(EmailMessage email);

        /// <summary>Sends an email asynchronously.</summary>
        /// <param name="email">The email to send.</param>
        /// <returns>Task&lt;System.Boolean&gt;. <c>True</c> if sent successfully, <c>false</c> otherwise.</returns>
        /// <exception cref="Exceptions.RequestFailedException{T}">Error during processing.</exception>
        Task<bool> SendAsync(EmailMessage email);
    }

    /// <summary>Email message.</summary>
    public class EmailMessage
    {
        /// <summary>Gets or sets the recipient list (send as blind carbon copy).</summary>
        /// <value>List of string recipients.</value>
        public List<string> To { get; } = new List<string>();

        /// <summary>Gets or sets the email subject.</summary>
        /// <value>The email subject.</value>
        public string Subject { get; set; }

        /// <summary>Gets or sets the name of the email template to use.</summary>
        /// <value>The name of the template.</value>
        public string TemplateName { get; set; }

        /// <summary>Gets or sets the email content.</summary>
        /// <value>The email content.</value>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is plain text.
        /// </summary>
        /// <value><c>true</c> if this instance is plain text; otherwise, <c>false</c>.</value>
        public bool IsPlainText { get; set; }

        /// <summary>Gets or sets the email attachments.</summary>
        /// <value>The attachments.</value>
        public List<EmailAttachment> Attachments { get; } = new List<EmailAttachment>();
    }

    /// <summary>Email attachment.</summary>
    public class EmailAttachment
    {
        /// <summary>Attachment file name.</summary>
        public string Name { get; set; }

        /// <summary>Attachment content type, example 'application/pdf'.</summary>
        public string ContentType { get; set; }

        /// <summary>Content of the attachment.</summary>
        public Stream Content { get; set; }
    }
}