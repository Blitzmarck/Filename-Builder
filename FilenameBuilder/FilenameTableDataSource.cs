using System;
using System.Collections.Generic;
using System.Linq;
using AppKit;


namespace FilenameBuilder
{
    public class FilenameTableDataSource : NSTableViewDataSource
    {
        public List<Filename> Filenames = new List<Filename>();

        public int FileID;

        public override nint GetRowCount(NSTableView tableView)
        {
            return Filenames.Count;
        }

        public void Sort()
        {
            Filenames = Filenames.OrderByDescending(f => f.FileID).ToList();
        }
    }
}
