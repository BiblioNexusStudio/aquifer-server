# Aquifer Tests

This directory contains automated tests which must succeed as part of the CI/CD process.
Health checks are not included here; they run as part of deployment.

## Technologies

We currently use [XUnit](https://xunit.net/) for testing in .net.

.net testing best practices (consider these documents as guidance but not necessarily as law):
* [Unit testing](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices).
* [Integration testing](https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests).

Note that common test project configuration is in the `Directory.Build.props` file in this directory which
inherits from and overrides the Solution root's `Directory.Build.props` file.

## Running the Tests

Run the following from the solution root directory:

```bash
dotnet test
```

You can also use an IDE to run one or more tests with results visible in the IDE's Test Runner UI.

## Naming

1. Test projects should match the naming of the project under test but with an `.IntegrationTests` or `.UnitTests` suffix.
1. Test classes should be in their own file and class names should end with the `Tests` suffix, _not_ `TestFixture`.
1. When only a single class (or endpoint) is under test then the corresponding test class should be located in an equivalent
   folder/namespace hierarchy as the class under test.
1. Test method names should strive to follow [.net test naming conventions](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices#naming-your-tests)
   though shorter names are often acceptable.

## Testing Philosophy

Tests give us more confidence that our code works, both when writing code initially and when updating existing code.
[Test Driven Development (TDD)](https://en.wikipedia.org/wiki/Test-driven_development) is not a developer requirement
but the general philosophy helps developers to think like a consumer of the code that they are writing and to consider
edge cases as part of initial development.

Tests are not foolproof and manual developer testing is still an important part of the development process.

There are three different kinds of test, each of which provide different levels of testing coverage:

### Unit Tests

Simplistic definition: Unit Tests _do not_ have I/O dependencies.

1. Unit Test (simple): Test a small isolated method/class behavior with no dependencies (i.e. a utility class or utility method).
1. Unit Test (complex): Test larger functionality by faking/stubbing/mocking dependencies
   (using a tool like [NSubstitute](https://nsubstitute.github.io/)).
   These tests have less value, often inject business logic into tests, are fragile tests that break easily when application logic
   changes, and take longer to write.  Sometimes they are worth writing (if the fakes are very simple) but generally are best avoided
   in favor of factoring out the code under test so that a simple unit test can be used.

Unit tests should be very fast to execute because they run in a single process and don't make any network calls.

Examples:
* A test that ensures that a utility method correctly parses verse IDs.
* A test that creates a fake IP lookup service (which doesn't actually make web requests) in order to ensure that complicated
  consuming logic around IP addresses works correctly.
* A test of a single UI view model that mocks all models and services in order to ensure that the view model acts appropriately.
* A test of a single UI component via a tool like [Jest](https://jestjs.io/) which doesn't make any API calls by providing fake data.

### Integration Tests

Simplistic definition: Integration tests _do_ have I/O dependencies.

Integration tests test full functionality of modules/components, ideally without any fakes/stubs/mocks.
I/O operations are often included (which makes this kind of testing more difficult), though they are often isolated to only
the application under test.

Integration tests are slower to execute because they make network calls.

Examples:
* A test that sends a web request to a back-end API and ensures that the response is correct.  The API will do all normal operations
  including auth, calling downstream APIs, making DB calls, etc.
* A test of a service that makes database calls to a distributed DB and ensures that the result is correct.

### End-to-end (E2E) Tests

Simplistic definition: E2E tests test the application just like an end user; some people also call these Integration Tests.

E2E tests are slow to execute because they emulate user behavior.

Examples:
* A test that opens a browser, interacts with UI elements to cause the front-end to send a web request to the back-end,
waits for the UI to update, and confirms that the UI has the correct display of information.
* A test that navigates a mobile app's menu and ensures that all main pages display content with no error messages when opened.
  To be clear, this includes making all I/O calls from the mobile app just like normal as part of the testing
  (e.g. talk to the API from the mobile app).

### Which kind of test should I write?

The more advanced the testing and the more live dependencies the more likely it is that we'll get transient test failures.
Thus, ideally Unit Tests are more thorough (and include regression tests whenever we fix bugs) and E2E testing only tests
the happy path to make sure that basic functionality doesn't break when we make changes.  Integration tests are a medium
between these that test less functionality (such as only the back-end) but has transient dependencies prone to failure.

As a general rule, write more unit tests but avoid writing complicated fakes/mocks/stubs.  If you need significant complexity
for fakes/mocks/stubs then you are just rewriting the business logic in your test and should probably be using
an integration test of the _actual_ business logic instead of duplicating it in your tests.  E2E tests are slow and fragile
which is why we only want to use them for the happy path as an automated form of QA testing.
