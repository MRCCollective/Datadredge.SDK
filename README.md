# Webbstatistik.SDK

Server-side pageview tracking SDK for [Webbstatistik analytics](https://webbstatistik.se).

## Installation

```bash
dotnet add package Webbstatistik.SDK
```

## Configuration

Add to your `Program.cs`:

```csharp
builder.Services.AddServerPageviewTracking(builder.Configuration);

var app = builder.Build();
app.UseServerPageviewTracking();
```

Add to your `appsettings.json`:

```json
{
  "Tracking:ServerPageviews": {
    "WebbstatistikBaseUrl": "https://webbstatistik.se",
    "SiteKey": "your-site-key",
    "WebsiteId": "your-website-id"
  }
}
```

## Usage

The SDK automatically tracks pageviews for HTML responses. Events are queued and batched to the Webbstatistik analytics backend.
