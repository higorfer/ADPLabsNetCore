using ADPLabsNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ADPLabsTest
{
    public class Tests
    {

        [Test]
        public async Task TestGetAdpTask()
        {
            using var application = new WebApplicationFactory<Program>();

            var client = application.CreateClient();

            var task = await client.GetAsync("/GetAdpTask");
            Assert.That(task.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task TestSubmitAdpTask()
        {
            using var application = new WebApplicationFactory<Program>();

            var client = application.CreateClient();
            
            var task = await client.GetAsync("/ProcessTask");
            Assert.That(task.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}