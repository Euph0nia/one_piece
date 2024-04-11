using System.Diagnostics;
using System.Windows.Forms;
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

        //private List<long> reactionTimes = new List<long>();
        private List<Button> h_buttons = new List<Button>();
        private List<Button> r_buttons = new List<Button>();
        private List<Button> all_buttons = new List<Button>();

        public Keyboard()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            SetupKey();
            textBox.AppendText($"Вас приветствует тест определяющий реакцию пользователя на события от клавиатуры.\r\n" +
                                $"В тесте представлено 10 событий, после чего подводится итог. \r\n");

            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i] is Button)
                {
                    Button button = (Button)Controls[i];
                    all_buttons.Add(button); 

                    if (button.Top < 50) 
                    {
                        h_buttons.Add(button);
                    }
                    else 
                    {
                        r_buttons.Add(button);
                    }
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
            textBox.AppendText($"\n\nТест завершен, среднее время реакции {avgTime / clickCount} мс.\r\n");
            for (int i = 0; i < 20; i++)
            {
                all_buttons[i].BackColor = DefaultBackColor;
            }
            _timer.Stop();
            //timer.Stop();
            stopwatch.Stop();
        }

        private void Keyboard_KeyDown(object sender, KeyEventArgs e)
        {
            if (clickCount == 10)
            {
                stopTest();
            }
            else if (e.KeyCode == Keys.Enter && this.ContainsFocus)
            {
                Application.Restart();
            }
            else if ((e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) ||
                     (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9))
            {
                int keyValue;
                if (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
                {
                    keyValue = (int)e.KeyCode - (int)Keys.NumPad0;
                }
                else
                {
                    keyValue = (int)e.KeyCode - (int)Keys.D0;
                }

                if (stopwatch != null && stopwatch.IsRunning)
                {
                    if (button != null && Int32.Parse(button.Text) != keyValue)
                    {
                        MessageBox.Show($"Неверно нажатая клавиша! Ожидалась кнопка {button.Text}, нажата кнопка {(char)(keyValue + '0')}.\r\n");
                    }
                    else
                    {
                        stopwatch.Stop();
                        button.BackColor = DefaultBackColor;
                        textBox.AppendText($"Время нажатия: {stopwatch.ElapsedMilliseconds} мс. \r\n");
                        clickCount++;
                        avgTime += stopwatch.ElapsedMilliseconds;
                    }
                }
                _timer.Interval = new Random().Next(2000, 3000);
                _timer.Start();
            }
        }



        private void timer_Tick(object sender, EventArgs e)
        {
            _timer.Stop();
            if (button != null) { button.BackColor = DefaultBackColor; }
            Random random = new Random();

            button = all_buttons[random.Next(all_buttons.Count)];
            keyCode = 48 + Int32.Parse(button.Text);
            button.BackColor = Color.BlueViolet;



            stopwatch = Stopwatch.StartNew();
        }
    }
}