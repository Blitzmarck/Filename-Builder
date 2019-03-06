using System;
using AppKit;
using Foundation;

namespace FilenameBuilder
{

    public partial class PreviousFilenameController : NSViewController
    {
        private NSTableView _table;

        public string previousFilenameString;

        public ViewController Delegate { get; set; }



        public PreviousFilenameController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this._table = new NSTableView();
            FilenameTableDataSource fds = new FilenameTableDataSource();
            _table.DataSource = fds;
        }


    }
}
