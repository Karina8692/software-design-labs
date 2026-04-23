using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Lab2.Tests;

public class MessengerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public MessengerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task SystemFlow_CreateUser_SendMessage_RetrieveHistory()
    {
        var newUser = new { name = "Test User" };
        var createUserResponse = await _client.PostAsJsonAsync("/users", newUser);
        createUserResponse.EnsureSuccessStatusCode();

        var newMessage = new { conversationId = 99, senderId = 1, text = "Це тестове повідомлення" };
        var sendResponse = await _client.PostAsJsonAsync("/messages", newMessage);
        sendResponse.EnsureSuccessStatusCode();

        var historyResponse = await _client.GetAsync("/conversations/99/messages");
        historyResponse.EnsureSuccessStatusCode();

        var messages = await historyResponse.Content.ReadAsStringAsync();
        Assert.Contains("Це тестове повідомлення", messages);
    }
}