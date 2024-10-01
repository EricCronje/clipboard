using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;
using cb = ClipboardUtility.ClipboardUtility;

namespace UnitTestClipboard
{
    [TestClass]
    public class ClipboardUnitTest
    {


        /// <summary>
        /// Valid functional test
        /// the same way it will be used in the wild.
        /// </summary>
        [TestMethod]
        public void ValidAddToClipboard()
        {
            //arrange
            using (cb clipboard = new cb())
            {
                //assign
                string[] inputArray = { "Dream", null, " ", "" };
                bool isSuccessfull = clipboard.Use(inputArray);
                //assert
                Assert.IsFalse(clipboard == null);
                Assert.IsTrue(isSuccessfull);
                Assert.AreEqual(Clipboard.GetText(), inputArray[0]);
                Assert.IsTrue(!string.IsNullOrEmpty(inputArray[0]));
                Assert.IsTrue(clipboard.Output == "Dream");
                //clean
                Clipboard.Clear();
            }
        }

        /// <summary>
        /// Invalid functional test
        /// Sending an empty string
        /// Expected 
        ///     * Return empty string.
        ///     * Store nothing on system clipboard
        ///     * If there was something then the clipboard will be cleared.
        /// </summary>
        [TestMethod]
        public void InValidToClipboard()
        {
            //send null to the first parameter.

            //arrange
            using (cb clipboard = new cb())
            {
                //clear
                Clipboard.Clear();
                //assign
                bool result = clipboard.Use(new string[] { "", "", " ", "" });
                ////assert
                Assert.IsTrue(result);
                string check = Clipboard.GetText().ToString();
                Assert.IsTrue(check == "");
                Assert.IsTrue(clipboard.Output == clipboard.cHelpMessage);
            }

            //tests

            // unit tests - check
            // functional tests
            // integration tests
            // stress tests

        }

        /// <summary>
        /// Invalid functional test
        /// Sending an empty string
        /// Expected:
        ///     * output the help context
        ///     * Clipboard cleared
        ///     * same as -h or ""
        /// </summary>
        [TestMethod]
        public void InValidAddEmptyToClipboard()
        {
            //arrange
            using (cb clipboard = new cb())
            {
                //assign
                string[] inputArray = { "" };
                bool isSuccessfull = clipboard.Use(inputArray);
                //assert
                Assert.IsFalse(clipboard == null);
                Assert.IsTrue(isSuccessfull);
                string check = Clipboard.GetText().ToString();
                string checkInput = Clipboard.GetText().ToString();
                Assert.AreEqual(check, checkInput);
                Assert.IsTrue(string.IsNullOrEmpty(inputArray[0]));
                Assert.IsTrue(clipboard.Output == clipboard.cHelpMessage);
                //clean
                Clipboard.Clear();
            }
        }

        [TestMethod]
        public void InValidAddWhiteSpacesToClipboard()
        {
            //arrange
            using (cb clipboard = new cb())
            {
                //assign
                string[] inputArray = { " " };
                bool isSuccessfull = clipboard.Use(inputArray);
                //assert
                Assert.IsFalse(clipboard == null);
                Assert.IsFalse(isSuccessfull);
                string check = Clipboard.GetText().ToString();
                string checkInput = inputArray[0];
                Assert.AreEqual(" ", checkInput);
                Assert.IsTrue(!string.IsNullOrEmpty(inputArray[0]));
                Assert.IsTrue(clipboard.Output == "Nothing added to clipboard.");
                //clean
                Clipboard.Clear();
            }
        }

        [TestMethod]
        public void ValidAddAValidSentenceToClipboard()
        {
            //arrange
            using (cb clipboard = new cb())
            {
                //assign
                string[] inputArray = { "The big tree \tis growing apples!\r\nM\'Clachlen said \"Yes - it is\" said Mike, and walked away.  " };
                bool isSuccessfull = clipboard.Use(inputArray);
                //assert
                Assert.IsFalse(clipboard == null);
                Assert.IsTrue(!string.IsNullOrEmpty(inputArray[0]));
                Assert.IsTrue(isSuccessfull);
                string check = Clipboard.GetText().ToString();
                string checkInput = inputArray[0];
                Assert.IsTrue(check == checkInput);
                Assert.IsTrue(clipboard.Output == checkInput);
                //clean
                Clipboard.Clear();
            }
        }
        /// <summary>
        /// passing -q
        /// Expected:
        ///     * No output
        ///     * copy to clipboard
        /// </summary>
        [TestMethod]
        public void ValidToClipboardWith_q_Option()
        {
            //arrange
            using (cb clipboard = new cb())
            {
                //assign
                string[] inputArray = { "The big tree \tis growing apples!\r\nM\'Clachlen said \"Yes - it is\" said Mike, and walked away.  ", "-q" };
                bool isSuccessfull = clipboard.Use(inputArray);
                //assert
                Assert.IsFalse(clipboard == null);
                Assert.IsTrue(!string.IsNullOrEmpty(inputArray[0]));
                Assert.IsTrue(isSuccessfull);
                string check = Clipboard.GetText().ToString();
                string checkInput = inputArray[0];
                Assert.IsTrue(check == checkInput);
                Assert.IsTrue(clipboard.Output.Length == 0);
                //clean
                Clipboard.Clear();
            }
        }

