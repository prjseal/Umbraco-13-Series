using Umbraco.Cms.Core.Services;

namespace Freelancer.Testing;

public class TestClass(IMediaService mediaService)
{
    public void DoSomething()
    {
        var thing = mediaService.GetById(1);
    }
}