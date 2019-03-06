namespace FilenameBuilder
{
    public class Filename
    {

        public int FileID { get; set; } = 0;

        public string StoredFilename { get; set; } = "";

        public Filename()
        {
        }

        public Filename(int inputID, string inputFilename)
        {
            FileID = inputID;
            StoredFilename = inputFilename;
        }

    }
}
