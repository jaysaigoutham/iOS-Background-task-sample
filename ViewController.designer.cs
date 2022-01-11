// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace OfflineSyncSample
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UIButton btnDownload { get; set; }

		[Outlet]
		UIKit.UIButton btnUpload { get; set; }

		[Outlet]
		UIKit.UITextField txtFldAPIDelay { get; set; }

		[Action ("onDownloadClick:")]
		partial void onDownloadClick (Foundation.NSObject sender);

		[Action ("onUploadClick:")]
		partial void onUploadClick (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnDownload != null) {
				btnDownload.Dispose ();
				btnDownload = null;
			}

			if (btnUpload != null) {
				btnUpload.Dispose ();
				btnUpload = null;
			}

			if (txtFldAPIDelay != null) {
				txtFldAPIDelay.Dispose ();
				txtFldAPIDelay = null;
			}
		}
	}
}
