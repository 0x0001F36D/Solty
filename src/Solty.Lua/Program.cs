
namespace Solty.Lua
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    using NLua;

    class Program
    {
        public class HttpClientProxy
        {
            private readonly HttpClient _client = new HttpClient();

            public async Task<string> Get(string url)
            {
                var got = await this._client.GetAsync(url);
                var content = await got.Content.ReadAsStringAsync();
                return content;
            }
        }

        static async Task Main(string[] args)
        {
            var lua = new Lua();
            lua["http"] = new HttpClientProxy();

            var func = lua.LoadFile("./http.lua");
            var result = await (func.Call()[0] as Task<string>);            
            Console.WriteLine(result);


            Console.ReadKey();
            
        }
    }
}