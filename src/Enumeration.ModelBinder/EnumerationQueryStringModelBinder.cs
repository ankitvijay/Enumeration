using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AV.Enumeration.ModelBinder
{
    /// <summary>
    /// Defines <see cref="EnumerationQueryStringModelBinder"/>
    /// </summary>
    public static class EnumerationQueryStringModelBinder
    {
        /// <summary>
        /// Creates an instance of <see cref="EnumerationQueryStringModelBinder{TEnumeration}"/>
        /// </summary>
        /// <typeparam name="TEnumeration"></typeparam>
        /// <returns></returns>
        public static EnumerationQueryStringModelBinder<TEnumeration> CreateInstance<TEnumeration>()
            where TEnumeration : Enumeration
        {
            return new EnumerationQueryStringModelBinder<TEnumeration>();
        }
    }

    /// <summary>
    /// Defines <see cref="EnumerationQueryStringModelBinder{T}"/>
    /// </summary>
    /// <typeparam name="TEnumeration">The <typeparamref name="TEnumeration"/> instance.</typeparam>
    public class EnumerationQueryStringModelBinder<TEnumeration> : IModelBinder
        where TEnumeration : Enumeration
    {
        /// <inheritdoc />
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var enumerationName = bindingContext.ValueProvider.GetValue(bindingContext.FieldName);
            if (Enumeration.TryGetFromValueOrName<TEnumeration>(enumerationName.FirstValue, out var result))
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
