using System;
using System.Windows.Forms;

namespace CalculatorNameSpace
{
    public partial class Calculator : Form
    {
        private Double resultValue = 0;
        private String operationPerformed = "";
        private bool isOperationPerformed = false;

        public Calculator()
        {
            InitializeComponent();
        }

        // Resets the value in the main text box
        private void ResetTextBox(bool clearAll)
        {
            // Clearing all gets rid of the current result, as well as clearing the text box
            if (clearAll)
            {
                resultValue = 0;
                Label_Current_Operation.Text = "";
            }

            TextBox_Result.Text = "0";
        }

        private void Number_Button_Click(object sender, EventArgs e)
        {
            // Clear the text box if these conditions are met, going to swap in result
            if((TextBox_Result.Text == "0") || isOperationPerformed)
                TextBox_Result.Clear(); 

            Button button = (Button)sender;

            // Handle adding a period, ensuring only one period is shown in the text box
            if (button.Text == ".")
            {
                if (!TextBox_Result.Text.Contains("."))
                {
                    TextBox_Result.Text = TextBox_Result.Text + button.Text;
                }
            }
            else
            {
                TextBox_Result.Text = TextBox_Result.Text + button.Text;
            }

            isOperationPerformed = false;
        }

        private void Operator_Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if(resultValue != 0)
            {
                // When a number is already shown and operator pressed, perform the operation
                Button_Equals.PerformClick();
                ApplyOperation(button, false);
            }

            ApplyOperation(button, true);
        }

        private void ApplyOperation(Button but, bool doParseText)
        {
            // Get the type of operation selected based on the text in the button
            operationPerformed = but.Text;

            if(doParseText)
                resultValue = Double.Parse(TextBox_Result.Text);
            
            isOperationPerformed = true;
            Label_Current_Operation.Text = resultValue + " " + operationPerformed;
        }

        private void ClearEntry_Button_Click(object sender, EventArgs e)
        {
            ResetTextBox(false);
        }

        private void ClearAll_Button_Click(object sender, EventArgs e)
        {
            ResetTextBox(true);
        }

        private void Equals_Button_Click(object sender, EventArgs e)
        {
            // Perform correct operation based on the character that was in the button
            switch (operationPerformed)
            {
                case "+":
                    TextBox_Result.Text = (resultValue + Double.Parse(TextBox_Result.Text)).ToString();
                    break;
                case "-":
                    TextBox_Result.Text = (resultValue - Double.Parse(TextBox_Result.Text)).ToString();
                    break;
                case "x":
                    TextBox_Result.Text = (resultValue * Double.Parse(TextBox_Result.Text)).ToString();
                    break;
                case "÷":
                    TextBox_Result.Text = (resultValue / Double.Parse(TextBox_Result.Text)).ToString();
                    break;
            }

            resultValue = Double.Parse(TextBox_Result.Text);
            operationPerformed = "";
            Label_Current_Operation.Text = "";
        }

        private void HandleKeyBoardInput(object sender, KeyPressEventArgs e)
        {
            // Check pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // Allow decimal numbers
            if ((e.KeyChar == '.') && TextBox_Result.Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }
    }
}