        /// <summary>
        /// Valid test cb With -h option
        /// Expected:
        ///     * bring up help options
        ///     * do not copy to clipboard.
        /// </summary>
        [TestMethod]
        public void ValidToClipboardWith_h_Option()
        {
            Clipboard.Clear();
            //arrange
            using (cb clipboard = new cb())
            {
                //assign
                string[] inputArray = { "-h" };
                bool isSuccessfull = clipboard.Use(inputArray);
                //assert
                Assert.IsTrue(isSuccessfull);
                string check = Clipboard.GetText().ToString();
                Assert.IsTrue(check == "");
                Assert.IsTrue(clipboard.Output.Length > 0);
                Assert.IsTrue(clipboard.Output == "cb: cb [-h] <string to copy to the clipboard>[-q] [-v]\r\n\tCopies the string send to the system clipboard.\r\n\tAvailable to CTRL + v or shift + Insert to paste the content.\r\n\t\r\n\tWithout arguments, 'cb' shows the help content.\r\n\t\r\n\tOtherwise, 'cb' will copy the passed string to the system clipboard and outputs the string passed in the first argument.\r\n\t\r\n\tOptions:\r\n\t\t-q\t\t:\tWill not output the string passed.\r\n\t\t-h\t\t:\tWill bring up this help content. (Also with no arguments.)\r\n\r\n\tExit Status:\r\n\t'cb' returns true.\r\n\t\r\n\tExamples:\r\n\t\r\n\tUsed in windows bash.\r\n\tcb \"$(ls)\"\r\n\t\r\n\tResult:\r\n\tList the directory content with \\r\\n. Control line feed.\r\n\tThen CTRL+v or shift+insert to paste the content from the clipboard.");
                //clean
                Clipboard.Clear();
            }
        }

        [TestMethod]
        public void ValidToClipboardWith_Empty_Option()
        {
            Clipboard.Clear();
            //arrange
            using (cb clipboard = new cb())
            {
                //assign
                string[] inputArray = { "" };
                bool isSuccessfull = clipboard.Use(inputArray);
                //assert
                Assert.IsTrue(isSuccessfull);
                string check = Clipboard.GetText().ToString();
                Assert.IsTrue(check == "");
                Assert.IsTrue(clipboard.Output.Length > 0);
                Assert.IsTrue(clipboard.Output == clipboard.cHelpMessage);
                //clean
                Clipboard.Clear();
            }
        }

        [TestMethod]
        public void ValidNoOptionsCB()
        {
            Clipboard.Clear();
            //arrange
            using (cb clipboard = new cb())
            {
                //assign
                string[] inputArray = { };
                bool isSuccessfull = clipboard.Use(inputArray);
                //assert
                Assert.IsTrue(isSuccessfull);
                string check = Clipboard.GetText().ToString();
                Assert.IsTrue(check == "");
                Assert.IsTrue(clipboard.Output.Length > 0);
                Assert.IsTrue(clipboard.Output == "cb: cb [-h] <string to copy to the clipboard>[-q] [-v]\r\n\tCopies the string send to the system clipboard.\r\n\tAvailable to CTRL + v or shift + Insert to paste the content.\r\n\t\r\n\tWithout arguments, 'cb' shows the help content.\r\n\t\r\n\tOtherwise, 'cb' will copy the passed string to the system clipboard and outputs the string passed in the first argument.\r\n\t\r\n\tOptions:\r\n\t\t-q\t\t:\tWill not output the string passed.\r\n\t\t-h\t\t:\tWill bring up this help content. (Also with no arguments.)\r\n\r\n\tExit Status:\r\n\t'cb' returns true.\r\n\t\r\n\tExamples:\r\n\t\r\n\tUsed in windows bash.\r\n\tcb \"$(ls)\"\r\n\t\r\n\tResult:\r\n\tList the directory content with \\r\\n. Control line feed.\r\n\tThen CTRL+v or shift+insert to paste the content from the clipboard.");
                //clean
                Clipboard.Clear();
            }
        }

        [TestMethod]
        public void ValidVersionOption()
        {
            Clipboard.Clear();
            //arrange
            using (cb clipboard = new cb())
            {
                //assign
                string[] inputArray = { "-v" };
                bool isSuccessfull = clipboard.Use(inputArray);
                //assert
                Assert.IsTrue(isSuccessfull);
                string check = Clipboard.GetText().ToString();
                Assert.IsTrue(check == "");
                Assert.IsTrue(clipboard.Output.Length > 0);
                Assert.IsTrue(clipboard.Output == $"Version 5.1.2");
                //clean
                Clipboard.Clear();
            }
        }
    }
}
