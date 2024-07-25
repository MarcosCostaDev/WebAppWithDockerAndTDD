using InsuranceWebApi.Domain.Entities;
using InsuranceWebApi.Infra.Contexts;

namespace InsuranceWebApi.Test.Infra.Contexts;

public class CacheContextTests
{
    [Fact]
    public void Get_ReturnsAllItems()
    {
        var cache = new CacheContext<Advisor>();

        var item1 = new Advisor("John Doe", "123456789", "123 Main St", "123-4567");
        var item2 = new Advisor("Doe John", "987654321", "123 Main St", "123-4567");
    
        cache.Put(item1.Sin, item1);
        cache.Put(item2.Sin, item2);

        var result = cache.Get();

        // Assert
        result.Should().HaveCount(2);
        result.Should().Contain(item1).And.Contain(item2);
    }

    [Fact]
    public void Get_ByKey_ReturnsCorrectItem()
    {
        var cache = new CacheContext<Advisor>();
        var item1 = new Advisor("John Doe", "123456789", "123 Main St", "123-4567");
        cache.Put(item1.Sin, item1);

        var result = cache.Get(item1.Sin);

        // Assert
        result.Should().Be(item1);
    }

    [Fact]
    public void Put_AddsItemToCache()
    {
        var cache = new CacheContext<Advisor>();
        var item1 = new Advisor("John Doe", "123456789", "123 Main St", "123-4567");

        cache.Put(item1.Sin, item1);

        // Assert
        cache.Get(item1.Sin).Should().Be(item1);
    }

    [Fact]
    public void Put_OverCapacity_RemovesOldestItem()
    {
        var cache = new CacheContext<Advisor>(capacity: 2);
        var item1 = new Advisor("John Doe", "123456789", "123 Main St", "123-4567");
        var item2 = new Advisor("Doe John", "987654321", "123 Main St", "123-4567");
        var item3 = new Advisor("A valid John", "987674321", "123 Main St", "123-4567");

        cache.Put(item1.Sin, item1);
        cache.Put(item2.Sin, item2);
        cache.Put(item3.Sin, item3);

        // Assert
        cache.Get(item1.Sin).Should().Be(item1);
        cache.Get(item2.Sin).Should().BeNull();
        cache.Get(item3.Sin).Should().Be(item3);
    }

    [Fact]
    public void Delete_RemovesItemFromCache()
    {
        var cache = new CacheContext<Advisor>();
        var item1 = new Advisor("John Doe", "123456789", "123 Main St", "123-4567");
        cache.Put(item1.Sin, item1);

        cache.Delete(item1.Sin);

        // Assert
        cache.Get(item1.Sin).Should().BeNull();
    }
}
