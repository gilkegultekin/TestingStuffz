using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostgreSqlTest.Dto
{
    public class BlogDto
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
        public List<PostDto> Posts { get; set; }
    }
}
