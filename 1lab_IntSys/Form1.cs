using System.Diagnostics;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace _1lab_IntSys
{
    public partial class Keyboard : Form
    {
        private int keyCode;
        private Button button;
        private System.Windows.Forms.Timer _timer;
        private Stopwatch stopwatch;
        private int clickCount = 0;
        private float avgTime;

        private List<long> reactionTimes = new List<long>();
        private List<Button> up_buttons = new List<Button>();
        private List<Button> r_buttons = new List<Button>();
        private List<Button> all_buttons = new List<Button>();


        public Keyboard()
        {
            InitializeComponent();
            SetupKey();
            textBox.AppendText($"Вас приветствует тест определяющий реакцию пользователя на события от клавиатуры.\r\n" +
                                $"В тесте представлено 10 событий, после чего подводится итог. \r\n");
            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i] is Button)
                {
                    all_buttons.Add((Button)Controls[i]);
                }
            }

            SetupTimer();
        }

        private void SetupKey()
        {
            this.KeyPreview = true;
            this.KeyDown += Keyboard_KeyDown;
        }

        private void SetupTimer()
        {
            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = new Random().Next(2000, 3000);
            _timer.Tick += timer_Tick;
            _timer.Start();
        }

        private void stopTest()
        {
            textBox.AppendText($"\n\nТест завершен, среднее время реакции {avgTime}\r\n");
            
        }

        private void Keyboard_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == keyCode)
            {
                if (stopwatch != null && stopwatch.IsRunning)
                {
                    stopwatch.Stop();
                    button.BackColor = DefaultBackColor;

                    reactionTimes.Add(stopwatch.ElapsedMilliseconds);
                    avgTime =+ stopwatch.ElapsedMilliseconds;
                    textBox.AppendText($"Время нажатия: {stopwatch.ElapsedMilliseconds} мс. \r\n");
                    clickCount++;
                }
                if (clickCount == 10) { stopTest(); }
                _timer.Interval = new Random().Next(2000, 3000);
                _timer.Start();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            Random random = new Random();

            button = all_buttons[random.Next(all_buttons.Count)];
            keyCode = 48 + Int32.Parse(button.Text);
            button.BackColor = Color.BlueViolet;
            

            stopwatch = Stopwatch.StartNew();
        }
    }
}