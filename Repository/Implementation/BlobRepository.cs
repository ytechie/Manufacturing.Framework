using Manufacturing.Framework.Configuration;
using Manufacturing.Framework.Repository.Entities;
using Manufacturing.Framework.Repository.Interface;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manufacturing.Framework.Repository.Implementation
{
    public class BlobRepository : IBlobRepository
    {
        private readonly CloudStorageAccount _storageAccount;
        private readonly CloudBlobClient _blobClient;

        public BlobRepository(StorageConfiguration configuration)
        {
            // Initialize the Azure Blob Storage handlers
            _storageAccount = CloudStorageAccount.Parse(configuration.GetConnectionString());
            _blobClient = _storageAccount.CreateCloudBlobClient();
        }

        /// <summary>
        /// Creates a new blob at the specified location.  If the blob already exists, the content will be overwritten.
        /// </summary>
        public BlobInfo CreateBlob(string containerName, string blobPath, Stream data)
        {
            BlobInfo blobInfo;

            try
            {
                // Retreive the container
                CloudBlobContainer container = _blobClient.GetContainerReference(containerName);

                // Create the container if it doesn't already exist.
                container.CreateIfNotExists();

                // Get a reference to the blob, so we can either create it or overwrite it
                CloudBlockBlob fileBlob = container.GetBlockBlobReference(blobPath);

                // Upload the bytes
                fileBlob.UploadFromStream(data);

                blobInfo = BlobInfo.FromBlob(fileBlob);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.ToString());
                throw;
            }

            return blobInfo;
        }

        /// <summary>
        /// Retreives the data of the specified blob as a memory stream
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="blobPath"></param>
        /// <returns></returns>
        public Stream GetBlob(string containerName, string blobPath)
        {
            MemoryStream blobStream = null;

            try
            {
                // Retreive the container
                CloudBlobContainer container = _blobClient.GetContainerReference(containerName);

                // If the container exists, then grab it
                if (container.Exists())
                {


                    CloudBlockBlob fileBlob = container.GetBlockBlobReference(blobPath);

                    // If the blob exists, then download it to a memory stream
                    if (fileBlob.Exists())
                    {
                        blobStream = new MemoryStream();
                        fileBlob.DownloadToStream(blobStream);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.ToString());
                throw;
            }

            return blobStream;
        }

        /// <summary>
        /// Retreives information about a blob, but not the actual data
        /// </summary>
        public BlobInfo GetBlobInfo(string containerName, string blobPath)
        {
            BlobInfo info = null;

            try
            {
                // Retreive the container
                CloudBlobContainer container = _blobClient.GetContainerReference(containerName);

                // If the container exists, then grab it
                if (container.Exists())
                {
                    CloudBlockBlob fileBlob = container.GetBlockBlobReference(blobPath);

                    // If the blob exists, then download it to a memory stream
                    if (fileBlob.Exists())
                    {
                        fileBlob.FetchAttributes();
                        info = BlobInfo.FromBlob(fileBlob);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.ToString());
                throw;
            }

            return info;
        }

        /// <summary>
        /// Initiates a download of a blob file to the supplied stream
        /// </summary>
        public void DownloadBlobToStream(string containerName, string blobPath, Stream downloadStream)
        {
            try
            {
                // Retreive the container
                CloudBlobContainer container = _blobClient.GetContainerReference(containerName);

                // If the container exists, then grab it
                if (container.Exists())
                {
                    CloudBlockBlob fileBlob = container.GetBlockBlobReference(blobPath);

                    // If the blob exists, then download it to a memory stream
                    if (fileBlob.Exists())
                    {
                        fileBlob.DownloadToStream(downloadStream);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.ToString());
                throw;
            }
        }

        /// <summary>
        /// Returns the name/path of all blobs in a container, under the specified virtual directory
        /// </summary>
        public List<BlobInfo> GetBlobsInDirectory(string containerName, string directoryPath)
        {
            List<BlobInfo> blobList = new List<BlobInfo>();

            try
            {
                // Retreive the container
                CloudBlobContainer container = _blobClient.GetContainerReference(containerName);

                // If the container exists, then grab it
                if (container.Exists())
                {
                    CloudBlobDirectory directory = container.GetDirectoryReference(directoryPath);
                    blobList = directory
                        .ListBlobs()
                        .OfType<CloudBlockBlob>()
                        .Select(BlobInfo.FromBlob)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.ToString());
                throw;
            }

            return blobList;
        }

        /// <summary>
        /// Deletes a blob from the specified container
        /// </summary>
        public void DeleteBlob(string containerName, string blobPath)
        {
            try
            {
                // Retreive the container
                CloudBlobContainer container = _blobClient.GetContainerReference(containerName);

                if (container.Exists())
                {
                    // Get a reference to the blob, so we can either create it or overwrite it
                    CloudBlockBlob fileBlob = container.GetBlockBlobReference(blobPath);
                    fileBlob.DeleteIfExists();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.ToString());
                throw;
            }
        }

        /// <summary>
        /// Copies a blob to a new location within a container, and deletes the original copy.  This process
        /// is recursive under the specified directory.
        /// </summary>
        public void MoveBlob(string sourceContainerName, string sourcePath, string destContainerName, string destPath)
        {
            try
            {
                // Retreive the container
                CloudBlobContainer sourceContainer = _blobClient.GetContainerReference(sourceContainerName);
                CloudBlobContainer destContainer = _blobClient.GetContainerReference(destContainerName);

                // Create the container if it doesn't already exist.
                destContainer.CreateIfNotExists();

                // Get a reference to the blob, so we can either create it or overwrite it
                CloudBlockBlob sourceBlob = sourceContainer.GetBlockBlobReference(sourcePath);
                CloudBlockBlob destinationBlob = destContainer.GetBlockBlobReference(destPath);

                // Copy to the new location
                destinationBlob.StartCopyFromBlob(sourceBlob);

                // Delete the old one
                sourceBlob.DeleteIfExists();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.ToString());
                throw;
            }
        }

        /// <summary>
        /// Delete's all blobs within the container, under the specified virtual directory, that are older than the number of days specified.
        /// </summary>
        public void ExpireBlobs(string containerName, string virtualDirectoryRoot, int expirationDays)
        {
            try
            {
                // Retreive the container
                CloudBlobContainer container = _blobClient.GetContainerReference(containerName);

                // If the container exists, then grab it
                if (container.Exists())
                {
                    CloudBlobDirectory directory = container.GetDirectoryReference(virtualDirectoryRoot);

                    // Get all blobs under the specified virtual directory that are older than the 
                    // specified number of days.
                    var blobList = directory
                        .ListBlobs(true)
                        .OfType<CloudBlockBlob>()
                        .Where(x => x.Properties.LastModified.HasValue
                                    && x.Properties.LastModified.Value.AddDays(expirationDays) < DateTime.Today)
                        .ToList();

                    // Delete 'em all!
                    blobList.ForEach(x => x.DeleteIfExists());
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.ToString());
                throw;
            }
        }
    }
}
