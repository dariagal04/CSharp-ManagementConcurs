namespace App;

partial class Login 
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    private System.Windows.Forms.TextBox usernameTxt;
    private System.Windows.Forms.Label labelUsername;
    private System.Windows.Forms.TextBox passwordTxt;
    private System.Windows.Forms.Label labelPassword;
    private System.Windows.Forms.Button loginBtn;
    //private System.Windows.Forms.Button exitBtn;
    
     
    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(400, 300);
        this.Text = "Log in";
        
        this.labelUsername = new System.Windows.Forms.Label();
        this.labelUsername.Text = "Username: ";
        this.labelUsername.Location = new System.Drawing.Point(50, 35);
        this.labelUsername.AutoSize=true;
        this.usernameTxt = new System.Windows.Forms.TextBox();
        this.usernameTxt.Location = new System.Drawing.Point(120, 30);
        this.usernameTxt.Size = new System.Drawing.Size(200, 20);
        this.Controls.Add(this.labelUsername);
        this.Controls.Add(this.usernameTxt);
        
        this.labelPassword = new System.Windows.Forms.Label();
        this.labelPassword.Text = "Password: ";
        this.labelPassword.Location = new System.Drawing.Point(50, 65);
        this.labelPassword.AutoSize=true;
        this.passwordTxt = new System.Windows.Forms.TextBox();
        this.passwordTxt.Location = new System.Drawing.Point(120, 60);
        this.passwordTxt.Size = new System.Drawing.Size(200, 20);
        this.passwordTxt.UseSystemPasswordChar = true;
        this.Controls.Add(this.labelPassword);
        this.Controls.Add(this.passwordTxt);
        
        this.loginBtn = new System.Windows.Forms.Button();
        this.loginBtn.Text = "Login";
        this.loginBtn.Location = new System.Drawing.Point(100, 150);
        this.loginBtn.UseVisualStyleBackColor = true;
        this.loginBtn.Click += new System.EventHandler(this.BtnLogin_Click);
        this.Controls.Add(this.loginBtn);
        
       
        
        this.ResumeLayout(false);
    }

    #endregion
}