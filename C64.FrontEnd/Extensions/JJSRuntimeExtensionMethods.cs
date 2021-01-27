using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace C64.FrontEnd.Extensions
{
    public static class IJSRuntimeExtensionMethods
    {
        public static async ValueTask<bool> Confirm(this IJSRuntime js, string message)
        {
            return await js.InvokeAsync<bool>("confirm", message);
        }

        public static async ValueTask MyFunction(this IJSRuntime js, string message)
        {
            await js.InvokeVoidAsync("my_function", message);
        }

        public static async ValueTask<object> SetInLocalStorage(this IJSRuntime js, string key, string content)
        {
            try
            {
                return await js.InvokeAsync<object>("localStorage.setItem", key, content);
            }
            catch
            {
                return await ValueTask.FromResult<object>(null);
            }
        }

        public static async ValueTask<string> GetFromLocalStorage(this IJSRuntime js, string key)
        {
            try
            {
                return await js.InvokeAsync<string>("localStorage.getItem", key);
            }
            catch
            {
                return await ValueTask.FromResult<string>(null);
            }
        }

        public static async ValueTask<object> RemoveItem(this IJSRuntime js, string key)
        {
            try
            {
                return await js.InvokeAsync<object>("localStorage.removeItem", key);
            }
            catch
            {
                return await ValueTask.FromResult<object>(null);
            }
        }

        public static ValueTask<bool> ScrollToElementId(this IJSRuntime js, string elementId)
        {
            return js.InvokeAsync<bool>("scrollToElementId", elementId);
        }

        public static async ValueTask<object> SetRedoTrigger(this IJSRuntime js, string trigger)
        {
            try
            {
                return await js.SetInLocalStorage(trigger, DateTime.Now.ToString());
            }
            catch
            {
                return await ValueTask.FromResult<object>(null);
            }
        }

        public static async ValueTask<bool> CanRedoAction(this IJSRuntime js, string trigger, TimeSpan minimumInterval)
        {
            try
            {
                string created;
                try
                {
                    created = await js.GetFromLocalStorage(trigger);
                }
                catch (InvalidOperationException)
                {
                    return true;
                }

                if (DateTime.TryParse(created, out var result))
                {
                    var span = DateTime.Now.Subtract(result);

                    if (span < minimumInterval)
                        return false;
                }
                return true;
            }
            catch
            {
                return true;
            }
        }
    }
}