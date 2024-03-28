namespace _1lab_IntSys
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // �������� ��� ������� �������
            int keyValue = -1;
            switch (e.KeyCode)
            {
                case Keys.D0:
                    keyValue = 0;
                    break;
                case Keys.D1:
                    keyValue = 1;
                    break;
                case Keys.D2:
                    keyValue = 2;
                    break;
                case Keys.D3:
                    keyValue = 3;
                    break;
                case Keys.D4:
                    keyValue = 4;
                    break;
                case Keys.D5:
                    keyValue = 5;
                    break;
                case Keys.D6:
                    keyValue = 6;
                    break;
                case Keys.D7:
                    keyValue = 7;
                    break;
                case Keys.D8:
                    keyValue = 8;
                    break;
                case Keys.D9:
                    keyValue = 9;
                    break;
            }

            // ���� ������ �������� ������� �� 0 �� 9
            if (keyValue >= 0 && keyValue <= 9)
            {
                // �������� ���� ��������������� ������
                ChangeButtonColor(keyValue);
            }
        }

        private void ChangeButtonColor(int buttonNumber)
        {
            // ������� ������ � ��������������� �������
            string buttonName = "button" + buttonNumber;
            Button targetButton = Controls.Find(buttonName, true)[0] as Button;

            // ������ ���� ������ �� ������
            targetButton.BackColor = Color.Black;
        }

    }
}