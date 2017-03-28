using DAL;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Service.Controllers
{
    public class ServiceController : ApiController
    {
        public Switch Get()
        {
            return SwitchRepo.Last();
        }

        public void Post(Switch sw)
        {
            SwitchRepo.Add(sw);
        }
}
}
