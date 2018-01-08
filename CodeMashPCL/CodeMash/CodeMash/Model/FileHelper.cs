//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using PCLStorage;

//namespace CodeMash.Model
//{
//    public class FileHelper
//    {
//        private const string subFolderName = "CodeMash";

//        public async Task CreateFile(string fileName, string fileContent)
//        {
//            var rootFolder = FileSystem.Current.LocalStorage;
//            var folder = await rootFolder.CreateFolderAsync(subFolderName, CreationCollisionOption.OpenIfExists);
//            var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
//            await file.WriteAllTextAsync(fileContent);
//        }

//        public async Task DeleteFile(string fileName)
//        {
//            var rootFolder = FileSystem.Current.LocalStorage;
//            var folder = await rootFolder.GetFolderAsync(subFolderName);
//            var file = await folder.GetFileAsync(fileName);
//            await file.DeleteAsync();
//        }

//        public async Task<string> LoadFile(string fileName)
//        {
//            try
//            {
//                var rootFolder = FileSystem.Current.LocalStorage;
//                var folder = await rootFolder.GetFolderAsync(subFolderName);
//                var file = await folder.GetFileAsync(fileName);
//                return await file.ReadAllTextAsync();
//            }
//            catch(Exception ex)
//            {
//                string h = ex.Message;
//            }
//            return null;
//        }

//        public async Task<bool> CheckifFileExists(string fileName)
//        {
//            var result = false;
//            var rootFolder = FileSystem.Current.LocalStorage;
//            if (await rootFolder.CheckExistsAsync(subFolderName) == ExistenceCheckResult.FolderExists)
//            {
//                var folder = await rootFolder.GetFolderAsync(subFolderName);
//                result = await folder.CheckExistsAsync(fileName) == ExistenceCheckResult.FileExists;
//            }
//            return result;
//        }

//    }
//}
