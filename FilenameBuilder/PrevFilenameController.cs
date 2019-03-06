using System;
using AppKit;
using Foundation;
using Plugin.Clipboard;

namespace FilenameBuilder
{
    public partial class PrevFilenameController : NSViewController
    {
        public FilenameTableDataSource Datasource = new FilenameTableDataSource();

        public ViewController Delegate { get; set; }

        public PrevFilenameController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            PrevFNTable.DataSource = Datasource;
            PrevFNTable.Delegate = new FilenameTableDelegate(Datasource);
        }

        partial void CopyFilename(NSObject sender)
        {
            if (PrevFNTable.RowCount < 1)
            {
                NotifyCopied.StringValue = "No filenames generated yet";
            }

            else if ((int)PrevFNTable.SelectedRow != -1)
            {
                string fn = Datasource.Filenames[(int)PrevFNTable.SelectedRow].StoredFilename;
                CrossClipboard.Current.SetText(fn);
                NotifyCopied.StringValue = "Row " + ((int)PrevFNTable.SelectedRow + 1) + " (Job " + fn.Substring(0, 6) + ") Copied!";
            }
            else
            {
                NotifyCopied.StringValue = "No rows have been selected";
            }
        }
    }
}
