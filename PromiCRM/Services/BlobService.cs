using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Services
{
    public class BlobService : IBlobService
    {
        //blobService client injecting
        private readonly BlobServiceClient _blobClient;
        public BlobService(BlobServiceClient blobClient)
        {
            _blobClient = blobClient;
        }

        public async Task<IEnumerable<string>> AllBlobs(string containerName)
        {
            // access container which holds files
            var containerClient = _blobClient.GetBlobContainerClient(containerName);
            var files = new List<string>();
            //get all blobs
            var blobs = containerClient.GetBlobsAsync();
            // stream these files and save them in our list
            await foreach(var item in blobs)
            {
                // to get name of it we need to wait until its available
                files.Add(item.Name);
            }
            return files;
        
        
        }

        public async Task<bool> DeleteBlob(string name, string containerName)
        {
            //get access to container which holds all files
            var containerClient = _blobClient.GetBlobContainerClient(containerName);
            //getting that blob(file)
            var blobClient = containerClient.GetBlobClient(name);
            // if file exist delete it. it'll return true on false based on result
            return await blobClient.DeleteIfExistsAsync();


        }

        public async Task<string> GetBlob(string name, string containerName)
        {
            //get access to blob container which holds all files
            var containerClient = _blobClient.GetBlobContainerClient(containerName);
            // access to file inside container with specified name
            var blobClient = containerClient.GetBlobClient(name);
            //get url of that file and display it. we just ged url so we can directly display them
            return blobClient.Uri.AbsoluteUri;
        }

        public async Task<bool> UploadBlob(string name, IFormFile file, string containerName)
        {
            //get access to blob container that holds files
            var containerClient = _blobClient.GetBlobContainerClient(containerName);
            // checking if file exist. if the file exist it'll be replaced.
            // if it doesnt exist it will create temp space until its uploaded
            var blobClient = containerClient.GetBlobClient(name);
            //
            var httpHeaders = new BlobHttpHeaders()
            {
                ContentType = file.ContentType
            };
            //upload file. here we basically taking file thats of type IFormFile and converting
            // it to open stream. and this stream can be uploaded to storage
            var res = await blobClient.UploadAsync(file.OpenReadStream(), httpHeaders);

            //check if uploaded succesfully. if not return false
            if(res != null)
                return true;

            return false;

        }
    }
}
