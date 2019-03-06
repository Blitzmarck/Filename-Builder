// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace FilenameBuilder
{
	[Register ("PrevFilenameController")]
	partial class PrevFilenameController
	{
		[Outlet]
		AppKit.NSTextField NotifyCopied { get; set; }

		[Outlet]
		AppKit.NSTableView PrevFNTable { get; set; }

		[Outlet]
		AppKit.NSTableColumn PrevFNTableCol { get; set; }

		[Action ("CopyFilename:")]
		partial void CopyFilename (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (PrevFNTable != null) {
				PrevFNTable.Dispose ();
				PrevFNTable = null;
			}

			if (PrevFNTableCol != null) {
				PrevFNTableCol.Dispose ();
				PrevFNTableCol = null;
			}

			if (NotifyCopied != null) {
				NotifyCopied.Dispose ();
				NotifyCopied = null;
			}
		}
	}
}
