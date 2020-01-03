using System;
using Microsoft.AspNetCore.Mvc;
using Redirector.Models;

namespace Redirector.Controllers
{
    public class RedirectController : Controller
    {
        private readonly FactoryManager _factoryManager;

        public RedirectController(FactoryManager factoryManager)
        {
            _factoryManager = factoryManager;
        }
        
        [Route("{*url}", Order = 999)]
        public IActionResult CatchAll(string url)
        {
            var path = Request.Path.HasValue ? Request.Path.Value : "/";

            path = StandardisePath(path);

            var redirection = GetRedirection(path);

            if (redirection.Return404)
            {
                return new NotFoundResult();
            }
            
            return new RedirectResult(redirection.RedirectUrl, redirection.IsPermanent);
        }

        private Redirection GetRedirection(string path)
        {
            using (var session = _factoryManager.Instance.OpenSession())
            {
                var redirection = session
                                      .QueryOver<Redirection>()
                                      .Where(x => x.Path == path)
                                      .SingleOrDefault() ??
                                  new Redirection
                                  {
                                    Created = DateTime.UtcNow,
                                    Path = path,
                                    RedirectUrl = "https://blog.iannelson.uk/",
                                    IsPermanent = false,
                                    Checked = false
                                  };

                redirection.HitCount++;
                redirection.LastHit = DateTime.UtcNow;

                session.Save(redirection);
                session.Flush();

                return redirection;
            }
        }

        private static string StandardisePath(string path)
        {
            var temp = path.ToLowerInvariant();

            if (temp.EndsWith("/"))
            {
                temp = temp.Substring(0, temp.Length - 1);
            }

            if (temp.StartsWith("/"))
            {
                temp = temp.Substring(1, temp.Length - 1);
            }

            return temp;
        }
    }
}