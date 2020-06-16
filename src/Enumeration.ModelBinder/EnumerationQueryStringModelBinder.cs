using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AV.Enumeration.ModelBinder
{
    public static class EnumerationQueryStringModelBinder
    {
        public static EnumerationQueryStringModelBinder<T> CreateInstance<T>()
            where T : Enumeration
        {
            return new EnumerationQueryStringModelBinder<T>();
        }
    }

    public class EnumerationQueryStringModelBinder<T> : IModelBinder
        where T : Enumeration
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var enumerationName = bindingContext.ValueProvider.GetValue(bindingContext.FieldName);
            if (Enumeration.TryGetFromValueOrName<T>(enumerationName.FirstValue, out var result))
            {
                bindingContext.Result = ModelBindingResult.Success(result);
            }
            else
            {
                bindingContext.Result = ModelBindingResult.Failed();
                
                bindingContext.ModelState.AddModelError(bindingContext.FieldName,
                    $"{enumerationName.FirstValue} is not supported.");
            }

            return Task.CompletedTask;
        }
    }
}
