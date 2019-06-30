using OBJEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OBJEGUI {
    public partial class OBJEGUI : Form {
        public OBJEGUI() {
            InitializeComponent();
        }

        public string param1;
        public string param2;

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == '\n' || e.KeyChar == '\r') {
                try {
                    listBox1.Items[listBox1.SelectedIndex] = textBox1.Text;
                } catch { }
            }
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                textBox1.Text = listBox1.Items[listBox1.SelectedIndex].ToString();
            } catch { }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            openFileDialog1.ShowDialog();

        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            saveFileDialog1.ShowDialog();
        }

        OBJHelper Editor;
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e) {
            Editor = new OBJHelper(File.ReadAllBytes(openFileDialog1.FileName));
            listBox1.Items.Clear();
            foreach (string String in Editor.Import()) {
                listBox1.Items.Add(String);
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e) {
            string[] Strings = new string[listBox1.Items.Count];
            for (int i = 0; i < Strings.Length; i++) {
                Strings[i] = listBox1.Items[i].ToString();
            }

            File.WriteAllBytes(saveFileDialog1.FileName, Editor.Export(Strings));
            MessageBox.Show("Saved");
        }

        private void ImportToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
        }

        private void OpenFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            listBox1.Items.Clear();
            foreach (string line in File.ReadLines(openFileDialog2.FileName))
            {
                listBox1.Items.Add(line);
            }
        }

        private void OBJEGUI_Load(object sender, EventArgs e)
        {
            if (param1 != null && param2 != null) {
                List<String> StringList = new List<String>();
                Editor = new OBJHelper(File.ReadAllBytes(param1));
                listBox1.Items.Clear();
                foreach (string String in Editor.Import())
                {
                    StringList.Add(String); 
                }
                StringList.Clear();
                foreach (string line in File.ReadLines(param2))
                {
                    StringList.Add(line);
                }
                string[] Strings = new string[StringList.Count];
                for (int i = 0; i < Strings.Length; i++)
                {
                    Strings[i] = StringList[i].ToString();
                }

                File.WriteAllBytes(param1, Editor.Export(Strings));
                System.Windows.Forms.Application.ExitThread();
            }
        }
    }
}
