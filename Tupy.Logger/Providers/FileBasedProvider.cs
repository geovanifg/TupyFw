using System;
using System.IO;
using System.Threading.Tasks;

namespace Tupy.Logger.Providers
{
    public abstract class FileBasedProvider
    {
        protected ExecutionResponse CheckFolder(string folderPath)
        {
            ExecutionResponse result = new ExecutionResponse(false, "Não foi possível criar a pasta.");

            try
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                result = new ExecutionResponse(true);
            }
            catch (UnauthorizedAccessException ex)
            {
                result.Content = ex.Message;
            }
            catch (ArgumentNullException ex)
            {
                result.Content = ex.Message;
            }
            catch (PathTooLongException ex)
            {
                result.Content = ex.Message;
            }
            catch (DirectoryNotFoundException ex)
            {
                result.Content = ex.Message;
            }
            catch (IOException ex)
            {
                result.Content = ex.Message;
            }
            catch (Exception ex)
            {
                result.Content = ex.Message;
            }

            return result;
        }

        protected ExecutionResponse CheckFile(string completePath)
        {
            ExecutionResponse result = new ExecutionResponse(false, "Não foi possível criar o arquivo.");

            try
            {
                if (!File.Exists(completePath))
                {
                    File.Create(completePath);
                }
                result = new ExecutionResponse(true);
            }
            catch (UnauthorizedAccessException ex)
            {
                result.Content = ex.Message;
            }
            catch (ArgumentNullException ex)
            {
                result.Content = ex.Message;
            }
            catch (PathTooLongException ex)
            {
                result.Content = ex.Message;
            }
            catch (DirectoryNotFoundException ex)
            {
                result.Content = ex.Message;
            }
            catch (IOException ex)
            {
                result.Content = ex.Message;
            }
            catch (Exception ex)
            {
                result.Content = ex.Message;
            }

            return result;
        }

        protected async Task<ExecutionResponse> WriteFileOperationAsync(Func<ExecutionResponse> action)
        {
            ExecutionResponse result = new ExecutionResponse(false, "Não foi escrever no arquivo.");

            try
            {
                await Task.Run(() =>
                {
                    result = action?.Invoke();
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                result.Content = ex.Message;
            }
            catch (ArgumentNullException ex)
            {
                result.Content = ex.Message;
            }
            catch (PathTooLongException ex)
            {
                result.Content = ex.Message;
            }
            catch (DirectoryNotFoundException ex)
            {
                result.Content = ex.Message;
            }
            catch (IOException ex)
            {
                result.Content = ex.Message;
            }
            catch (Exception ex)
            {
                result.Content = ex.Message;
            }

            return result;
        }
    }
}
