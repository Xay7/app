using ChatApi.Models;
using ChatApi.DAL;
namespace ChatApi.Interfaces;

public interface IKeycloakUserContext
{
    Task<ChatApi.DAL.KeycloakUserContext.User?> FindUserByLogin(string login);
}