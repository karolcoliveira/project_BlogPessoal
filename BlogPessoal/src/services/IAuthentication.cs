using System.Threading.Tasks;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;

namespace BlogPessoal.src.services
{
    /// <summary>
    /// <para>Summary: Interface Responsible for representing authentication actions</para>
    /// <para>Created by: Karol Oliveira</para>
    /// <para>Version: 1.0</para>
    /// <para>Data: 13/05/2022</para>
    /// </summary>
    public interface IAuthentication
    {
        string CodifyPassword(string password);
       Task AddUserWithoutDuplicateAsync(AddUserDTO user);
        string CreateToken(UserModel user);
       Task <AuthorizationDTO> GetAuthorizationAsync(AuthenticationDTO authentication);
    }
}
