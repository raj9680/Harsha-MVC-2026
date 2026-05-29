using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace harsha_mvc.Models
{
    public class PersonBinderProvider : IModelBinderProvider
    {
        // means wherever 'Person' class is used, 'CustomPersonModelBinder' will be executed.
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if(context.Metadata.ModelType == typeof(Person))
            {
                return new BinderTypeModelBinder(typeof(CustomPersonModelBinder));
            }
            return null;
        }
    }
}
