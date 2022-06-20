# Playwright Contributions

[![build](https://github.com/hlaueriksson/playwright-dotnet-contrib/actions/workflows/build.yml/badge.svg)](https://github.com/hlaueriksson/playwright-dotnet-contrib/actions/workflows/build.yml)
[![CodeFactor](https://www.codefactor.io/repository/github/hlaueriksson/playwright-dotnet-contrib/badge)](https://www.codefactor.io/repository/github/hlaueriksson/playwright-dotnet-contrib)

[![PlaywrightContrib.Extensions](https://img.shields.io/nuget/v/PlaywrightContrib.Extensions.svg?label=PlaywrightContrib.Extensions)](https://www.nuget.org/packages/PlaywrightContrib.Extensions)
[![PlaywrightContrib.FluentAssertions](https://img.shields.io/nuget/v/PlaywrightContrib.FluentAssertions.svg?label=PlaywrightContrib.FluentAssertions)](https://www.nuget.org/packages/PlaywrightContrib.FluentAssertions)
[![PlaywrightContrib.PageObjects](https://img.shields.io/nuget/v/PlaywrightContrib.PageObjects.svg?label=PlaywrightContrib.PageObjects)](https://www.nuget.org/packages/PlaywrightContrib.PageObjects)

Contributions to Playwright for .NET 🎭🧪

Playwright Contributions offered extensions to Playwright for .NET

It provided a convenient way to write readable and reliable browser tests in C#

> [Playwright for .NET](https://github.com/microsoft/playwright-dotnet) is the official language port of Playwright.
>
> [Playwright](https://github.com/microsoft/playwright) is a Node.js library to automate Chromium, Firefox and WebKit with a single API. Playwright is built to enable cross-browser web automation that is ever-green, capable, reliable and fast.

## Deprecation ⚠️

This project is legacy and is no longer maintained:

- Is is based on the `IElementHandle` interface and was first built with version `1.12.1` of `Microsoft.Playwright`
- The use of [`ElementHandle`](https://playwright.dev/dotnet/docs/api/class-elementhandle) is discouraged, use [`Locator`](https://playwright.dev/dotnet/docs/api/class-locator) objects and web-first assertions instead
- The Locator API was introduced in version [`1.14`](https://playwright.dev/dotnet/docs/release-notes#version-114) of `Microsoft.Playwright`
- [Locator vs ElementHandle](https://playwright.dev/dotnet/docs/locators#locator-vs-elementhandle) describes the difference between the old and new way to access elements
- You can use the vanilla API to achieve the same thing without using this package:
  - [Locator](https://playwright.dev/dotnet/docs/api/class-locator)
  - [Assertions](https://playwright.dev/dotnet/docs/test-assertions)
  - [Page Object Models](https://playwright.dev/dotnet/docs/pom)

## Introduction

`Microsoft.Playwright` is a great library to automate *Chromium*, *Firefox* and *WebKit* in .NET / C# on *Linux*, *macOS* and *Windows*.

Playwright Contributions consists of a three libraries that helped you write browser automation tests:

- [`PlaywrightContrib.Extensions`](PlaywrightContrib.Extensions.md)
  - A library with extension methods for writing tests with the Playwright API
- [`PlaywrightContrib.FluentAssertions`](PlaywrightContrib.FluentAssertions.md)
  - A library for writing tests with [`FluentAssertions`](https://github.com/fluentassertions/fluentassertions) against the Playwright API
- [`PlaywrightContrib.PageObjects`](PlaywrightContrib.PageObjects.md)
  - A library for writing browser tests using the _page object pattern_ with the Playwright API

## Samples

Sample projects are located in the [`samples`](/samples/) folder:
- The `PlaywrightContrib.Sample.NUnit` project showcase how to use the three legacy libraries ❌
- The `PlaywrightVanilla.Sample.NUnit` project on the other hand, uses the vanilla API to achieve the same thing ✔️
