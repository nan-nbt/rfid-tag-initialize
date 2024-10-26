using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;
using System.IO.Ports;
using UR4RFID;

/**
 * RFIDClient Mass Initialize Batch (NEW)
 * Created By. Anan IT on 20240927
 * */
namespace RFIDTagInitialize
{
    public partial class FrmTagInitial : Form
    {
        static ur4Reader ur4 = new ur4Reader();
        private Thread autoInitializeThread;
        private Thread readTagThread;
        private CancellationTokenSource autoInitializeCts;
        private bool isAutoInitializing = false;
        private bool isReadingTag = false;
        private bool isConnected = false;

        private string INIT_EPC_DATA = "00 00 00 00 00 00 00 00 00 00 00 00";

        int mask_flag = 0;
        int cnt = 0;
        int power = 12;
        //int maxPower = 24;

        internal delegate void ShowUICallBack(string Info, bool uiIsShow);

        // Constructor when Object FrmTagInitial is Instantiated
        public FrmTagInitial()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            compFrmEnabled(true);
            txtAutoEPC.Text = INIT_EPC_DATA;

            cbxSetPower.SelectedIndex = power - 1;
            cbxTagType.SelectedIndex = 1;

            string[] myPorts = SerialPort.GetPortNames();
            foreach (string port in myPorts)
            {
                cbxComPort.Items.Add(port);
            }
            if (cbxComPort.Items.Count > 0)
                cbxComPort.SelectedIndex = cbxComPort.Items.Count - 1;

            ur4.CallAlert += new ur4Reader.MessageReceivedEventHandler(oReceiveMsg);
            autoInitializeCts = new CancellationTokenSource();

            string titleForm = this.Text;
            this.Text = titleForm + " | " + FrmOAuth2Login.userName + " [" + Program.compiledTime + "]";
        }

        /**
         * Event when Tab Control (Automatic/Manual) is changed
         * */
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indexTab = tabControl1.SelectedIndex;
            DisplayTabInformation(indexTab); 

