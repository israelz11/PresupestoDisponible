using PresupDisponible.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PresupDisponible.Models.Security
{
    public class GenericHistory
    {
        #region Properties 

        public HttpRequestBase Request { get; set; }
        public HttpSessionStateBase Session { get; set; }
        public string Description { get; set; }
        public string Module { get; set; }
        public Boolean AllowedCache { get; set; }

        #endregion

        #region Methods

        public GenericHistory()
        {

        }

        #endregion
    }
}