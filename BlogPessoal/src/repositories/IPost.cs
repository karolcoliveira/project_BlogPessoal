using BlogPessoal.src.models;
using System.Collections.Generic;
using BlogPessoal.src.dtos;

namespace BlogPessoal.src.repositories
{ /// <summary>
   /// <para>Sumary: Interface to represent CRUD actions in posts</para>
   /// <para>Created by: Karol Oliveira</para>
   /// <para>Version: 1.0</para>
   /// <para>Date: 29/04/2022</para>
   /// </summary>
public interface IPost
    {
        void AddPost(AddPostDTO post);
        void UpdatePost(UpdatePostDTO post);
        void DeletePost(int id);
        PostModel GetPostById(int id);
        List<PostModel> GetAllPosts();
        List<PostModel> GetPostBySearch(string title, string descriptionTheme, string nameCreator);
    }
}
