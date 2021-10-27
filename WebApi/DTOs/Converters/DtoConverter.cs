using DataAccess.Model;
using System.Collections.Generic;

namespace WebApi.DTOs.Converters
{
    public static class DtoConverter
    {
        #region Author conversion methods
        public static AuthorDto ToDto(this Author authorToConvert)
        {
            var authorDto = new AuthorDto();
            authorToConvert.CopyPropertiesTo(authorDto);
            return authorDto;
        }

        public static Author FromDto(this AuthorDto authorDtoToConvert)
        {
            var author = new Author();
            authorDtoToConvert.CopyPropertiesTo(author);
            return author;
        }

        public static IEnumerable<AuthorDto> ToDtos(this IEnumerable<Author> authorsToConvert)
        {
            foreach (var author in authorsToConvert)
            {
                yield return author.ToDto();
            }
        }

        public static IEnumerable<Author> FromDtos(this IEnumerable<AuthorDto> authorDtosToConvert)
        {
            foreach (var authorDto in authorDtosToConvert)
            {
                yield return authorDto.FromDto();
            }
        }
        #endregion

        #region BlogPost conversion methods
        public static BlogPostDto ToDto(this BlogPost blogPostToConvert)
        {
            var blogPostDto = new BlogPostDto();
            blogPostToConvert.CopyPropertiesTo(blogPostDto);
            return blogPostDto;
        }

        public static BlogPost FromDto(this BlogPostDto blogPostDtoToConvert)
        {
            var blogPost = new BlogPost();
            blogPostDtoToConvert.CopyPropertiesTo(blogPost);
            return blogPost;
        }

        public static IEnumerable<BlogPostDto> ToDtos(this IEnumerable<BlogPost> blogPostsToConvert)
        {
            foreach (var blogPost in blogPostsToConvert)
            {
                yield return blogPost.ToDto();
            }
        }

        public static IEnumerable<BlogPost> FromDtos(this IEnumerable<BlogPostDto> blogPostDtosToConvert)
        {
            foreach (var blogPostDto in blogPostDtosToConvert)
            {
                yield return blogPostDto.FromDto();
            }
        }
        #endregion
    }
}