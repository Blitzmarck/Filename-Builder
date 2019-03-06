using System;
using AppKit;

namespace FilenameBuilder
{
    public class FilenameTableDelegate : NSTableViewDelegate
    {
        private const string CellIdentifier = "FilenameTableCell";

        private FilenameTableDataSource DataSource;

        public FilenameTableDelegate(FilenameTableDataSource datasource)
        {
            DataSource = datasource;
        }

        public override NSView GetViewForItem(NSTableView tableView, NSTableColumn tableColumn, nint row)
        {
            // This pattern allows you reuse existing views when they are no-longer in use.
            // If the returned view is null, you instance up a new view
            // If a non-null view is returned, you modify it enough to reflect the new data
            NSTextField view = (NSTextField)tableView.MakeView(CellIdentifier, this);
            if (view == null)
            {
                view = new NSTextField
                {
                    Identifier = CellIdentifier,
                    BackgroundColor = NSColor.Clear,
                    Bordered = false
                };
            }

            // Setup view based on the column selected
            if (tableColumn.Title.Equals("Filename"))
            {
                view.StringValue = DataSource.Filenames[(int)row].StoredFilename;
            }

            return view;
        }

        public override bool ShouldReorder(NSTableView tableView, nint columnIndex, nint newColumnIndex)
        {
            return true;
        }
    }
}
