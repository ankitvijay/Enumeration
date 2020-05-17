using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AV.Enumeration.ModelBinder
{
    /// <summary>
    /// <see>
    ///     <cref>ExtractEnumerationModelBinderProvider</cref>
    /// </see>
    /// </summary>
    public static class ExtractEnumerationModelBinder
    {
        /// <summary>
        /// Create instance of ExtractEnumerationModelBinder
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ExtractEnumerationModelBinder<T> CreateInstance<T>()
            where T : Enumeration
        {
            return new ExtractEnumerationModelBinder<T>();
        }
    }

    /// <inheritdoc />
    public class ExtractEnumerationModelBinder<T> : IModelBinder
        where T : Enumeration
    {
        /// <summary>
        /// <inheritdoc cref="IModelBinder"/>
        /// </summary>
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
                
                bindingContext.ModelState.AddModelError(nameof(bindingContext.FieldName), "Invalid address type");
            }

            return Task.CompletedTask;
        }
    }
}
