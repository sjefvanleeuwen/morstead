using System;
using Microsoft.Extensions.Configuration;
using Octokit;
using Octokit.Helpers;
using Octokit.Internal;
using Xunit;
using Xunit.Extensions.Ordering;

namespace Vs.Cmd.GitHubFileStorage
{
    [TestCaseOrderer("Xunit.Extensions.Ordering.TestCaseOrderer", "Xunit.Extensions.Ordering")]
    public class GithubTests
    {
        [Fact, Order(1)]
        public async void Test1()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("cred.json", optional: false)
                .Build();

            var apikey = configuration.GetSection("github")["api-key"];
            var repo = configuration.GetSection("github")["repo"];
            var user = configuration.GetSection("github")["user"];
            var product = configuration.GetSection("github")["product"];


            var client = new Octokit.GitHubClient(new Connection(new ProductHeaderValue(product),
                new InMemoryCredentialStore(new Credentials(apikey))));

            try
            {
                await client.Repository.Delete(user, repo);
            }
            catch
            {

            }

            var result = await client.Repository.Create(new NewRepository(repo));
            try
            {
                var ret = await client.Repository.Content.CreateFile(user, repo, @"test.txt",
                    new CreateFileRequest("test commit", "hello-world", "master"));

                var brancheTask1 = await client.Git.Reference.CreateBranch(user, repo, "acceptation");
                var brancheTask2 = await client.Git.Reference.CreateBranch(user, repo, "test");
                var brancheTask3 = await client.Git.Reference.CreateBranch(user, repo, "production");


                ret = await client.Repository.Content.CreateFile(user, repo, @"test2.txt",
                    new CreateFileRequest("test commit", "hello-world", "master"));

                ret = await client.Repository.Content.UpdateFile(user, repo, @"test2.txt",
                    new UpdateFileRequest("test commit 2", "hello-universe", ret.Content.Sha, "master"));

                // raw content url's are needed to be stored in the directory for discovery.
                var raw = ret.Content.DownloadUrl;


                var mergeTask = await client.Repository.Merging.Create(user, repo, new NewMerge("master", "production"));


                var brancheTask4 = await client.Git.Reference.CreateBranch(user, repo, "release-1");

            }
            catch (Exception ex)
            {
            }
            finally
            {
                await client.Repository.Delete(user, repo);
            }
        }
    }
}
