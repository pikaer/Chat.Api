using Chat.Model.Utils;
using Chat.Repository;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Service
{
    class GoldCoinService
    {
        private GoldCoinRespository goldCoinDal = SingletonProvider<GoldCoinRespository>.Instance;
    }
}
