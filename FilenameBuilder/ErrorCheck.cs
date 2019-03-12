﻿namespace FilenameBuilder
{
    public static class ErrorCheck
    {

        private enum NameOfField
        {
            Job_Number,
            Counter,
            Total_Pages,
            Customer_Code,
            Campaign_Name,
            Original_Filename,
            Width,
            Height,
            Stock,
            Quantity,
            Revision_Version,
            Finishing,
            Colour,
            Comment,
            File_Path
        };

        public static string CheckEmpty(string input, int type)
        {
            string typeName = ((NameOfField)type).ToString().Replace('_', ' ');
            return input.Length == 0 ? string.Format("- {0} can't be empty\n", typeName) : "";
        }

        public static string CheckNumerical(string input, int type, bool checkForZero)
        {
            string typeName = ((NameOfField)type).ToString().Replace('_', ' ');
            input = input.Replace(",", string.Empty);

            // Numerical Check & checking for zero
            if ((int.TryParse(input, out int p)) && input.Length > 0)
            {
                if (int.Parse(input) <= 0 && checkForZero)
                {
                    return string.Format("- {0} must be a positive number\n", typeName);
                }

                // Additional Check for Job Num
                if (type == 0 && input.Length > 6)
                {
                    return string.Format("- {0} must be a positive 6 digit number\n", typeName);
                }
                return "";
            }

            return string.Format("- {0} must be a number\n", typeName);
        }

        public static string CheckFloat(string input, int type)
        {
            string typeName = ((NameOfField)type).ToString().Replace('_', ' ');
            input = input.Replace(",", string.Empty);

            // Float Check
            if ((float.TryParse(input, out float p)) && input.Length > 0)
            {
                if (float.Parse(input) <= 0)
                {
                    return string.Format("- {0} must be a positive number\n", typeName);
                }
                return "";
            }
            return string.Format("- {0} must be a number\n", typeName);
        }

        public static string CheckPages(string count, string total)
        {
            // Numerical Check
            string errorString = CheckNumerical(count, 1, true) + CheckNumerical(total, 2, true);

            if (errorString.Length > 0)
            {
                return errorString;
            }

            // Check if counter is greater than total
            if (int.Parse(count) > int.Parse(total))
            {
                return "- Counter must be less than Total Pages\n";
            }
            return "";
        }

        public static string CheckUnderscore(string inputString)
        {
            int stringLength = inputString.Length;
            int result = stringLength - inputString.Replace("_", "").Length;
            if (result == 8)
            {
                return "";
            }
            return "- There cannot be any underscores inputted\n";
        }

        public static string CheckInvalidChar(string inputString)
        {
            if (inputString.Contains(':'))
            {
                return "- There cannot be any colons inputted\n";
            }
            if (inputString.Contains('_'))
            {
                return "- There cannot be any underscores inputted\n";
            }
            return "";
        }

        public static string CheckErrorFill(string inputString)
        {
            if (inputString.Length == 0)
            {
                return "- No input detected\n";
            }
            if (!inputString.Contains('_'))
            {
                return "- Invalid filename inputted\n";
            }

            string[] checkInputs = inputString.Split("_");

            if (checkInputs.Length != 9)
            {
                return "- Invalid amount of underscores used";
            }

            if (!checkInputs[1].Contains("-") || !checkInputs[1].Contains("p") || checkInputs[1].Length != 4)
            {
                return "- Invalid Counter/Total PDFs entry";
            }

            if (!checkInputs[5].Contains("x"))
            {
                return "- Invalid Width/Length entry";
            }

            if (!checkInputs[7].Contains("Q") || checkInputs[7].Length == 1)
            {
                return "- Invalid Quantity entry";
            }

            if (!checkInputs[8].Contains("R") || checkInputs[8].Length == 1)
            {
                return "- Invalid Revision entry";
            }

            if (checkInputs[8].Contains("."))
            {
                return "- Please remove the .pdf extension";
            }
            return "";
        }

    }
}
