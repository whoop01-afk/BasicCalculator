using CalculatorPrivateAssembly;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicCalculator
{
    public partial class FrmBasicCalculator : Form
    {
        public FrmBasicCalculator()
        {
            InitializeComponent();

            comboBox1.Items.AddRange(new object[] { "+", "-", "*", "/" });
            comboBox1.SelectedIndex = 0;

            textBox1.KeyPress += NumericOnly_KeyPress;
            textBox2.KeyPress += NumericOnly_KeyPress;
            textBox1.Leave += txtNum_Leave;
            textBox2.Leave += txtNum_Leave;
        }

        private void NumericOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = (TextBox)sender;

            if (char.IsControl(e.KeyChar)) return;
            if (char.IsDigit(e.KeyChar)) return;
            if (e.KeyChar == '.' && !txt.Text.Contains(".")) return;
            if (e.KeyChar == '-' && txt.SelectionStart == 0 && !txt.Text.Contains("-")) return;

            e.Handled = true;
        }

        private void txtNum_Leave(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(txt.Text)) txt.Text = "0";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter the first number.", "Validation");
                textBox1.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please enter the second number.", "Validation");
                textBox2.Focus();
                return;
            }

            float num1, num2;
            if (!float.TryParse(textBox1.Text, out num1))
            {
                MessageBox.Show("First number is not valid.", "Validation");
                textBox1.Focus();
                return;
            }
            if (!float.TryParse(textBox2.Text, out num2))
            {
                MessageBox.Show("Second number is not valid.", "Validation");
                textBox2.Focus();
                return;
            }

            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an operator.", "Validation");
                comboBox1.Focus();
                return;
            }

            float result = 0;

            switch (comboBox1.Text)
            {
                case "+": result = BasicComputation.Addition(num1, num2); break;
                case "-": result = BasicComputation.Subtraction(num1, num2); break;
                case "*": result = BasicComputation.Multiplication(num1, num2); break;
                case "/":
                    if (num2 == 0)
                    {
                        MessageBox.Show("Cannot divide by zero.", "Validation");
                        textBox2.Focus();
                        textBox2.SelectAll();
                        return;
                    }
                    result = BasicComputation.Division(num1, num2);
                    break;
            }

            if (float.IsInfinity(result) || float.IsNaN(result))
            {
                MessageBox.Show("The result is too large to display.", "Validation");
                return;
            }

            label4.Text = result.ToString();
        }
    }
}




