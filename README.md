# Elmah.Rollbar
Elmah.Rollbar is an addon for Elmah that lets you send errors directly to Rollbar. It uses the awesome RollbarSharp project (https://github.com/mroach/RollbarSharp) to connect to Rollbar.

# Installation
```install-package Elmah.Rollbar```

# Configuration

All you need to do to get started is add the following to the appSettings section of your web.config:

```
<add key="Rollbar.AccessToken" value="YOUR_ACCESS_TOKEN"/>
<add key="Rollbar.Environment" value="production"/>
```
