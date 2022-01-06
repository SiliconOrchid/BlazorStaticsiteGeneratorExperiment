THIS IS AN UNSUPPORTED EXPERIMENT THAT DIDN'T GO ANYWHERE


# BlazorStaticsiteGeneratorExperiment
A project that uses Blazor as a rendering engine, to create fully static html website.    Also loops-in Contentful to provide CMS functionality.



This repo is currently work-in-progress for an upcoming blog article.

The project currently is really close, but does not work consistently/reliably.    There is some sort of transient problem that means it works sometimes, but often breaks.

I think it's to do with injected-service-scope,  or perhaps spawned webservers hosting differently????

---


The idea is to try and leverage Blazor pre-rendering to generate a fully static website (i.e. lots of separate html pages) similar to how something like the static-site-generator "Hugo" works.      

> N.b.  When talking about "static sites", I would clarify that this is not the same thing as the blazor wasm "payload" being static content.

The intent is that this would be used for static content such as a brochure website or personal blogsite.  This isn't going to be useful for anyone who needs an actual application that is creating dynamic content.


---

There's a bit of a pipeline needed, in order to make this work.

* Blazor supports pre-rendering.  The intent of this is to have pages cached as plain html, which can be quickly transmitted across the web and displayed in the browser to the user, creating illusion of fast response.   The larger/slower blazor-app wasm payload can then be download, without any obvious delays, to the user.      Javascript SPA frameworks commonly support this feature.

* By encapsulating the Blazor-App in a host web service, we can hack a feedback loop, meaning that we can serve static fully-rendered html pages, derived from the original blazor app.

* By further encapsulating the web service in a console app, we can make calls to URL routes to render a page and capture the response as a string, which can then be saved to disk/storage.  The idea is that this console app would likely end up in an automated build pipeline - which is why I've called the VS project a "Build Agent".


---


I'm quite open in saying that I'm basing this project on a series of blogs by Andrew Lock.  I'm intending to expand upon those ideas by framing the solution as a console project (rather than triggered by Unit Testing) and introducing an integrating with a headless CMS for content (using CMS service "ContentFul")

* https://andrewlock.net/using-source-generators-to-find-all-routable-components-in-a-webassembly-app/
* https://andrewlock.net/enabling-prerendering-for-blazor-webassembly-apps/
* https://andrewlock.net/using-source-generators-with-a-custom-attribute--to-generate-a-nav-component-in-a-blazor-app/
