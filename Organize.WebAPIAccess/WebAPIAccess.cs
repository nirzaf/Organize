using Organize.Shared.Contracts;
using Organize.Shared.Enitites;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Organize.WebAPIAccess
{
    public class WebAPIAccess : IPersistenceService
    {
        private readonly HttpClient _httpClient;

        public WebAPIAccess(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<User> GetUserByTokenAsync()
        {
            var response = await _httpClient.GetAsync("api/users/");
            await ThrowExceptionIfResponseIsNotSuccessfulAsync(response);

            var foundUser = await response.Content.ReadFromJsonAsync<User>();
            return foundUser;
        }

        public async Task<User> AuthenticateAndGetUserAsync(User user)
        {
            var response = await _httpClient.PostAsJsonAsync<User>("api/users/authenticate", user);
            await ThrowExceptionIfResponseIsNotSuccessfulAsync(response);

            var foundUser = await response.Content.ReadFromJsonAsync<User>();
            return foundUser;
        }

        public async Task DeleteAsync<T>(T entity) where T : BaseEntity
        {
            var requestUri = EntityRouteAssignments.DeleteEntityRouteAssignment[typeof(T)] + "/" + entity.Id;
            Console.WriteLine(requestUri);
            var response = await _httpClient.DeleteAsync(requestUri);
            await ThrowExceptionIfResponseIsNotSuccessfulAsync(response);
        }

        public async Task<IEnumerable<T>> GetAsync<T>(Expression<Func<T, bool>> whereExpression) where T : BaseEntity
        {
            var requestUri = EntityRouteAssignments.GetEntityRouteAssignment[typeof(T)];
            var list = await _httpClient.GetFromJsonAsync<IList<T>>(requestUri);

            return list;
        }

        public Task InitAsync()
        {
            return Task.FromResult(true);
        }

        public async Task<int> InsertAsync<T>(T entity) where T : BaseEntity
        {
            var requestUri = EntityRouteAssignments.PostEntityRouteAssignment[typeof(T)];
            var response = await _httpClient.PostAsJsonAsync<T>(requestUri, entity);
            await ThrowExceptionIfResponseIsNotSuccessfulAsync(response);
            var idString = await response.Content.ReadAsStringAsync();

            return Convert.ToInt32(idString);
        }

        public async Task UpdateAsync<T>(T entity) where T : BaseEntity
        {
            var requestUri = EntityRouteAssignments.PutEntityRouteAssignment[typeof(T)];
            var response = await _httpClient.PutAsJsonAsync<T>(requestUri, entity);
            await ThrowExceptionIfResponseIsNotSuccessfulAsync(response);
        }

        private async Task ThrowExceptionIfResponseIsNotSuccessfulAsync(HttpResponseMessage responseMessage)
        {
            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorMessage = await TryToGetMessageAsync(responseMessage);
                throw new Exception(errorMessage.Message);
            }
        }

        private async Task<ErrorMessage> TryToGetMessageAsync(HttpResponseMessage responseMessage)
        {
            try
            {
                return await responseMessage.Content.ReadFromJsonAsync<ErrorMessage>();
            }
            catch
            {
                return new ErrorMessage { Message = "Unknown Error" };
            }
        }   
    }
}
