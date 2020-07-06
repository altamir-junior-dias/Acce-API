Acce API
========

## What is Acce API?

Acce API is a simple library that provides a lot of classes to accelerate API building.

## How do I get started?

First download the package in you project using this command:
```
dotnet add package acce-api
```

Then you can create your repositories using **RepositoryBase<>**, services using **ServiceBase<>** and controllers using **BaseController**.


## Building respositories

### 1. Repository entity (record) definition

The entity must be inherited from **Acce.Repositories.RecordBase** and defines a **Table** attribute at class definition, a **Key** attribute at primary key property defintion. All columns must have a propery with the same name and type.

```
[Table("customers")]
public class CustomerRecord : RecordBase
{
    [Key]
    public long CustomerId { get; set; }
    public string Name { get; set; }        
}
```

### 2. Repository interface definition

The interface must be inherited from **Acce.Repositories.IRepository<T>** where **T** must be inherited from **RecordBase**.

```
public interface ICustomersRepository : IRepository<CustomerRecord>
{   
}
```

That interface request to implement some methods that **Acce.Repositories.RepositoryBase<T>** had already implemented:
- SearchById
- SearchByCriteria
- SearchAll
- Insert
- Update
- Delete

### 3. Repository definition

The repository must be inherited from **Acce.Repositories.RepositoryBase<T>** where **T** must be inherited from **RecordBase**.

```
public class CustomersRepository : RepositoryBase<CustomerRecord>, ICustomersRepository
{
}
```

Because of **Acce.Repositories.RepositoryBase<T>** inheritance, some methods are available to be used:
- SearchById: get a specific item based in the id (primary key)
- SearchByCriteria: get items using **dynamic** value as filter
- SearchAll: get items
- Insert: inserts a new item and returns the new id
- Update: updates an item based in the id (primary key) and returns if the updation was completed
- Delete: deletes an item based in the id (primary key) and returns if the deletion was completed


## Building services

### 1. Domain entity definition

The entity must be inherited from **Acce.Domain.EntityBase** that already have a **Id** property.

```
public class Customer : EntityBase
{
    public string Name { get; set; }
}
```

### 2. Service interface definition

The interface must be inherited from **Acce.Domain.IService<T>** where **T** must be inherited from **EntityBase**.

```
public interface ICustomersService : IService<Customer>
{
}
```

That interface request to implement some methods that **Acce.Domain.ServiceBase<T>** had already implemented:
- SearchById
- SearchByCriteria
- SearchAll
- Insert
- Update
- Delete

### 3. Service definition

The repository must be inherited from **Acce.Domain.ServiceBase<T>** where **T** must be inherited from **EntityBase**.

```
public class CustomersService : ServiceBase<Customer, CustomerRecord, ICustomersRepository>, ICustomersService
{
    public CustomersService(ICustomersRepository repository, IUnitOfWork unitOfWork, IMapper mapper) : base(repository, unitOfWork, mapper) 
    {
    }
}
```

Because of **Acce.Repositories.ServiceBase<T>** inheritance, some methods are available to be used:
- SearchById: get a specific item based in the id (primary key), mapping the result from repository entity **(RecordBase)** to domain entity **(EntityBase)**
- SearchByCriteria: get items using **dynamic** value as filter, mapping the results from repository entity **(RecordBase)** to domain entity **(EntityBase)**
- SearchAll: get items, mapping the results from repository entity **(RecordBase)** to domain entity **(EntityBase)**
- Insert: inserts a new item, mapped from domain entity **(EntityBase)** to repository entity **(RecordBase)** after run **Validation**, and returns the new id
- Update: updates an item, mapped from domain entity **(EntityBase)** to repository entity **(RecordBase)** based in the id (primary key) after run **Validation**. Throws **ItemNotFoundException** if the item was not found
- Delete: deletes an item based in the id (primary key) and returns if the deletion was completed. Throws **ItemNotFoundException** if the item was not found

### 4. Mapping entities definition

Acce uses **Automapper** to defines mapping between **EntityBase** and **RecordBase** types.

```
internal static class CustomersMapper
{
    public static void Config(IMapperConfigurationExpression config)
    {
        config.CreateMap<CustomerRecord, Customer>()
            .ForMember(domain => domain.Id, conf => conf.MapFrom(record => record.CustomerId));

        config.CreateMap<Customer, CustomerRecord>()
            .ForMember(record => record.CustomerId, conf => conf.MapFrom(domain => domain.Id));
    }
}
```

## Building API

### 1. API Setup definition

**Startup.ConfigurationServices** method must define the injection to repositories, services, *Acce** types and Database connection. Also **Automapper** configuration.

```
public void ConfigureServices(IServiceCollection services)
{
    ...

    services.AddScoped<IDbConnection>(_ => GetConnection());
    BootstrappingInjector.Config(services);

    //put here injection to repositories and services

    var mapperConfiguration = new MapperConfiguration(config => {
        ApiMapper.Config(config);
        DomainMapper.Config(config);
    });

    services.AddSingleton(mapperConfiguration.CreateMapper());
    ...
}

private SqlConnection GetConnection() {
    var connection = new SqlConnection();
    //define here the connection string
    connection.ConnectionString = "";

    return connection;
}
```

### 2. Controller definition

The controller must be inherited from **Acce.Controller.BaseController**, that have a method to **ExecuteProcess** that catch some **Exceptions**.

```
[ApiController]
[Route("customers")]
public class CustomersController : BaseController
{
    private readonly ICustomersService service;
    private readonly IMapper mapper;

    public CustomersController(ICustomersService service, IMapper mapper)
    {
        this.service = service;
        this.mapper = mapper;
    }

    [HttpGet, Route("")]
    public ActionResult GetAll() 
    {
        Func<dynamic> process = () => { return service.SearchAll(); };

        return ExecuteProcess(process);
    }

    [HttpPost, Route("")]
    public ActionResult Add([FromBody] CustomerDTO customer)
    {
        Func<long> process = () => { return service.Insert(mapper.Map<Customer>(customer)); };

        return ExecuteProcess(process);
    }

    [HttpPut, Route("{id}")]
    public ActionResult Update(long id, [FromBody] CustomerDTO customer)
    {
        Action process = () => { service.Update(mapper.Map<Customer>(customer, opt => opt.AfterMap((_, entity) => ((EntityBase)entity).Id = id))); };

        return ExecuteProcess(process);
    }

    [HttpDelete, Route("{id}")]
    public ActionResult Delete(long id)
    {
        Action process = () => { service.Delete(id); };

        return ExecuteProcess(process);
    }

    [HttpGet, Route("{id}")]
    public ActionResult Get(long id) 
    {
        Func<dynamic> process = () => { return service.SearchById(id); };

        return ExecuteProcess(process);
    }
}
```