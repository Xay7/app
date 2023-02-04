using System.Collections.ObjectModel;
using ChatApi.Interfaces;
using Flurl;
using Flurl.Http;
using IdentityModel.Client;
using ChatApi.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ChatApi.DAL;

public class KeycloakUserContext : IKeycloakUserContext
{
    private readonly IConfiguration config;
    private readonly ILogger<KeycloakUserContext> logger;

    private IFlurlRequest Client => new Url(config["Endpoints:UserManagement:BaseUrl"])
        .ConfigureRequest(c => { });

    public KeycloakUserContext(IConfiguration config, ILogger<KeycloakUserContext> logger)
    {
        this.config = config;
        this.logger = logger;
    }

    public async Task<User?> FindUserByLogin(string login)
    {
        return await FindUserBy("username", login);
    }

    private async Task<User?> FindUserBy(string key, string value)
    {
        if (string.IsNullOrEmpty(value))
            return null;

        var queryParams = new Dictionary<string, object>
        {
            [key] = value
        };
        var users = await Client
            .AppendPathSegment(config["Endpoints:UserManagement:UserApiUrl"])
            .WithOAuthBearerToken(await GetAccessTokenAsync())
            .SetQueryParams(queryParams)
            .GetJsonAsync<IEnumerable<User>>();

        return users
            .Select(u => new User { UserName = u.UserName})
            .FirstOrDefault();
    }

    private async Task<string> GetAccessTokenAsync()
    {
        using var client = new HttpClient();
        var tokens = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = config["Endpoints:UserManagement:BaseUrl"] + config["Endpoints:UserManagement:TokenUrl"],
            ClientId = config["Endpoints:UserManagement:ClientId"],
            ClientSecret = config["Endpoints:UserManagement:ClientSecret"]
        });
        logger.LogInformation(config["Endpoints:UserManagement:BaseUrl"]);
        logger.LogInformation(config["Endpoints:UserManagement:TokenUrl"]);
        logger.LogInformation(config["Endpoints:UserManagement:ClientId"]);
        logger.LogInformation(config["Endpoints:UserManagement:ClientSecret"]);
        logger.LogError(tokens.ToString());
        if (tokens.IsError)
            throw new Exception();
        return tokens.AccessToken;
    }

    public class User
    {
        [JsonProperty("id")] public string? Id { get; set; }
        [JsonProperty("createdTimestamp")] public long CreatedTimestamp { get; set; }
        [JsonProperty("username")] public string? UserName { get; set; }
        [JsonProperty("enabled")] public bool? Enabled { get; set; }
        [JsonProperty("totp")] public bool? Totp { get; set; }
        [JsonProperty("emailVerified")] public bool? EmailVerified { get; set; }
        [JsonProperty("firstName")] public string? FirstName { get; set; }
        [JsonProperty("lastName")] public string? LastName { get; set; }
        [JsonProperty("email")] public string? Email { get; set; }

        [JsonProperty("disableableCredentialTypes")]
        public ReadOnlyCollection<string>? DisableableCredentialTypes { get; set; }

        [JsonProperty("requiredActions")] public ReadOnlyCollection<string>? RequiredActions { get; set; }
        [JsonProperty("notBefore")] public int? NotBefore { get; set; }
        [JsonProperty("access")] public UserAccess? Access { get; set; }
        [JsonProperty("attributes")] public Dictionary<string, IEnumerable<string>>? Attributes { get; set; }
        [JsonProperty("clientConsents")] public IEnumerable<UserConsent>? ClientConsents { get; set; }
        [JsonProperty("clientRoles")] public IDictionary<string, object>? ClientRoles { get; set; }
        [JsonProperty("credentials")] public IEnumerable<Credentials>? Credentials { get; set; }
        [JsonProperty("federatedIdentities")] public IEnumerable<FederatedIdentity>? FederatedIdentities { get; set; }
        [JsonProperty("federationLink")] public string? FederationLink { get; set; }
        [JsonProperty("groups")] public IEnumerable<string>? Groups { get; set; }
        [JsonProperty("origin")] public string? Origin { get; set; }
        [JsonProperty("realmRoles")] public IEnumerable<string>? RealmRoles { get; set; }
        [JsonProperty("self")] public string? Self { get; set; }

        [JsonProperty("serviceAccountClientId")]
        public string? ServiceAccountClientId { get; set; }
    }

    public class Credentials
    {
        [JsonProperty("algorithm")] public string? Algorithm { get; set; }
        [JsonProperty("config")] public IDictionary<string, string>? Config { get; set; }
        [JsonProperty("counter")] public int? Counter { get; set; }
        [JsonProperty("createdDate")] public long? CreatedDate { get; set; }
        [JsonProperty("device")] public string? Device { get; set; }
        [JsonProperty("digits")] public int? Digits { get; set; }
        [JsonProperty("hashIterations")] public int? HashIterations { get; set; }
        [JsonProperty("hashSaltedValue")] public string? HashSaltedValue { get; set; }
        [JsonProperty("period")] public int? Period { get; set; }
        [JsonProperty("salt")] public string? Salt { get; set; }
        [JsonProperty("temporary")] public bool? Temporary { get; set; }
        [JsonProperty("type")] public string? Type { get; set; }
        [JsonProperty("value")] public string? Value { get; set; }
    }

    public class UserAccess
    {
        [JsonProperty("manageGroupMembership")]
        public bool? ManageGroupMembership { get; set; }

        [JsonProperty("view")] public bool? View { get; set; }
        [JsonProperty("mapRoles")] public bool? MapRoles { get; set; }
        [JsonProperty("impersonate")] public bool? Impersonate { get; set; }
        [JsonProperty("manage")] public bool? Manage { get; set; }
    }

    public class FederatedIdentity
    {
        [JsonProperty("identityProvider")] public string? IdentityProvider { get; set; }
        [JsonProperty("userId")] public string? UserId { get; set; }
        [JsonProperty("userName")] public string? UserName { get; set; }
    }

    public class UserConsent
    {
        [JsonProperty("clientId")] public string? ClientId { get; set; }
        [JsonProperty("grantedClientScopes")] public IEnumerable<string>? GrantedClientScopes { get; set; }
        [JsonProperty("createdDate")] public long? CreatedDate { get; set; }
        [JsonProperty("lastUpdatedDate")] public long? LastUpdatedDate { get; set; }
    }
}