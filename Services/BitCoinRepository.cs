using BitCoinManagerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitCoinManager.Services
{
    public class BitCoinRepository
    {
        private readonly GlobalizationHandler _global;
        //private readonly BitCoinRepositoryClient _repositoryClient;

        public BitCoinRepository(GlobalizationHandler global) //, BitCoinRepositoryClient repositoryClient)
        {
            _global = global;
            //_repositoryClient = repositoryClient;
        }

        public bool ValidateLogin(User user)
        {
            return true;
        }
    }
}
