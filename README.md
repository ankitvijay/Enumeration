[![Build Status](https://dev.azure.com/ankitvijay/Enumeration/_apis/build/status/ankitvijay.Enumeration?branchName=master)](https://dev.azure.com/ankitvijay/Enumeration/_build/latest?definitionId=2&branchName=master) [![NuGet](https://img.shields.io/nuget/v/AV.Enumeration.svg)](https://www.nuget.org/packages/AV.Enumeration) [![NuGet](https://img.shields.io/nuget/dt/AV.Enumeration.svg)](https://www.nuget.org/packages/AV.Enumeration) <a href="https://twitter.com/vijayankit"><img src="https://img.shields.io/twitter/follow/vijayankit.svg?style=social&label=@vijayankit" alt="vijayankit" align="right" /></a>


# Nuget Packages
|  Package      | Link |
| ---------    | ---------| 
| AV.Enumeration | [![NuGet](https://img.shields.io/nuget/v/AV.Enumeration.svg)](https://www.nuget.org/packages/AV.Enumeration) |
| AV.Enumeration.ModelBinder | [![NuGet](https://img.shields.io/nuget/v/AV.Enumeration.ModelBinder.svg)](https://www.nuget.org/packages/AV.Enumeration.ModelBinder)|
| AV.Enumeration.SystemTextJson | [![NuGet](https://img.shields.io/nuget/v/AV.Enumeration.SystemTextJson.svg)](https://www.nuget.org/packages/AV.Enumeration.SystemTextJson)|
| AV.Enumeration.NewtonsoftJson | [![NuGet](https://img.shields.io/nuget/v/AV.Enumeration.NewtonsoftJson.svg)](https://www.nuget.org/packages/AV.Enumeration.NewtonsoftJson)|
| AV.Enumeration.NSwag | [![NuGet](https://img.shields.io/nuget/v/AV.Enumeration.NSwag.svg)](https://www.nuget.org/packages/AV.Enumeration.NSwag)|
 
# Enumeration class
This project implements Enumeration class as an alternate to Enum types. The implementation is inspired from famous [eShopOnContainers](https://github.com/dotnet-architecture/eShopOnContainers/blob/dev/src/Services/Ordering/Ordering.Domain/SeedWork/Enumeration.cs) example.

The project provides following NuGet packages:

- AV.Enumeration - The Enumeration class package.
- AV.Enumeration.ModelBinder - Custom `ModelBinder` to allow Enumeration class pass as a query string parameter.
- AV.Enumeration.SystemTextJson - `System.Text.Json` serialization support for Enumeration class.
- AV.Enumeration.NewtonsoftJson - `Newtonsoft.Json` serialization support for Enumeration class. 
- AV.Enumeration.NSwag - NSwag support for Enumeration class to generate Enumeration as an Enum type schema.


## Want to know more about Enumeration class?
See my Enumeration class [blog post series]( https://ankitvijay.net/2020/06/12/series-enumeration-classes-ddd-and-beyond/)

# Give a Star ⭐️
Found this repository helpful? You can give a star. :)

# Usage

- `PaymentType` Enumeration class  (Import: `AV.Enumeration`)

```csharp
public class PaymentType : Enumeration
{
    public static readonly PaymentType DebitCard = new PaymentType(0);

    public static readonly PaymentType CreditCard = new PaymentType(1);

    private PaymentType(int value, [CallerMemberName] string name = null) : base(value, name)
    {
    }
}
````

- `PaymentType` Enumeration class with Behaviour  (Import: `AV.Enumeration`)

```csharp
public abstract class PaymentType : Enumeration
{
    public static readonly PaymentType DebitCard = new DebitCardType();

    public static readonly PaymentType CreditCard = new CreditCardType();

    public abstract string Code { get; }

    private PaymentType(int value, string name = null) : base(value, name)
    {
    }

    private class DebitCardType : PaymentType
    {
        public DebitCardType() : base(0, "DebitCard")
        {
        }

        public override string Code => "DC";
    }

    private class CreditCardType : PaymentType
    {
        public CreditCardType() : base(1, "CreditCard")
        {
        }

        public override string Code => "CC";
    }
}
```


- `System.Text.Json` Serialization/Deserialization (Import: `AV.Enumeration.SystemTextJson`)

```csharp
public class EnumerationJsonConverterTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public EnumerationJsonConverterTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void EnumerationSerializesAndDeserializesCorrectly()
    {
        var expected = new Transaction
        {
            Amount = 100,
            PaymentType = PaymentType.CreditCard
        };

        var json = JsonSerializer.Serialize(expected,
            new JsonSerializerOptions
            {
                Converters =
                {
                    new EnumerationJsonConverter()
                }
            });

        _testOutputHelper.WriteLine(json);

        var actual= JsonSerializer.Deserialize<Transaction>(json, new JsonSerializerOptions()
        {
            Converters = { new EnumerationJsonConverter() }
        });

        Assert.Equal(expected.Amount, actual.Amount);
        Assert.Equal(expected.PaymentType, actual.PaymentType);
    }
}
```

- `Newtonsoft.Json` Serialization/Deserialization (Import: `AV.Enumeration.NewtonsoftJson`)

```csharp
public class EnumerationJsonConverterTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public EnumerationJsonConverterTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void EnumerationIsSerializesAndDeserializesCorrectly()
        {
            var expected = new Transaction
            {
                Amount = 100,
                PaymentType = PaymentType.CreditCard
            };

            var json = JsonConvert.SerializeObject(expected, Formatting.Indented, new EnumerationJsonConverter());

            _testOutputHelper.WriteLine(json);

            var actual = JsonConvert.DeserializeObject<Transaction>(json, new EnumerationJsonConverter());

            Assert.Equal(expected.Amount, actual.Amount);
            Assert.Equal(expected.PaymentType, actual.PaymentType);
        }
    }
```

- `PaymentType` Enumeration as a query string parameter (Import: `AV.Enumeration.ModelBinder`)

```csharp
// Startup.cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers(options =>
    {
        options.ModelBinderProviders.Insert(0, new EnumerationQueryStringModelBinderProvider());
    });
}
```

```csharp

// Controller
[ApiController]
[Route("[controller]")]
public class TransactionController : ControllerBase
{
    [HttpGet]
    [Route("code")]
    public string Get(PaymentType paymentType)
    {
        return paymentType.Code;
    }
}
```

## Support
Has my work been helpful to you? You can extend your support :blush:

<a href="https://www.buymeacoffee.com/ankitvijay" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/default-red.png" alt="Buy Me A Coffee" width="150"  ></a>

