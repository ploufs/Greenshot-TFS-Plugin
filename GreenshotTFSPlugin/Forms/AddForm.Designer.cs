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
            ((System.ComponentModel.ISupportInitialize)(this.textbox_Priority)).BeginInit();
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
            this.label3.Location = new System.Drawing.Point(9, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Assigned to";
            // 
            // combobox_AssignTo
            // 
            this.combobox_AssignTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.combobox_AssignTo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.combobox_AssignTo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combobox_AssignTo.FormattingEnabled = true;
            this.combobox_AssignTo.Location = new System.Drawing.Point(104, 118);
            this.combobox_AssignTo.Name = "combobox_AssignTo";
            this.combobox_AssignTo.Size = new System.Drawing.Size(513, 21);
            this.combobox_AssignTo.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 258);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Title";
            // 
            // textbox_title
            // 
            this.textbox_title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox_title.Location = new System.Drawing.Point(104, 252);
            this.textbox_title.Name = "textbox_title";
            this.textbox_title.Size = new System.Drawing.Size(513, 20);
            this.textbox_title.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(9, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "Area path";
            // 
            // combobox_AreaPath
            // 
            this.combobox_AreaPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.combobox_AreaPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.combobox_AreaPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combobox_AreaPath.FormattingEnabled = true;
            this.combobox_AreaPath.Location = new System.Drawing.Point(104, 145);
            this.combobox_AreaPath.Name = "combobox_AreaPath";
            this.combobox_AreaPath.Size = new System.Drawing.Size(513, 21);
            this.combobox_AreaPath.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(9, 206);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 20);
            this.label6.TabIndex = 15;
            this.label6.Text = "Severity";
            // 
            // combobox_severity
            // 
            this.combobox_severity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.combobox_severity.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.combobox_severity.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combobox_severity.FormattingEnabled = true;
            this.combobox_severity.Location = new System.Drawing.Point(104, 199);
            this.combobox_severity.Name = "combobox_severity";
            this.combobox_severity.Size = new System.Drawing.Size(513, 21);
            this.combobox_severity.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 229);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Priority";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(9, 175);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 20);
            this.label8.TabIndex = 13;
            this.label8.Text = "Iteration path";
            // 
            // combobox_IterationPath
            // 
            this.combobox_IterationPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.combobox_IterationPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.combobox_IterationPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combobox_IterationPath.FormattingEnabled = true;
            this.combobox_IterationPath.Location = new System.Drawing.Point(104, 172);
            this.combobox_IterationPath.Name = "combobox_IterationPath";
            this.combobox_IterationPath.Size = new System.Drawing.Size(513, 21);
            this.combobox_IterationPath.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(9, 94);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 20);
            this.label9.TabIndex = 7;
            this.label9.Text = "State";
            // 
            // combobox_state
            // 
            this.combobox_state.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.combobox_state.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.combobox_state.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combobox_state.FormattingEnabled = true;
            this.combobox_state.Location = new System.Drawing.Point(104, 91);
            this.combobox_state.Name = "combobox_state";
            this.combobox_state.Size = new System.Drawing.Size(513, 21);
            this.combobox_state.TabIndex = 8;
            // 
            // button_save
            // 
            this.button_save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_save.Enabled = false;
            this.button_save.Location = new System.Drawing.Point(542, 353);
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
            this.textbox_description.Location = new System.Drawing.Point(104, 278);
            this.textbox_description.Multiline = true;
            this.textbox_description.Name = "textbox_description";
            this.textbox_description.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textbox_description.Size = new System.Drawing.Size(513, 60);
            this.textbox_description.TabIndex = 22;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 281);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "Description";
            // 
            // textbox_Priority
            // 
            this.textbox_Priority.Location = new System.Drawing.Point(104, 227);
            this.textbox_Priority.Name = "textbox_Priority";
            this.textbox_Priority.Size = new System.Drawing.Size(89, 20);
            this.textbox_Priority.TabIndex = 18;
            this.textbox_Priority.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // AddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 386);
            this.Controls.Add(this.textbox_Priority);
            this.Controls.Add(this.textbox_description);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.combobox_state);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.combobox_IterationPath);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.combobox_severity);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.combobox_AreaPath);
            this.Controls.Add(this.textbox_title);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.combobox_AssignTo);
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
    }
}