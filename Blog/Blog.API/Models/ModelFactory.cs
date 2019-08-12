using AutoMapper;
using Blog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.API.Models
{
    public static class ModelFactory
    {
        public static CategoryModel CreateModel(Category category)
        {
            var categoryModel = Mapper.Map<Category, CategoryModel>(category);
            if (categoryModel.Articles != null && categoryModel.Articles.Count() > 0)
            {
                categoryModel.Articles = category.Articles.Select(x => CreateModel(x));
            }
            return categoryModel;
        }

        public static ArticleModel CreateModel(Article article)
        {
            var articleModel = Mapper.Map<Article, ArticleModel>(article);
            return articleModel;
        }

        public static UserModel CreateModel(User user)
        {
            var userModel = Mapper.Map<User, UserModel>(user);
            return userModel;
        }
    }
}
