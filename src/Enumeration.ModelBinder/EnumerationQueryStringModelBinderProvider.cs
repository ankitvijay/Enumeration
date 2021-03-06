﻿using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AV.Enumeration.ModelBinder
{
    /// <summary>
    /// Defines <see cref="EnumerationQueryStringModelBinderProvider"/>
    /// </summary>
    public class EnumerationQueryStringModelBinderProvider : IModelBinderProvider
    {
        /// <inheritdoc />
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var fullyQualifiedAssemblyName = context.Metadata.ModelType.FullName;

            if (fullyQualifiedAssemblyName == null)
            {
                return null;
            }

            var enumType = context.Metadata.ModelType.Assembly.GetType
                (fullyQualifiedAssemblyName, false);

            if (enumType == null || !enumType.IsSubclassOf(typeof(Enumeration)))
            {
                return null;
            }

            var methodInfo = typeof(EnumerationQueryStringModelBinder)
                .GetMethod("CreateInstance"
                    , BindingFlags.Static | BindingFlags.Public);

            if (methodInfo == null)
            {
                throw new InvalidOperationException("Invalid operation");
            }

            var genericMethod = methodInfo.MakeGenericMethod(enumType);
            var invoke = genericMethod.Invoke(null, null);

            return invoke as IModelBinder;
        }
    }
}
