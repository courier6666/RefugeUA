using RefugeUA.FileManager;

namespace RefugeUA.WebApp.Server.Services
{
    public class FileManager : IFileManager
    {
        private readonly IWebHostEnvironment env;

        public FileManager(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public Task<bool> FileExistsAsync(string directory, string filename)
        {
            var path = Path.Combine(env.WebRootPath, directory, filename);
            return Task.FromResult(File.Exists(path));
        }

        public async Task<byte[]?> GetFileAsync(string directory, string filename)
        {
            var path = Path.Combine(env.WebRootPath, directory, filename);

            if (!await this.FileExistsAsync(directory, filename))
            {
                return null;
            }

            try
            {
                var content = await File.ReadAllBytesAsync(path);
                return content;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<DeleteFileResult> RemoveFileAsync(string directory, string filename)
        {
            var path = Path.Combine(env.WebRootPath, directory, filename);

            if (!await this.FileExistsAsync(directory, filename))
            {
                return DeleteFileResult.FileNotFound;
            }

            try
            {
                File.Delete(path);
                return DeleteFileResult.Success;
            }
            catch (Exception ex)
            {
                return DeleteFileResult.Failure;
            }
        }

        public async Task<string?> UploadFileAsync(byte[] content, string directory, string format)
        {
            var filename = $"{DateTime.Now:yyyy-MM-ddTHH_mm_ss_fff}-{Guid.NewGuid()}-file.{format}";

            var path = Path.Combine(env.WebRootPath, directory, filename);

            await File.WriteAllBytesAsync(path, content);
            return filename;
        }

        public async Task<bool> UploadFileWithFilenameAsync(byte[] content, string directory, string filename, string format)
        {
            var path = Path.Combine(env.WebRootPath, directory, filename, $".{format}");

            try
            {
                await File.WriteAllBytesAsync(path, content);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
