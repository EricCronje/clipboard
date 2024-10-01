/*
#################################################################
#            (       )                                          #
#  *   )  (  )\ ) ( /(                                          #
#` )  /(( )\(()/( )\())                                         #
# ( )(_))((_)/(_)|(_)\                                          #
#(_(_()|(_)_(_))   ((_)                                         #
#|_   _|| _ )_ _| / _ \                                         #
#  | |  | _ \| | | (_) |                                        #
#  |_|  |___/___| \___/  Version 4.0.2 (ClipboardUtility.cs)    #
#########################################################################################################################
#1      - Add - Created the field cVersion to be used on th -v option.  -   TB10    -   20241001    -   V2.0.0          #
#2      - Add - private static bool IsVersionOption(string[] args)      -   TB10    -   20241001    -   V3.0.0          #
#3      - Add - append the version to the out put when -v is used.      -   TB10    -   20241001    -   V4.0.0          #
#4      - Bug - Specify the 0th argument not the 1st e.g cb -v          -   TB10    -   20241001    -   V4.0.1          #
#5      - Bug - Specify that there is only one argument not more        -   TB10    -   20241001    -   V4.0.2          #
#########################################################################################################################
*/

using System;
using System.Text;
using System.Windows.Forms;

namespace ClipboardUtility
{
    public class ClipboardUtility : IDisposable
    {
        private StringBuilder _outputSB;

        public ClipboardUtility() { _outputSB = new StringBuilder(); }

        public string Output { get { return _outputSB.ToString(); } }

        public readonly string cHelpMessage = "cb: cb [-h] <string to copy to the clipboard>[-q] [-v]\r\n\tCopies the string send to the system clipboard.\r\n\tAvailable to CTRL + v or shift + Insert to paste the content.\r\n\t\r\n\tWithout arguments, 'cb' shows the help content.\r\n\t\r\n\tOtherwise, 'cb' will copy the passed string to the system clipboard and outputs the string passed in the first argument.\r\n\t\r\n\tOptions:\r\n\t\t-q\t\t:\tWill not output the string passed.\r\n\t\t-h\t\t:\tWill bring up this help content. (Also with no arguments.)\r\n\r\n\tExit Status:\r\n\t'cb' returns true.\r\n\t\r\n\tExamples:\r\n\t\r\n\tUsed in windows bash.\r\n\tcb \"$(ls)\"\r\n\t\r\n\tResult:\r\n\tList the directory content with \\r\\n. Control line feed.\r\n\tThen CTRL+v or shift+insert to paste the content from the clipboard.";

        public readonly string cVersion = "Version 4.0.2"; //#1
        public void Dispose()
        {
            _outputSB?.Clear(); // Null propagation
            _outputSB = null;
        }

        public bool Use(string[] args, string invalidResponseMessage = "Nothing added to clipboard.")
        {
            bool actioned = false;
            if (args != null)
            {
                actioned = Implement(args, invalidResponseMessage);
            }

            return actioned;
        }

        /// <summary>
        /// Clears the cLipboard and the internal return string 
        /// </summary>
        public void Clear() { Clipboard.Clear(); _outputSB.Clear(); }

        /// <summary>
        /// Inner implementation of the clipboard logic - to make it testable.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool Implement(string[] args, string invalidResponseMessage)
        {
            Clipboard.Clear();
            bool isQuiet = IsQuieteOption(args);
            bool isHelpOption = IsHelpOption(args);
            bool isVersionOption = IsVersionOption(args);
            SetOutputMessage(invalidResponseMessage, isQuiet, isHelpOption, isVersionOption);
            
            if (isHelpOption || isVersionOption) { return true; }

            if (IsArgValid(args[0]) && args[0] != "-q" && args[0] != "-v")
            {
                Clipboard.SetText(args[0]); 
                SetOutputMessage(args[0], isQuiet);
                return true;
            }
            return false;
        }

        private static bool IsQuieteOption(string[] args)
        {
            return (args != null && args.Length > 1 && IsArgValid(args[1]) && args[1] == "-q");
        }

        private static bool IsVersionOption(string[] args) //#2
        {
            return (args != null && args.Length == 1 && IsArgValid(args[0]) && args[0] == "-v"); //#4 #5
        }

        private static bool IsHelpOption(string[] args)
        {
            bool noOptions = args != null && args.Length == 0;
            if (noOptions) { return true; }
            bool isHOption = args != null && args.Length > 0 && IsArgValid(args[0]) && args[0] == "-h";
            bool isBlank = args != null && args[0] == "";
            return (isHOption || isBlank);
        }

        private static bool IsArgValid(string arg)
        {
            return !string.IsNullOrWhiteSpace(arg);
        }

        private void SetOutputMessage(string invalidResponseMessage, bool isQuiet = false, bool isHelpOtpion = false, bool isVersionOption = false)
        {
            _outputSB.Clear();
            if (!isHelpOtpion && !isQuiet && !isVersionOption && _outputSB != null){_outputSB.AppendLine(invalidResponseMessage);}
            if (isHelpOtpion){_outputSB.AppendLine(cHelpMessage);}
            if (isVersionOption) {_outputSB.AppendLine(cVersion);} //#3
        }
    }
}
