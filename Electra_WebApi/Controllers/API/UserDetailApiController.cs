using EntityClass;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Electra_WebApi.Controllers
{
    public class UserDetailApiController : ApiController
    {
        private readonly CraModel db = new CraModel();

        // GET: api/UserDetailApi
        public IQueryable<User_detail> GetUserDetail()
        {
            return db.User_detail;
        }
        // GET: api/UserDetailApi/5
        [ResponseType(typeof(User_detail))]
        public async Task<IHttpActionResult> GetUserDetail(string id)
        {
            User_detail uDet = await db.User_detail.FindAsync(id);
            if (uDet == null)
            {
                return NotFound();
            }
            return Ok(uDet);
        }
        // POST: api/UserDetailApi
        [ResponseType(typeof(User_detail))]
        public async Task<IHttpActionResult> PostUserDetail(User_detail uDet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.User_detail.Add(uDet);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(uDet.User_Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = uDet.User_Name }, uDet);
        }
        // DELETE: api/UserDetailApi/5
        [ResponseType(typeof(User_detail))]
        public async Task<IHttpActionResult> DeleteUserDetail(string id)
        {
            User_detail uDet = await db.User_detail.FindAsync(id);
            if (uDet == null)
            {
                return NotFound();
            }
            db.User_detail.Remove(uDet);
            await db.SaveChangesAsync();
            return Ok(uDet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool UserExists(string uName)
        {
            return db.User_detail.Count(e => e.User_Name.ToUpper().Trim() == uName.Trim().ToUpper()) > 0;
        }
    }
}
