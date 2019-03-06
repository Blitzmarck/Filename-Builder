using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AppKit;
using Foundation;

namespace FilenameBuilder
{
    public partial class ViewController : NSViewController
    {

        private FilenameTableDataSource FNDataSource;

        public ViewController(IntPtr handle) : base(handle) { }

        #region Override Methods

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            FNDataSource = new FilenameTableDataSource();
            CountRejectBtn.Hidden = true;
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

                default:
                    break;
            }
        }

        #endregion Override Methods

        #region Partial Methods

        partial void BuildBtn(NSObject sender)
        {
            // Get inputs
            List<string> fStringArr = new List<string>(new string[] {
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
                                        revVerTxtBox.StringValue});

            // Clear output fields
            errorOutputBox.StringValue = resultTxtBox.StringValue = "";


            // Error Check
            StringBuilder sb = new StringBuilder();
            sb.Append(ErrorCheck.CheckNumerical(fStringArr[0], 0, false)); /*ErrorCheck.CheckNumerical(jobNumTxtBox.StringValue, 0, false)*/

            // Special Counter/Total error Check
            if (rejectCheck.State == NSCellStateValue.On)
            {
                sb.Append(ErrorCheck.CheckNumerical(fStringArr[1], 1, true) +
                          ErrorCheck.CheckNumerical(fStringArr[2], 2, true));
            }
            else
            {
                sb.Append(ErrorCheck.CheckPages(fStringArr[1], fStringArr[2]));
            }

            sb.Append(
                      ErrorCheck.CheckEmpty(fStringArr[3], 3) +
                      ErrorCheck.CheckEmpty(fStringArr[4], 4) +
                      ErrorCheck.CheckEmpty(fStringArr[5], 5) +
                      ErrorCheck.CheckFloat(fStringArr[6], 6) +
                      ErrorCheck.CheckFloat(fStringArr[7], 7) +
                      ErrorCheck.CheckEmpty(fStringArr[8], 8) +
                      ErrorCheck.CheckNumerical(fStringArr[9], 9, true) +
                      ErrorCheck.CheckNumerical(fStringArr[10], 10, true));

            if (sb.Length == 0)
            {
                //Create filename string
                string result = string.Format("{0}_p{1}-{2}_{3}_{4}_{5}_{6}x{7}_{8}_Q{9}_R{10}",
                                                        fStringArr[0]/*jobNumTxtBox.StringValue*/,
                                                        fStringArr[1],
                                                        fStringArr[2],
                                                        fStringArr[3],
                                                        fStringArr[4],
                                                        fStringArr[5],
                                                        RemoveFloatComma(fStringArr[6]),
                                                        RemoveFloatComma(fStringArr[7]),
                                                        fStringArr[8],
                                                        RemoveIntComma(fStringArr[9]),
                                                        fStringArr[10]
                                                        );

                //Check for Invalid character entries
                sb.Append(ErrorCheck.CheckUnderscore(result) +
                          ErrorCheck.CheckSemicolon(result));

                if (sb.Length == 0)
                {
                    // Display created string and add to Previous Filename table
                    resultTxtBox.StringValue = result;
                    FNDataSource.Filenames.Add(new Filename(FNDataSource.FileID, result));
                    FNDataSource.FileID++;

                    // Notify success and highlight created string
                    notificationLabel.StringValue = "Success!";
                    resultTxtBox.SelectText(sender);

                    // Increment Counter
                    int count = int.Parse(fStringArr[1]), total = int.Parse(fStringArr[2]);
                    if(count < total)
                    {
                        count++;
                        counterTxtBox.StringValue = count.ToString();
                    }


                    if(fStringArr[0].Equals("000000"))
                    {
                        jobNumTxtBox.StringValue = "000000";
                    }
                }
                else
                {
                    DisplayErrors(sb);
                }
            }
            else
            {
                DisplayErrors(sb);
            }
        }

        partial void GetPreviousFilenames(NSObject sender)
        {
            PerformSegue("ShowPreviousFilenames", this);
        }

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

        partial void ClearButton(NSObject sender)
        {

            jobNumTxtBox.StringValue = ""; 
            counterTxtBox.StringValue = "";
            totalPDFsTxtBox.StringValue = "";
            custCodeTxtBox.StringValue = "";
            campNameTxtBox.StringValue = "";
            origFNTxtBox.StringValue = "";
            widthTxtBox.StringValue = "";
            heightTxtBox.StringValue = "";
            stockTxtBox.StringValue = "";
            quantTxtBox.StringValue = "";
            revVerTxtBox.StringValue = "";
        }

        partial void RemoveInvalidChar(NSObject sender)
        {
            List<string> fieldsToCheck = new List<string>(new string[] { 
                                         custCodeTxtBox.StringValue, 
                                         campNameTxtBox.StringValue,
                                         origFNTxtBox.StringValue,
                                         stockTxtBox.StringValue});

            for(int i  = 0;i < fieldsToCheck.Count;i++) 
            {
                fieldsToCheck[i] = fieldsToCheck[i].Replace("_", " ").Replace(":", " ");
            }

            custCodeTxtBox.StringValue = fieldsToCheck[0];
            campNameTxtBox.StringValue = fieldsToCheck[1];
            origFNTxtBox.StringValue = fieldsToCheck[2];
            stockTxtBox.StringValue = fieldsToCheck[3];
        }

        partial void RejectCheckbox(NSObject sender)
        {
            if (rejectCheck.State == NSCellStateValue.On)
            {
                CountRejectBtn.Hidden = false;
                totalPDFsTxtBox.Enabled = false;
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
            dlg.AllowedFileTypes = new string[] { "pdf", "ai" };

            if (dlg.RunModal() == 1)
            {
                int count = 0;
                foreach (var item in dlg.Urls){ count++; }
                totalPDFsTxtBox.StringValue = count.ToString();
            }
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

        private int RemoveIntComma(string inputString)
        {
            return int.Parse(inputString.Replace(",", string.Empty));
        }

        private float RemoveFloatComma(string inputString)
        {
            return float.Parse(inputString.Replace(",", string.Empty));
        }

        private void DisplayErrors(StringBuilder sb)
        {
            // Display error messages
            errorOutputBox.StringValue = sb.ToString();
            errorOutputBox.SizeToFit();
            notificationLabel.StringValue = "Errors Detected:";
        }
        #endregion Private Methods
    }
}