using BlogPessoal.src.models;
using BlogPessoal.src.dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPessoal.src.repositories
{ 
/// <summary>
/// <para>Sumary: Interface to represent CRUD actions in posts</para>
/// <para>Created by: Karol Oliveira</para>
/// <para>Version: 1.0</para>
/// <para>Date: 29/04/2022</para>
/// </summary>
public interface IPost
    {
        Task AddPostAsync(AddPostDTO post);
        Task UpdatePostAsync(UpdatePostDTO post);
        Task DeletePostAsync(int id);
        Task <PostModel> GetPostByIdAsync(int id);
        Task <List<PostModel>> GetAllPostsAsync();
        Task <List<PostModel>> GetAllPostsBySearchAsync(string title, string descriptionTheme, string nameCreator);
    }
}
