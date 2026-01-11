using System.Net.Http.Json;
using System.Text.Json;
using EventPlanner.Mobile.Models;
using System.Net.Http.Headers;

namespace EventPlanner.Mobile.Services;

public class ApiService
{
    private readonly HttpClient _http;
    public static string? AccessToken { get; private set; }
    public static bool IsAdmin { get; private set; }

    public ApiService()
    {
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };

        _http = new HttpClient(handler)
        {
            BaseAddress = new Uri(GetBaseUrl()),
            Timeout = TimeSpan.FromSeconds(5)
        };
    }

    private void EnsureAuthorization()
    {
        if (!string.IsNullOrWhiteSpace(AccessToken))
        {
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", AccessToken);
        }
    }

    private static string GetBaseUrl()
    {
#if ANDROID
        return "https://10.0.2.2:7187/";
#else
        return "https://localhost:7187/";
#endif
    }

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task<List<EventItem>> GetEventItemsAsync()
    {
        EnsureAuthorization();
        return await _http.GetFromJsonAsync<List<EventItem>>(
            "api/EventItems", JsonOptions
        ) ?? new List<EventItem>();
    }

    public async Task<EventItem?> CreateEventItemAsync(EventItem dto)
    {
        EnsureAuthorization();
        var res = await _http.PostAsJsonAsync("api/EventItems", dto);
        if (!res.IsSuccessStatusCode) return null;

        return await res.Content.ReadFromJsonAsync<EventItem>();
    }

    public async Task<bool> DeleteEventItemAsync(int id)
    {
        EnsureAuthorization();
        var res = await _http.DeleteAsync($"api/EventItems/{id}");
        return res.IsSuccessStatusCode;
    }

    public async Task<List<Organizer>> GetOrganizersAsync()
    {
        EnsureAuthorization();
        return await _http.GetFromJsonAsync<List<Organizer>>("api/Organizers")
               ?? new List<Organizer>();
    }

    public async Task<Organizer?> CreateOrganizerAsync(Organizer dto)
    {
        EnsureAuthorization();
        var res = await _http.PostAsJsonAsync("api/Organizers", dto);
        if (!res.IsSuccessStatusCode) return null;

        return await res.Content.ReadFromJsonAsync<Organizer>();
    }

    public async Task<bool> DeleteOrganizerAsync(int id)
    {
        EnsureAuthorization();
        var res = await _http.DeleteAsync($"api/Organizers/{id}");
        return res.IsSuccessStatusCode;
    }

    public async Task<bool> LoginAsync(string email, string password)
    {
        var res = await _http.PostAsJsonAsync("login?useCookies=false", new
        {
            email,
            password
        });

        if (!res.IsSuccessStatusCode) return false;

        var tokens = await res.Content.ReadFromJsonAsync<LoginResponse>(JsonOptions);
        if (tokens == null || string.IsNullOrWhiteSpace(tokens.AccessToken))
            return false;

        AccessToken = tokens.AccessToken;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", AccessToken);

        var me = await GetMeAsync();
        IsAdmin = me?.IsAdmin ?? false;

        return true;
    }

    public async Task<MeResponse?> GetMeAsync()
    {
        EnsureAuthorization();
        var res = await _http.GetAsync("api/account/me");
        if (!res.IsSuccessStatusCode) return null;

        return await res.Content.ReadFromJsonAsync<MeResponse>(JsonOptions);
    }

    public static void Logout()
    {
        AccessToken = null;
        IsAdmin = false;
    }
    public async Task<bool> RegisterToEventAsync(int participantId, int eventId)
    {
        EnsureAuthorization();

        var res = await _http.PostAsJsonAsync("api/registrations", new
        {
            participantId,
            eventId
        });

        return res.IsSuccessStatusCode;
    }

    public async Task<List<Participant>> GetParticipantsAsync()
    {
        EnsureAuthorization();
        return await _http.GetFromJsonAsync<List<Participant>>("api/Participants", JsonOptions)
               ?? new List<Participant>();
    }

    public async Task<Participant?> CreateParticipantAsync(Participant dto)
    {
        EnsureAuthorization();
        var res = await _http.PostAsJsonAsync("api/Participants", dto);
        if (!res.IsSuccessStatusCode) return null;
        return await res.Content.ReadFromJsonAsync<Participant>(JsonOptions);
    }

    public async Task<bool> DeleteParticipantAsync(int id)
    {
        EnsureAuthorization();
        var res = await _http.DeleteAsync($"api/Participants/{id}");
        return res.IsSuccessStatusCode;
    }

    public async Task<bool> CreateRegistrationAsync(Registration dto)
    {
        EnsureAuthorization();

        var res = await _http.PostAsJsonAsync("api/Registrations", dto);

        return res.IsSuccessStatusCode;
    }





}