            if (isConnected && isAutoInitializing && indexTab != 0)
            {
                StopAutoInitialize();
                timerThread();
            }
            else if (isConnected && !isAutoInitializing && indexTab == 0)
            {
                StartAutoInitialize();
            }
        }

        // Display information about which Tab is currently selected
        private void DisplayTabInformation(int indexTab)
        {
            if (indexTab == 0)
            {
                ShowUI("Automatic Initialize Tab selected.", true);
            }
            else if (indexTab == 1)
            {
                ShowUI("Manual Initialize Tab selected.", true);
            }
            else
            {
                ShowUI("Other Tab selected.", true);
            }
        }

        // Connect/Disconnect Reader (Click Event)
        private void imgReaderStatus_Click(object sender, EventArgs e)
        {
            object readerStatus = imgReaderStatus.Tag;
            if (readerStatus != null && readerStatus.Equals("connected"))
            {
                if (isAutoInitializing)
                {
                    StopAutoInitialize();
                }

                if (isReadingTag)
                {
                    StopReadTag();
                }
            
                DisconnectReader();
            }
            else if (tabControl1.SelectedIndex == 0)
            {
                ConnectReader();
                StartAutoInitialize();
            }
            else
            {
                ConnectReader();
            }
        }

        // Auto scroll down to the new text on log field when there is a new text (Text Changed Event)
        private void rtbProgMsg_TextChanged(object sender, EventArgs e)
        {
            rtbProgMsg.ScrollToCaret();
        }

        // Restart the Reader Connection and Thread Auto Initialize (Click Event)
        private void btnRestart_Click(object sender, EventArgs e)
        {
            if (ur4.IsConnected)
            {
                StopAutoInitialize();
                ShowUI("Attempting to restart the reader...", true);

                DisconnectReader();

                if (!ur4.IsConnected)
                {
                    ConnectReader();
                    StartAutoInitialize();
                    ShowUI("Reader restart successfully.", true);
                }
                else
                {
                    ShowUI("Failed to restart the reader!", true);
                }
            }
            else
            {
                ShowUI("Reader is not connected.", true);
            }
        }

        // For make sure UI not blocked by asynchronously stop the Thread (autoInitializeThread)
        private void timerThread()
        {
            // Do not block the UI thread; wait for the thread to finish asynchronously.
            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 100;  // Polling interval to check the thread status
            timer.Tick += (s, args) =>
            {
                if (autoInitializeThread != null && !autoInitializeThread.IsAlive)
                {
                    timer.Stop();  // Stop the timer once the thread finishes
                }
            };
            timer.Start();
        }

        // Clear the log message (Click Event)
        private void btnClearProgMsg_Click(object sender, EventArgs e)
        {
            rtbProgMsg.Text = "";
        }

        // For handle FrmTagInitial closed event
        private void FrmTagInitialize_FormClosed(object sender, FormClosedEventArgs e)
        {
            // logout OAuth 2 session
            FrmOAuth2Login oFrm = new FrmOAuth2Login(false);
            oFrm.StartPosition = FormStartPosition.CenterScreen;
            oFrm.ShowDialog();

            // Stop the auto-initialization
            StopAutoInitialize();
    
            // Disconnect the reader if it's connected
            if (ur4.IsConnected)
            {
                ur4.Disconnect();
            }

            // Cancel any ongoing operations
            if (autoInitializeCts != null)
            {
                autoInitializeCts.Cancel();
            }

            // Safely wait for the auto-initialize thread to finish if it exists
            if (autoInitializeThread != null && autoInitializeThread.IsAlive)
            {
                try
                {
                    autoInitializeThread.Join(); // Wait for the thread to finish
                }
                catch (ThreadInterruptedException)
                {
                    // Handle the situation if the thread was interrupted
                }
                catch (Exception ex)
                {
                    // Log or handle other exceptions as necessary
                    ShowUI("Error while waiting for thread to finish: " + ex.Message, true);
                }
            }
        }

        // Set Reader Power event Item Changed
        private void cbxSetPower_SelectedIndexChanged(object sender, EventArgs e)
        {
            power = cbxSetPower.SelectedIndex + 1;
        }

        /**
         * Manual Tab Events
         * */
        // Manual Read Tag event
        private void btnReadTag_Click(object sender, EventArgs e)
        {
            if (btnReadTag.Text.Equals("Start Read Tag"))
            {
                StartReadTag();
            }
            else
            {
                StopReadTag();
            }
        }

        // Manual Mask Tag event
        private void btnMask_Click(object sender, EventArgs e)
        {
            mask_flag = 0;
            if (!String.IsNullOrEmpty(txtManualEPC.Text) && txtManualEPC.Text.Length == 24)
            {
                string maskResult = ur4.SetAcqG2Mask("1", "32", "96", txtManualEPC.Text);
                if (maskResult.IndexOf("Success") > -1)
                {
                    mask_flag = 1;
                    btnUnmask.Enabled = true;
                    ShowUI("Mask Success! " + txtManualEPC.Text, true);
                }
                else
                {
                    ShowUI("Mask Failed! " + txtManualEPC.Text, true);
                }
            }
            else
            {
                ShowUI("Mask Length <> 24 Character!", true);
            }
        }

        // Manual Unmask Tag event
        private void btnUnmask_Click(object sender, EventArgs e)
        {
            if (ur4.SetAcqG2unMask().IndexOf("Success") > -1)
            {
                mask_flag = 0;
                txtManualEPC.Text = "";
                ShowUI("Unmask Success!", true);
            }
            else
            {
                ShowUI("Unmask Failed!", true);
            }
        }

        // Manual Write Tag event
        private void btnWriteTag_Click(object sender, EventArgs e)
        {
            string result = ur4.ProgramEPC(txtWrite.Text, txtManualEPC.Text);
            txtWrite.Text = "";
            ShowUI(result, true);
        }

        // Clear Tag Log in Manual Tab
        private void btnClearTagLog_Click(object sender, EventArgs e)
        {
            lbxTag.Items.Clear();
        }

        /**
         * Group function  for handle events
         * */
        // Connecting to the Reader
        private void ConnectReader()
        {
            cnt = 0;
            lblCount.Text = cnt.ToString();
            ShowUI("Attempting to connect to the reader...", true);

            if (!string.IsNullOrEmpty(cbxTagType.Text) && cbxTagType.Text.Equals("AlienUHF"))
            {
                ShowUI("Selected Tag Type: " + cbxTagType.Text, true);
                ShowUI("Connecting to reader on port: " + cbxComPort.SelectedItem.ToString(), true);

                ur4.Connect(cbxComPort.SelectedItem.ToString());
                if (ur4.IsConnected)
                {
                    ShowUI("Reader connected successfully.", true);
                    imgReaderStatus.Image = Properties.Resources.connect;
                    imgReaderStatus.Tag = "connected";
                    compFrmEnabled(false);
                    compTabEnabled(true);
                    isConnected = true;

                    cbxSetPower.SelectedIndex = power - 1;
                    setOutputPower(1, power); // set output power MinPower dBm.
                }
                else
                {
                    ShowUI("Failed to connect to reader!", true);
                }
            }
            else
            {
                MessageBox.Show("Please select Reader Type AlienUHF!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ShowUI("Failed to connect to reader!", true);
            }
        }

        // Disconnecting the Reader
        private void DisconnectReader()
        {
            if (ur4.IsConnected)
            {
                ur4.Disconnect();

                if (!ur4.IsConnected)
                {
                    ShowUI("Reader disconnected successfully.", true);
                    imgReaderStatus.Image = Properties.Resources.disconnect;
                    imgReaderStatus.Tag = "disconnected";
                    compFrmEnabled(true);
                    compTabEnabled(false);
                    isConnected = false;
                    isAutoInitializing = false;

                    setOutputPower(1, power); // set output power MinPower dBm.

                    // Do not block the UI thread; wait for the thread to finish asynchronously.
                    if (tabControl1.SelectedIndex == 0)
                    {
                        timerThread();
                    }
                }
                else
                {
                    ShowUI("Failed to disconnect the reader!", true);
                }
            }
            else
            {
                ShowUI("Reader is not connected.", true);
            }
        }

        // Start Auto Initialize Method
        private void StartAutoInitialize()
        {
            // Ensure auto-initialization only starts if the reader is connected
            if (ur4.IsConnected && !isAutoInitializing)
            {
                isAutoInitializing = true;
                autoInitializeCts = new CancellationTokenSource();
                var token = autoInitializeCts.Token;

                autoInitializeThread = new Thread(() => AutoInitialize(token));
                autoInitializeThread.Start();
            }
        }

        // Stop Auto Initialize Method
        private void StopAutoInitialize()
        {
            if (isAutoInitializing)
            {
                autoInitializeCts.Cancel();
                if (autoInitializeThread != null)
                {
                    autoInitializeThread.Join(); // Wait for the thread to finish
                    autoInitializeThread = null; // Reset the thread reference
                }

                isAutoInitializing = false;
            }
        }

        // Thread of Initialize Tag Automatically 
        private void AutoInitialize(CancellationToken token)
        {
            int i = 1;
            //cnt = 1;
            while (!token.IsCancellationRequested)
            {
                string getTag = "";
                while (!token.IsCancellationRequested)
                {
                    string readTag = ur4.Read(mask_flag, txtAutoEPC.Text);
                    if (readTag != INIT_EPC_DATA.Replace(" ", "") && readTag.Length == 24)
                    {
                        getTag = readTag;
                        break;
                    }
                }

                // Check again after reading the tag
                if (token.IsCancellationRequested) break;

                // Maks and Write Tag process
                if (!getTag.Equals(INIT_EPC_DATA.Replace(" ", "")) && getTag.Length == 24)
                {
                    if (ur4.SetAcqG2Mask("1", "32", "96", getTag).IndexOf("Success") > -1)
                    {
                        mask_flag = 1;

                        //setOutputPower(1, 22);
                        //setOutputPower(1, maxPower); // set output power MaxPower dBm.
                        if (ur4.ProgramEPC(INIT_EPC_DATA.Replace(" ", ""), getTag).Equals("寫碼結果:True"))
                        {
                            cnt = i++;
                            Invoke((MethodInvoker)(() => ShowUI("Tag Initialized Successful!", true)));
                            Invoke((MethodInvoker)(() => lblCount.Text = cnt.ToString()));
                        }

                        if (ur4.SetAcqG2unMask().IndexOf("Success") > -1)
                        {
                            mask_flag = 0;
                            //setOutputPower(1, 11);
                            //setOutputPower(1, minPower); // set output power MinPower dBm.
                        }
                    }
                }

                if (token.IsCancellationRequested) break;
                Thread.Sleep(500);
            }
        }

        // Set Output Power Method
        private void setOutputPower(int save, int power)
        {
            //ur4.SetPower(save, power);
            if (ur4.SetPower(save, power).IndexOf("Success") > -1)
            {
                ShowUI("Reader Output Power: " + power + " dBm!", true);
            }
        }

        // For handle message received from the Reader
        void oReceiveMsg(object sender, UR4RFID.ur4Reader.AlertEventArgs e)
        {
            ShowUI(e.uuiData, true);
        }

        // Display information to Log field
        public void ShowUI(string Info, bool uiIsShow)
        {
            try
            {
                if (rtbProgMsg.InvokeRequired)
                {
                    // call back on this same method, but in a different thread.
                    rtbProgMsg.Invoke(
                         new ShowUICallBack(ShowUI), // the method to call back on
                         new object[] { Info, uiIsShow });
                }
                else
                {
                    // you are in this method on the correct thread.
                    Info = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + ":  " + Info + "\r\n\r\n";
                    if (uiIsShow)
                        rtbProgMsg.AppendText(Info);
                }
            }
            catch (Exception ex)
            {
                // you are in this method on the correct thread.
                Info = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + ":  " + "Thread:" + ex.Message + "\r\n\r\n";
            }
        }

        // Start Multiple Read Tag Method
        private void StartReadTag()
        {
            btnReadTag.Text = "Stop Read Tag";
            isReadingTag = true;

            readTagThread = new Thread(ReadTags);
            readTagThread.Start();
        }

        // Stop Multiple Read Tag Method
        private void StopReadTag()
        {
            btnReadTag.Text = "Start Read Tag";
            isReadingTag = false;

            // Wait for the thread to finish before proceeding
            if (readTagThread != null && readTagThread.IsAlive)
            {
                readTagThread.Join();
            }
        }

        // Thread of Multiple Read Tag in Manual Tab
        private void ReadTags()
        {
            while (isReadingTag)
            {
                string result = ur4.Read(mask_flag, txtManualEPC.Text);
                if (!String.IsNullOrEmpty(result))
                {
                    // Update the listbox in the UI thread
                    Invoke((MethodInvoker)delegate
                    {
                        lbxTag.Items.Add(result);
                        lbxTag.TopIndex = lbxTag.Items.Count - 1;
                    });
                }

                Thread.Sleep(500); // Add a small delay to avoid CPU overload
            }
        }

        // For handle component FrmTagInitial is Enabled/Disabled
        private void compFrmEnabled(bool flag)
        {
            cbxTagType.Enabled = flag;
            cbxComPort.Enabled = flag;
            cbxSetPower.Enabled = flag;
        }

        // For handle component Tab is Enabled/Disabled
        private void compTabEnabled(bool flag)
        {
            btnRestart.Enabled = flag;
            btnReadTag.Enabled = flag;
            btnClearTagLog.Enabled = flag;
            btnMask.Enabled = flag;
            btnUnmask.Enabled = flag;
            btnWriteTag.Enabled = flag;
            txtWrite.Enabled = flag;
            txtManualEPC.Enabled = flag;
        }

    }
}
