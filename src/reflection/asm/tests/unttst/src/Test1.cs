using OpenEchoSystem.Core.xExtensions.RunTime;

namespace OpenEchoSystem.Core.xReflection.Assembly;

internal interface IUnitOfWork : IDisposable, IAsyncDisposable, INotEngaged
{
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
internal interface ICommandUnitOfWork : IUnitOfWork
{
}
internal abstract class UnitOfWorkBase : IUnitOfWork
{
    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public ValueTask DisposeAsync()
    {
        throw new NotImplementedException();
    }

    public int SaveChanges()
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
internal class CommandUnitOfWorkDirect : UnitOfWorkBase, IUnitOfWork
{

}
internal class CommandUnitOfWork : UnitOfWorkBase
{

}
internal class EfCommandUnitOfWork : CommandUnitOfWork, ICommandUnitOfWork
{

}
public sealed class Test1
{

    [Fact]
    public void BaseTypeMustContainsInterfaceBecauseDirectlyImplemented()
    {
        var interfaces = typeof(UnitOfWorkBase).GetInterfaces(InterfaceFilter.NotInheritedFromInterfaces);
        Assert.Contains(interfaces, x => x == typeof(IUnitOfWork));
    }
    [Fact]
    public void AncestorTypeMustNotContainInterfaceBecauseDirectlyImplemented()
    {
        var interfaces = typeof(CommandUnitOfWork).GetInterfaces(InterfaceFilter.NotExclusiveInterfaces);
        Assert.DoesNotContain(interfaces, x => x == typeof(IUnitOfWork));
    }
    [Fact]
    public void AncestorTypeWithDirectInterfaceAdditonallyToBaseClassMustContainInterfaceBecauseDirectlyImplemented()
    {
        //var primary = typeof(CommandUnitOfWorkDirect).GetPrimaryInterface();
        var interfaces = typeof(CommandUnitOfWorkDirect).GetInterfaces(InterfaceFilter.NotExclusiveInterfaces);
        Assert.DoesNotContain(interfaces, x => x == typeof(IUnitOfWork));
    }
    [Fact]
    public void TopAncestorTypeMustNotContainInterfaceBecauseDirectlyImplemented()
    {
        var interfaces = typeof(EfCommandUnitOfWork).GetInterfaces(InterfaceFilter.NotExclusiveInterfaces);
        Assert.DoesNotContain(interfaces, x => x == typeof(IUnitOfWork));
        Assert.Contains(interfaces, x => x == typeof(ICommandUnitOfWork));
        Assert.DoesNotContain(interfaces, x => x == typeof(IAsyncDisposable));
        Assert.DoesNotContain(interfaces, x => x == typeof(INotEngaged));
        Assert.DoesNotContain(interfaces, x => x == typeof(IDisposable));
    }
    [Fact]
    public void InheritedFromInterfaceFilterAllInterfacesInheritFromInterfaces()
    {
        var interfaces = typeof(EfCommandUnitOfWork).GetInterfaces(InterfaceFilter.InheritedFromInterface);
        Assert.Contains(interfaces, x => x == typeof(INotEngaged));
        Assert.DoesNotContain(interfaces, x => x == typeof(ICommandUnitOfWork));
        Assert.Contains(interfaces, x => x == typeof(IDisposable));
        Assert.Contains(interfaces, x => x == typeof(IAsyncDisposable));
         interfaces = typeof(CommandUnitOfWork).GetInterfaces(InterfaceFilter.InheritedFromInterface);
        Assert.DoesNotContain(interfaces, x => x == typeof(IUnitOfWork));
        Assert.DoesNotContain(interfaces, x => x == typeof(ICommandUnitOfWork));
        Assert.Contains(interfaces, x => x == typeof(IAsyncDisposable));
        Assert.Contains(interfaces, x => x == typeof(IDisposable));
        interfaces = typeof(UnitOfWorkBase).GetInterfaces(InterfaceFilter.InheritedFromInterface);
        Assert.DoesNotContain(interfaces, x => x == typeof(IUnitOfWork));
        Assert.Contains(interfaces, x => x == typeof(IAsyncDisposable));
        Assert.Contains(interfaces, x => x == typeof(IDisposable));
    }

}
