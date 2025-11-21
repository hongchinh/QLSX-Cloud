using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace QLSX.Web.JavaScript
{
    public class Css
    {
        private readonly IJSRuntime _jsRuntime;

        public Css(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }
        
        public async Task ShowBodyOverflow()
        {
            await _jsRuntime.InvokeAsync<string>(JSInteropConstants.ShowBodyOverflow);
        }

        public async Task<string> HideBodyOverflow()
        {
            return await _jsRuntime.InvokeAsync<string>(JSInteropConstants.HideBodyOverflow);
        }

        public async Task<string> ShowEditCustomers()
        {
            return await _jsRuntime.InvokeAsync<string>(JSInteropConstants.ShowEditCustomers);
        }

        public async Task<string> HideEditCustomers()
        {
            return await _jsRuntime.InvokeAsync<string>(JSInteropConstants.HideEditCustomers);
        }
    }
}
