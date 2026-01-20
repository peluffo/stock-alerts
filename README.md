# Signal Alerting Platform

**Not financial advice. Educational/informational only.**

This repository contains a production-grade starter for a stock/ETF/options signal alerting platform. It **only generates alerts** and does not place trades or connect to brokerage execution systems.

## Architecture

```
/src
  Api            ASP.NET Core Web API (.NET 8)
  Domain         Entities + strategy interfaces
  Application    Use cases + strategy engine
  Infrastructure EF Core + Redis + Finnhub client + notifications
  Worker         Background scanning + fanout
/ui              Angular 17+ standalone SPA
```

## Finnhub Free Tier Safety
- Centralized token bucket rate limiting (default 55 calls/minute).
- Redis caching to respect short TTLs for quotes/candles.
- Batching is expected in the worker scan loop.

## Local Development

1. Copy `.env.example` to `.env` and update values.
2. Start infra:
   ```bash
   docker-compose up -d
   ```
3. Run backend projects (requires .NET 8 SDK).
4. Run the UI (requires Node + Angular CLI).

## Disclaimer
This platform provides market **alerts only**. It does not execute trades. Always consult a qualified financial professional.
