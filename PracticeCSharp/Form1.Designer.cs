
namespace PracticeCSharp
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.работаСФайламиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьКакToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьПапкуВПроводникеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.проверкаБДToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.работаСToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьЧекToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьЧекToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изменитьЧекToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчетToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.работаСКоллективомToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сменитьПользователяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьИзСпискаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьИзСпискаToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Rockwell", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(366, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Refill workspace v 0.1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lucida Console", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(356, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(220, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Должность : ФИО";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(810, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(179, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Текущая дата и время";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(810, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Дата и время";
            // 
            // работаСФайламиToolStripMenuItem
            // 
            this.работаСФайламиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.создатьToolStripMenuItem,
            this.сохранитьToolStripMenuItem,
            this.сохранитьКакToolStripMenuItem,
            this.открытьПапкуВПроводникеToolStripMenuItem,
            this.проверкаБДToolStripMenuItem});
            this.работаСФайламиToolStripMenuItem.Name = "работаСФайламиToolStripMenuItem";
            this.работаСФайламиToolStripMenuItem.Size = new System.Drawing.Size(120, 20);
            this.работаСФайламиToolStripMenuItem.Text = "Работа с файлами";
            this.работаСФайламиToolStripMenuItem.Click += new System.EventHandler(this.работаСФайламиToolStripMenuItem_Click);
            // 
            // создатьToolStripMenuItem
            // 
            this.создатьToolStripMenuItem.Name = "создатьToolStripMenuItem";
            this.создатьToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.создатьToolStripMenuItem.Text = "Создать";
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            // 
            // сохранитьКакToolStripMenuItem
            // 
            this.сохранитьКакToolStripMenuItem.Name = "сохранитьКакToolStripMenuItem";
            this.сохранитьКакToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.сохранитьКакToolStripMenuItem.Text = "Сохранить как";
            // 
            // открытьПапкуВПроводникеToolStripMenuItem
            // 
            this.открытьПапкуВПроводникеToolStripMenuItem.Name = "открытьПапкуВПроводникеToolStripMenuItem";
            this.открытьПапкуВПроводникеToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.открытьПапкуВПроводникеToolStripMenuItem.Text = "Открыть папку в проводнике";
            // 
            // проверкаБДToolStripMenuItem
            // 
            this.проверкаБДToolStripMenuItem.Name = "проверкаБДToolStripMenuItem";
            this.проверкаБДToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.проверкаБДToolStripMenuItem.Text = "Подключение к БД";
            this.проверкаБДToolStripMenuItem.Click += new System.EventHandler(this.проверкаБДToolStripMenuItem_Click);
            // 
            // работаСToolStripMenuItem
            // 
            this.работаСToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.создатьЧекToolStripMenuItem,
            this.удалитьЧекToolStripMenuItem,
            this.изменитьЧекToolStripMenuItem,
            this.отчетToolStripMenuItem});
            this.работаСToolStripMenuItem.Name = "работаСToolStripMenuItem";
            this.работаСToolStripMenuItem.Size = new System.Drawing.Size(128, 20);
            this.работаСToolStripMenuItem.Text = "Работа с Заправкой";
            // 
            // создатьЧекToolStripMenuItem
            // 
            this.создатьЧекToolStripMenuItem.Name = "создатьЧекToolStripMenuItem";
            this.создатьЧекToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.создатьЧекToolStripMenuItem.Text = "Создать чек";
            // 
            // удалитьЧекToolStripMenuItem
            // 
            this.удалитьЧекToolStripMenuItem.Name = "удалитьЧекToolStripMenuItem";
            this.удалитьЧекToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.удалитьЧекToolStripMenuItem.Text = "Удалить чек";
            // 
            // изменитьЧекToolStripMenuItem
            // 
            this.изменитьЧекToolStripMenuItem.Name = "изменитьЧекToolStripMenuItem";
            this.изменитьЧекToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.изменитьЧекToolStripMenuItem.Text = "Изменить чек";
            // 
            // отчетToolStripMenuItem
            // 
            this.отчетToolStripMenuItem.Name = "отчетToolStripMenuItem";
            this.отчетToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.отчетToolStripMenuItem.Text = "Отчет";
            // 
            // работаСКоллективомToolStripMenuItem
            // 
            this.работаСКоллективомToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сменитьПользователяToolStripMenuItem,
            this.удалитьИзСпискаToolStripMenuItem,
            this.удалитьИзСпискаToolStripMenuItem1});
            this.работаСКоллективомToolStripMenuItem.Name = "работаСКоллективомToolStripMenuItem";
            this.работаСКоллективомToolStripMenuItem.Size = new System.Drawing.Size(143, 20);
            this.работаСКоллективомToolStripMenuItem.Text = "Работа с Коллективом";
            // 
            // сменитьПользователяToolStripMenuItem
            // 
            this.сменитьПользователяToolStripMenuItem.Name = "сменитьПользователяToolStripMenuItem";
            this.сменитьПользователяToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.сменитьПользователяToolStripMenuItem.Text = "Сменить пользователя";
            // 
            // удалитьИзСпискаToolStripMenuItem
            // 
            this.удалитьИзСпискаToolStripMenuItem.Name = "удалитьИзСпискаToolStripMenuItem";
            this.удалитьИзСпискаToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.удалитьИзСпискаToolStripMenuItem.Text = "Добавить в список";
            // 
            // удалитьИзСпискаToolStripMenuItem1
            // 
            this.удалитьИзСпискаToolStripMenuItem1.Name = "удалитьИзСпискаToolStripMenuItem1";
            this.удалитьИзСпискаToolStripMenuItem1.Size = new System.Drawing.Size(200, 22);
            this.удалитьИзСпискаToolStripMenuItem1.Text = "Удалить из списка";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.работаСФайламиToolStripMenuItem,
            this.работаСToolStripMenuItem,
            this.работаСКоллективомToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1053, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1053, 665);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem работаСФайламиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem создатьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьКакToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьПапкуВПроводникеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem проверкаБДToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem работаСToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem создатьЧекToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьЧекToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изменитьЧекToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отчетToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem работаСКоллективомToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сменитьПользователяToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьИзСпискаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьИзСпискаToolStripMenuItem1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}

