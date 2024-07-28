using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembler
{
    public partial class Form1 : Form
    {
        Mano mano = new Mano();
        int pc;
        int line = 1;
        public static char[] AddBinaryArrays(char[] a, char[] b)
        {
            int length = Math.Max(a.Length, b.Length);
            char[] result = new char[length + 1];

            int carry = 0;
            int aIndex = a.Length - 1;
            int bIndex = b.Length - 1;
            int resultIndex = length;

            while (aIndex >= 0 || bIndex >= 0 || carry > 0)
            {
                int bitA = aIndex >= 0 ? a[aIndex] - '0' : 0;
                int bitB = bIndex >= 0 ? b[bIndex] - '0' : 0;

                int sum = bitA + bitB + carry;
                result[resultIndex] = (char)((sum % 2) + '0');
                carry = sum / 2;

                aIndex--;
                bIndex--;
                resultIndex--;
            }

            // Remove leading zero if there is one
            if (result[0] == '0')
            {
                char[] trimmedResult = new char[result.Length - 1];
                Array.Copy(result, 1, trimmedResult, 0, trimmedResult.Length);
                return trimmedResult;
            }

            return result;
        }
        public Form1()
        {
            InitializeComponent();
            customizeDesign();
        }
        //design
        private void customizeDesign()
        {
            subBtnPc.Visible = false;
            subBtnIR.Visible = false;
            subBtnNZP.Visible = false;
            subBtnR0.Visible = false;
            subBtnR1.Visible = false;
            subBtnR2.Visible = false;
            subBtnR3.Visible = false;
            subBtnR4.Visible = false;
            subBtnR5.Visible = false;
            subBtnR6.Visible = false;
            subBtnR7.Visible = false;
        }
        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }
        private void btnPC_Click(object sender, EventArgs e)
        {
            showSubMenu(subBtnPc);
        }

        private void btnIR_Click(object sender, EventArgs e)
        {
            showSubMenu(subBtnIR);
        }

        private void btnNZP_Click(object sender, EventArgs e)
        {
            showSubMenu(subBtnNZP);
        }

        private void btnR_Click(object sender, EventArgs e)
        {
            showSubMenu(subBtnR0);
            showSubMenu(subBtnR1);
            showSubMenu(subBtnR2);
            showSubMenu(subBtnR3);
            showSubMenu(subBtnR4);
            showSubMenu(subBtnR5);
            showSubMenu(subBtnR6);
            showSubMenu(subBtnR7);
        }


        //code
        private void button1_Click(object sender, EventArgs e)
        {
            ////
            button2.Enabled = true;
            AlertText.Text = "";
            line = 1;
            string input = richTextBox1.Text;
            string[] lineOrder = input.Split('\n');
            ///
            string fileName = "MEMORY.txt";
            string projectPath = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(projectPath, fileName);
            var map = new Dictionary<string, string>();
            string temp = "";

            for (int i = 0; i < lineOrder.Length; i++)
            {
                string[] instruct = lineOrder[i].Split(' ');
                if (instruct[0] == "ORG")
                {
                    string number = instruct[1];
                    if (number[0] == 'x')
                    {
                        number = number.Replace("x", "");
                        int decima = Convert.ToInt32(number, 16);
                        mano.pc = decima;
                        pc = mano.pc;
                    }
                }
                else if (instruct[0].Contains(','))
                {
                    instruct[0] = instruct[0].Replace(",", "");
                    string binary = Convert.ToString(i - 1, 2);
                    map.Add(instruct[0], binary);
                    int index = lineOrder[i].IndexOf(',');
                    lineOrder[i] = lineOrder[i].Substring(index + 2);
                }
            }

            for (int i = 0; i < lineOrder.Length; i++)
            {
                char[] reg = new char[16];
                string[] instruct = lineOrder[i].Split(' ');
                int t = 0;
                if (instruct[0] == "ORG")
                {
                    continue;
                }
                else if (instruct[0] == "ADD")
                {
                    string[] varible = instruct[1].Split(',');

                    reg[t] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '1';
                    for (int j = 0; j < varible.Length; j++)
                    {
                        if (j == 2)
                        {
                            reg[t += 1] = '0';
                            reg[t += 1] = '0';
                            reg[t += 1] = '0';

                        }

                        if (varible[j] == "R0")
                        {
                            reg[t += 1] = '0';
                            reg[t += 1] = '0';
                            reg[t += 1] = '0';
                        }
                        else if (varible[j] == "R1")
                        {
                            reg[t += 1] = '0';
                            reg[t += 1] = '0';
                            reg[t += 1] = '1';
                        }
                        else if (varible[j] == "R2")
                        {
                            reg[t += 1] = '0';
                            reg[t += 1] = '1';
                            reg[t += 1] = '0';
                        }
                        else if (varible[j] == "R3")
                        {
                            reg[t += 1] = '0';
                            reg[t += 1] = '1';
                            reg[t += 1] = '1';
                        }
                        else if (varible[j] == "R4")
                        {
                            reg[t += 1] = '1';
                            reg[t += 1] = '0';
                            reg[t += 1] = '0';
                        }
                        else if (varible[j] == "R5")
                        {
                            reg[t += 1] = '1';
                            reg[t += 1] = '0';
                            reg[t += 1] = '1';
                        }
                        else if (varible[j] == "R6")
                        {
                            reg[t += 1] = '1';
                            reg[t += 1] = '1';
                            reg[t += 1] = '0';
                        }
                        else if (varible[j] == "R7")
                        {
                            reg[t += 1] = '1';
                            reg[t += 1] = '1';
                            reg[t += 1] = '1';
                        }
                        else
                        {
                            reg[10] = '1';
                            t = 16;
                            if (map.ContainsKey(varible[j]))
                            {
                                string key = map[varible[j]];
                                char[] value = new char[5];
                                for (int w = 0; w < 5; w++)
                                {
                                    if (w < key.Length)
                                    {
                                        value[4 - w] = key[key.Length - w - 1];
                                    }
                                    else
                                    {
                                        value[4 - w] = '0';
                                    }
                                }
                                for (int w = 4; w >= 0; w--)
                                {
                                    reg[t -= 1] = value[w];
                                }
                            }
                            else
                            {
                                string number = varible[j];
                                if (number[0] == 'x')
                                {
                                    number = number.Replace("x", "");
                                    int decima = Convert.ToInt32(number, 16);
                                    string binary = Convert.ToString(decima, 2);
                                    char[] value = new char[5];
                                    for (int w = 0; w < 5; w++)
                                    {
                                        if (w < binary.Length)
                                        {
                                            value[4 - w] = binary[binary.Length - w - 1];
                                        }
                                        else
                                        {
                                            value[4 - w] = '0';
                                        }
                                    }
                                    for (int w = 4; w >= 0; w--)
                                    {
                                        reg[t -= 1] = value[w];
                                    }

                                }
                                else if (number[0] == '#')
                                {
                                    number = number.Replace("#", "");
                                    int decima = int.Parse(number);
                                    string binary = Convert.ToString(decima, 2);
                                    char[] value = new char[5];
                                    for (int w = 0; w < 5; w++)
                                    {
                                        if (w < binary.Length)
                                        {
                                            value[4 - w] = binary[binary.Length - w - 1];
                                        }
                                        else
                                        {
                                            if (!number.Contains('-'))
                                            {
                                                value[4 - w] = '0';
                                            }

                                        }
                                    }
                                    for (int w = 4; w >= 0; w--)
                                    {
                                        reg[t -= 1] = value[w];
                                    }
                                }
                                else if (number[0] == 'b')
                                {
                                    number = number.Replace("b", "");
                                    char[] value = new char[5];
                                    for (int w = 0; w < 5; w++)
                                    {
                                        if (w < number.Length)
                                        {
                                            value[4 - w] = number[number.Length - w - 1];
                                        }
                                        else
                                        {
                                            value[4 - w] = number[0];
                                        }
                                    }
                                    for (int w = 4; w >= 0; w--)
                                    {
                                        reg[t -= 1] = value[w];
                                    }
                                }
                            }


                        }
                    }
                }
                else if (instruct[0] == "AND")
                {
                    string[] varible = instruct[1].Split(',');

                    reg[t] = '0';
                    reg[t += 1] = '1';
                    reg[t += 1] = '0';
                    reg[t += 1] = '1';
                    for (int j = 0; j < varible.Length; j++)
                    {
                        if (j == 2)
                        {
                            reg[t += 1] = '0';
                            reg[t += 1] = '0';
                            reg[t += 1] = '0';

                        }

                        if (varible[j] == "R0")
                        {
                            reg[t += 1] = '0';
                            reg[t += 1] = '0';
                            reg[t += 1] = '0';
                        }
                        else if (varible[j] == "R1")
                        {
                            reg[t += 1] = '0';
                            reg[t += 1] = '0';
                            reg[t += 1] = '1';
                        }
                        else if (varible[j] == "R2")
                        {
                            reg[t += 1] = '0';
                            reg[t += 1] = '1';
                            reg[t += 1] = '0';
                        }
                        else if (varible[j] == "R3")
                        {
                            reg[t += 1] = '0';
                            reg[t += 1] = '1';
                            reg[t += 1] = '1';
                        }
                        else if (varible[j] == "R4")
                        {
                            reg[t += 1] = '1';
                            reg[t += 1] = '0';
                            reg[t += 1] = '0';
                        }
                        else if (varible[j] == "R5")
                        {
                            reg[t += 1] = '1';
                            reg[t += 1] = '0';
                            reg[t += 1] = '1';
                        }
                        else if (varible[j] == "R6")
                        {
                            reg[t += 1] = '1';
                            reg[t += 1] = '1';
                            reg[t += 1] = '0';
                        }
                        else if (varible[j] == "R7")
                        {
                            reg[t += 1] = '1';
                            reg[t += 1] = '1';
                            reg[t += 1] = '1';
                        }
                        else
                        {
                            reg[10] = '1';
                            t = 16;
                            if (map.ContainsKey(varible[j]))
                            {
                                string key = map[varible[j]];
                                char[] value = new char[5];
                                for (int w = 0; w < 5; w++)
                                {
                                    if (w < key.Length)
                                    {
                                        value[4 - w] = key[key.Length - w - 1];
                                    }
                                    else
                                    {
                                        value[4 - w] = '0';
                                    }
                                }
                                for (int w = 4; w >= 0; w--)
                                {
                                    reg[t -= 1] = value[w];
                                }
                            }
                            else
                            {
                                string number = varible[j];
                                if (number[0] == 'x')
                                {
                                    number = number.Replace("x", "");
                                    int decima = Convert.ToInt32(number, 16);
                                    string binary = Convert.ToString(decima, 2);
                                    char[] value = new char[5];
                                    for (int w = 0; w < 5; w++)
                                    {
                                        if (w < binary.Length)
                                        {
                                            value[4 - w] = binary[binary.Length - w - 1];
                                        }
                                        else
                                        {
                                            value[4 - w] = '0';
                                        }
                                    }
                                    for (int w = 4; w >= 0; w--)
                                    {
                                        reg[t -= 1] = value[w];
                                    }

                                }
                                else if (number[0] == '#')
                                {
                                    number = number.Replace("#", "");
                                    int decima = int.Parse(number);
                                    string binary = Convert.ToString(decima, 2);
                                    char[] value = new char[5];
                                    for (int w = 0; w < 5; w++)
                                    {
                                        if (w < binary.Length)
                                        {
                                            value[4 - w] = binary[binary.Length - w - 1];
                                        }
                                        else
                                        {
                                            if (!number.Contains('-'))
                                            {
                                                value[4 - w] = '0';
                                            }
                                        }
                                    }
                                    for (int w = 4; w >= 0; w--)
                                    {
                                        reg[t -= 1] = value[w];
                                    }
                                }
                                else if (number[0] == 'b')
                                {
                                    number = number.Replace("b", "");
                                    char[] value = new char[5];
                                    for (int w = 0; w < 5; w++)
                                    {
                                        if (w < number.Length)
                                        {
                                            value[4 - w] = number[number.Length - w - 1];
                                        }
                                        else
                                        {
                                            value[4 - w] = number[0];
                                        }
                                    }
                                    for (int w = 4; w >= 0; w--)
                                    {
                                        reg[t -= 1] = value[w];
                                    }
                                }
                            }



                        }
                    }
                }
                else if (instruct[0] == "NOT")
                {
                    string[] varible = instruct[1].Split(',');

                    reg[t] = '1';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '1';
                    for (int j = 0; j < varible.Length; j++)
                    {

                        if (varible[j] == "R0")
                        {
                            reg[t += 1] = '0';
                            reg[t += 1] = '0';
                            reg[t += 1] = '0';
                        }
                        else if (varible[j] == "R1")
                        {
                            reg[t += 1] = '0';
                            reg[t += 1] = '0';
                            reg[t += 1] = '1';
                        }
                        else if (varible[j] == "R2")
                        {
                            reg[t += 1] = '0';
                            reg[t += 1] = '1';
                            reg[t += 1] = '0';
                        }
                        else if (varible[j] == "R3")
                        {
                            reg[t += 1] = '0';
                            reg[t += 1] = '1';
                            reg[t += 1] = '1';
                        }
                        else if (varible[j] == "R4")
                        {
                            reg[t += 1] = '1';
                            reg[t += 1] = '0';
                            reg[t += 1] = '0';
                        }
                        else if (varible[j] == "R5")
                        {
                            reg[t += 1] = '1';
                            reg[t += 1] = '0';
                            reg[t += 1] = '1';
                        }
                        else if (varible[j] == "R6")
                        {
                            reg[t += 1] = '1';
                            reg[t += 1] = '1';
                            reg[t += 1] = '0';
                        }
                        else if (varible[j] == "R7")
                        {
                            reg[t += 1] = '1';
                            reg[t += 1] = '1';
                            reg[t += 1] = '1';
                        }
                    }
                    reg[11] = reg[12] = reg[13] = reg[14] = reg[15] = '1';
                }
                else if (instruct[0] == "LD")
                {
                    string[] varible = instruct[1].Split(',');

                    reg[t] = '0'; reg[t += 1] = '0'; reg[t += 1] = '1'; reg[t += 1] = '0';

                    if (varible[0] == "R0")
                    {
                        reg[t += 1] = '0';
                        reg[t += 1] = '0';
                        reg[t += 1] = '0';
                    }
                    else if (varible[0] == "R1")
                    {
                        reg[t += 1] = '0';
                        reg[t += 1] = '0';
                        reg[t += 1] = '1';
                    }
                    else if (varible[0] == "R2")
                    {
                        reg[t += 1] = '0';
                        reg[t += 1] = '1';
                        reg[t += 1] = '0';
                    }
                    else if (varible[0] == "R3")
                    {
                        reg[t += 1] = '0';
                        reg[t += 1] = '1';
                        reg[t += 1] = '1';
                    }
                    else if (varible[0] == "R4")
                    {
                        reg[t += 1] = '1';
                        reg[t += 1] = '0';
                        reg[t += 1] = '0';
                    }
                    else if (varible[0] == "R5")
                    {
                        reg[t += 1] = '1';
                        reg[t += 1] = '0';
                        reg[t += 1] = '1';
                    }
                    else if (varible[0] == "R6")
                    {
                        reg[t += 1] = '1';
                        reg[t += 1] = '1';
                        reg[t += 1] = '0';
                    }
                    else if (varible[0] == "R7")
                    {
                        reg[t += 1] = '1';
                        reg[t += 1] = '1';
                        reg[t += 1] = '1';
                    }

                    t = 16;
                    if (map.ContainsKey(varible[1]))
                    {
                        string key = map[varible[1]];
                        int decima = Convert.ToInt32(key, 2);
                        string PcOffset = Convert.ToString(decima, 2);
                        char[] value = new char[9];
                        for (int w = 0; w <= 8; w++)
                        {
                            if (w < PcOffset.Length)
                            {
                                value[8 - w] = PcOffset[PcOffset.Length - w - 1];
                            }
                            else
                            {
                                value[8 - w] = '0';
                            }
                        }
                        for (int w = 8; w >= 0; w--)
                        {
                            reg[t -= 1] = value[w];
                        }
                    }
                    else
                    {
                        string number = varible[1];
                        if (number[0] == 'x')
                        {
                            number = number.Replace("x", "");
                            int decima = Convert.ToInt32(number, 16);
                            string binary = Convert.ToString(decima, 2);
                            char[] value = new char[9];
                            for (int w = 0; w < 9; w++)
                            {
                                if (w < binary.Length)
                                {
                                    value[8 - w] = binary[binary.Length - w - 1];
                                }
                                else
                                {
                                    value[8 - w] = '0';
                                }
                            }
                            for (int w = 8; w >= 0; w--)
                            {
                                reg[t -= 1] = value[w];
                            }

                        }
                        else if (number[0] == '#')
                        {
                            number = number.Replace("#", "");
                            int decima = int.Parse(number);
                            string binary = Convert.ToString(decima, 2);
                            char[] value = new char[9];
                            for (int w = 0; w < 9; w++)
                            {
                                if (w < binary.Length)
                                {
                                    value[8 - w] = binary[binary.Length - w - 1];
                                }
                                else
                                {
                                    if (!number.Contains('-'))
                                    {
                                        value[8 - w] = '0';
                                    }
                                }
                            }
                            for (int w = 8; w >= 0; w--)
                            {
                                reg[t -= 1] = value[w];
                            }
                        }
                        else if (number[0] == 'b')
                        {
                            number = number.Replace("b", "");
                            char[] value = new char[9];
                            for (int w = 0; w < 9; w++)
                            {
                                if (w < number.Length)
                                {
                                    value[8 - w] = number[number.Length - w - 1];
                                }
                                else
                                {
                                    value[8 - w] = number[0];
                                }
                            }
                            for (int w = 8; w >= 0; w--)
                            {
                                reg[t -= 1] = value[w];
                            }
                        }
                    }
                }
                else if (instruct[0] == "LDI")
                {
                    string[] varible = instruct[1].Split(',');

                    reg[t] = '1'; reg[t += 1] = '0'; reg[t += 1] = '1'; reg[t += 1] = '0';

                    if (varible[0] == "R0")
                    {
                        reg[t += 1] = '0';
                        reg[t += 1] = '0';
                        reg[t += 1] = '0';
                    }
                    else if (varible[0] == "R1")
                    {
                        reg[t += 1] = '0';
                        reg[t += 1] = '0';
                        reg[t += 1] = '1';
                    }
                    else if (varible[0] == "R2")
                    {
                        reg[t += 1] = '0';
                        reg[t += 1] = '1';
                        reg[t += 1] = '0';
                    }
                    else if (varible[0] == "R3")
                    {
                        reg[t += 1] = '0';
                        reg[t += 1] = '1';
                        reg[t += 1] = '1';
                    }
                    else if (varible[0] == "R4")
                    {
                        reg[t += 1] = '1';
                        reg[t += 1] = '0';
                        reg[t += 1] = '0';
                    }
                    else if (varible[0] == "R5")
                    {
                        reg[t += 1] = '1';
                        reg[t += 1] = '0';
                        reg[t += 1] = '1';
                    }
                    else if (varible[0] == "R6")
                    {
                        reg[t += 1] = '1';
                        reg[t += 1] = '1';
                        reg[t += 1] = '0';
                    }
                    else if (varible[0] == "R7")
                    {
                        reg[t += 1] = '1';
                        reg[t += 1] = '1';
                        reg[t += 1] = '1';
                    }

                    t = 16;
                    if (map.ContainsKey(varible[1]))
                    {
                        string key = map[varible[1]];
                        int decima = Convert.ToInt32(key, 2);
                        string PcOffset = Convert.ToString(decima, 2);
                        char[] value = new char[9];
                        for (int w = 0; w <= 8; w++)
                        {
                            if (w < PcOffset.Length)
                            {
                                value[8 - w] = PcOffset[PcOffset.Length - w - 1];
                            }
                            else
                            {
                                value[8 - w] = '0';
                            }
                        }
                        for (int w = 8; w >= 0; w--)
                        {
                            reg[t -= 1] = value[w];
                        }
                    }
                    else
                    {
                        string number = varible[1];
                        if (number[0] == 'x')
                        {
                            number = number.Replace("x", "");
                            int decima = Convert.ToInt32(number, 16);
                            string binary = Convert.ToString(decima, 2);
                            char[] value = new char[9];
                            for (int w = 0; w < 9; w++)
                            {
                                if (w < binary.Length)
                                {
                                    value[8 - w] = binary[binary.Length - w - 1];
                                }
                                else
                                {
                                    value[8 - w] = '0';
                                }
                            }
                            for (int w = 8; w >= 0; w--)
                            {
                                reg[t -= 1] = value[w];
                            }

                        }
                        else if (number[0] == '#')
                        {
                            number = number.Replace("#", "");
                            int decima = int.Parse(number);
                            string binary = Convert.ToString(decima, 2);
                            char[] value = new char[9];
                            for (int w = 0; w < 9; w++)
                            {
                                if (w < binary.Length)
                                {
                                    value[8 - w] = binary[binary.Length - w - 1];
                                }
                                else
                                {
                                    if (!number.Contains('-'))
                                    {
                                        value[8 - w] = '0';
                                    }
                                }
                            }
                            for (int w = 8; w >= 0; w--)
                            {
                                reg[t -= 1] = value[w];
                            }
                        }
                        else if (number[0] == 'b')
                        {
                            number = number.Replace("b", "");
                            char[] value = new char[9];
                            for (int w = 0; w < 9; w++)
                            {
                                if (w < number.Length)
                                {
                                    value[8 - w] = number[number.Length - w - 1];
                                }
                                else
                                {
                                    value[8 - w] = number[0];
                                }
                            }
                            for (int w = 8; w >= 0; w--)
                            {
                                reg[t -= 1] = value[w];
                            }
                        }
                    }
                }
                else if (instruct[0] == "LDR")
                {
                    string[] varible = instruct[1].Split(',');

                    reg[t] = '0';
                    reg[t += 1] = '1';
                    reg[t += 1] = '1';
                    reg[t += 1] = '0';
                    for (int j = 0; j < 2; j++)
                    {
                        if (varible[j] == "R0")
                        {
                            reg[t += 1] = '0';
                            reg[t += 1] = '0';
                            reg[t += 1] = '0';
                        }
                        else if (varible[j] == "R1")
                        {
                            reg[t += 1] = '0';
                            reg[t += 1] = '0';
                            reg[t += 1] = '1';
                        }
                        else if (varible[j] == "R2")
                        {
                            reg[t += 1] = '0';
                            reg[t += 1] = '1';
                            reg[t += 1] = '0';
                        }
                        else if (varible[j] == "R3")
                        {
                            reg[t += 1] = '0';
                            reg[t += 1] = '1';
                            reg[t += 1] = '1';
                        }
                        else if (varible[j] == "R4")
                        {
                            reg[t += 1] = '1';
                            reg[t += 1] = '0';
                            reg[t += 1] = '0';
                        }
                        else if (varible[j] == "R5")
                        {
                            reg[t += 1] = '1';
                            reg[t += 1] = '0';
                            reg[t += 1] = '1';
                        }
                        else if (varible[j] == "R6")
                        {
                            reg[t += 1] = '1';
                            reg[t += 1] = '1';
                            reg[t += 1] = '0';
                        }
                        else if (varible[j] == "R7")
                        {
                            reg[t += 1] = '1';
                            reg[t += 1] = '1';
                            reg[t += 1] = '1';
                        }
                    }

                    t = 16;
                    if (map.ContainsKey(varible[2]))
                    {
                        string key = map[varible[2]];
                        char[] value = new char[6];
                        for (int w = 0; w < 6; w++)
                        {
                            if (w < key.Length)
                            {
                                value[5 - w] = key[key.Length - w - 1];
                            }
                            else
                            {
                                value[5 - w] = '0';
                            }
                        }
                        for (int w = 5; w >= 0; w--)
                        {
                            reg[t -= 1] = value[w];
                        }
                    }
                    else
                    {
                        string number = varible[2];
                        if (number[0] == 'x')
                        {
                            number = number.Replace("x", "");
                            int decima = Convert.ToInt32(number, 16);
                            string binary = Convert.ToString(decima, 2);
                            char[] value = new char[6];
                            for (int w = 0; w < 6; w++)
                            {
                                if (w < binary.Length)
                                {
                                    value[5 - w] = binary[binary.Length - w - 1];
                                }
                                else
                                {
                                    value[5 - w] = '0';
                                }
                            }
                            for (int w = 5; w >= 0; w--)
                            {
                                reg[t -= 1] = value[w];
                            }

                        }
                        else if (number[0] == '#')
                        {
                            number = number.Replace("#", "");
                            int decima = int.Parse(number);
                            string binary = Convert.ToString(decima, 2);
                            char[] value = new char[6];
                            for (int w = 0; w < 6; w++)
                            {
                                if (w < binary.Length)
                                {
                                    value[5 - w] = binary[binary.Length - w - 1];
                                }
                                else
                                {
                                    if (!number.Contains('-'))
                                    {
                                        value[5 - w] = '0';
                                    }

                                }
                            }
                            for (int w = 5; w >= 0; w--)
                            {
                                reg[t -= 1] = value[w];
                            }
                        }
                        else if (number[0] == 'b')
                        {
                            number = number.Replace("b", "");
                            char[] value = new char[6];
                            for (int w = 0; w < 6; w++)
                            {
                                if (w < number.Length)
                                {
                                    value[5 - w] = number[number.Length - w - 1];
                                }
                                else
                                {
                                    value[5 - w] = number[0];
                                }
                            }
                            for (int w = 5; w >= 0; w--)
                            {
                                reg[t -= 1] = value[w];
                            }
                        }
                    }
                }
                else if (instruct[0] == "LEA")
                {
                    string[] varible = instruct[1].Split(',');

                    reg[t] = '1';
                    reg[t += 1] = '1';
                    reg[t += 1] = '1';
                    reg[t += 1] = '0';
                    if (varible[0] == "R0")
                    {
                        reg[t += 1] = '0';
                        reg[t += 1] = '0';
                        reg[t += 1] = '0';
                    }
                    else if (varible[0] == "R1")
                    {
                        reg[t += 1] = '0';
                        reg[t += 1] = '0';
                        reg[t += 1] = '1';
                    }
                    else if (varible[0] == "R2")
                    {
                        reg[t += 1] = '0';
                        reg[t += 1] = '1';
                        reg[t += 1] = '0';
                    }
                    else if (varible[0] == "R3")
                    {
                        reg[t += 1] = '0';
                        reg[t += 1] = '1';
                        reg[t += 1] = '1';
                    }
                    else if (varible[0] == "R4")
                    {
                        reg[t += 1] = '1';
                        reg[t += 1] = '0';
                        reg[t += 1] = '0';
                    }
                    else if (varible[0] == "R5")
                    {
                        reg[t += 1] = '1';
                        reg[t += 1] = '0';
                        reg[t += 1] = '1';
                    }
                    else if (varible[0] == "R6")
                    {
                        reg[t += 1] = '1';
                        reg[t += 1] = '1';
                        reg[t += 1] = '0';
                    }
                    else if (varible[0] == "R7")
                    {
                        reg[t += 1] = '1';
                        reg[t += 1] = '1';
                        reg[t += 1] = '1';
                    }
                    t = 16;
                    if (map.ContainsKey(varible[1]))
                    {
                        string key = map[varible[1]];
                        int decima = Convert.ToInt32(key, 2);
                        string PcOffset = Convert.ToString(decima, 2);
                        char[] value = new char[9];
                        for (int w = 0; w <= 8; w++)
                        {
                            if (w < PcOffset.Length)
                            {
                                value[8 - w] = PcOffset[PcOffset.Length - w - 1];
                            }
                            else
                            {
                                value[8 - w] = '0';
                            }
                        }
                        for (int w = 8; w >= 0; w--)
                        {
                            reg[t -= 1] = value[w];
                        }
                    }
                    else
                    {
                        string number = varible[1];
                        if (number[0] == 'x')
                        {
                            number = number.Replace("x", "");
                            int decima = Convert.ToInt32(number, 16);
                            string binary = Convert.ToString(decima, 2);
                            char[] value = new char[9];
                            for (int w = 0; w < 9; w++)
                            {
                                if (w < binary.Length)
                                {
                                    value[8 - w] = binary[binary.Length - w - 1];
                                }
                                else
                                {
                                    value[8 - w] = '0';
                                }
                            }
                            for (int w = 8; w >= 0; w--)
                            {
                                reg[t -= 1] = value[w];
                            }

                        }
                        else if (number[0] == '#')
                        {
                            number = number.Replace("#", "");
                            int decima = int.Parse(number);
                            string binary = Convert.ToString(decima, 2);
                            char[] value = new char[9];
                            for (int w = 0; w < 9; w++)
                            {
                                if (w < binary.Length)
                                {
                                    value[8 - w] = binary[binary.Length - w - 1];
                                }
                                else
                                {
                                    if (!number.Contains('-'))
                                    {
                                        value[8 - w] = '0';
                                    }
                                }
                            }
                            for (int w = 8; w >= 0; w--)
                            {
                                reg[t -= 1] = value[w];
                            }
                        }
                        else if (number[0] == 'b')
                        {
                            number = number.Replace("b", "");
                            char[] value = new char[9];
                            for (int w = 0; w < 9; w++)
                            {
                                if (w < number.Length)
                                {
                                    value[8 - w] = number[number.Length - w - 1];
                                }
                                else
                                {
                                    value[8 - w] = number[0];
                                }
                            }
                            for (int w = 8; w >= 0; w--)
                            {
                                reg[t -= 1] = value[w];
                            }
                        }
                    }
                }
                else if (instruct[0] == "ST")
                {

                    string[] varible = instruct[1].Split(',');

                    reg[t] = '0'; reg[t += 1] = '0'; reg[t += 1] = '1'; reg[t += 1] = '1';

                    if (varible[0] == "R0")
                    {
                        reg[t += 1] = '0';
                        reg[t += 1] = '0';
                        reg[t += 1] = '0';
                    }
                    else if (varible[0] == "R1")
                    {
                        reg[t += 1] = '0';
                        reg[t += 1] = '0';
                        reg[t += 1] = '1';
                    }
                    else if (varible[0] == "R2")
                    {
                        reg[t += 1] = '0';
                        reg[t += 1] = '1';
                        reg[t += 1] = '0';
                    }
                    else if (varible[0] == "R3")
                    {
                        reg[t += 1] = '0';
                        reg[t += 1] = '1';
                        reg[t += 1] = '1';
                    }
                    else if (varible[0] == "R4")
                    {
                        reg[t += 1] = '1';
                        reg[t += 1] = '0';
                        reg[t += 1] = '0';
                    }
                    else if (varible[0] == "R5")
                    {
                        reg[t += 1] = '1';
                        reg[t += 1] = '0';
                        reg[t += 1] = '1';
                    }
                    else if (varible[0] == "R6")
                    {
                        reg[t += 1] = '1';
                        reg[t += 1] = '1';
                        reg[t += 1] = '0';
                    }
                    else if (varible[0] == "R7")
                    {
                        reg[t += 1] = '1';
                        reg[t += 1] = '1';
                        reg[t += 1] = '1';
                    }

                    t = 16;
                    if (map.ContainsKey(varible[1]))
                    {
                        string key = map[varible[1]];
                        int decima = Convert.ToInt32(key, 2);
                        string PcOffset = Convert.ToString(decima, 2);
                        char[] value = new char[9];
                        for (int w = 0; w <= 8; w++)
                        {
                            if (w < PcOffset.Length)
                            {
                                value[8 - w] = PcOffset[PcOffset.Length - w - 1];
                            }
                            else
                            {
                                value[8 - w] = '0';
                            }
                        }
                        for (int w = 8; w >= 0; w--)
                        {
                            reg[t -= 1] = value[w];
                        }
                    }
                    else
                    {
                        string number = varible[1];
                        if (number[0] == 'x')
                        {
                            number = number.Replace("x", "");
                            int decima = Convert.ToInt32(number, 16);
                            string binary = Convert.ToString(decima, 2);
                            char[] value = new char[9];
                            for (int w = 0; w < 9; w++)
                            {
                                if (w < binary.Length)
                                {
                                    value[8 - w] = binary[binary.Length - w - 1];
                                }
                                else
                                {
                                    value[8 - w] = '0';
                                }
                            }
                            for (int w = 8; w >= 0; w--)
                            {
                                reg[t -= 1] = value[w];
                            }

                        }
                        else if (number[0] == '#')
                        {
                            number = number.Replace("#", "");
                            int decima = int.Parse(number);
                            string binary = Convert.ToString(decima, 2);
                            char[] value = new char[9];
                            for (int w = 0; w < 9; w++)
                            {
                                if (w < binary.Length)
                                {
                                    value[8 - w] = binary[binary.Length - w - 1];
                                }
                                else
                                {
                                    if (!number.Contains('-'))
                                    {
                                        value[4 - w] = '0';
                                    }
                                }
                            }
                            for (int w = 8; w >= 0; w--)
                            {
                                reg[t -= 1] = value[w];
                            }
                        }
                        else if (number[0] == 'b')
                        {
                            number = number.Replace("b", "");
                            char[] value = new char[9];
                            for (int w = 0; w < 9; w++)
                            {
                                if (w < number.Length)
                                {
                                    value[8 - w] = number[number.Length - w - 1];
                                }
                                else
                                {
                                    value[8 - w] = number[0];
                                }
                            }
                            for (int w = 8; w >= 0; w--)
                            {
                                reg[t -= 1] = value[w];
                            }
                        }
                    }
                }
                else if (instruct[0] == "STI")
                {

                    string[] varible = instruct[1].Split(',');

                    reg[t] = '1'; reg[t += 1] = '0'; reg[t += 1] = '1'; reg[t += 1] = '1';

                    if (varible[0] == "R0")
                    {
                        reg[t += 1] = '0';
                        reg[t += 1] = '0';
                        reg[t += 1] = '0';
                    }
                    else if (varible[0] == "R1")
                    {
                        reg[t += 1] = '0';
                        reg[t += 1] = '0';
                        reg[t += 1] = '1';
                    }
                    else if (varible[0] == "R2")
                    {
                        reg[t += 1] = '0';
                        reg[t += 1] = '1';
                        reg[t += 1] = '0';
                    }
                    else if (varible[0] == "R3")
                    {
                        reg[t += 1] = '0';
                        reg[t += 1] = '1';
                        reg[t += 1] = '1';
                    }
                    else if (varible[0] == "R4")
                    {
                        reg[t += 1] = '1';
                        reg[t += 1] = '0';
                        reg[t += 1] = '0';
                    }
                    else if (varible[0] == "R5")
                    {
                        reg[t += 1] = '1';
                        reg[t += 1] = '0';
                        reg[t += 1] = '1';
                    }
                    else if (varible[0] == "R6")
                    {
                        reg[t += 1] = '1';
                        reg[t += 1] = '1';
                        reg[t += 1] = '0';
                    }
                    else if (varible[0] == "R7")
                    {
                        reg[t += 1] = '1';
                        reg[t += 1] = '1';
                        reg[t += 1] = '1';
                    }

                    t = 16;
                    if (map.ContainsKey(varible[1]))
                    {
                        string key = map[varible[1]];
                        int decima = Convert.ToInt32(key, 2);
                        string PcOffset = Convert.ToString(decima, 2);
                        char[] value = new char[9];
                        for (int w = 0; w <= 8; w++)
                        {
                            if (w < PcOffset.Length)
                            {
                                value[8 - w] = PcOffset[PcOffset.Length - w - 1];
                            }
                            else
                            {
                                value[8 - w] = '0';
                            }
                        }
                        for (int w = 8; w >= 0; w--)
                        {
                            reg[t -= 1] = value[w];
                        }
                    }
                    else
                    {
                        string number = varible[1];
                        if (number[0] == 'x')
                        {
                            number = number.Replace("x", "");
                            int decima = Convert.ToInt32(number, 16);
                            string binary = Convert.ToString(decima, 2);
                            char[] value = new char[9];
                            for (int w = 0; w < 9; w++)
                            {
                                if (w < binary.Length)
                                {
                                    value[8 - w] = binary[binary.Length - w - 1];
                                }
                                else
                                {
                                    value[8 - w] = '0';
                                }
                            }
                            for (int w = 8; w >= 0; w--)
                            {
                                reg[t -= 1] = value[w];
                            }

                        }
                        else if (number[0] == '#')
                        {
                            number = number.Replace("#", "");
                            int decima = int.Parse(number);
                            string binary = Convert.ToString(decima, 2);
                            char[] value = new char[9];
                            for (int w = 0; w < 9; w++)
                            {
                                if (w < binary.Length)
                                {
                                    value[8 - w] = binary[binary.Length - w - 1];
                                }
                                else
                                {
                                    if (!number.Contains('-'))
                                    {
                                        value[4 - w] = '0';
                                    }
                                }
                            }
                            for (int w = 8; w >= 0; w--)
                            {
                                reg[t -= 1] = value[w];
                            }
                        }
                        else if (number[0] == 'b')
                        {
                            number = number.Replace("b", "");
                            char[] value = new char[9];
                            for (int w = 0; w < 9; w++)
                            {
                                if (w < number.Length)
                                {
                                    value[8 - w] = number[number.Length - w - 1];
                                }
                                else
                                {
                                    value[8 - w] = number[0];
                                }
                            }
                            for (int w = 8; w >= 0; w--)
                            {
                                reg[t -= 1] = value[w];
                            }
                        }
                    }
                }
                else if (instruct[0] == "STR")
                {
                    string[] varible = instruct[1].Split(',');

                    reg[t] = '0';
                    reg[t += 1] = '1';
                    reg[t += 1] = '1';
                    reg[t += 1] = '1';
                    for (int j = 0; j < 2; j++)
                    {
                        if (varible[j] == "R0")
                        {
                            reg[t += 1] = '0';
                            reg[t += 1] = '0';
                            reg[t += 1] = '0';
                        }
                        else if (varible[j] == "R1")
                        {
                            reg[t += 1] = '0';
                            reg[t += 1] = '0';
                            reg[t += 1] = '1';
                        }
                        else if (varible[j] == "R2")
                        {
                            reg[t += 1] = '0';
                            reg[t += 1] = '1';
                            reg[t += 1] = '0';
                        }
                        else if (varible[j] == "R3")
                        {
                            reg[t += 1] = '0';
                            reg[t += 1] = '1';
                            reg[t += 1] = '1';
                        }
                        else if (varible[j] == "R4")
                        {
                            reg[t += 1] = '1';
                            reg[t += 1] = '0';
                            reg[t += 1] = '0';
                        }
                        else if (varible[j] == "R5")
                        {
                            reg[t += 1] = '1';
                            reg[t += 1] = '0';
                            reg[t += 1] = '1';
                        }
                        else if (varible[j] == "R6")
                        {
                            reg[t += 1] = '1';
                            reg[t += 1] = '1';
                            reg[t += 1] = '0';
                        }
                        else if (varible[j] == "R7")
                        {
                            reg[t += 1] = '1';
                            reg[t += 1] = '1';
                            reg[t += 1] = '1';
                        }
                    }

                    t = 16;
                    if (map.ContainsKey(varible[2]))
                    {
                        string key = map[varible[2]];
                        char[] value = new char[6];
                        for (int w = 0; w < 6; w++)
                        {
                            if (w < key.Length)
                            {
                                value[5 - w] = key[key.Length - w - 1];
                            }
                            else
                            {
                                value[5 - w] = '0';
                            }
                        }
                        for (int w = 5; w >= 0; w--)
                        {
                            reg[t -= 1] = value[w];
                        }
                    }
                    else
                    {
                        string number = varible[2];
                        if (number[0] == 'x')
                        {
                            number = number.Replace("x", "");
                            int decima = Convert.ToInt32(number, 16);
                            string binary = Convert.ToString(decima, 2);
                            char[] value = new char[6];
                            for (int w = 0; w < 6; w++)
                            {
                                if (w < binary.Length)
                                {
                                    value[5 - w] = binary[binary.Length - w - 1];
                                }
                                else
                                {
                                    value[5 - w] = '0';
                                }
                            }
                            for (int w = 5; w >= 0; w--)
                            {
                                reg[t -= 1] = value[w];
                            }

                        }
                        else if (number[0] == '#')
                        {
                            number = number.Replace("#", "");
                            int decima = int.Parse(number);
                            string binary = Convert.ToString(decima, 2);
                            char[] value = new char[6];
                            for (int w = 0; w < 6; w++)
                            {
                                if (w < binary.Length)
                                {
                                    value[5 - w] = binary[binary.Length - w - 1];
                                }
                                else
                                {
                                    if (!number.Contains('-'))
                                    {
                                        value[5 - w] = '0';
                                    }

                                }
                            }
                            for (int w = 5; w >= 0; w--)
                            {
                                reg[t -= 1] = value[w];
                            }
                        }
                        else if (number[0] == 'b')
                        {
                            number = number.Replace("b", "");
                            char[] value = new char[6];
                            for (int w = 0; w < 6; w++)
                            {
                                if (w < number.Length)
                                {
                                    value[5 - w] = number[number.Length - w - 1];
                                }
                                else
                                {
                                    value[5 - w] = number[0];
                                }
                            }
                            for (int w = 5; w >= 0; w--)
                            {
                                reg[t -= 1] = value[w];
                            }
                        }
                    }
                }
                else if (instruct[0] == "BRn")
                {
                    reg[t] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '1';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    t = 16;
                    if (map.ContainsKey(instruct[1]))
                    {
                        int decima = Convert.ToInt32(map[instruct[1]], 2);
                        string binary = Convert.ToString(decima, 2);
                        char[] value = new char[9];
                        for (int w = 0; w <= 8; w++)
                        {
                            if (w < binary.Length)
                            {
                                value[8 - w] = binary[binary.Length - w - 1];
                            }
                            else
                            {
                                value[8 - w] = '0';
                            }
                        }
                        for (int w = 8; w >= 0; w--)
                        {
                            reg[t -= 1] = value[w];
                        }

                    }
                }
                else if (instruct[0] == "BRz")
                {
                    reg[t] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '1';
                    reg[t += 1] = '0';
                    t = 16;
                    if (map.ContainsKey(instruct[1]))
                    {
                        int decima = Convert.ToInt32(map[instruct[1]], 2);
                        string binary = Convert.ToString(decima, 2);
                        char[] value = new char[9];
                        for (int w = 0; w <= 8; w++)
                        {
                            if (w < binary.Length)
                            {
                                value[8 - w] = binary[binary.Length - w - 1];
                            }
                            else
                            {
                                value[8 - w] = '0';
                            }
                        }
                        for (int w = 8; w >= 0; w--)
                        {
                            reg[t -= 1] = value[w];
                        }

                    }
                }
                else if (instruct[0] == "BRp")
                {
                    reg[t] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '1';
                    t = 16;
                    if (map.ContainsKey(instruct[1]))
                    {
                        int decima = Convert.ToInt32(map[instruct[1]], 2);
                        string binary = Convert.ToString(decima, 2);
                        char[] value = new char[9];
                        for (int w = 0; w <= 8; w++)
                        {
                            if (w < binary.Length)
                            {
                                value[8 - w] = binary[binary.Length - w - 1];
                            }
                            else
                            {
                                value[8 - w] = '0';
                            }
                        }
                        for (int w = 8; w >= 0; w--)
                        {
                            reg[t -= 1] = value[w];
                        }

                    }
                }
                else if (instruct[0] == "BRzp")
                {
                    reg[t] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '1';
                    reg[t += 1] = '1';
                    t = 16;
                    if (map.ContainsKey(instruct[1]))
                    {
                        int decima = Convert.ToInt32(map[instruct[1]], 2);
                        string binary = Convert.ToString(decima, 2);
                        char[] value = new char[9];
                        for (int w = 0; w <= 8; w++)
                        {
                            if (w < binary.Length)
                            {
                                value[8 - w] = binary[binary.Length - w - 1];
                            }
                            else
                            {
                                value[8 - w] = '0';
                            }
                        }
                        for (int w = 8; w >= 0; w--)
                        {
                            reg[t -= 1] = value[w];
                        }

                    }
                }
                else if (instruct[0] == "BRnz")
                {
                    reg[t] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '1';
                    reg[t += 1] = '1';
                    reg[t += 1] = '0';
                    t = 16;
                    if (map.ContainsKey(instruct[1]))
                    {
                        int decima = Convert.ToInt32(map[instruct[1]], 2);
                        string binary = Convert.ToString(decima, 2);
                        char[] value = new char[9];
                        for (int w = 0; w <= 8; w++)
                        {
                            if (w < binary.Length)
                            {
                                value[8 - w] = binary[binary.Length - w - 1];
                            }
                            else
                            {
                                value[8 - w] = '0';
                            }
                        }
                        for (int w = 8; w >= 0; w--)
                        {
                            reg[t -= 1] = value[w];
                        }

                    }
                }
                else if (instruct[0] == "BRnp")
                {
                    reg[t] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '1';
                    reg[t += 1] = '0';
                    reg[t += 1] = '1';
                    t = 16;
                    if (map.ContainsKey(instruct[1]))
                    {
                        int decima = Convert.ToInt32(map[instruct[1]], 2);
                        string binary = Convert.ToString(decima, 2);
                        char[] value = new char[9];
                        for (int w = 0; w <= 8; w++)
                        {
                            if (w < binary.Length)
                            {
                                value[8 - w] = binary[binary.Length - w - 1];
                            }
                            else
                            {
                                value[8 - w] = '0';
                            }
                        }
                        for (int w = 8; w >= 0; w--)
                        {
                            reg[t -= 1] = value[w];
                        }

                    }
                }
                else if (instruct[0] == "BRnzp")
                {
                    reg[t] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '1';
                    reg[t += 1] = '1';
                    reg[t += 1] = '1';
                    t = 16;
                    if (map.ContainsKey(instruct[1]))
                    {
                        int decima = Convert.ToInt32(map[instruct[1]], 2);
                        string binary = Convert.ToString(decima, 2);
                        char[] value = new char[9];
                        for (int w = 0; w <= 8; w++)
                        {
                            if (w < binary.Length)
                            {
                                value[8 - w] = binary[binary.Length - w - 1];
                            }
                            else
                            {
                                value[8 - w] = '0';
                            }
                        }
                        for (int w = 8; w >= 0; w--)
                        {
                            reg[t -= 1] = value[w];
                        }

                    }
                }
                else if (instruct[0] == "BR")
                {
                    reg[t] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    t = 16;
                    if (map.ContainsKey(instruct[1]))
                    {
                        int decima = Convert.ToInt32(map[instruct[1]], 2);
                        string binary = Convert.ToString(decima, 2);
                        char[] value = new char[9];
                        for (int w = 0; w <= 8; w++)
                        {
                            if (w < binary.Length)
                            {
                                value[8 - w] = binary[binary.Length - w - 1];
                            }
                            else
                            {
                                value[8 - w] = '0';
                            }
                        }
                        for (int w = 8; w >= 0; w--)
                        {
                            reg[t -= 1] = value[w];
                        }

                    }
                }
                else if (instruct[0] == "JMP")

                {
                    reg[t] = '1';
                    reg[t += 1] = '1';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    if (instruct[1] == "R0")
                    {
                        reg[t += 1] = '0';
                        reg[t += 1] = '0';
                        reg[t += 1] = '0';
                    }
                    else if (instruct[1] == "R1")
                    {
                        reg[t += 1] = '0';
                        reg[t += 1] = '0';
                        reg[t += 1] = '1';
                    }
                    else if (instruct[1] == "R2")
                    {
                        reg[t += 1] = '0';
                        reg[t += 1] = '1';
                        reg[t += 1] = '0';
                    }
                    else if (instruct[1] == "R3")
                    {
                        reg[t += 1] = '0';
                        reg[t += 1] = '1';
                        reg[t += 1] = '1';
                    }
                    else if (instruct[1] == "R4")
                    {
                        reg[t += 1] = '1';
                        reg[t += 1] = '0';
                        reg[t += 1] = '0';
                    }
                    else if (instruct[1] == "R5")
                    {
                        reg[t += 1] = '1';
                        reg[t += 1] = '0';
                        reg[t += 1] = '1';
                    }
                    else if (instruct[1] == "R6")
                    {
                        reg[t += 1] = '1';
                        reg[t += 1] = '1';
                        reg[t += 1] = '0';
                    }
                    else if (instruct[1] == "R7")
                    {
                        reg[t += 1] = '1';
                        reg[t += 1] = '1';
                        reg[t += 1] = '1';
                    }
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';

                }
                else if (instruct[0] == "RET")
                {
                    reg[t] = '1';
                    reg[t += 1] = '1';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '1';
                    reg[t += 1] = '1';
                    reg[t += 1] = '1';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                }
                else if (instruct[0] == "JSRR")
                {
                    reg[t] = '0';
                    reg[t += 1] = '1';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    if (instruct[1] == "R0")
                    {
                        reg[t += 1] = '0';
                        reg[t += 1] = '0';
                        reg[t += 1] = '0';
                    }
                    else if (instruct[1] == "R1")
                    {
                        reg[t += 1] = '0';
                        reg[t += 1] = '0';
                        reg[t += 1] = '1';
                    }
                    else if (instruct[1] == "R2")
                    {
                        reg[t += 1] = '0';
                        reg[t += 1] = '1';
                        reg[t += 1] = '0';
                    }
                    else if (instruct[1] == "R3")
                    {
                        reg[t += 1] = '0';
                        reg[t += 1] = '1';
                        reg[t += 1] = '1';
                    }
                    else if (instruct[1] == "R4")
                    {
                        reg[t += 1] = '1';
                        reg[t += 1] = '0';
                        reg[t += 1] = '0';
                    }
                    else if (instruct[1] == "R5")
                    {
                        reg[t += 1] = '1';
                        reg[t += 1] = '0';
                        reg[t += 1] = '1';
                    }
                    else if (instruct[1] == "R6")
                    {
                        reg[t += 1] = '1';
                        reg[t += 1] = '1';
                        reg[t += 1] = '0';
                    }
                    else if (instruct[1] == "R7")
                    {
                        reg[t += 1] = '1';
                        reg[t += 1] = '1';
                        reg[t += 1] = '1';
                    }
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';

                }
                else if (instruct[0] == "JSR")
                {
                    reg[t] = '0';
                    reg[t += 1] = '1';
                    reg[t += 1] = '0';
                    reg[t += 1] = '0';
                    reg[t += 1] = '1';
                    if (map.ContainsKey(instruct[1]))
                    {

                        int decima = Convert.ToInt32(map[instruct[1]], 2);
                        string binary = Convert.ToString(decima, 2);
                        char[] value = new char[11];
                        for (int w = 0; w <= 10; w++)
                        {
                            if (w < binary.Length)
                            {
                                value[10 - w] = binary[binary.Length - w - 1];
                            }
                            else
                            {
                                value[10 - w] = '0';
                            }
                        }
                        for (int w = 10; w >= 0; w--)
                        {
                            reg[t -= 1] = value[w];
                        }
                    }

                }
                else if (instruct[0] == "DEC")
                {
                    int decima = int.Parse(instruct[1]);
                    string binary = Convert.ToString(decima, 2);
                    for (int j = 0; j < 16; j++)
                    {
                        if (j < 16 - (binary.Length))
                        {
                            reg[j] = '0';
                        }
                        else { reg[j] = binary[j - (16 - (binary.Length))]; }

                    }

                }
                else if (instruct[0] == "HEX")
                {
                    int decima = Convert.ToInt32(instruct[1], 16);
                    string binary = Convert.ToString(decima, 2);
                    for (int j = 0; j < 16; j++)
                    {
                        if (j < 16 - (binary.Length))
                        {
                            reg[j] = '0';
                        }
                        else { reg[j] = binary[j - (16 - (binary.Length))]; }

                    }
                }
                else if (instruct[0] == "BIN")
                {
                    string binary = instruct[1];
                    for (int j = 0; j < 16; j++)
                    {
                        if (j < 16 - (binary.Length))
                        {
                            reg[j] = binary[0];
                        }
                        else { reg[j] = binary[j - (16 - (binary.Length))]; }

                    }
                }
                else if (instruct[0] == "HALT")
                {
                    reg[t] = '1';
                    reg[t += 1] = '1';
                    reg[t += 1] = '1';
                    reg[t += 1] = '1';
                    reg[t += 1] = '1';
                    reg[t += 1] = '1';
                    reg[t += 1] = '1';
                    reg[t += 1] = '1';
                    reg[t += 1] = '1';
                    reg[t += 1] = '1';
                    reg[t += 1] = '1';
                    reg[t += 1] = '1';
                    reg[t += 1] = '1';
                    reg[t += 1] = '1';
                    reg[t += 1] = '1';
                    reg[t += 1] = '1';
                }

                for (int j = 0; j < 16; j++)
                {
                    mano.memory[mano.pc + i - 1, j] = reg[j];
                }

                foreach (char w in reg)
                {

                    temp += w;

                }
                temp += "\n";

            }
            File.WriteAllText(filePath, temp);

            AlertText.Text = "Success complie! \n";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string order = "";
            for (int i = 0; i < 16; i++)
            {
                mano.IR[i] = mano.memory[mano.pc, i];
            }
            for (int i = 0; i < 4; i++)
            {
                order += mano.IR[i];
            }
            mano.pc += 1;
            line++;

            //ADD
            if (order == "0001")
            {
                string DR = "";
                string SR1 = "";
                string SR2 = "";
                for (int j = 4; j < 10; j++)
                {
                    if (j <= 6)
                    {
                        DR += mano.IR[j];
                    }
                    else
                    {
                        SR1 += mano.IR[j];
                    }

                }

                if (mano.IR[10] == '0')
                {
                    for (int j = 13; j < 16; j++) { SR2 += mano.IR[j]; }
                    char[] R1 = mano.map[SR1];
                    char[] R2 = mano.map[SR2];
                    char[] sum = new char[16];
                    sum = AddBinaryArrays(R2, R1);
                    for (int j = 1; j <= 16; j++)
                    {
                        mano.map[DR][j - 1] = sum[j];
                    }

                }
                else
                {
                    char[] R2 = new char[5];
                    for (int j = 11; j < 16; j++) { R2[j - 11] = mano.IR[j]; }
                    char[] R1 = mano.map[SR1];
                    char[] sum = new char[16];
                    sum = AddBinaryArrays(R2, R1);
                    for (int j = 1; j <= 16; j++)
                    {
                        mano.map[DR][j - 1] = sum[j];
                    }
                }
                //flag N,Z,P
                if (mano.map[DR][0] == '0')
                {
                    mano.P = 1;
                    mano.N = 0;
                }
                else
                {
                    mano.P = 0;
                    mano.N = 1;
                }
                mano.Z = 1;
                for (int j = 0; j < 16; j++)
                {
                    if (mano.map[DR][j] != '0')
                    {
                        mano.Z = 0;
                    }
                }
            }
            //AND
            else if (order == "0101")
            {
                string DR = "";
                string SR1 = "";
                string SR2 = "";
                for (int j = 4; j < 10; j++)
                {
                    if (j <= 6)
                    {
                        DR += mano.IR[j];
                    }
                    else
                    {
                        SR1 += mano.IR[j];
                    }

                }
                if (mano.IR[10] == '0')
                {
                    for (int j = 13; j < 16; j++) { SR2 += mano.IR[j]; }
                    char[] R1 = mano.map[SR1];
                    char[] R2 = mano.map[SR2];
                    char[] R3 = new char[16];
                    for (int j = 0; j < R1.Length; j++)
                    {
                        R3[j] = (R1[j] == '1' && R2[j] == '1') ? '1' : '0';
                    }
                    mano.map[DR] = R3;
                }
                else
                {
                    char[] R2 = new char[5];
                    for (int j = 11; j < 16; j++) { R2[j - 11] = mano.IR[j]; }
                    char[] R1 = mano.map[SR1];
                    char[] R3 = new char[16];
                    for (int j = 0; j < R1.Length; j++)
                    {
                        R3[j] = (R1[j] == '1' && R2[j] == '1') ? '1' : '0';
                    }
                    mano.map[DR] = R3;
                }
                //flag N,Z,P
                if (mano.map[DR][0] == '0')
                {
                    mano.P = 1;
                    mano.N = 0;
                }
                else
                {
                    mano.P = 0;
                    mano.N = 1;
                }
                mano.Z = 1;
                for (int j = 0; j < 16; j++)
                {
                    if (mano.map[DR][j] != '0')
                    {
                        mano.Z = 0;
                    }
                }
            }
            //NOT
            else if (order == "1001")
            {
                string DR = "";
                string SR1 = "";
                for (int j = 4; j < 10; j++)
                {
                    if (j <= 6)
                    {
                        DR += mano.IR[j];
                    }
                    else
                    {
                        SR1 += mano.IR[j];
                    }
                }
                char[] R1 = mano.map[SR1];
                char[] R3 = new char[16];
                for (int j = 0; j < R1.Length; j++)
                {
                    R3[j] = (R1[j] == '1') ? '0' : '1';
                }
                mano.map[DR] = R3;
                //flag N,Z,P
                if (mano.map[DR][0] == '0')
                {
                    mano.P = 1;
                    mano.N = 0;
                }
                else
                {
                    mano.P = 0;
                    mano.N = 1;
                }
                mano.Z = 1;
                for (int j = 0; j < 16; j++)
                {
                    if (mano.map[DR][j] != '0')
                    {
                        mano.Z = 0;
                    }
                }
            }
            //LD
            else if (order == "0010")
            {
                string DR = "";
                string PcOffset = "";
                char[] R1 = new char[16];
                for (int j = 4; j < 16; j++)
                {
                    if (j <= 6)
                    {
                        DR += mano.IR[j];
                    }
                    else
                    {
                        PcOffset += mano.IR[j];
                    }

                }
                int decimal1 = Convert.ToInt32(PcOffset, 2);
                decimal1 += pc;
                string memory = "";
                for (int j = 0; j < 16; j++)
                {
                    memory += mano.memory[decimal1, j];
                }
                for (int j = 0; j < 16; j++)
                {
                    if (j < 16 - (memory.Length))
                    {
                        R1[j] = memory[0];
                    }
                    else { R1[j] = memory[j - (16 - (memory.Length))]; }

                }
                mano.map[DR] = R1;
                //flag N,Z,P
                if (mano.map[DR][0] == '0')
                {
                    mano.P = 1;
                    mano.N = 0;
                }
                else
                {
                    mano.P = 0;
                    mano.N = 1;
                }
                mano.Z = 1;
                for (int j = 0; j < 16; j++)
                {
                    if (mano.map[DR][j] != '0')
                    {
                        mano.Z = 0;
                    }
                }

            }
            //LDI
            else if (order == "1010")
            {
                string DR = "";
                string PcOffset = "";
                for (int j = 4; j < 16; j++)
                {
                    if (j <= 6)
                    {
                        DR += mano.IR[j];
                    }
                    else
                    {
                        PcOffset += mano.IR[j];
                    }

                }

                int decimal1 = Convert.ToInt32(PcOffset, 2);
                decimal1 += pc;
                string memory = "";
                for (int j = 0; j < 16; j++)
                {
                    memory += mano.memory[decimal1, j];
                }
                int offset = Convert.ToInt32(memory, 2);
                for (int j = 0; j < 16; j++)
                {
                    mano.map[DR][j] = mano.memory[offset, j];
                }
                //flag N,Z,P
                if (mano.map[DR][0] == '0')
                {
                    mano.P = 1;
                    mano.N = 0;
                }
                else
                {
                    mano.P = 0;
                    mano.N = 1;
                }
                mano.Z = 1;
                for (int j = 0; j < 16; j++)
                {
                    if (mano.map[DR][j] != '0')
                    {
                        mano.Z = 0;
                    }
                }
            }
            //LDR
            else if (order == "0110")
            {
                string DR = "";
                string SR1 = "";
                string PcOffset = "";
                char[] R_1 = new char[16];
                for (int j = 4; j < 16; j++)
                {
                    if (j <= 6)
                    {
                        DR += mano.IR[j];
                    }
                    else if (7 <= j && j < 10)
                    {
                        SR1 += mano.IR[j];
                    }
                    else
                    {
                        PcOffset += mano.IR[j];
                    }

                }
                char[] offset = new char[16];
                for (int j = 15, w = PcOffset.Length - 1; j >= 0; j--, w--)
                {
                    if (w >= 0)
                    {
                        offset[j] = PcOffset[w];
                    }
                    else
                    {
                        offset[j] = PcOffset[0];
                    }
                }

                char[] sumBIN = new char[16];
                sumBIN = AddBinaryArrays(mano.map[SR1], offset);
                string sum = "";
                for (int j = 1; j <= 16; j++)
                {
                    sum += sumBIN[j];
                }
                int decima = Convert.ToInt32(sum, 2);
                for (int j = 0; j < 16; j++)
                {
                    mano.map[DR][j] = mano.memory[decima, j];
                }
                //flag N,Z,P
                if (mano.map[DR][0] == '0')
                {
                    mano.P = 1;
                    mano.N = 0;
                }
                else
                {
                    mano.P = 0;
                    mano.N = 1;
                }
                mano.Z = 1;
                for (int j = 0; j < 16; j++)
                {
                    if (mano.map[DR][j] != '0')
                    {
                        mano.Z = 0;
                    }
                }


            }
            //LEA
            else if (order == "1110")
            {
                string DR = "";
                string PcOffset = "";
                char[] R1 = new char[16];
                for (int j = 4; j < 16; j++)
                {
                    if (j <= 6)
                    {
                        DR += mano.IR[j];
                    }
                    else
                    {
                        PcOffset += mano.IR[j];
                    }

                }
                int decimal1 = Convert.ToInt32(PcOffset, 2);
                decimal1 += pc;
                PcOffset = Convert.ToString(decimal1, 2);
                for (int j = 0; j < 16; j++)
                {
                    if (j < 16 - (PcOffset.Length))
                    {
                        R1[j] = '0';
                    }
                    else { R1[j] = PcOffset[j - (16 - (PcOffset.Length))]; }

                }
                mano.map[DR] = R1;

            }
            //ST
            else if (order == "0011")
            {
                string DR = "";
                string PcOffset = "";
                for (int j = 4; j < 16; j++)
                {
                    if (j <= 6)
                    {
                        DR += mano.IR[j];
                    }
                    else
                    {
                        PcOffset += mano.IR[j];
                    }
                }
                int decimal1 = Convert.ToInt32(PcOffset, 2);
                decimal1 += pc;
                for (int j = 0; j < 16; j++)
                {
                    mano.memory[decimal1, j] = mano.map[DR][j];
                }

            }
            //STI
            else if (order == "1011")
            {
                string DR = "";
                string PcOffset = "";
                for (int j = 4; j < 16; j++)
                {
                    if (j <= 6)
                    {
                        DR += mano.IR[j];
                    }
                    else
                    {
                        PcOffset += mano.IR[j];
                    }
                }
                int decimal1 = Convert.ToInt32(PcOffset, 2);
                decimal1 += pc;
                string memory = "";
                for (int j = 0; j < 16; j++)
                {
                    memory += mano.memory[decimal1, j];
                }
                int offset = Convert.ToInt32(memory, 2);
                for (int j = 0; j < 16; j++)
                {
                    mano.memory[offset, j] = mano.map[DR][j];
                }

            }
            //STR
            else if (order == "0111")
            {
                string DR = "";
                string SR1 = "";
                string PcOffset = "";
                char[] R_1 = new char[16];
                for (int j = 4; j < 16; j++)
                {
                    if (j <= 6)
                    {
                        DR += mano.IR[j];
                    }
                    else if (7 <= j && j < 10)
                    {
                        SR1 += mano.IR[j];
                    }
                    else
                    {
                        PcOffset += mano.IR[j];
                    }

                }
                char[] offset = new char[16];
                for (int j = 15, w = PcOffset.Length - 1; j >= 0; j--, w--)
                {
                    if (w >= 0)
                    {
                        offset[j] = PcOffset[w];
                    }
                    else
                    {
                        offset[j] = PcOffset[0];
                    }
                }

                char[] sumBIN = new char[16];
                sumBIN = AddBinaryArrays(mano.map[SR1], offset);
                string sum = "";
                for (int j = 1; j <= 16; j++)
                {
                    sum += sumBIN[j];
                }
                int decima = Convert.ToInt32(sum, 2);
                for (int j = 0; j < 16; j++)
                {
                    mano.memory[decima, j] = mano.map[DR][j];
                }
            }
            //BRn
            else if (order == "0000")
            {
                string DR = "";
                string PcOffset = "";
                for (int j = 4; j < 16; j++)
                {
                    if (j <= 6)
                    {
                        DR += mano.IR[j];
                    }
                    else
                    {
                        PcOffset += mano.IR[j];
                    }

                }
                
                int offset = Convert.ToInt32(PcOffset, 2);
                offset += pc;
                if (DR == "100")
                {
                    if (mano.N == 1)
                    {
                        mano.pc = offset;
                    }
                }
                else if (DR == "010")
                {
                    if (mano.Z == 1)
                    {
                        mano.pc = offset;
                    }

                }
                else if (DR == "001")
                {
                    if (mano.P == 1)
                    {
                        mano.pc = offset;
                    }
                }
                else if (DR == "011")
                {
                    if (mano.P == 1 || mano.Z == 1)
                    {
                        mano.pc = offset;

                    }
                }
                else if (DR == "110")
                {
                    if (mano.N == 1 || mano.Z == 1)
                    {

                        mano.pc = offset;

                    }
                }
                else if (DR == "101")
                {
                    if (mano.P == 1 || mano.N == 1)
                    {

                        mano.pc = offset;

                    }
                }
                else if (DR == "111")
                {
                    if (mano.P == 1 || mano.Z == 1 || mano.N == 1)
                    {

                        mano.pc = offset;

                    }
                }
                else if (DR == "000")
                {
                    if (true)
                    {
                        mano.pc = offset;
                    }
                }

            }
            //JMP ,RET
            else if (order == "1100")
            {
                string DR = "";
                for (int j = 7; j < 10; j++)
                {
                    DR += mano.IR[j];

                }
                if (DR == "111")
                {
                    string str = new string(mano.map["111"]);
                    mano.pc = int.Parse(str);
                }
                else
                {
                    string decimal2 = mano.pc.ToString();
                    char[] chars = decimal2.ToCharArray();
                    mano.map["111"] = chars;
                    string str = new string(mano.map[DR]);
                    mano.pc = Convert.ToInt32(str, 2);
                }
            }
            //JSR,JSRR
            else if (order == "0100")
            {
                if (mano.IR[4] == '1')
                {
                    string R = mano.pc.ToString();
                    char[] chars = R.ToCharArray();
                    mano.map["111"] = chars;
                    string PcOffset = "";
                    for (int j = 5; j < 16; j++)
                    {
                        PcOffset += mano.IR[j];
                    }
                    int decimal1 = Convert.ToInt32(PcOffset, 2);
                    decimal1 += pc;
                    mano.pc = decimal1;

                }
                else
                {
                    string DR = "";
                    for (int j = 7; j < 10; j++)
                    {
                        DR += mano.IR[j];
                    }
                    string R = mano.pc.ToString();
                    char[] chars = R.ToCharArray();
                    mano.map["111"] = chars;
                    string str = new string(mano.map[DR]);
                    mano.pc = Convert.ToInt32(str, 2);
                }


            }
            //HALT
            else if (order == "1111")
            {
                button2.Enabled = false;
            }


            //Show Pc
            char[] reg_pc = new char[16];
            string pcbinary = Convert.ToString(mano.pc, 2);
            for (int j = 0; j < 16; j++)
            {
                if (j < 16 - (pcbinary.Length))
                {
                    reg_pc[j] = '0';
                }
                else { reg_pc[j] = pcbinary[j - (16 - (pcbinary.Length))]; }

            }
            pcbinary = "";
            for (int i = 0; i < 16; i++)
            {
                pcbinary += reg_pc[i];
            }
            textBoxPc.Text = pcbinary;
            //Show IR
            string strIR = "";
            for (int i = 0; i < 16; i++)
            {
                strIR += mano.IR[i];
            }
            textBoxIR.Text = strIR;
            //Show N,Z,P
            string N = mano.N.ToString();
            string Z = mano.Z.ToString();
            string P = mano.P.ToString();
            textBoxNZP.Text = N + Z + P;
            //Show R
            string strR0 = "";
            for (int i = 0; i < 16; i++)
            {
                strR0 += mano.map["000"][i];
            }
            textBoxR0.Text = "R0:   " + strR0;

            string strR1 = "";
            for (int i = 0; i < 16; i++)
            {
                strR1 += mano.map["001"][i];
            }
            textBoxR1.Text = "R1:   " + strR1;

            string strR2 = "";
            for (int i = 0; i < 16; i++)
            {
                strR2 += mano.map["010"][i];
            }
            textBoxR2.Text = "R2:   " + strR2;

            string strR3 = "";
            for (int i = 0; i < 16; i++)
            {
                strR3 += mano.map["011"][i];
            }
            textBoxR3.Text = "R3:   " + strR3;

            string strR4 = "";
            for (int i = 0; i < 16; i++)
            {
                strR4 += mano.map["100"][i];
            }
            textBoxR4.Text = "R4:   " + strR4;

            string strR5 = "";
            for (int i = 0; i < 16; i++)
            {
                strR5 += mano.map["101"][i];
            }
            textBoxR5.Text = "R5:   " + strR5;

            string strR6 = "";
            for (int i = 0; i < 16; i++)
            {
                strR6 += mano.map["110"][i];
            }
            textBoxR6.Text = "R6:   " + strR6;

            string strR7 = "";
            for (int i = 0; i < 16; i++)
            {
                strR7 += mano.map["111"][i];
            }
            textBoxR7.Text = "R7:   " + strR7;

            AlertText.Text += " Address of memory: " + mano.pc + "\n";
            AlertText.Text += " Line " +line.ToString()+ "\n";

        }

        
    }

}


