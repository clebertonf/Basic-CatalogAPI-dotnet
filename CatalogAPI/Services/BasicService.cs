namespace CatalogAPI.Services;
public class BasicService : IBasicService
{
    public string GetMessage()
    {
        return $"Ola mundo! {DateTime.Now.ToString()}";
    }
}
