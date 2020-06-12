using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AV.Enumeration.ModelBinder
{
    public static class EnumerationQueryParameterModelBinder
    {
        public static EnumerationQueryParameterModelBinder<T> CreateInstance<T>()
            where T : Enumeration
        {
            return new EnumerationQueryParameterModelBinder<T>();
        }
    }

    public class EnumerationQueryParameterModelBinder<T> : IModelBinder
        where T : Enumeration
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var enumerationName = bindingContext.ValueProvider.GetValue(bindingContext.FieldName);
            if (string.IsNullOrEmpty(enumerationName.FirstValue))
            {
                bindingContext.Result = ModelBindingResult.Success(default(T));
            }
            else if (Enumeration.TryGetFromValueOrName<T>(enumerationName.FirstValue, out var result))
            {
                bindingContext.Result = ModelBindingResult.Success(result);
            }
            else
            {
                bindingContext.Result = ModelBindingResult.Failed();
                
                bindingContext.ModelState.AddModelError(nameof(bindingContext.FieldName),
                    $"{enumerationName.FirstValue} is not supported.");
            }

            return Task.CompletedTask;
        }
    }
}
