namespace ADSPE
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.generationsTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.populationTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.benchmarkComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.selectionComboBox = new System.Windows.Forms.ComboBox();
            this.startButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.muationTextBox = new System.Windows.Forms.TextBox();
            this.crossoverTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Number of generations";
            // 
            // generationsTextBox
            // 
            this.generationsTextBox.Location = new System.Drawing.Point(144, 36);
            this.generationsTextBox.Name = "generationsTextBox";
            this.generationsTextBox.Size = new System.Drawing.Size(118, 20);
            this.generationsTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Population size";
            // 
            // populationTextBox
            // 
            this.populationTextBox.Location = new System.Drawing.Point(144, 80);
            this.populationTextBox.Name = "populationTextBox";
            this.populationTextBox.Size = new System.Drawing.Size(118, 20);
            this.populationTextBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Crossover probability";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 234);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Benchmark";
            // 
            // benchmarkComboBox
            // 
            this.benchmarkComboBox.FormattingEnabled = true;
            this.benchmarkComboBox.Items.AddRange(new object[] {
            "applu.tra",
            "compress.tra",
            "epic.tra",
            "fpppp.tra",
            "ijpeg.tra",
            "mpeg2d.tra",
            "mpeg2e.tra",
            "pegwitd.tra",
            "perl.tra",
            "toast.tra"});
            this.benchmarkComboBox.Location = new System.Drawing.Point(144, 231);
            this.benchmarkComboBox.Name = "benchmarkComboBox";
            this.benchmarkComboBox.Size = new System.Drawing.Size(118, 21);
            this.benchmarkComboBox.TabIndex = 7;
            this.benchmarkComboBox.Text = "applu.tra";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 196);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Selection Operator";
            // 
            // selectionComboBox
            // 
            this.selectionComboBox.FormattingEnabled = true;
            this.selectionComboBox.Items.AddRange(new object[] {
            "BinaryTournament",
            "BestSolutionSelection",
            "RandomSelection",
            "BinaryTournament2"});
            this.selectionComboBox.Location = new System.Drawing.Point(144, 193);
            this.selectionComboBox.Name = "selectionComboBox";
            this.selectionComboBox.Size = new System.Drawing.Size(118, 21);
            this.selectionComboBox.TabIndex = 9;
            this.selectionComboBox.Text = "BinaryTournament2";
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(27, 300);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 61);
            this.startButton.TabIndex = 10;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 117);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Mutation";
            // 
            // muationTextBox
            // 
            this.muationTextBox.Location = new System.Drawing.Point(144, 117);
            this.muationTextBox.Name = "muationTextBox";
            this.muationTextBox.Size = new System.Drawing.Size(118, 20);
            this.muationTextBox.TabIndex = 12;
            this.muationTextBox.Text = "0.4";
            // 
            // crossoverTextBox
            // 
            this.crossoverTextBox.Location = new System.Drawing.Point(144, 154);
            this.crossoverTextBox.Name = "crossoverTextBox";
            this.crossoverTextBox.Size = new System.Drawing.Size(118, 20);
            this.crossoverTextBox.TabIndex = 13;
            this.crossoverTextBox.Text = "0.6";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 373);
            this.Controls.Add(this.crossoverTextBox);
            this.Controls.Add(this.muationTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.selectionComboBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.benchmarkComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.populationTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.generationsTextBox);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Automatic Design Space Explorer ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox generationsTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox populationTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox benchmarkComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox selectionComboBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox muationTextBox;
        private System.Windows.Forms.TextBox crossoverTextBox;
    }
}

