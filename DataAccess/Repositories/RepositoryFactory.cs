using System;

namespace DataAccess.Repositories
{
    public static class RepositoryFactory
    {

        public static T CreateRepository<T> (string connectionstring) where T : class
        {
            switch (typeof(T).Name)
            {
                case "IAuthorRepository": return new AuthorRepository(connectionstring) as T;
                case "IBlogPostRepository": return new BlogPostRepository(connectionstring) as T;
            }
            throw new ArgumentException($"Unknown type {typeof(T).FullName}");
        }
    }
}