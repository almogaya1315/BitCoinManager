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

        public bool ValidateLogin(User user)
        {
            user = Task.Run(async () => await _repositoryClient.GetuserAsync(user, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
            return user.Id > 0;
        }

        public int CreateUser(User user)
        {
            user.Id =Task.Run(async () => await _repositoryClient.CreateuserAsync(user, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
            return user.Id;
        }

        public int CreateOrder(int userId, Order order)
        {
            order.Id = Task.Run(async () => await _repositoryClient.CreateorderAsync(userId, order, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
            return order.Id;
        }
    }
}
