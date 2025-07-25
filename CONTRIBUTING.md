# Contributing to Core Framework

Welcome to the Core Framework community! We're excited to have you contribute to our modern .NET 9 framework. This guide will help you understand our development process and standards.

## Quick Links

- [Architecture Overview](docs/architecture/overview.md)
- [Getting Started Guide](docs/guides/getting-started.md)
- [Testing Strategy](research/testing-strategy.md)
- [Implementation Patterns](research/implementation-patterns.md)
- [Performance Guidelines](research/performance-optimizations.md)

## Development Process

We use GitHub for collaboration and follow modern development practices:

1. Fork the repo and create your branch from `main`
2. Set up your development environment:dotnet restore
dotnet build
dotnet test3. Make your changes following our guidelines
4. Ensure all tests pass and add new ones
5. Update documentation and examples
6. Submit a pull request

## Code Quality Standards

### Performance First
- Use modern .NET 9 features appropriately
- Leverage Span<T>/Memory<T> for performance
- Consider AOT compilation impact
- Minimize allocations
- Use hardware intrinsics where beneficial
- Profile code changes with benchmarks

### Architecture Guidelines
- Follow modular design principles
- Maintain clean separation of concerns
- Use dependency injection appropriately
- Keep components focused and cohesive
- Consider cloud-native scenarios
- Follow [Implementation Patterns](research/implementation-patterns.md)

### Coding Style// Example of preferred code style
public sealed class ExampleComponent : IExampleComponent
{
    private readonly ILogger<ExampleComponent> _logger;
    private readonly CancellationToken _cancellationToken;

    public ExampleComponent(ILogger<ExampleComponent> logger, 
                          CancellationToken cancellationToken = default)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _cancellationToken = cancellationToken;
    }

    public async ValueTask<Result<T>> ProcessAsync<T>(ReadOnlyMemory<byte> data)
        where T : notnull
    {
        try
        {
            // Implementation
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.LogError(ex, "Processing failed");
            return Result<T>.Error(ex);
        }
    }
}
### Testing Requirements

#### Unit Tests
- Use xUnit for unit testing
- Follow Arrange-Act-Assert pattern
- Test both success and failure paths
- Mock external dependencies
- Test edge cases thoroughly
[Theory]
[InlineData(...)]
public async Task MethodName_Scenario_ExpectedBehavior()
{
    // Arrange
    using var cts = new CancellationTokenSource();
    
    // Act
    var result = await sut.MethodAsync(input, cts.Token);
    
    // Assert
    result.Should().NotBeNull();
}
#### Performance Tests
- Include benchmarks for performance-critical code
- Test with different data sizes
- Consider memory allocation patterns
- Verify AOT compatibility

### Documentation Standards

#### XML Documentation/// <summary>
/// Processes the input data asynchronously.
/// </summary>
/// <typeparam name="T">The result type.</typeparam>
/// <param name="data">The input data to process.</param>
/// <returns>A <see cref="ValueTask{TResult}"/> representing the async operation.</returns>
/// <exception cref="ArgumentNullException">Thrown when data is null.</exception>
public async ValueTask<T> ProcessAsync<T>(ReadOnlyMemory<byte> data)
#### Markdown Documentation
- Keep README.md updated
- Document breaking changes
- Include usage examples
- Update architecture documentation
- Maintain API documentation

## Pull Request Process

1. Update relevant documentation
2. Add/update unit tests
3. Add/update benchmarks if needed
4. Ensure all tests pass
5. Update CHANGELOG.md
6. Request review from maintainers

## Commit Messages

Follow conventional commits:feat(component): add new feature
fix(component): fix specific issue
docs(component): update documentation
perf(component): improve performance
test(component): add/update tests
## Getting Help

- Review [Architecture Overview](docs/architecture/overview.md)
- Check [Implementation Patterns](research/implementation-patterns.md)
- Join discussions in Issues/PRs
- Read through documentation

## License

By contributing, you agree that your contributions will be licensed under the project's MIT License.