using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using static Hyperbliss.ApiClient;
using System.Diagnostics;

namespace Hyperbliss.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void Initialize()
        {
            ApiClient.ConfigureUrl = (config) => {
                config.BaseUrl = "http://jsonplaceholder.typicode.com/";
            };
        }

        [TestMethod]
        public async Task OneLineAsyncGetMethod()
        {
            foreach (var comment in (dynamic) await new ApiClient("http://jsonplaceholder.typicode.com/").GetAsync("comments"))
            {
                Debug.WriteLine((string) comment.body);
            }
        }

        [TestMethod]
        public void OneLineGetMethod()
        {
            foreach (var comment in (dynamic)new ApiClient("http://jsonplaceholder.typicode.com/").Get("comments"))
            {
                Debug.WriteLine((string)comment.body);
            }
        }

        [TestMethod]
        public void SimpleGetMethodWithMappings()
        {
            ApiClient client = new ApiClient();

            var comments = client.GetArray(
                "/comments",
                    (j) => new {
                        Name = j.Map("name"),
                        Body = j.Map("body")
                    }
                );
            
            foreach (var comment in comments)
            {
                string x = comment.Name;
                Debug.WriteLine(x);
            }
        }

        [TestMethod]
        public void SimplePost()
        {
            ApiClient client = new ApiClient();
            client.RequestCreated += (sender, e) => {
                Debug.WriteLine(e.Request.RequestUri);
            };

            client.Post(
                "/comments", new {
                    body = "Hello World"
                }
            );
        }

        [TestMethod]
        public async Task GetError()
        {
            try
            {
                foreach (var comment in (dynamic) await new ApiClient("http://show404.typicode.com/").GetAsync("comments"))
                {
                    Debug.WriteLine((string)comment.body);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            
        }
    }
}
