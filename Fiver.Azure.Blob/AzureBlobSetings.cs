using System;

namespace Fiver.Azure.Blob
{
    public class AzureBlobSetings
    {
        public AzureBlobSetings(string storageAccount, 
                                       string storageKey,
                                       string containerName)
        {
            if (string.IsNullOrEmpty(storageAccount))
                throw new ArgumentNullException("StorageAccount");

            if (string.IsNullOrEmpty(storageKey))
                throw new ArgumentNullException("StorageKey");

            if (string.IsNullOrEmpty(containerName))
                throw new ArgumentNullException("ContainerName");

            this.StorageAccount = storageAccount;
            this.StorageKey = storageKey;
            this.ContainerName = containerName;
        }

        public AzureBlobSetings(string storageAccount, 
            string storageKey,
            string containerName,
            bool useHttps): this(storageAccount, storageKey, containerName)
        {
            this.UseHttps = useHttps;
        }

        public AzureBlobSetings(string storageAccount,
            string storageKey,
            string containerName,
            bool useHttps,
            bool assumeContainerExists) : this(storageAccount, storageKey, containerName, useHttps)
        {
            this.AssumeContainerExists = assumeContainerExists;
        }

        public bool UseHttps { get; }

        public bool AssumeContainerExists { get; }

        public string StorageAccount { get; }
        public string StorageKey { get; }
        public string ContainerName { get; }
    }
}
