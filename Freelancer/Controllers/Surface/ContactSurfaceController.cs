using Freelancer.Configuration;
using Freelancer.Models.ViewModels;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Mail;
using Umbraco.Cms.Core.Models.Email;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace Freelancer.Controllers.Surface;

public class ContactSurfaceController : SurfaceController
{
    private readonly IEmailSender _emailSender;
    private readonly ILogger<ContactSurfaceController> _logger;
    private readonly FreelancerConfig _freelancerConfig;

    public ContactSurfaceController(
        IUmbracoContextAccessor umbracoContextAccessor,
        IUmbracoDatabaseFactory databaseFactory,
        ServiceContext services,
        AppCaches appCaches,
        IProfilingLogger profilingLogger,
        IPublishedUrlProvider publishedUrlProvider,
        IEmailSender emailSender,
        ILogger<ContactSurfaceController> logger,
        IOptions<FreelancerConfig> freelancerConfig) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
    {
        _emailSender = emailSender;
        _logger = logger;
        _freelancerConfig = freelancerConfig.Value;
    }

    public async Task<IActionResult> Submit(ContactViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return CurrentUmbracoPage();
        }

        try
        {
            var subject = string.Format("Enquiry from: {0} - {1}", model.Name, model.Email);
            EmailMessage message = new(_freelancerConfig?.EmailSettings?.From,
                _freelancerConfig?.EmailSettings?.To, subject, model.Message, false);
            await _emailSender.SendAsync(message, emailType: "Contact");

            TempData["ContactSuccess"] = true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Contact Form Submission Error");
            TempData["ContactSuccess"] = false;
        }

        return RedirectToCurrentUmbracoPage();
    }
}