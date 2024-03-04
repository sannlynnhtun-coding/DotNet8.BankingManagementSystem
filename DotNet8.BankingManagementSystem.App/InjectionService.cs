//using Microsoft.JSInterop;
//using System.Threading.Tasks;

//namespace DotNet8.BankingManagementSystem.App
//{
//    public class InjectionService : IInjectionService
//    {
//        private readonly IJSRuntime _jSRuntime;

//        public InjectionService(IJSRuntime jSRuntime)
//        {
//            _jSRuntime = jSRuntime;
//        }

//        public async Task<T> InvokeAsync<T>(string identifier, params object[] args)
//        {
//            return await _jSRuntime.InvokeAsync<T>(identifier, args);
//        }

//        public async Task InvokeVoidAsync(string identifier, params object[] args)
//        {
//            await _jSRuntime.InvokeVoidAsync(identifier, args);
//        }

//        public async Task EnableLoading(bool enable)
//        {
//            await _jSRuntime.InvokeVoidAsync("jsFunctions.enableLoading", enable);
//        }

//        public async Task DisableLoading()
//        {
//            await _jSRuntime.InvokeVoidAsync("jsFunctions.enableLoading", false);
//        }

//        public async Task EndInterval()
//        {
//            await _jSRuntime.InvokeVoidAsync("jsFunctions.endInterval");
//        }
//    }
//}
