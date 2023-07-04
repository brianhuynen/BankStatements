using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;

using BankStatements.Models.Domain;

using System.Net;
using Xunit.Abstractions;
using Xunit.Sdk;
using NuGet.Protocol;
using System.Text.Json;
using System.Text;

namespace BankStatementsTest
{
    public class BankStatementTests : IClassFixture<WebApplicationFactory<Program>>
    {
        readonly HttpClient _client;
        private readonly ITestOutputHelper _output;

        public BankStatementTests(WebApplicationFactory<Program> app, ITestOutputHelper output)
        {
            _client = app.CreateClient();
            _output = output;
        }
        /// <summary>
        /// Getting all the statements in the DB should return 200 OK
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GET_Retrieves_Statements()
        {
            var response = await _client.GetAsync("/api/bankstatements");
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }
        /// <summary>
        /// Getting all the IBAN in the DB should return 200 OK
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GET_Retrieves_Ibans()
        {
            var response = await _client.GetAsync("/api/ibans");
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }
        /// <summary>
        /// A succesful POST operation should return 201 Created
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task POST_Statement_ShouldReturn_201Created()
        {
            BankStatement statement = new BankStatement
            {
                Id = 2,
                ReferenceId = 2,
                IbanNo = "NL12BANK3456789100",
                BalanceStart = 0,
                Mutation = 1,
                BalanceEnd = 1,
            };
            var jsonString = JsonSerializer.Serialize(statement);
            var postMessage = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/bankstatements", postMessage);
            Assert.True(response.StatusCode == HttpStatusCode.Created);
        }
        /// <summary>
        /// The database has been preseeded with an entry with ReferenceId = 1, so adding another statement with the same
        /// ReferenceId shuld return a 409
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task POST_Statement_WithDupeReference_ShouldReturn_409Conflict()
        {
            BankStatement statement = new BankStatement
            {
                Id = 2,
                ReferenceId = 1,
                IbanNo = "NL12BANK3456789100",
                BalanceStart = 0,
                Mutation = 1,
                BalanceEnd = 1,
            };
            var jsonString = JsonSerializer.Serialize(statement);
            var postMessage = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/bankstatements", postMessage);
            Assert.True(response.StatusCode == HttpStatusCode.Conflict);
        }
        /// <summary>
        /// If you try to submit the same statement twice, it should return a 409 Status code 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task POST_PostingDuplicateStatement_ShouldReturn_409Conflict()
        {
            BankStatement statement = new BankStatement
            {
                Id = 2,
                ReferenceId = 1,
                IbanNo = "NL12BANK3456789100",
                BalanceStart = 0,
                Mutation = 1,
                BalanceEnd = 1,
            };
            var jsonString1 = JsonSerializer.Serialize(statement);
            var jsonString2 = JsonSerializer.Serialize(statement);

            var postMessage1 = new StringContent(jsonString1, Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/bankstatements", postMessage1);

            var postMessage2 = new StringContent(jsonString2, Encoding.UTF8, "application/json");
            var response2 = await _client.PostAsync("/api/bankstatements", postMessage2);
            Assert.True(response2.StatusCode == HttpStatusCode.Conflict);
        }
        /// <summary>
        /// If BalanceEnd does not match BalanceStart + Mutation, return 409 Conflict
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task POST_Statement_WithWrongEndBalance_ShouldReturn_409Conflict()
        {
            BankStatement statement = new BankStatement
            {
                Id = 2,
                ReferenceId = 2,
                IbanNo = "NL12BANK3456789100",
                BalanceStart = 0,
                Mutation = 1,
                BalanceEnd = 0,
            };
            var jsonString = JsonSerializer.Serialize(statement);
            var postMessage = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/bankstatements", postMessage);
            Assert.True(response.StatusCode == HttpStatusCode.Conflict);
        }
        /// <summary>
        /// A statement with an invalid Iban should not be processed and return 422 Unprocessable Entity
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task POST_Statement_WithInvalidIban_ShouldReturn_422Unprocessable()
        {
            BankStatement statement = new BankStatement
            {
                Id = 2,
                ReferenceId = 2,
                IbanNo = "NL12BANK3456789101",
                BalanceStart = 0,
                Mutation = 1,
                BalanceEnd = 0,
            };
            var jsonString = JsonSerializer.Serialize(statement);
            var postMessage = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/bankstatements", postMessage);
            Assert.True(response.StatusCode == HttpStatusCode.UnprocessableEntity);
        }
        /// <summary>
        /// If any of the entries in the JSON object is not of an expected type, return 400 Bad Request.
        /// Here referenceId is a letter instead of expected long
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task POST_Statement_WithInvalidJSON_ShouldReturn_400BadRequest1()
        {
            string statement = "{" +
                "\"id\":4," +
                "\"referenceId\":a," +
                "\"ibanNo\":\"NL12BANK3456789101\"," +
                "\"balanceStart\":0," +
                "\"mutation\":1," +
                "\"balanceEnd\":1}";
            var jsonString = JsonSerializer.Serialize(statement);
            var postMessage = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/bankstatements", postMessage);
            Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
        }
        /// <summary>
        /// If any of the entries in the JSON object is not of an expected type, return 400 Bad Request.
        /// Here balanceStart is a letter instead of expected double
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task POST_Statement_WithInvalidJSON_ShouldReturn_400BadRequest2()
        {
            string statement = "{" +
                "\"id\":4," +
                "\"referenceId\":4," +
                "\"ibanNo\":\"NL12BANK3456789101\"," +
                "\"balanceStart\":a," +
                "\"mutation\":1," +
                "\"balanceEnd\":1}";
            var jsonString = JsonSerializer.Serialize(statement);
            var postMessage = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/bankstatements", postMessage);
            Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
        }
        /// <summary>
        /// If any of the entries in the JSON object is not of an expected type, return 400 Bad Request.
        /// Here mutation is a letter instead of expected double
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task POST_Statement_WithInvalidJSON_ShouldReturn_400BadRequest3()
        {
            string statement = "{" +
                "\"id\":4," +
                "\"referenceId\":4," +
                "\"ibanNo\":\"NL12BANK3456789101\"," +
                "\"balanceStart\":0," +
                "\"mutation\":a," +
                "\"balanceEnd\":0}";
            var jsonString = JsonSerializer.Serialize(statement);
            var postMessage = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/bankstatements", postMessage);
            Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
        }
        /// <summary>
        /// If any of the entries in the JSON object is not of an expected type, return 400 Bad Request.
        /// Here balanceEnd is a letter instead of expected double
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task POST_Statement_WithInvalidJSON_ShouldReturn_400BadRequest4()
        {

            string statement = "{" +
                "\"id\":4," +
                "\"referenceId\":4," +
                "\"ibanNo\":\"NL12BANK3456789101\"," +
                "\"balanceStart\":0," +
                "\"mutation\":1," +
                "\"balanceEnd\":a}";
            var jsonString = JsonSerializer.Serialize(statement);
            var postMessage = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/bankstatements", postMessage);
            Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
        }
    }
}