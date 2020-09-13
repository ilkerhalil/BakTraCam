﻿using BakTraCam.Core.Business.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using BakTraCam.Service.DataContract;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BakTraCam.Core.Business.Application
{
    public class BakimApplication : ApplicationBase<BakimApplication>, IBakimApplication
    {
        private readonly IBakimDomain _bakimDom;

        public BakimApplication(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _bakimDom = serviceProvider.GetService<IBakimDomain>();
        }

     
        public Task<IEnumerable<BakimModelBasic>> BakimlistesiGetirAsync()
        {
            return _bakimDom.BakimlariGetirAsync();
        }
        public Task<BakimModel> KaydetBakimAsync(BakimModel bakimModel)
        {
            return _bakimDom.KaydetBakimAsync(bakimModel);
        }
       
    }
}
