using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostgreSqlTest.Domain;
using PostgreSqlTest.Dto;

namespace PostgreSqlTest.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
	    private readonly BlogContext _context;

	    private readonly IMapper _mapper;

	    public ValuesController(BlogContext context, IMapper mapper)
	    {
	        _context = context;
	        _mapper = mapper;
	    }

	    // GET api/values
		[HttpGet]
		public async Task<ActionResult<IEnumerable<BlogDto>>> Get()
		{
		    var list = await _context.Blogs.Include(b => b.Posts).ToListAsync();

		    var blogDtos = _mapper.Map<List<Blog>, List<BlogDto>>(list);

            return blogDtos;
		    //return new string[] { "value1", "value2" };
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public ActionResult<string> Get(int id)
		{
			return "value";
		}

		// POST api/values
		[HttpPost]
		public async Task Post([FromBody] Blog entity)
		{
		    await _context.Blogs.AddAsync(entity);
		    await _context.SaveChangesAsync();
		}

	    // POST api/values/post
        [HttpPost]
        [Route("post")]
	    public async Task CreatePost([FromBody] Post entity)
	    {
	        await _context.Posts.AddAsync(entity);
	        await _context.SaveChangesAsync();
	    }

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
