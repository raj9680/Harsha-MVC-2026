using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace harsha_mvc.Models
{
    public class CustomPersonModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            Person person = new Person();

            if(bindingContext.ValueProvider.GetValue("PersonName").Length > 0)
            {
                person.PersonName = bindingContext.ValueProvider.GetValue("PersonName").FirstValue;
            }

            if(bindingContext.ValueProvider.GetValue("Phone").Length > 0)
            {
                person.Phone = " "+bindingContext.ValueProvider.GetValue("Phone").FirstValue;
            }

            bindingContext.Result = ModelBindingResult.Success(person);
            return Task.CompletedTask;
        }
    }
}
