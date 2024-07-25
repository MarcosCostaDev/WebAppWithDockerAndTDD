using InsuranceWebApi.Domain.Entities.Abstracts;

namespace InsuranceWebApi.Infra.Contexts;

public class CacheContext<T> where T : AbstractEntity
{
    private readonly int _capacity;
    private readonly IDictionary<string, T> _data;

    public CacheContext(int capacity = 5)
    {
        _capacity = capacity;
        _data = new Dictionary<string, T>();
    }

    public IEnumerable<T> Get()
    {
        return _data.Select(p => p.Value);
    }

    public T? Get(string key)
    {
        return _data.TryGetValue(key, out T? value) ? value : null!;
    }

    public void Put(string key, T @object)
    {
        if (!_data.ContainsKey(key) && _data.Keys.Count == _capacity)
        {
            var last = _data.Last();
            _data.Remove(last.Key);
        }
        _data[key] = @object;
    }

    public void Delete(string key)
    {
        if(_data.ContainsKey(key))
        { 
            _data.Remove(key); 
        }
    }
}
