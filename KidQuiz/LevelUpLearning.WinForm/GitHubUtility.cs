//TODO: Not sure if this class should go to a common project, or stay in WinForm
using Octokit;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace LevelUpLearning.WinForms
{
    public static class GitHubUtility
    {
        private static GitHubClient gitHub = new GitHubClient(new ProductHeaderValue("LevelUpLearning"));


        //TODO: Add new version check into this class

        public static GitHubFileResponse GetSpellingLists()
        {
            //TODO: I just don't like how this is done - come back and make it... not like this
            var response = new GitHubFileResponse();
            var filePaths = GetFilePaths("Spelling%20Lists", true);
            foreach (var path in filePaths)
            {
                response.Files.Add(new GitHubFile() { Path = path });
            }
            return response;
        }
        public static void DownloadFiles(GitHubFileResponse fileResponse)
        {
            var sb = new StringBuilder();
            
            foreach (var f in fileResponse.Files)
            {
                //TODO: Make these async calls, speed up the process
                try
                {
                    using (var client = new WebClient())
                    {
                        f.Contents = client.DownloadString(f.Path);
                    }
                }
                catch (Exception ex)
                {
                    sb.AppendLine(ex.ToString());
                }
            }

            fileResponse.ErrorMessage = sb.ToString();
        }
        private static List<string> GetFilePaths(string path, bool includeSubfolders)
        {
            var contents = new List<string>();

            var repoContents = gitHub.Repository.Content.GetAllContents("NoahWright87", "LevelUpLearning", path).Result;

            foreach (var c in repoContents)
            {
                if (c.Type == ContentType.Dir && includeSubfolders)
                {
                    contents.AddRange(GetFilePaths(c.Path, true));
                }
                else if (c.Type == ContentType.File)
                {
                    contents.Add(c.DownloadUrl);
                }
            }

            return contents;
        }
    }
    public class GitHubFileResponse
    {
        public bool ContentsWereFetched { get; set; } = false;
        public string ErrorMessage { get; set; }
        public List<GitHubFile> Files { get; set; } = new List<GitHubFile>();
    }
    public class GitHubFile
    {
        public string Path { get; set; }
        public string Contents { get; set; }
    }
}
