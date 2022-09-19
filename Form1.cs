using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Net;
using System.ComponentModel;
using System.Net.Http;

namespace RYTV3
{
    public partial class Form1 : Form
    {
        private string filePath;
        private string fileContent;
        private int numberofVideos = 1;
        public int videonumbers;
        public string videoselection;
        public string ytid;
        public string thumbnail;
        static Random rnd = new Random();
        public string v1;
        public string v2;
        public string v3;
        public string v4;
        public string v5;
        public string v6;
        public List<String> youtubelist = new List<String>();
        /// <summary>
        /// CONST
        /// </summary>
        // Day of the week
        public string Q_Day;
        // Current Month 
        public string Q_Month;
        // Current Date
        public string Q_Date;
        // Current Weather 
        public string Q_Weather;
        public string Q_Temp;


        public Form1()
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;
            InitializeComponent();
        }

        /// <summary>
        /// Load youtube list from File.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadList_btn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                    Properties.Settings.Default.filelocation = filePath;
                    Properties.Settings.Default.Save();
                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        // read the content and then put it int a list of strings 
                        fileContent = reader.ReadToEnd();
                        var videolist = File.ReadAllLines(filePath);
                        var youtubelist = new List<string>(videolist);
                        // count the number of videos 
                        var lineCount = File.ReadAllLines(filePath).Length;
                        status_lbl.ForeColor = Color.Blue;
                        status_lbl.Text = Convert.ToString(lineCount) + " Videos Loaded";
                    }
                }
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int number;
            number = Convert.ToInt32(numericUpDown1.Value);
            videonumbers = number;
            switch (number)
            {
                case 1:
                    // only show 1 groupbox 
                    gb1.Show();
                    gb2.Hide();
                    gb3.Hide();
                    gb4.Hide();
                    gb5.Hide();
                    gb6.Hide();
                    // change location to center on screen
                    gb1.Location = new Point(406, 69);
                    // since we only have on group box make it larger
                    gb1.Width = 550;
                    gb1.Height = 450;
                    break;
                case 2:
                    // move groupbox back to default location
                    gb1.Location = new Point(200, 69);
                    // reset to gb1 to default size
                    gb1.Width = 522;
                    gb1.Height = 422;
                    // move gb2 over to the right 
                    gb2.Location = new Point(756, 69);
                    // make gb2 larger 
                    gb2.Width = 522;
                    gb2.Height = 422;
                    gb1.Update();
                    gb1.Show();
                    gb2.Show();
                    gb3.Hide();
                    gb4.Hide();
                    gb5.Hide();
                    gb6.Hide();
                    break;
                case 3:
                    // set gb1 &  gb2 to default location.
                    gb1.Location = new Point(181, 52);
                    gb2.Location = new Point(550, 52);
                    // set gb1 & gb2 back to default size
                    gb1.Width = 322;
                    gb1.Height = 220;
                    gb2.Width = 322;
                    gb2.Height = 220;
                    gb3.Width = 322;
                    gb3.Height = 220;
                    gb1.Show();
                    gb2.Show();
                    gb3.Show();
                    gb4.Hide();
                    gb5.Hide();
                    gb6.Hide();
                    break;
                case 4:
                    // set gb1 &  gb2 to default location.
                   // gb1.Location = new Point(250, 69);
                   // gb2.Location = new Point(606, 69);
                    // set gb1 & gb2 back to default size
                    gb1.Width = 322;
                    gb1.Height = 220;
                    gb2.Width = 322;
                    gb2.Height = 220;
                    gb3.Width = 322;
                    gb3.Height = 220;
                    gb4.Location = new Point(181, 314);
                    gb4.Width = 322;
                    gb4.Height = 220;
                    gb1.Show();
                    gb2.Show();
                    gb3.Show();
                    gb4.Show();
                    gb5.Hide();
                    gb6.Hide();
                    break;
                case 5:
                    // set gb1 &  gb2 to default location.
                    gb1.Width = 322;
                    gb1.Height = 220;
                    gb2.Width = 322;
                    gb2.Height = 220;
                    gb3.Width = 322;
                    gb3.Height = 220;
                    gb4.Location = new Point(181, 314);
                    gb4.Width = 322;
                    gb4.Height = 220;
                    gb1.Show();
                    gb2.Show();
                    gb3.Show();
                    gb4.Show();
                    gb5.Show();
                    gb6.Hide();
                    break;
                case 6:
                    // set gb1 &  gb2 to default location.
                    gb1.Width = 322;
                    gb1.Height = 220;
                    gb2.Width = 322;
                    gb2.Height = 220;
                    gb3.Width = 322;
                    gb3.Height = 220;
                    gb4.Location = new Point(181, 314);
                    gb4.Width = 322;
                    gb4.Height = 220;
                    gb1.Show();
                    gb2.Show();
                    gb3.Show();
                    gb4.Show();
                    gb5.Show();
                    gb6.Show();
                    break;
            }
        }
        /// <summary>
        ///  Load the youtube list into the list of strings 
        /// </summary>
        private void loadlist()
        {
            string filelocation;
            bool fileex;
            Properties.Settings.Default.Reload();
            filelocation = Properties.Settings.Default.filelocation;
            fileex = File.Exists(filelocation);
            if (fileex == false)
            {
                return;
            }
            using (StreamReader Reader = new StreamReader(filelocation))
            {
                while (Reader.EndOfStream == false)
                    youtubelist.Add(Reader.ReadLine());
            }
        }
        /// <summary>
        ///  Get the Thumbnail
        /// </summary>
        /// <param name="yturl"></param>
        /// 
        private void getThumbNail(string ytaddress)
        {

            thumbnail = "https://img.youtube.com/vi/" + ytaddress + "/hqdefault.jpg";

        }

        // Extract just the Unique ID.
        private void getytID(string yturl)
        {

            ytid = yturl.Replace("https://www.youtube.com/watch?v=", "");
            getThumbNail(ytid);
        }
        /// <summary>
        /// get a Random Video from youtube list
        /// </summary>
        /// 
        private void RndVideo()
        {
            loadlist();
            int r = rnd.Next(youtubelist.Count);
            videoselection = (string)youtubelist[r];
        }
        /// <summary>
        /// Get a Random Videos button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void getVids_btn_Click(object sender, EventArgs e)
        {
            int choices;
            choices = Convert.ToInt32(numericUpDown1.Value);
            for (int i = 0; i < choices; i++)
            {
                bool checkfile;
                checkfile = File.Exists(filePath);
                if (checkfile == false)
                {
                    MessageBox.Show("Please Load youtube list First");
                    return;
                } // end of if statement
                // Zero = 1 (Count starts at Zero instead of 1)
                // this switch provide each picturebox with a video choice based on how many groupboxes are displayed.
                switch (i)
                {
                    // Only 1 Video
                    case 0:
                    start0:
                        Thread t = new Thread(new ThreadStart(RndVideo));
                        t.Start();
                        if (videoselection == null)
                        {
                            RndVideo();
                        }
                        // link used for pb click
                        v1 = videoselection;
                        //Console.WriteLine(videoselection);
                        try
                        {
                            getytID(videoselection);
                            pb1.Load(thumbnail);
                            //pb11.LoadImage(thumbnail);
                        }
                        catch (Exception f)
                        {
                            Console.WriteLine("{0} Exception caught.", f);
                            goto start0;
                        }
                        break;

                    // Two Videos 
                    case 1:
                    start1:
                        Thread t1 = new Thread(new ThreadStart(RndVideo));
                        t1.Start();
                        if (videoselection == null)
                        {
                            RndVideo();
                        }
                        // used for pb2 click
                        v2 = videoselection;
                        try
                        {
                            getytID(videoselection);
                            Console.WriteLine(thumbnail);
                            pb2.Load(thumbnail);
                            //pb11.LoadImage(thumbnail);
                        }
                        catch (Exception f)
                        {
                            Console.WriteLine("{0} Exception caught.", f);
                            goto start1;
                        }
                        break;

                    // Three Videos 
                    case 2:
                    start2:
                        Thread t2 = new Thread(new ThreadStart(RndVideo));
                        t2.Start();
                        if (videoselection == null)
                        {
                            RndVideo();
                        }
                        // used for pb3 click 
                        v3 = videoselection;
                        //Console.WriteLine(videoselection);
                        try
                        {
                            getytID(videoselection);
                            Console.WriteLine(thumbnail);
                            pb3.Load(thumbnail);
                            //pb11.LoadImage(thumbnail);
                        }
                        catch (Exception f)
                        {
                            Console.WriteLine("{0} Exception caught.", f);
                            goto start2;
                        }
                        break;

                    // Four Videos 
                    case 3:
                    start3:
                        Thread t3 = new Thread(new ThreadStart(RndVideo));
                        t3.Start();
                        if (videoselection == null)
                        {
                            RndVideo();
                        }
                        // used for pb4 click 
                        v4 = videoselection;
                        //Console.WriteLine(videoselection);
                        try
                        {
                            getytID(videoselection);
                            Console.WriteLine(thumbnail);
                            pb4.Load(thumbnail);
                            //pb11.LoadImage(thumbnail);
                        }
                        catch (Exception f)
                        {
                            Console.WriteLine("{0} Exception caught.", f);
                            goto start3;
                        }
                        break;
                    case 4:
                    start4:
                        Thread t4 = new Thread(new ThreadStart(RndVideo));
                        t4.Start();
                        if (videoselection == null)
                        {
                            RndVideo();
                        }
                        // used for pb5 click 
                        v5 = videoselection;
                        //Console.WriteLine(videoselection);
                        try
                        {
                            getytID(videoselection);
                            Console.WriteLine(thumbnail);
                            pb5.Load(thumbnail);
                            //pb11.LoadImage(thumbnail);
                        }
                        catch (Exception f)
                        {
                            Console.WriteLine("{0} Exception caught.", f);
                            goto start4;
                        }
                        break;
                    case 5:
                    start5:
                        Thread t5 = new Thread(new ThreadStart(RndVideo));
                        t5.Start();
                        if (videoselection == null)
                        {
                            RndVideo();
                        }
                        // used for pb6 click 
                        v6 = videoselection;
                        //Console.WriteLine(videoselection);
                        try
                        {
                            getytID(videoselection);
                            Console.WriteLine(thumbnail);
                            pb6.Load(thumbnail);
                            //pb11.LoadImage(thumbnail);
                        }
                        catch (Exception f)
                        {
                            Console.WriteLine("{0} Exception caught.", f);
                            goto start5;
                        }
                        break;

                } // end of switch

            } // end of for statment
        }
        /// <summary>
        /// When the Form Loads Set these Settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load_1(object sender, EventArgs e)
        {
            // set number spinner to 2
            numericUpDown1.Value = 2;
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            build_label.Text = String.Format("version {0}", version);
            // check license 
            WebClient client = new WebClient();
            Stream stream = client.OpenRead("https://pastebin.com/raw/vjEgqBMq");
            StreamReader reader = new StreamReader(stream);
            String content = reader.ReadToEnd();
            content = content.TrimEnd();
            Console.WriteLine(content);
            if (content != "wings2022")
            {
                lic_lbl.Text = "Invalid License";
                lic_lbl.ForeColor = System.Drawing.Color.DarkGreen;
                MessageBox.Show("License has epired or is invalid, Please contact JJS Sofrware LLC");
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                lic_lbl.Text = "OK";
                lic_lbl.ForeColor = System.Drawing.Color.DarkGreen;
            }


        }
        /// <summary>
        /// pb 1 click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pb1_Click(object sender, EventArgs e)
        {
            // Stop Null box clicks 
            if (v1 == null)
            {
                return;
            }
            System.Diagnostics.Process.Start(v1);
        }

        private void pb2_Click(object sender, EventArgs e)
        {
            // Stop Null box clicks 
            if (v2 == null)
            {
                return;
            }
            System.Diagnostics.Process.Start(v2);
        }

        private void pb3_Click(object sender, EventArgs e)
        {
            // Stop Null box clicks 
            if (v3 == null)
            {
                return;
            }
            System.Diagnostics.Process.Start(v3);
        }

        private void pb4_Click(object sender, EventArgs e)
        {
            // Stop Null box clicks 
            if (v4 == null)
            {
                return;
            }
            System.Diagnostics.Process.Start(v4);
        }

        private void pb5_Click(object sender, EventArgs e)
        {
            // Stop Null box clicks 
            if (v5 == null)
            {
                return;
            }
            System.Diagnostics.Process.Start(v5);
        }

        private void pb6_Click(object sender, EventArgs e)
        {
            // Stop Null box clicks 
            if (v6 == null)
            {
                return;
            }
            System.Diagnostics.Process.Start(v6);
        }


        ///
        ////
        ////  -   Start of Questions Section
        ///
        ///
        /////
        /// 



        //// =========== FUNCTIONS FOR QUESTIONS ==========
        /// 
          public void DaysOfTheWeek(string day)
        {
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            synthesizer.Volume = 100;  // 0...100
            synthesizer.Rate = -2;     // -10...10
            synthesizer.Speak(day);
            Q_Day = day;
        }
        public void CurrentMonth(string month)
        {
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            synthesizer.Volume = 100;  // 0...100
            synthesizer.Rate = -2;     // -10...10
            synthesizer.Speak(month);
            Q_Month = month;
        }
        public void MonthDate(string monthDate)
        {
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            synthesizer.Volume = 100;  // 0...100
            synthesizer.Rate = -2;     // -10...10
            synthesizer.Speak(monthDate);
            Q_Date = monthDate;
        }
        // Day of the Month
        public void DateOfMonth(string Date)
        {
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            synthesizer.Volume = 100;  // 0...100
            synthesizer.Rate = -2;     // -10...10
            synthesizer.Speak(Date);
            Q_Date = Date;
        }
        /// <summary>
        ///  Weather Function 
        /// </summary>
        /// <param name="Date"></param>
        public void CurrentWeather(string Weather)
        {
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            synthesizer.Volume = 100;  // 0...100
            synthesizer.Rate = -2;     // -10...10
            synthesizer.Speak("The Weather is" + Weather);
            Q_Weather = Weather;
        }

        ///  The Temperature Function 
        ///
        ///
        public void CurrentTemp(string temp)
        {
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            synthesizer.Volume = 100;  // 0...100
            synthesizer.Rate = -2;     // -10...10
            synthesizer.Speak("The Temperature is" + temp);
            Q_Temp = temp;
        }
        

            //// ============== END OF QUESTION FUNCTIONS ================
        /// 
        /// <summary>
        ///  Days of the Week Button. 
        ///  These buttonss
        ///  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        /// MONDAY 
        private void button1_Click(object sender, EventArgs e)
        {
            DaysOfTheWeek("Monday");
            //string text = button1.Text;
            //SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            //synthesizer.Volume = 100;  // 0...100
            //synthesizer.Rate = -2;     // -10...10

            // Synchronous
            //synthesizer.Speak("Monday");
            // Set Const For Day of the Week 
            //Q_Day = "Monday";
        }

        // TUESDAY 
        private void button2_Click(object sender, EventArgs e)
        {
            DaysOfTheWeek("Tuesday");
        }
         /// Wednesday
        private void button3_Click(object sender, EventArgs e)
        {
            DaysOfTheWeek("Wednesday");
        }

         // THURSDAY--
        private void button4_Click(object sender, EventArgs e)
        {
            DaysOfTheWeek("Thursday");
        }

         /// FRIDAY  
        private void button5_Click(object sender, EventArgs e)
        {
            DaysOfTheWeek("Friday");
        }
        
         // SUNDAY 
        private void button7_Click(object sender, EventArgs e)
        {
            DaysOfTheWeek("Saturday");
        }
        // SUNDAY
        private void button6_Click(object sender, EventArgs e)
        {
            DaysOfTheWeek("Sunday");
        }
        /// <summary>
        ///  GO TO NEXT TAB 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            flatTabControl2.SelectedIndex = (flatTabControl2.SelectedIndex + 1 < flatTabControl2.TabCount) ?
                             flatTabControl2.SelectedIndex + 1 : flatTabControl2.SelectedIndex;
        }
        ///
        ///    WHAT MONTH IS IT?
        /// 
        /// <summary>
        ///  January Button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button16_Click(object sender, EventArgs e)
        {
            CurrentMonth("January");
        }
        // February Button 
        private void button15_Click(object sender, EventArgs e)
        {
            CurrentMonth("February");
        }
         /// <summary>
         ///  March Button 
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
        private void button14_Click(object sender, EventArgs e)
        {
            CurrentMonth("March");
        }
        /// <summary>
        ///  April Button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button13_Click(object sender, EventArgs e)
        {
            CurrentMonth("April");
        }
        /// <summary>
        ///   May Button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button12_Click(object sender, EventArgs e)
        {
            CurrentMonth("May");
        }
        /// <summary>
        ///    Month Of June Button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button10_Click(object sender, EventArgs e)
        {
            CurrentMonth("June");
        }
        /// <summary>
        ///  The Month of July
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button11_Click(object sender, EventArgs e)
        {
            CurrentMonth("July");
        }
        /// <summary>
        ///  The Month of August Button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button17_Click(object sender, EventArgs e)
        {
            CurrentMonth("August");
        }
        /// <summary>
        ///  The month of September Button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button18_Click(object sender, EventArgs e)
        {
            CurrentMonth("September");
        }
        /// <summary>
        ///  The month of October
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button19_Click(object sender, EventArgs e)
        {
            CurrentMonth("October");
        }
        /// <summary>
        /// The Month of Novmember 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button20_Click(object sender, EventArgs e)
        {
            CurrentMonth("November");

        }
        /// <summary>
        ///  The Month of December
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button21_Click(object sender, EventArgs e)
        {
            CurrentMonth("December");
        }


        /// <summary>
        ///  Go to the next Tab Button (tab 3)
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, EventArgs e)
        {
            flatTabControl2.SelectedIndex = (flatTabControl2.SelectedIndex + 1 < flatTabControl2.TabCount) ?
                            flatTabControl2.SelectedIndex + 1 : flatTabControl2.SelectedIndex;
        }
        /// <summary>
        ///  Numbers of the Month
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

         ////
         ///  Number 1 
         ///
        private void button22_Click(object sender, EventArgs e)
        {
            DateOfMonth("1");
        }
        /// <summary>
        ///  Number 2 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button23_Click(object sender, EventArgs e)
        {
            DateOfMonth("2");
        }
        /// <summary>
        ///  Number 3 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button24_Click(object sender, EventArgs e)
        {
            DateOfMonth("3");
        }
        /// <summary>
        ///  Number 4 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button25_Click(object sender, EventArgs e)
        {
            DateOfMonth("4");
        }
        /// <summary>
        ///   Number 5 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button26_Click(object sender, EventArgs e)
        {
            DateOfMonth("5");
        }
        /// <summary>
        ///  Number 6th
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button27_Click(object sender, EventArgs e)
        {
            DateOfMonth("6");
        }
        /// <summary>
        ///  Number 7tn 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button28_Click(object sender, EventArgs e)
        {
            DateOfMonth("7");
        }
        /// <summary>
        ///  number 8
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button35_Click(object sender, EventArgs e)
        {
            DateOfMonth("8");
        }
        /// <summary>
        /// Number 9
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button34_Click(object sender, EventArgs e)
        {
            DateOfMonth("9");
        }
        /// <summary>
        /// Number 10
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button33_Click(object sender, EventArgs e)
        {
            DateOfMonth("10");
        }
        /// <summary>
        ///  Number 11
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button53_Click(object sender, EventArgs e)
        {
            DateOfMonth("11");
        }
        /// <summary>
        ///  Number 12
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button32_Click(object sender, EventArgs e)
        {
            DateOfMonth("12");
        }

        private void button31_Click(object sender, EventArgs e)
        {
            DateOfMonth("13");
        }

        private void button30_Click(object sender, EventArgs e)
        {
            DateOfMonth("14");
        }
        //number 15
        private void button29_Click(object sender, EventArgs e)
        {
            DateOfMonth("15");
        }
        /// <summary>
        ///  Number 16 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button42_Click(object sender, EventArgs e)
        {
            DateOfMonth("16");
        }
        /// <summary>
        ///  Number 17
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button41_Click(object sender, EventArgs e)
        {
            DateOfMonth("17");
        }
        /// <summary>
        ///  Number 18
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button40_Click(object sender, EventArgs e)
        {
            DateOfMonth("18");
        }
        /// <summary>
        ///  Number 19
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button39_Click(object sender, EventArgs e)
        {
            DateOfMonth("19");
        }
        /// <summary>
        ///  Number 20
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button38_Click(object sender, EventArgs e)
        {
            DateOfMonth("20");
        }
        /// <summary>
        ///  Number 21 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button37_Click(object sender, EventArgs e)
        {
            DateOfMonth("21");
        }
         /// <summary>
         ///  Number 22 
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
        private void button36_Click(object sender, EventArgs e)
        {
            DateOfMonth("22");
        }
        /// <summary>
        ///  Number 23 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button49_Click(object sender, EventArgs e)
        {
            DateOfMonth("23");
        }
        /// <summary>
        ///  Number 24 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button48_Click(object sender, EventArgs e)
        {
            DateOfMonth("24");
        }
        /// <summary>
        ///  Number 25  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button47_Click(object sender, EventArgs e)
        {
            DateOfMonth("25");
        }
        /// <summary>
        ///  Number 26
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button46_Click(object sender, EventArgs e)
        {
            DateOfMonth("26");
        }
        /// <summary>
        /// Number 27 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button45_Click(object sender, EventArgs e)
        {
            DateOfMonth("27");
        }
        /// <summary>
        /// Number 28
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button44_Click(object sender, EventArgs e)
        {
            DateOfMonth("28");
        }
        /// <summary>
        /// 
        /// Number 29
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button43_Click(object sender, EventArgs e)
        {
            DateOfMonth("29");
        }
        /// <summary>
        /// Number 30
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button50_Click(object sender, EventArgs e)
        {
            DateOfMonth("30");
        }
        /// <summary>
        ///  nUmber 31
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button51_Click(object sender, EventArgs e)
        {
            DateOfMonth("31");
        }
        /// <summary>
        ///  WEATHER  SECTION 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        //// SUNNY WEATHER 
        private void button54_Click(object sender, EventArgs e)
        {
            CurrentWeather("Sunny");
        }
        /// <summary>
        ///  WEATHER - RANINY
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button55_Click(object sender, EventArgs e)
        {
            CurrentWeather("Rainy");
        }
        /// <summary>
        ///  SNOWY WEATHER 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button56_Click(object sender, EventArgs e)
        {
            CurrentWeather("Snowy");
        }
        /// <summary>
        /// WEATHER - CLOUDY
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button57_Click(object sender, EventArgs e)
        {
            CurrentWeather("cloudy");
        }
        /// <summary>
        /// WEATHER - FOGGY
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button58_Click(object sender, EventArgs e)
        {
            CurrentWeather("Foggy");
        }
        
        /// <summary>
        ///  WEATHER - BREEZY 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button59_Click(object sender, EventArgs e)
        {
            CurrentWeather("Breezy");
        }
        /// <summary>
        /// TEMPATURE  BUTTONS 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button61_Click(object sender, EventArgs e)
        {
            CurrentTemp("Cold");
        }

        /// <summary>
        ///   Temperature  Cool
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button62_Click(object sender, EventArgs e)
        {
            CurrentTemp("Cool");
        }
        /// <summary>
        /// Temperature  Hot 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button63_Click(object sender, EventArgs e)
        {
            CurrentTemp("Hot");
        }
        /// <summary>
        ///   Temperature - Warm 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button64_Click(object sender, EventArgs e)
        {
            CurrentTemp("Warm");
        }
        private void button52_Click(object sender, EventArgs e)
        {
            flatTabControl2.SelectedIndex = (flatTabControl2.SelectedIndex + 1 < flatTabControl2.TabCount) ?
                           flatTabControl2.SelectedIndex + 1 : flatTabControl2.SelectedIndex;
        }

        private void button60_Click(object sender, EventArgs e)
        {
            flatTabControl2.SelectedIndex = (flatTabControl2.SelectedIndex + 1 < flatTabControl2.TabCount) ?
                           flatTabControl2.SelectedIndex + 1 : flatTabControl2.SelectedIndex;
        }

        private void button65_Click(object sender, EventArgs e)
        {
            Display_Date.Visible = true;
            flatTabControl2.SelectedIndex = (flatTabControl2.SelectedIndex + 1 < flatTabControl2.TabCount) ?
                           flatTabControl2.SelectedIndex + 1 : flatTabControl2.SelectedIndex;
            //Display_Date.Text = Q_Day;
           // Display_Month.Text = Q_Month;
           // Display_Date.Text = Q_Date;
            DateTime myDateTime = DateTime.Now;
            string year = myDateTime.Year.ToString();
            // Display_Year.Text = year;
            Display_Date.Text = Q_Day + ", " + Q_Month + " " + Q_Date + ", " + year;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                //tabPage3
                flatTabControl1.TabPages.Remove(tabPage3);
            }
           if (!checkBox1.Checked)
            {
                //flatTabControl1.TabPages.Add(3,TabPage3);
                flatTabControl1.TabPages.Add(tabPage3);
            }

        }
    }
    }
