using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefugeUA.FileManager
{
    public interface IFileManager
    {
        Task<string?> UploadFileAsync(byte[] content, string directory, string format);

        Task<bool> UploadFileWithFilenameAsync(byte[] content, string directory, string filename, string format);

        Task<DeleteFileResult> RemoveFileAsync(string directory, string path);

        Task<byte[]?> GetFileAsync(string directory, string filename);

        Task<bool> FileExistsAsync(string directory, string filename);
    }
}
