using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tupy.Extensions;

namespace Tupy.Logger.Providers
{
    public class TextFileProvider : FileBasedProvider, IProvider
    {
        private const string PREFIX = "Log-";
        private const string EXTENSION = ".txt";

        public ProviderTypes Type { get => ProviderTypes.TextFile; }
        public string ID { get; set; }
        public string FolderPath { get; set; }

        private DateTime? DecomposeFileName(string name)
        {
            DateTime? result = null;

            if (name.Length == 16)
            {
                var text = name.Substring(PREFIX.Length, 8);
                if (DateTime.TryParseExact(text, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                {
                    result = date;
                }
            }
            return result;
        }

        private string GetFileName(DateTime date)
        {
            var datetext = date.ToStringYYYYMMDD();
            var filename = $"{PREFIX}{datetext}{EXTENSION}";

            return filename;
        }

        private string GetSourceFolder(string sourceName)
        {
            var path = Path.Combine(FolderPath, sourceName);

            return path;
        }

        private string GetCompleteFileName(string sourceName, DateTime date)
        {
            var filepath = GetSourceFolder(sourceName);

            var filename = GetFileName(date);

            var completepath = Path.Combine(filepath, filename);

            return completepath;
        }

        private async Task<ExecutionResponse> DeleteFileAsync(string completePath)
        {
            ExecutionResponse result = new ExecutionResponse(false, "Não foi possível excluir o arquivo.");

            try
            {
                if (File.Exists(completePath))
                {
                    await Task.Factory.StartNew(() => File.Delete(completePath));
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

        private string FormatLine(EventEntry entry)
        {
            var list = new List<string>
            {
                //entry.TimeGenerated.ToString("yyyy-MM-ddTHH:mm:ss.fff"),
                entry.TimeGenerated.ToStringFull(),
                entry.Source,
                entry.EntryType.ToString(),
                entry.Message,
                entry.Category,
                entry.Data,
                entry.MachineName,
                entry.UserName
            };

            //var result = list.Select(r => r + ";").ToString();
            var result = string.Join(";", list);

            return result;
        }

        private async Task<ExecutionResponse> WriteLogAsync(string content, string filePath)
        {
            ExecutionResponse result = new ExecutionResponse(false, "Não foi escrever no arquivo.");

            try
            {
                await Task.Run(() =>
                {
                    File.AppendAllLines(filePath, new string[] { content });
                    result = new ExecutionResponse(true);
                });

                ////byte[] encodedText = Encoding.Unicode.GetBytes(text);
                //byte[] encodedText = Encoding.UTF8.GetBytes(content);

                //using (FileStream source = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
                //{
                //    await source.WriteAsync(encodedText, 0, encodedText.Length);
                //};
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

        public async Task<ExecutionResponse> WriteEntryAsync(EventEntry entry)
        {
            var folderpath = GetSourceFolder(entry.Source);
            var filepath = GetCompleteFileName(entry.Source, entry.TimeGenerated.Date);

            var result = CheckFolder(folderpath);

            if (!result.IsSuccess)
                return result;

            result = CheckFile(filepath);

            if (!result.IsSuccess)
                return result;

            var line = FormatLine(entry);

            result = await WriteLogAsync(line, filepath);

            return result;
        }

        private async Task<ExecutionResponse> RemoveFiles(string folderPath, DateTime limitDate)
        {
            ExecutionResponse result = null;

            DateTime? filenamedate = null;

            List<string> deleteerrors = new List<string>();

            string completepath = null;

            var files = Directory
                .EnumerateFiles(folderPath, PREFIX + "*.txt", SearchOption.TopDirectoryOnly)
                .Select(r => r.Substring(folderPath.Length + 1))
                .ToList()
                .OrderBy(r => r);


            foreach (var item in files)
            {
                filenamedate = DecomposeFileName(item);

                if (filenamedate.HasValue && ((DateTime)filenamedate) <= limitDate)
                {
                    completepath = Path.Combine(folderPath, item);

                    result = await DeleteFileAsync(completepath);

                    if (!result.IsSuccess)
                        deleteerrors.Add(item);
                }
            }

            if (deleteerrors.Count == 0)
                result = new ExecutionResponse(true);
            else
            {
                result = new ExecutionResponse(false)
                {
                    Message = "Ocorreram erros durante a exclusão dos arquivos",
                    Content = deleteerrors.Select(r => r + ",").ToString()
                };
            }

            return result;
        }

        public async Task<ExecutionResponse> RemoveBeforeAsync(string sourceName, DateTime date)
        {
            ExecutionResponse result = null;

            var limitdate = DateTime.Today.AddDays(-1);

            if (date.Date < limitdate)
                limitdate = date.Date;

            var folderpath = GetSourceFolder(sourceName);

            result = CheckFolder(folderpath);

            if (!result.IsSuccess)
            {
                result.Source = this.GetType().Name;
                return result;
            }

            result = await RemoveFiles(folderpath, limitdate);

            if (!result.IsSuccess)
            {
                result.Source = this.GetType().Name;
            }

            return result;
        }
    }
}
