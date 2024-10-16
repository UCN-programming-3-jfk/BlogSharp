﻿using System;

namespace DataAccess.DaoClasses
{
    public static class DAOFactory
    {
        /// <summary>
        /// A factory which can create a specific repository.
        /// This centralizes the creation functionality here (high coupling)
        /// and thereby lowering the coupling in other parts of the code
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionstring"></param>
        /// <returns></returns>
        public static T CreateRepository<T> (string connectionstring) where T : class
        {
            if (typeof(T) == typeof(IAuthorDAO)) return new AuthorDAO(connectionstring) as T;
            if (typeof(T) == typeof(IBlogPostDAO)) return new BlogPostDAO(connectionstring) as T;
            throw new ArgumentException($"Unknown type {typeof(T).FullName}");
        }
    }
}