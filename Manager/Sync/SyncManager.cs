using System;
using System.IO;
using System.Threading.Tasks;
using Foundation;
using static OfflineSyncSample.Utiliies.Enums;

namespace OfflineSyncSample.Manager.Sync
{
    public class SyncManager
    {
        private const string APIURL_DOWNLOAD = "http://192.168.8.101/offlinSyncPOC/api/sync/downloadData?Duration=5";
        private const string APIURL_UPLOAD = "http://192.168.8.101/offlinSyncPOC/api/sync/uploadData?Duration=5";
        private string uploadFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "sample_file.png");
        private const string BackgroundSessionId = "com.xamarin.ios.offline.transfersession";
        private NSUrlSession syncSession = null;


        public NSUrlSession InitiateSync()
        {
            //Check more settings here
            //https://developer.apple.com/library/ios/documentation/Foundation/Reference/NSURLSessionConfiguration_class/index.html

            using (var config = NSUrlSessionConfiguration.CreateBackgroundSessionConfiguration(BackgroundSessionId))
            {
                config.HttpMaximumConnectionsPerHost = 6;
                config.NetworkServiceType = NSUrlRequestNetworkServiceType.Background; //Optional 
                config.AllowsCellularAccess = true; //determines whether connections should be made over a cellular network.
                config.WaitsForConnectivity = true; //indicates whether the session should wait for connectivity to become available, or fail immediately.
                config.AllowsConstrainedNetworkAccess = true; // indicates whether connections may use the network when the user has specified Low Data Mode.
                config.AllowsExpensiveNetworkAccess = true; // indicates whether connections may use a network interface that the system considers expensive.
                config.ShouldUseExtendedBackgroundIdleMode = true; //indicates whether TCP connections should be kept open when the app moves to the background.
                config.TimeoutIntervalForRequest = 600.0;
                config.TimeoutIntervalForResource = 120.0;
            
                return NSUrlSession.FromWeakConfiguration(config, new SyncManagerDelegate(this), new NSOperationQueue());
            }
        }

        public void UpdateSyncStatus(int taskIdentifier, SyncStatus syncStatus)
        {
            //TODO: Save status in DB or show as progress
            Console.WriteLine(taskIdentifier  +" - TaskId has been " + syncStatus.ToString());
        }

        public async Task<bool> UploadDataSampleAsync()
        {
            if (this.syncSession == null)
            {
                this.syncSession = this.InitiateSync();
            }

            await Task.Delay(1000); //Yes! you can run async calls alos here

            NSUrl handleURL = NSUrl.FromString(APIURL_UPLOAD);
            NSMutableUrlRequest request = new NSMutableUrlRequest(handleURL);
            request.HttpMethod = "POST";
            request.Body = NSData.FromString("{\"version\":\"v1\", \"cityid\": \"101010100\"}");

            NSData uploadDataBody = null;
            var uploadTask = this.syncSession.CreateDataTask(request);
            uploadTask.Resume();

            return true;
        }

        public async Task<bool> DownloadDataSample()
        {
            if (this.syncSession == null)
            {
                this.syncSession = this.InitiateSync();
            }

            await Task.Delay(1000); //Yes! you can run async calls alos here

            NSUrl handleURL = NSUrl.FromString(APIURL_DOWNLOAD);
            NSUrlRequest request = NSUrlRequest.FromUrl(handleURL);
            var downloadTask = syncSession.CreateDownloadTask(request); //CreateDownloadTask
            // Start the download now
            downloadTask.Resume();

            return true;
        }
    }
}
