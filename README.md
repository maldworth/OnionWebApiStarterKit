OnionWebApiStarterKit
=====================

A sample of a WebApi2 Endpoint using OWIN for bootstrapping (but still relies on IIS for hosting). It also follows the Onion Architecture, as well as uses Mediatr to separate out our business logic from the controller actions. This also lets the "Presentation" layer (WebApi in this example) only need a reference to the DomainModels, and services (could arguably be reduced to just the services, depending on your coding guidelines/conventions).
This solution is heavily modified from https://github.com/imranbaloch/ASPNETIdentityWithOnion
