﻿using BakTraCam.Core.Business.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using BakTraCam.Service.DataContract;
using System.Threading.Tasks;

namespace BakTraCam.Core.Business.Application
{
    public class BakimApplication : ApplicationBase<BakimApplication>, IBakimApplication
    {
        private readonly IBakimDomain _bakimDom;

        public BakimApplication(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _bakimDom = serviceProvider.GetService<IBakimDomain>();
        }
        public Task<IEnumerable<BakimModel>> BakimlistesiGetirAsync()
        {
            return _bakimDom.BakimlariGetirAsync();
        }

    }
}
