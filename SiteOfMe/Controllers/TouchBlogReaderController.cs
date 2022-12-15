using SiteOfMe.DAL;
using SiteOfMe.Dtos;
using SiteOfMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SiteOfMe.Controllers
{
    public class TouchBlogReaderController : ApiController
    {
        private readonly UnitOfWork _uow = new UnitOfWork();

        private readonly Expression<Func<Post, PostDto>> PostToSummaryDtoSelector = (post) =>  new PostDto {
            Id = post.PostId,
            Title = post.Title,
            Icon = post.Thumb,
            PublishDate = post.PublishDate,
            Author = post.User.DisplayName
        };

        private readonly Expression<Func<Post, PostDto>> PostToDtoSelector = (post) => new PostDto
        {
            Id = post.PostId,
            Title = post.Title,
            Icon = post.Thumb,
            PublishDate = post.PublishDate,
            Author = post.User.DisplayName,
            Text = post.Body.Value
        };

        // GET api/<controller>
        public IEnumerable<PostDto> Get()
        {
            var result = _uow.PostRep.GetPublishedPosts(null, null).Select(PostToSummaryDtoSelector);

            return result.ToList(); // executing query and returning DTOs list
        }

        // GET api/<controller>/5
        public PostDto Get(int id)
        {
            var result = _uow.PostRep.GetPublishedPosts(null, null).Select(PostToDtoSelector);

            return result.FirstOrDefault();
        }

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}