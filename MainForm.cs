using System;
using System.Drawing;
using System.Windows.Forms;

namespace Xero1
{
    public partial class MainForm : Form
    {
        private TextBox binaryTextBox = null!;
        private TextBox octalTextBox = null!;
        private TextBox decimalTextBox = null!;
        private TextBox hexTextBox = null!;
        private CheckBox prefixCheckBox = null!;
        private Button clearButton = null!;
        private Button copyButton = null!;
        private Label statusLabel = null!;
        private ComboBox bitSizeComboBox = null!;
        
        private bool isUpdating = false;

        public MainForm()
        {
            InitializeComponent();
            SetupEventHandlers();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Form properties
            this.Text = "Xero1 - Number Base Converter";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(240, 240, 240);

            // Title Label
            var titleLabel = new Label
            {
                Text = "Xero1 Number Base Converter",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 120, 215),
                Location = new Point(20, 20),
                Size = new Size(400, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(titleLabel);

            // Binary section
            CreateNumberSection("Binary (Base 2):", 70, out binaryTextBox, "Enter binary digits (0-1)");
            
            // Octal section  
            CreateNumberSection("Octal (Base 8):", 120, out octalTextBox, "Enter octal digits (0-7)");
            
            // Decimal section
            CreateNumberSection("Decimal (Base 10):", 170, out decimalTextBox, "Enter decimal digits (0-9)");
            
            // Hexadecimal section
            CreateNumberSection("Hexadecimal (Base 16):", 220, out hexTextBox, "Enter hex digits (0-9, A-F)");

            // Options section
            prefixCheckBox = new CheckBox
            {
                Text = "Show prefixes (0b, 0, 0x)",
                Location = new Point(20, 270),
                Size = new Size(200, 25),
                Checked = true,
                Font = new Font("Segoe UI", 9)
            };
            this.Controls.Add(prefixCheckBox);

            // Bit size selector
            var bitSizeLabel = new Label
            {
                Text = "Bit Size:",
                Location = new Point(240, 270),
                Size = new Size(60, 25),
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 9)
            };
            this.Controls.Add(bitSizeLabel);

            bitSizeComboBox = new ComboBox
            {
                Location = new Point(300, 270),
                Size = new Size(80, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9)
            };
            bitSizeComboBox.Items.AddRange(new object[] { "8-bit", "16-bit", "32-bit", "64-bit" });
            bitSizeComboBox.SelectedIndex = 2; // Default to 32-bit
            this.Controls.Add(bitSizeComboBox);

            // Buttons
            clearButton = new Button
            {
                Text = "Clear All",
                Location = new Point(20, 310),
                Size = new Size(100, 35),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            clearButton.FlatAppearance.BorderSize = 0;
            this.Controls.Add(clearButton);

            copyButton = new Button
            {
                Text = "Copy Results",
                Location = new Point(140, 310),
                Size = new Size(100, 35),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            copyButton.FlatAppearance.BorderSize = 0;
            this.Controls.Add(copyButton);

            // Status label
            statusLabel = new Label
            {
                Text = "Ready",
                Location = new Point(260, 320),
                Size = new Size(200, 20),
                ForeColor = Color.FromArgb(108, 117, 125),
                Font = new Font("Segoe UI", 8)
            };
            this.Controls.Add(statusLabel);

            this.ResumeLayout(false);
        }

        private void CreateNumberSection(string labelText, int yPosition, out TextBox textBox, string placeholder)
        {
            var label = new Label
            {
                Text = labelText,
                Location = new Point(20, yPosition),
                Size = new Size(150, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(73, 80, 87)
            };
            this.Controls.Add(label);

            textBox = new TextBox
            {
                Location = new Point(180, yPosition - 2),
                Size = new Size(280, 25),
                Font = new Font("Consolas", 10),
                PlaceholderText = placeholder
            };
            this.Controls.Add(textBox);
        }

        private void SetupEventHandlers()
        {
            binaryTextBox.TextChanged += (s, e) => ConvertFromBase(binaryTextBox, 2);
            octalTextBox.TextChanged += (s, e) => ConvertFromBase(octalTextBox, 8);
            decimalTextBox.TextChanged += (s, e) => ConvertFromBase(decimalTextBox, 10);
            hexTextBox.TextChanged += (s, e) => ConvertFromBase(hexTextBox, 16);
            
            prefixCheckBox.CheckedChanged += (s, e) => UpdateAllDisplays();
            clearButton.Click += ClearButton_Click;
            copyButton.Click += CopyButton_Click;
            bitSizeComboBox.SelectedIndexChanged += (s, e) => UpdateAllDisplays();
        }

        private void ConvertFromBase(TextBox sourceTextBox, int fromBase)
        {
            if (isUpdating) return;
            
            try
            {
                string input = sourceTextBox.Text.Trim();
                if (string.IsNullOrEmpty(input))
                {
                    ClearOtherTextBoxes(sourceTextBox);
                    statusLabel.Text = "Ready";
                    statusLabel.ForeColor = Color.FromArgb(108, 117, 125);
                    return;
                }

                if (!NumberConverter.IsValidForBase(input, fromBase))
                {
                    statusLabel.Text = $"Invalid {NumberConverter.GetBaseName(fromBase)} number";
                    statusLabel.ForeColor = Color.FromArgb(220, 53, 69);
                    return;
                }

                long decimalValue = NumberConverter.ToDecimal(input, fromBase);
                
                // Check bit size limits
                long maxValue = GetMaxValueForBitSize();
                if (decimalValue > maxValue)
                {
                    statusLabel.Text = $"Value exceeds {bitSizeComboBox.Text} limit";
                    statusLabel.ForeColor = Color.FromArgb(255, 193, 7);
                    return;
                }

                isUpdating = true;
                
                if (sourceTextBox != binaryTextBox)
                    binaryTextBox.Text = FormatOutput(NumberConverter.FromDecimal(decimalValue, 2), 2);
                if (sourceTextBox != octalTextBox)
                    octalTextBox.Text = FormatOutput(NumberConverter.FromDecimal(decimalValue, 8), 8);
                if (sourceTextBox != decimalTextBox)
                    decimalTextBox.Text = FormatOutput(NumberConverter.FromDecimal(decimalValue, 10), 10);
                if (sourceTextBox != hexTextBox)
                    hexTextBox.Text = FormatOutput(NumberConverter.FromDecimal(decimalValue, 16), 16);

                statusLabel.Text = $"Converted successfully (Value: {decimalValue})";
                statusLabel.ForeColor = Color.FromArgb(40, 167, 69);
            }
            catch (Exception ex)
            {
                statusLabel.Text = $"Error: {ex.Message}";
                statusLabel.ForeColor = Color.FromArgb(220, 53, 69);
            }
            finally
            {
                isUpdating = false;
            }
        }

        private string FormatOutput(string value, int baseValue)
        {
            return NumberConverter.GetFormattedOutput(value, baseValue, prefixCheckBox.Checked);
        }

        private void ClearOtherTextBoxes(TextBox except)
        {
            isUpdating = true;
            if (except != binaryTextBox) binaryTextBox.Clear();
            if (except != octalTextBox) octalTextBox.Clear();
            if (except != decimalTextBox) decimalTextBox.Clear();
            if (except != hexTextBox) hexTextBox.Clear();
            isUpdating = false;
        }

        private void UpdateAllDisplays()
        {
            // Trigger conversion from the first non-empty textbox
            if (!string.IsNullOrEmpty(binaryTextBox.Text))
                ConvertFromBase(binaryTextBox, 2);
            else if (!string.IsNullOrEmpty(octalTextBox.Text))
                ConvertFromBase(octalTextBox, 8);
            else if (!string.IsNullOrEmpty(decimalTextBox.Text))
                ConvertFromBase(decimalTextBox, 10);
            else if (!string.IsNullOrEmpty(hexTextBox.Text))
                ConvertFromBase(hexTextBox, 16);
        }

        private long GetMaxValueForBitSize()
        {
            return bitSizeComboBox.SelectedIndex switch
            {
                0 => 255,        // 8-bit
                1 => 65535,      // 16-bit
                2 => 4294967295, // 32-bit
                3 => long.MaxValue, // 64-bit
                _ => long.MaxValue
            };
        }

        private void ClearButton_Click(object? sender, EventArgs e)
        {
            isUpdating = true;
            binaryTextBox.Clear();
            octalTextBox.Clear();
            decimalTextBox.Clear();
            hexTextBox.Clear();
            statusLabel.Text = "All fields cleared";
            statusLabel.ForeColor = Color.FromArgb(108, 117, 125);
            isUpdating = false;
        }

        private void CopyButton_Click(object? sender, EventArgs e)
        {
            try
            {
                string results = $"Xero1 Conversion Results:\n" +
                               $"Binary: {binaryTextBox.Text}\n" +
                               $"Octal: {octalTextBox.Text}\n" +
                               $"Decimal: {decimalTextBox.Text}\n" +
                               $"Hexadecimal: {hexTextBox.Text}";
                
                Clipboard.SetText(results);
                statusLabel.Text = "Results copied to clipboard";
                statusLabel.ForeColor = Color.FromArgb(40, 167, 69);
            }
            catch (Exception ex)
            {
                statusLabel.Text = $"Copy failed: {ex.Message}";
                statusLabel.ForeColor = Color.FromArgb(220, 53, 69);
            }
        }
    }
}