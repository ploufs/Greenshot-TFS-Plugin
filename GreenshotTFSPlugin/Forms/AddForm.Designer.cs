namespace GreenshotTFSPlugin.Forms
{
    partial class AddForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddForm));
            this.label2 = new System.Windows.Forms.Label();
            this.textbox_defaultProject = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textbox_tfsUrl = new System.Windows.Forms.TextBox();
            this.label_workItemType = new System.Windows.Forms.Label();
            this.combobox_workItemType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.combobox_AssignTo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textbox_title = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.combobox_AreaPath = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.combobox_severity = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.combobox_IterationPath = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.combobox_state = new System.Windows.Forms.ComboBox();
            this.button_save = new System.Windows.Forms.Button();
            this.textbox_description = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textbox_Priority = new System.Windows.Forms.NumericUpDown();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textbox_SystemInfo = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textbox_ReproStep = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.combobox_reason = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.textbox_Priority)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(9, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "Project";
            // 
            // textbox_defaultProject
            // 
            this.textbox_defaultProject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox_defaultProject.Location = new System.Drawing.Point(104, 38);
            this.textbox_defaultProject.Name = "textbox_defaultProject";
            this.textbox_defaultProject.ReadOnly = true;
            this.textbox_defaultProject.Size = new System.Drawing.Size(513, 20);
            this.textbox_defaultProject.TabIndex = 4;
            // 
            // buttonConnect
            // 
            this.buttonConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonConnect.Location = new System.Drawing.Point(523, 10);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(94, 23);
            this.buttonConnect.TabIndex = 2;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tfs :";
            // 
            // textbox_tfsUrl
            // 
            this.textbox_tfsUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox_tfsUrl.Location = new System.Drawing.Point(104, 12);
            this.textbox_tfsUrl.Name = "textbox_tfsUrl";
            this.textbox_tfsUrl.ReadOnly = true;
            this.textbox_tfsUrl.Size = new System.Drawing.Size(413, 20);
            this.textbox_tfsUrl.TabIndex = 1;
            // 
            // label_workItemType
            // 
            this.label_workItemType.Location = new System.Drawing.Point(9, 70);
            this.label_workItemType.Name = "label_workItemType";
            this.label_workItemType.Size = new System.Drawing.Size(84, 20);
            this.label_workItemType.TabIndex = 5;
            this.label_workItemType.Text = "Work item type";
            // 
            // combobox_workItemType
            // 
            this.combobox_workItemType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.combobox_workItemType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.combobox_workItemType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combobox_workItemType.FormattingEnabled = true;
            this.combobox_workItemType.Location = new System.Drawing.Point(104, 64);
            this.combobox_workItemType.Name = "combobox_workItemType";
            this.combobox_workItemType.Size = new System.Drawing.Size(513, 21);
            this.combobox_workItemType.TabIndex = 6;
            this.combobox_workItemType.SelectedIndexChanged += new System.EventHandler(this.combobox_workItemType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Assigned to";
            // 
            // combobox_AssignTo
            // 
            this.combobox_AssignTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.combobox_AssignTo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.combobox_AssignTo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combobox_AssignTo.FormattingEnabled = true;
            this.combobox_AssignTo.Location = new System.Drawing.Point(98, 73);
            this.combobox_AssignTo.Name = "combobox_AssignTo";
            this.combobox_AssignTo.Size = new System.Drawing.Size(497, 21);
            this.combobox_AssignTo.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Title";
            // 
            // textbox_title
            // 
            this.textbox_title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox_title.Location = new System.Drawing.Point(98, 154);
            this.textbox_title.Name = "textbox_title";
            this.textbox_title.Size = new System.Drawing.Size(497, 20);
            this.textbox_title.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "Area path";
            // 
            // combobox_AreaPath
            // 
            this.combobox_AreaPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.combobox_AreaPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.combobox_AreaPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combobox_AreaPath.FormattingEnabled = true;
            this.combobox_AreaPath.Location = new System.Drawing.Point(98, 100);
            this.combobox_AreaPath.Name = "combobox_AreaPath";
            this.combobox_AreaPath.Size = new System.Drawing.Size(497, 21);
            this.combobox_AreaPath.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(3, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "Severity";
            // 
            // combobox_severity
            // 
            this.combobox_severity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.combobox_severity.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.combobox_severity.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combobox_severity.FormattingEnabled = true;
            this.combobox_severity.Location = new System.Drawing.Point(98, 18);
            this.combobox_severity.Name = "combobox_severity";
            this.combobox_severity.Size = new System.Drawing.Size(488, 21);
            this.combobox_severity.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Priority";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(3, 130);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 20);
            this.label8.TabIndex = 8;
            this.label8.Text = "Iteration path";
            // 
            // combobox_IterationPath
            // 
            this.combobox_IterationPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.combobox_IterationPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.combobox_IterationPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combobox_IterationPath.FormattingEnabled = true;
            this.combobox_IterationPath.Location = new System.Drawing.Point(98, 127);
            this.combobox_IterationPath.Name = "combobox_IterationPath";
            this.combobox_IterationPath.Size = new System.Drawing.Size(497, 21);
            this.combobox_IterationPath.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(3, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 20);
            this.label9.TabIndex = 0;
            this.label9.Text = "State";
            // 
            // combobox_state
            // 
            this.combobox_state.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.combobox_state.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.combobox_state.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combobox_state.FormattingEnabled = true;
            this.combobox_state.Location = new System.Drawing.Point(98, 18);
            this.combobox_state.Name = "combobox_state";
            this.combobox_state.Size = new System.Drawing.Size(497, 21);
            this.combobox_state.TabIndex = 1;
            // 
            // button_save
            // 
            this.button_save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_save.Enabled = false;
            this.button_save.Location = new System.Drawing.Point(542, 414);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(75, 23);
            this.button_save.TabIndex = 23;
            this.button_save.Text = "Save";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // textbox_description
            // 
            this.textbox_description.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox_description.Location = new System.Drawing.Point(98, 180);
            this.textbox_description.Multiline = true;
            this.textbox_description.Name = "textbox_description";
            this.textbox_description.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textbox_description.Size = new System.Drawing.Size(497, 103);
            this.textbox_description.TabIndex = 13;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 183);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Description";
            // 
            // textbox_Priority
            // 
            this.textbox_Priority.Location = new System.Drawing.Point(98, 46);
            this.textbox_Priority.Name = "textbox_Priority";
            this.textbox_Priority.Size = new System.Drawing.Size(89, 20);
            this.textbox_Priority.TabIndex = 3;
            this.textbox_Priority.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 93);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(609, 315);
            this.tabControl1.TabIndex = 24;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.combobox_reason);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.textbox_description);
            this.tabPage1.Controls.Add(this.combobox_AssignTo);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.combobox_AreaPath);
            this.tabPage1.Controls.Add(this.textbox_title);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.combobox_IterationPath);
            this.tabPage1.Controls.Add(this.combobox_state);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(601, 289);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textbox_ReproStep);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.textbox_SystemInfo);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.combobox_severity);
            this.tabPage2.Controls.Add(this.textbox_Priority);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(601, 289);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Custom";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textbox_SystemInfo
            // 
            this.textbox_SystemInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox_SystemInfo.Location = new System.Drawing.Point(98, 72);
            this.textbox_SystemInfo.Multiline = true;
            this.textbox_SystemInfo.Name = "textbox_SystemInfo";
            this.textbox_SystemInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textbox_SystemInfo.Size = new System.Drawing.Size(497, 109);
            this.textbox_SystemInfo.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 75);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "System info";
            // 
            // textbox_ReproStep
            // 
            this.textbox_ReproStep.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox_ReproStep.Location = new System.Drawing.Point(98, 187);
            this.textbox_ReproStep.Multiline = true;
            this.textbox_ReproStep.Name = "textbox_ReproStep";
            this.textbox_ReproStep.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textbox_ReproStep.Size = new System.Drawing.Size(497, 96);
            this.textbox_ReproStep.TabIndex = 7;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 190);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(62, 13);
            this.label12.TabIndex = 6;
            this.label12.Text = "Repro. step";
            // 
            // combobox_reason
            // 
            this.combobox_reason.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.combobox_reason.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.combobox_reason.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combobox_reason.FormattingEnabled = true;
            this.combobox_reason.Location = new System.Drawing.Point(98, 45);
            this.combobox_reason.Name = "combobox_reason";
            this.combobox_reason.Size = new System.Drawing.Size(497, 21);
            this.combobox_reason.TabIndex = 3;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(3, 48);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(84, 20);
            this.label13.TabIndex = 2;
            this.label13.Text = "Reason";
            // 
            // AddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 447);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.label_workItemType);
            this.Controls.Add(this.combobox_workItemType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textbox_defaultProject);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textbox_tfsUrl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddForm";
            this.Text = "Add work item";
            ((System.ComponentModel.ISupportInitialize)(this.textbox_Priority)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textbox_defaultProject;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textbox_tfsUrl;
        private System.Windows.Forms.Label label_workItemType;
        private System.Windows.Forms.ComboBox combobox_workItemType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox combobox_AssignTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textbox_title;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox combobox_AreaPath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox combobox_severity;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox combobox_IterationPath;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox combobox_state;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.TextBox textbox_description;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown textbox_Priority;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox textbox_SystemInfo;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textbox_ReproStep;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox combobox_reason;
        private System.Windows.Forms.Label label13;
    }
}