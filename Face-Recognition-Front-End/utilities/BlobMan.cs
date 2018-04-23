using System;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System.Linq;



using Xamarin.Forms;
using Plugin.Media.Abstractions;
namespace FaceRecognitionFrontEnd.utilities
{
    public class BlobMan
    {
        CloudBlobContainer _recofaContainer;
        public static BlobMan Instance { get; } = new BlobMan();

        BlobMan()
        {
            _recofaContainer = _blobClient.GetContainerReference("recfa");


        }
        const string connectionString = "DefaultEndpointsProtocol=https;AccountName=alialsaeedi19;AccountKey=gy6yyV+QS3X88zNElVugIxIR7YqwVdDNb1jidgnuFBBdV5hXq+gzw8CBtKF14BgRxhO1BUtK5evheqJ6OTensg==;EndpointSuffix=core.windows.net";
        CloudBlobClient _blobClient = CloudStorageAccount.Parse(connectionString).CreateCloudBlobClient();

        public async Task<List<Uri>> GetAllBlobUris(string localPath)
        {
            var contToken = new BlobContinuationToken();
            var allBlobs = await _recofaContainer.ListBlobsSegmentedAsync(contToken).ConfigureAwait(false);
            var uris = allBlobs.Results.Select(b => b.Uri).ToList();

            return uris;
        }

        public async Task<string> UploadFileAsync(string localPath, Stream image)
        {
            var uniqueBlobName = Guid.NewGuid().ToString();
            uniqueBlobName += Path.GetExtension(localPath);

            var blobRef = _recofaContainer.GetBlockBlobReference(uniqueBlobName);

            await blobRef.UploadFromStreamAsync(image).ConfigureAwait(false);
            return blobRef.Uri.AbsoluteUri;
                   
        }
       
    }
}
