using Microsoft.AspNetCore.Mvc;
using OneInc.Common.Model;
using OneInc.DAL.Repository;

namespace OneInc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncodedStringsController : Controller
    {
        private readonly IEncodedStringsRepo _repo;

        public EncodedStringsController(IEncodedStringsRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("{id}", Name = "GetEncodedStringById")]
        public ActionResult<EncodedString> GetEncodedStringById(string id)
        {
            var encodeString = _repo.GetEncodedStringById(id);

            if (encodeString != null)
            {
                return Ok(encodeString);
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult<EncodedString> CreateEncodedString(EncodedString encodedString)
        {

            _repo.CreatedEncodeString(encodedString);

            return CreatedAtRoute(nameof(GetEncodedStringById), new { Id = encodedString.Id }, encodedString);
        }
    }
}
