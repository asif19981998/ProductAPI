namespace ECommerce.Models;

public record Product(string Id, string Name, object Data);
public record ProductCreateRequest(string Name, object Data);


