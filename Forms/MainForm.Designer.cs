﻿using Coursework.Panels;

namespace Coursework
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code  

        private void InitializeComponent()
        {
            panelToolbar = new Panel();
            deleteShapeBtn = new Button();
            addShapeBtn = new Button();
            editShapeBtn = new Button();
            clearBtn = new Button();
            saveBtn = new Button();
            loadBtn = new Button();
            shapesInfoBtn = new Button();
            panelCanvas = new DoubleBufferedPanel();
            panelProperties = new Panel();
            panelToolbar.SuspendLayout();
            SuspendLayout();
            // 
            // panelToolbar
            // 
            panelToolbar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelToolbar.BackColor = Color.FromArgb(221, 235, 157);
            panelToolbar.Controls.Add(deleteShapeBtn);
            panelToolbar.Controls.Add(addShapeBtn);
            panelToolbar.Controls.Add(editShapeBtn);
            panelToolbar.Controls.Add(clearBtn);
            panelToolbar.Controls.Add(saveBtn);
            panelToolbar.Controls.Add(loadBtn);
            panelToolbar.Controls.Add(shapesInfoBtn);
            panelToolbar.Location = new Point(0, 0);
            panelToolbar.Margin = new Padding(0);
            panelToolbar.Name = "panelToolbar";
            panelToolbar.Size = new Size(800, 60);
            panelToolbar.TabIndex = 2;
            // 
            // deleteShapeBtn
            // 
            deleteShapeBtn.BackColor = Color.FromArgb(255, 253, 246);
            deleteShapeBtn.Cursor = Cursors.Hand;
            deleteShapeBtn.FlatStyle = FlatStyle.Flat;
            deleteShapeBtn.ForeColor = Color.Green;
            deleteShapeBtn.Location = new Point(230, 15);
            deleteShapeBtn.Name = "deleteShapeBtn";
            deleteShapeBtn.Size = new Size(100, 30);
            deleteShapeBtn.TabIndex = 2;
            deleteShapeBtn.Text = "Delete Shape";
            deleteShapeBtn.UseVisualStyleBackColor = false;
            // 
            // addShapeBtn
            // 
            addShapeBtn.BackColor = Color.FromArgb(255, 253, 246);
            addShapeBtn.Cursor = Cursors.Hand;
            addShapeBtn.FlatStyle = FlatStyle.Flat;
            addShapeBtn.ForeColor = Color.Green;
            addShapeBtn.Location = new Point(10, 15);
            addShapeBtn.Name = "addShapeBtn";
            addShapeBtn.Size = new Size(100, 30);
            addShapeBtn.TabIndex = 0;
            addShapeBtn.Text = "Add Shape";
            addShapeBtn.UseVisualStyleBackColor = false;
            // 
            // editShapeBtn
            // 
            editShapeBtn.BackColor = Color.FromArgb(255, 253, 246);
            editShapeBtn.Cursor = Cursors.Hand;
            editShapeBtn.FlatStyle = FlatStyle.Flat;
            editShapeBtn.ForeColor = Color.Green;
            editShapeBtn.Location = new Point(120, 15);
            editShapeBtn.Name = "editShapeBtn";
            editShapeBtn.Size = new Size(100, 30);
            editShapeBtn.TabIndex = 1;
            editShapeBtn.Text = "Edit Shape";
            editShapeBtn.UseVisualStyleBackColor = false;
            // 
            // clearBtn
            // 
            clearBtn.BackColor = Color.FromArgb(255, 253, 246);
            clearBtn.Cursor = Cursors.Hand;
            clearBtn.FlatStyle = FlatStyle.Flat;
            clearBtn.ForeColor = Color.Green;
            clearBtn.Location = new Point(340, 15);
            clearBtn.Name = "clearBtn";
            clearBtn.Size = new Size(100, 30);
            clearBtn.TabIndex = 3;
            clearBtn.Text = "Clear";
            clearBtn.UseVisualStyleBackColor = false;
            // 
            // saveBtn
            // 
            saveBtn.BackColor = Color.FromArgb(255, 253, 246);
            saveBtn.Cursor = Cursors.Hand;
            saveBtn.FlatStyle = FlatStyle.Flat;
            saveBtn.ForeColor = Color.Green;
            saveBtn.Location = new Point(450, 15);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(100, 30);
            saveBtn.TabIndex = 4;
            saveBtn.Text = "Save";
            saveBtn.UseVisualStyleBackColor = false;
            // 
            // loadBtn
            // 
            loadBtn.BackColor = Color.FromArgb(255, 253, 246);
            loadBtn.Cursor = Cursors.Hand;
            loadBtn.FlatStyle = FlatStyle.Flat;
            loadBtn.ForeColor = Color.Green;
            loadBtn.Location = new Point(560, 15);
            loadBtn.Name = "loadBtn";
            loadBtn.Size = new Size(100, 30);
            loadBtn.TabIndex = 5;
            loadBtn.Text = "Load";
            loadBtn.UseVisualStyleBackColor = false;
            // 
            // shapesInfoBtn
            // 
            shapesInfoBtn.BackColor = Color.FromArgb(255, 253, 246);
            shapesInfoBtn.Cursor = Cursors.Hand;
            shapesInfoBtn.FlatStyle = FlatStyle.Flat;
            shapesInfoBtn.ForeColor = Color.Green;
            shapesInfoBtn.Location = new Point(670, 15);
            shapesInfoBtn.Name = "shapesInfoBtn";
            shapesInfoBtn.Size = new Size(100, 30);
            shapesInfoBtn.TabIndex = 5;
            shapesInfoBtn.Text = "Shapes Info";
            shapesInfoBtn.UseVisualStyleBackColor = false;
            // 
            // panelCanvas
            // 
            panelCanvas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelCanvas.BackColor = Color.FromArgb(255, 253, 246);
            panelCanvas.Location = new Point(299, 60);
            panelCanvas.Margin = new Padding(0);
            panelCanvas.Name = "panelCanvas";
            panelCanvas.Size = new Size(501, 540);
            panelCanvas.TabIndex = 0;
            // 
            // panelProperties
            // 
            panelProperties.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panelProperties.BackColor = Color.FromArgb(160, 200, 120);
            panelProperties.Location = new Point(0, 60);
            panelProperties.Margin = new Padding(0);
            panelProperties.Name = "panelProperties";
            panelProperties.Size = new Size(299, 540);
            panelProperties.TabIndex = 1;
            // 
            // MainForm
            // 
            ClientSize = new Size(800, 611);
            Controls.Add(panelCanvas);
            Controls.Add(panelProperties);
            Controls.Add(panelToolbar);
            MinimumSize = new Size(650, 650);
            Name = "MainForm";
            Text = "Shape Editor";
            panelToolbar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelToolbar;
        public Panel panelProperties;
        public Button addShapeBtn;
        public Button editShapeBtn;
        public Button deleteShapeBtn;
        public Button clearBtn;
        public Button saveBtn;
        public Button loadBtn;
        public Button shapesInfoBtn;
        public DoubleBufferedPanel panelCanvas; // Use DoubleBufferedPanel
    }
}
