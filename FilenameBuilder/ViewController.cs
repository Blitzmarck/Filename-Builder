using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using AppKit;
using Foundation;

namespace FilenameBuilder
{
    public partial class ViewController : NSViewController
    {

        private FilenameTableDataSource FNDataSource;

        private List<string> fnStringArr = null;

        private List<NSTextField> allTextFields;

        private enum AllowedFiletypes
        {
            xml,
            pdf,
            ai
        }

        public ViewController(IntPtr handle) : base(handle) { }

        #region Override Methods

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            FNDataSource = new FilenameTableDataSource();
            CountRejectBtn.Hidden = true;
            allTextFields = new List<NSTextField>
            {
                jobNumTxtBox,
                counterTxtBox,
                totalPDFsTxtBox,
                custCodeTxtBox,
                campNameTxtBox,
                origFNTxtBox,
                widthTxtBox,
                heightTxtBox,
                stockTxtBox,
                quantTxtBox,
                revVerTxtBox,
                finishingTxtBox,
                colourTxtBox,
                commentTxtBox,
                filePathTxtBox,
                errorOutputBox,
                resultTxtBox
            };
        }

        public override void ViewWillAppear()
        {
            // Override Auto tab order and set manual
            View.Window.InitialFirstResponder = allTextFields[0];
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
            }
        }

        public override void PrepareForSegue(NSStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);

            switch (segue.Identifier)
            {
                case "ShowPreviousFilenames":
                    var prevFilename = segue.DestinationController as PrevFilenameController;
                    if (prevFilename != null)
                    {
                        FNDataSource.Sort();
                        prevFilename.Datasource = FNDataSource;
                    }
                    break;
            }
        }

        #endregion Override Methods

        #region Partial Methods

        partial void CounterStepValueChanged(NSObject sender)
        {
            string counterVal = counterTxtBox.StringValue;

            //Check if counter > total pages 
            string checkValid = ErrorCheck.CheckNumerical(counterVal, 1, true) +
                                ErrorCheck.CheckNumerical(totalPDFsTxtBox.StringValue, 2, true);

            if (checkValid.Length == 0)
            {
                StepValue(CounterStep, counterTxtBox, true, int.Parse(totalPDFsTxtBox.StringValue));
            }
        }

        partial void TotalPagesValueChanged(NSObject sender)
        {
            string totalPagesVal = totalPDFsTxtBox.StringValue;
            string checkValid = ErrorCheck.CheckNumerical(totalPagesVal, 2, true);
            if (checkValid.Length == 0)
            {
                StepValue(totalPDFsStep, totalPDFsTxtBox, false, 0);
            }
        }

        partial void RejectCheckbox(NSObject sender)
        {
            if (rejectCheck.State == NSCellStateValue.On)
            {
                CountRejectBtn.Hidden = false;
                totalPDFsTxtBox.Enabled = false;
                totalPDFsTxtBox.StringValue = "";
                totalPDFsStep.Enabled = false;
            }
            else
            {
                CountRejectBtn.Hidden = true;
                totalPDFsTxtBox.Enabled = true;
                totalPDFsStep.Enabled = true;
            }
        }

        partial void RejectCount(NSObject sender)
        {
            var dlg = NSOpenPanel.OpenPanel;
            dlg.CanChooseFiles = true;
            dlg.CanChooseDirectories = false;
            dlg.AllowsMultipleSelection = true;
            dlg.AllowedFileTypes = new string[] 
            {
                AllowedFiletypes.pdf.ToString(),
                AllowedFiletypes.ai.ToString()
            };

            if (dlg.RunModal() == 1)
            {
                int count = 0;
                foreach (var item in dlg.Urls)
                {
                    count++;
                }
                totalPDFsTxtBox.StringValue = count.ToString();
            }
        }

        partial void RemoveInvalidChar(NSObject sender)
        {
            List<NSTextField> fieldsToCheck = new List<NSTextField>{
                                         custCodeTxtBox,
                                         campNameTxtBox,
                                         origFNTxtBox,
                                         stockTxtBox,
                                         finishingTxtBox,
                                         colourTxtBox,
                                         commentTxtBox};

            for (int i = 0; i < fieldsToCheck.Count; i++)
            {
                fieldsToCheck[i].StringValue = fieldsToCheck[i].StringValue.Replace("_", " ").Replace(":", " ");
            }
        }

        partial void ImportXML(NSObject sender)
        {
            // User selects XML File
            using(var dlg = NSOpenPanel.OpenPanel)
            {
                dlg.CanChooseFiles = true;
                dlg.CanChooseDirectories = false;
                dlg.AllowsMultipleSelection = false;
                dlg.AllowedFileTypes = new string[] 
                { 
                    AllowedFiletypes.xml.ToString() 
                };

                if (dlg.RunModal() == 1)
                {
                    // Check filename string array exists
                    if (fnStringArr != null)
                    {
                        fnStringArr.Clear();
                    }
                    else
                    {
                        fnStringArr = new List<string>();
                    }

                    // Import XML
                    using (NSAlert alart = new NSAlert())
                    {
                        alart.MessageText = "Import XML File";
                        try
                        {
                            List<string> importedXMLData = XMLClass.ImportXML(dlg.Url.Path);
                            fnStringArr.AddRange(importedXMLData);

                            // Remove JobName (Unneeded)
                            fnStringArr.RemoveAt(4);

                            for (int i = 0; i < fnStringArr.Count; i++)
                            {
                                //Reject file and Total PDFs
                                if (rejectCheck.State == NSCellStateValue.On && i == 2)
                                {
                                    continue;
                                }
                                if (rejectCheck.State == NSCellStateValue.On && i == 10)
                                {
                                    fnStringArr[i] = (int.Parse(fnStringArr[i]) + 1).ToString();
                                }

                                allTextFields[i].StringValue = fnStringArr[i];
                            }
                            alart.InformativeText = "Successful import!";
                        }
                        catch (Exception)
                        {
                            alart.InformativeText = "The import failed, please ensure your XML document is correct";
                        }
                        alart.RunModal();
                    }
                }
            }

        }

        partial void ClearButton(NSObject sender)
        {
            foreach (var item in allTextFields)
            {
                item.StringValue = "";
            }
        }

        partial void GetFilePath(NSObject sender)
        {
            using (var dlg = NSOpenPanel.OpenPanel) 
            {
                dlg.CanChooseFiles = false;
                dlg.CanChooseDirectories = true;
                dlg.AllowsMultipleSelection = false;

                if (dlg.RunModal() == 1)
                {
                    filePathTxtBox.StringValue = dlg.DirectoryUrl.Path;
                }
            };
        }

        partial void BuildBtn(NSObject sender)
        {
            // Clear output fields
            errorOutputBox.StringValue = resultTxtBox.StringValue = "";

            // Get data
            fnStringArr = new List<string>(new string[] {
                                        jobNumTxtBox.StringValue,
                                        counterTxtBox.StringValue,
                                        totalPDFsTxtBox.StringValue,
                                        custCodeTxtBox.StringValue,
                                        campNameTxtBox.StringValue,
                                        origFNTxtBox.StringValue,
                                        widthTxtBox.StringValue,
                                        heightTxtBox.StringValue,
                                        stockTxtBox.StringValue,
                                        quantTxtBox.StringValue,
                                        revVerTxtBox.StringValue,
                                        finishingTxtBox.StringValue,
                                        colourTxtBox.StringValue,
                                        commentTxtBox.StringValue});

            string filePath = filePathTxtBox.StringValue;

            /**Error Check**/
            StringBuilder sb = new StringBuilder();

            // Counter/Total PDFs have special check when reject
            if (rejectCheck.State == NSCellStateValue.On)
            {
                sb.Append(ErrorCheck.CheckNumerical(fnStringArr[1], 1, true) +
                          ErrorCheck.CheckNumerical(fnStringArr[2], 2, true));
            }
            else
            {
                sb.Append(ErrorCheck.CheckPages(fnStringArr[1], fnStringArr[2]));
            }

            sb.Append(ErrorCheck.CheckNumerical(fnStringArr[0], 0, false) +
                      ErrorCheck.CheckEmpty(fnStringArr[3], 3) +
                      ErrorCheck.CheckEmpty(fnStringArr[4], 4) +
                      ErrorCheck.CheckEmpty(fnStringArr[5], 5) +
                      ErrorCheck.CheckFloat(fnStringArr[6], 6) +
                      ErrorCheck.CheckFloat(fnStringArr[7], 7) +
                      ErrorCheck.CheckEmpty(fnStringArr[8], 8) +
                      ErrorCheck.CheckNumerical(fnStringArr[9], 9, true) +
                      ErrorCheck.CheckNumerical(fnStringArr[10], 10, true) +
                      ErrorCheck.CheckEmpty(fnStringArr[11], 11) +
                      ErrorCheck.CheckEmpty(fnStringArr[12], 12) +
                      ErrorCheck.CheckEmpty(fnStringArr[13], 13) +
                      ErrorCheck.CheckEmpty(filePath, 14));

            // Invalid Char Check
            foreach (var item in fnStringArr)
            {
                sb.Append(ErrorCheck.CheckInvalidChar(item));
            }

            // Errors found are displayed and build stopped
            if (sb.Length != 0)
            {
                DisplayErrors(sb);
                return;
            }

            /**Renaming/Moving**/
            string folderNameString = string.Format("{0}_{1}", fnStringArr[0], fnStringArr[4]);
            string fileNameString = string.Format("{0}_{1}", fnStringArr[0], fnStringArr[5]);
            string newFilePath;
            //If Reject
            if (rejectCheck.State == NSCellStateValue.On)
            {
                newFilePath = filePath + "/Reject";
            }
            else
            {
                newFilePath = filePath + "/" + folderNameString;
            }

            // Folder Creation
            try
            {
                if (!Directory.Exists(newFilePath))
                {
                    Directory.CreateDirectory(newFilePath);
                }
            }
            catch (Exception)
            {
                sb.Append("- Unable to create folder\n" + newFilePath);
                DisplayErrors(sb);
                return;
            }

            // Rename/Move
            sb.Append(MoveAndRenameFile(filePath, newFilePath, fileNameString));

            if(sb.Length != 0)
            {
                DisplayErrors(sb);
                // return;
            }

            /**XML Creation**/
            bool saveOK = XMLClass.SaveXMLToFile(XMLClass.CreateXML(fnStringArr), newFilePath + "/" + fileNameString + ".xml");

            if (!saveOK)
            {
                sb.Append("- File Path is not valid\n");
                DisplayErrors(sb);
                return;
            }

            /**Add to Previous Filename table**/
            FNDataSource.Filenames.Add(new Filename(FNDataSource.FileID, fileNameString));
            FNDataSource.FileID++;

            /**Display and Notify user**/
            resultTxtBox.StringValue = fileNameString;
            notificationLabel.StringValue = "Success!";
            resultTxtBox.SelectText(sender);

            // /**Page Counter increment**/
            int count = int.Parse(fnStringArr[1]), total = int.Parse(fnStringArr[2]);
            if (count < total)
            {
                count++;
                counterTxtBox.StringValue = count.ToString();
            }

            if (fnStringArr[0].Equals("000000"))
            {
                jobNumTxtBox.StringValue = "000000";
            }
        }

        partial void GetPreviousFilenames(NSObject sender)
        {
            PerformSegue("ShowPreviousFilenames", this);
        }

        #endregion Partial Methods

        #region Private Methods

        private void StepValue(NSStepper stepper, NSTextField fieldToModify, bool isLimited, int limit)
        {
            int fieldIntVal = int.Parse(fieldToModify.StringValue);
            switch (stepper.IntValue)
            {
                case 2:
                    if (isLimited)
                    {
                        if (fieldIntVal < limit)
                        {
                            fieldToModify.StringValue = (fieldIntVal + 1).ToString();
                        }
                    }
                    else
                    {
                        fieldToModify.StringValue = (fieldIntVal + 1).ToString();
                    }
                    break;

                case 1:
                    if (int.Parse(fieldToModify.StringValue) >= 2)
                    {
                        fieldToModify.StringValue = (fieldIntVal - 1).ToString();
                    }
                    break;
            }
            stepper.IntValue = 0;
        }

        private void DisplayErrors(StringBuilder sb)
        {
            // Display error messages
            notificationLabel.StringValue = "Errors Detected:";
            errorOutputBox.StringValue = sb.ToString();
            errorOutputBox.SizeToFit();
            sb.Clear();
        }

        private StringBuilder MoveAndRenameFile(string oldFilePath, string newFilePath, string fileName)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                bool fileFound = false;
                foreach (string currFile in Directory.EnumerateFiles(oldFilePath))
                {
                    // Get filename
                    string currFileName = Path.GetFileNameWithoutExtension(currFile).Replace("_", " ").Replace(":", " ");

                    // File Comparison
                    bool fileCompare = (currFileName.Equals(fnStringArr[5]) || currFileName.Equals(fileName.Replace("_", " ")));

                    // Found file
                    if (fileCompare)
                    {
                        // Get Extension
                        string fileExt = Path.GetExtension(currFile);

                        // If old XML, delete
                        if (fileExt.Equals(".xml"))
                        {
                            File.Delete(currFile);
                            continue;
                        }

                        // If File, Rename/Move
                        if (fileExt.Equals(".pdf") || fileExt.Equals(".ai"))
                        {
                            if (currFileName.Equals(fnStringArr[5]) || currFileName.Equals(fileName.Replace("_", " ")))
                            {
                                // Move & Rename
                                string currLoc = oldFilePath + "/" + Path.GetFileName(currFile);
                                string newLoc = newFilePath + "/" + fileName + fileExt;
                                File.Move(currLoc, newLoc);
                                fileFound = true;
                            }
                        }
                    }
                }
                if (!fileFound)
                {
                    sb.Append("- 404 File not Found. Must manually move file");
                }

            }
            catch (Exception)
            {
                sb.Append("- Unable to Move file");
            }

            return sb;
        }

        #endregion Private Methods
    }
}