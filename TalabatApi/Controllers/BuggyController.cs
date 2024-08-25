using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Repository.Data;

namespace TalabatApi.Controllers
{
    public class BuggyController : ApiBaseController
    {
        private readonly StoreContext dbContext;

        public BuggyController(StoreContext dbContext)
        {
            this.dbContext = dbContext;
        }


    }
}
