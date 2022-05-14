using BitCoinManagerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitCoinManager.Services
{
    public class BitCoinRepository
    {
        private readonly BitCoinRepositoryApiClient _repositoryClient;

        public BitCoinRepository(BitCoinRepositoryApiClient repositoryClient)
        {
            _repositoryClient = repositoryClient;
        }

        public async Task<bool> ValidateLogin(User user)
        {
            user = await _repositoryClient.GetuserAsync(user);
            return user.Id > 0;
        }

        public async Task<int> CreateUser(User user)
        {
            user.Id = await _repositoryClient.CreateuserAsync(user);
            return user.Id;
        }

        public async Task<int> CreateOrder(int userId, Order order)
        {
            order.Id = await _repositoryClient.CreateorderAsync(userId, order);
            return order.Id;
        }
    }
}
