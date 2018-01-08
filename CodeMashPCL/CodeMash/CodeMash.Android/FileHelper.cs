
using System.IO;
using Xamarin.Forms;
using CodeMash.Droid;
using CodeMash.Data;

[assembly: Dependency(typeof(FileHelper))]
namespace CodeMash.Droid
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}