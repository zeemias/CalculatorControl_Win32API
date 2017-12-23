using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinApi
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindow(IntPtr hWnd, int uCmd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, [Out] StringBuilder lParam);

		//Перемещение курсора
        const int WM_MOUSEMOVE = 0x0200;
        //Нажатие левой кнопки мыши
        const int WM_LBUTTONDOWN = 0x0201;
        //Отпускание левой кнопки мыши
        const int WM_LBUTTONUP = 0x0202;
        //Найденный дескриптор идентифицирует окно того же самого типа, которое является самым высоким в Z - последовательности.
        const int GW_HWNDFIRST = 0;
        //Найденный дескриптор идентифицирует окно ниже определяемого окна в Z - последовательности.
        const int GW_HWNDNEXT = 2;
        //Извлеченный дескриптор идентифицирует дочернее окно наверху Z - последовательности.
        const int GW_CHILD = 5;
        //Получение текста из окна
        const int WM_GETTEXT = 0x000D;

        //Массив дескрипторов кнопок
        public IntPtr[] buttons = new IntPtr[28];
        //Дескриптор окна с ответом
        IntPtr dialogFrame = new IntPtr();

        public Form1()
        {
            InitializeComponent();
			//Находим дескриптор главного окна калькулятора по нозванию окна          
            IntPtr calcWnd = FindWindow(null, "Калькулятор");
            //Пока окно не найдено, выводим сообщение об ошибке, когда найдет, продолжит работу 
            while (calcWnd.ToInt32() == 0)
            {
            	//Сообщение об ошибке
                MessageBox.Show("Калькулятор закрыт! Откройте калькулятор и нажмите Ок!");
                //Поиск дескриптора
                calcWnd = FindWindow(null, "Калькулятор");
            }
            //Получаем дескриптор дочернего окна калькулятора
            IntPtr calcChild = GetWindow(calcWnd, GW_CHILD);
            //Получаем дескриптор следующего дочернего окна
            IntPtr frameChild = GetWindow(calcChild, GW_CHILD);
            //Получаем дескриптор следующего окна
            IntPtr nextChild = GetWindow(frameChild, GW_HWNDNEXT);
            //Получаем дескриптор следующего окна, в котором содержутся кнопки
            IntPtr buttonsFrame = GetWindow(nextChild, GW_HWNDNEXT);
            //Записываем кнопки калькулятора в массив дескрипторов
            buttons[0] = GetWindow(buttonsFrame, GW_CHILD);
            for(int i = 1; i < 28; i++)
            {
                buttons[i] = GetWindow(buttons[i-1], GW_HWNDNEXT);
            }
            IntPtr firstFrame = GetWindow(nextChild, GW_CHILD);
            IntPtr secondFrame = GetWindow(firstFrame, GW_HWNDNEXT);
            //Получаем дескриптор окна с ответом
            dialogFrame = GetWindow(secondFrame, GW_HWNDNEXT);
            //Получаем текст из окна с ответом
            showDialog(dialogFrame);
        }

        //Функция для получения текста из окна с ответом (принимает дискриптор окна с ответом)
        public void showDialog(IntPtr dialog)
        {
        	//Создаем строку для ответа
            StringBuilder title = new StringBuilder();
            //Получаем текст из окна с ответом
            SendMessage(dialog, WM_GETTEXT, (IntPtr)20, title);
            //Выводим текст в наше приложение
            textBox1.Text = title.ToString();
        }

        //Функция для нажатия на кнопку (принимает дискриптор кнопки)
        public void butClick(IntPtr but)
        {
        	//Наводим курсор на кнопку
            SendMessage(but, WM_MOUSEMOVE, 0, 0);
            //Нажимаем на левую кнопку мыши
            SendMessage(but, WM_LBUTTONDOWN, 0, 0);
            //Отпускаем левую кнопку мыши
            SendMessage(but, WM_LBUTTONUP, 0, 0);
        }

        private void prc_Click(object sender, EventArgs e)
        {
        	//Нажимаем на кнопку
            butClick(buttons[25]);
            //Получаем ответ
            showDialog(dialogFrame);
        }

        private void korn_Click(object sender, EventArgs e)
        {
            butClick(buttons[24]);
            showDialog(dialogFrame);
        }

        private void delx_Click(object sender, EventArgs e)
        {
            butClick(buttons[26]);
            showDialog(dialogFrame);
        }

        private void ce_Click(object sender, EventArgs e)
        {
            butClick(buttons[7]);
            showDialog(dialogFrame);
        }

        private void c_Click(object sender, EventArgs e)
        {
            butClick(buttons[12]);
            showDialog(dialogFrame);
        }

        private void del_Click(object sender, EventArgs e)
        {
            butClick(buttons[1]);
            showDialog(dialogFrame);
        }

        private void delenie_Click(object sender, EventArgs e)
        {
            butClick(buttons[19]);
            showDialog(dialogFrame);
        }

        private void sem_Click(object sender, EventArgs e)
        {
            butClick(buttons[2]);
            showDialog(dialogFrame);
        }

        private void vosem_Click(object sender, EventArgs e)
        {
            butClick(buttons[8]);
            showDialog(dialogFrame);
        }

        private void devat_Click(object sender, EventArgs e)
        {
            butClick(buttons[13]);
            showDialog(dialogFrame);
        }

        private void ymnzh_Click(object sender, EventArgs e)
        {
            butClick(buttons[20]);
            showDialog(dialogFrame);
        }

        private void chetiri_Click(object sender, EventArgs e)
        {
            butClick(buttons[3]);
            showDialog(dialogFrame);
        }

        private void pyat_Click(object sender, EventArgs e)
        {
            butClick(buttons[9]);
            showDialog(dialogFrame);
        }

        private void shest_Click(object sender, EventArgs e)
        {
            butClick(buttons[14]);
            showDialog(dialogFrame);
        }

        private void minus_Click(object sender, EventArgs e)
        {
            butClick(buttons[21]);
            showDialog(dialogFrame);
        }

        private void odin_Click(object sender, EventArgs e)
        {
            butClick(buttons[4]);
            showDialog(dialogFrame);
        }

        private void dva_Click(object sender, EventArgs e)
        {
            butClick(buttons[10]);
            showDialog(dialogFrame);
        }

        private void tri_Click(object sender, EventArgs e)
        {
            butClick(buttons[15]);
            showDialog(dialogFrame);
        }

        private void plus_Click(object sender, EventArgs e)
        {
            butClick(buttons[22]);
            showDialog(dialogFrame);
        }

        private void plusminus_Click(object sender, EventArgs e)
        {
            butClick(buttons[18]);
            showDialog(dialogFrame);
        }

        private void nol_Click(object sender, EventArgs e)
        {
            butClick(buttons[5]);
            showDialog(dialogFrame);
        }

        private void zap_Click(object sender, EventArgs e)
        {
            butClick(buttons[16]);
            showDialog(dialogFrame);
        }

        private void rovn_Click(object sender, EventArgs e)
        {
            butClick(buttons[27]);
            showDialog(dialogFrame);
        }

        private void mc_Click(object sender, EventArgs e)
        {
            butClick(buttons[0]);
            showDialog(dialogFrame);
        }

        private void mr_Click(object sender, EventArgs e)
        {
            butClick(buttons[6]);
            showDialog(dialogFrame);
        }

        private void ms_Click(object sender, EventArgs e)
        {
            butClick(buttons[11]);
            showDialog(dialogFrame);
        }

        private void mp_Click(object sender, EventArgs e)
        {
            butClick(buttons[17]);
            showDialog(dialogFrame);
        }

        private void mm_Click(object sender, EventArgs e)
        {
            butClick(buttons[23]);
            showDialog(dialogFrame);
        }
    }
}
