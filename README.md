
|  Metric      | Status |
| ---------    | ---------| 
| Build | [![Build Status](https://dev.azure.com/ankitvijay/Enumeration/_apis/build/status/Enumeration-CI?branchName=master)](https://dev.azure.com/ankitvijay/Enumeration/_build/latest?definitionId=1&branchName=master)|
| AV.Enumeration | ![NuGet Status](https://img.shields.io/nuget/v/AV.Enumeration.svg) |
| AV.Enumeration.ModelBinder | ![NuGet Status](https://img.shields.io/nuget/v/AV.Enumeration.ModelBinder.svg) |
| AV.Enumeration.SystemTextJson | ![NuGet Status](https://img.shields.io/nuget/v/AV.Enumeration.SystemTextJson.svg) |
| AV.Enumeration.NewtonsoftJson | ![NuGet Status](https://img.shields.io/nuget/v/AV.Enumeration.NewtonsoftJson.svg) |

# Enumeration class
This project implements Enumeration class as an alternate to Enum types. The implementation is inspired from famous [eShopOnContainers](https://github.com/dotnet-architecture/eShopOnContainers/blob/dev/src/Services/Ordering/Ordering.Domain/SeedWork/Enumeration.cs) example.

The project provides following NuGet packages:

- AV.Enumeration - The Enumeration class package.
- AV.Enumeration.ModelBinder - Custom `ModelBinder` to allow Enumeration class pass as a query string parameter.
- AV.Enumeration.SystemTextJson - `System.Text.Json` serialization support for Enumeration class.
- AV.Enumeration.NewtonsoftJson - `Newtonsoft.Json` serialization support for Enumeration class. 



## Want to know more about Enumeration class?
See: https://ankitvijay.net/2020/06/12/series-enumeration-classes-ddd-and-beyond/

# Usage

- `PaymentType` Enumeration

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

- `PaymentType` Enumeration with Behaviour

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


- `System.Text.Json` Serialization/Deserialization

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

- `Newtonsoft.Json` Serialization/Deserialization

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

        [Fact]
        public void CanConvertReturnsTrueForEnumeration()
        {
            var converter = new EnumerationJsonConverter();

            Assert.True(converter.CanConvert(typeof(PaymentType)));
            Assert.False(converter.CanConvert(typeof(string)));
            Assert.False(converter.CanConvert(typeof(int)));
        }
    }
```
