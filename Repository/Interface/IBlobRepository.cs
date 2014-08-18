using Manufacturing.Framework.Repository.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manufacturing.Framework.Repository.Interface
{
    public interface IBlobRepository
    {
        Stream GetBlob(string containerName, string blobPath);
        BlobInfo GetBlobInfo(string containerName, string blobPath);
        BlobInfo CreateBlob(string containerName, string blobPath, Stream data);
        void DeleteBlob(string containerName, string blobPath);
        void DownloadBlobToStream(string containerName, string blobPath, Stream downloadStream);
        List<BlobInfo> GetBlobsInDirectory(string containerName, string directoryPath);
        void MoveBlob(string sourceContainerName, string sourcePath, string destContainerName, string destPath);
        void ExpireBlobs(string containerName, string virtualDirectoryRoot, int expirationDays);
    }
}
