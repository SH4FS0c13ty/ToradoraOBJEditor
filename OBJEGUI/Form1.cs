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

        public string arg1;
        public string arg2;
        public string arg3;

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

        private void ImportToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
        }

        private void ExportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            saveFileDialog2.ShowDialog();
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
            MessageBox.Show("OBJ file saved successfully!", "Toradora! OBJ Editor", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OpenFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            listBox1.Items.Clear();
            foreach (string line in File.ReadLines(openFileDialog2.FileName))
            {
                listBox1.Items.Add(line);
            }
        }
        private void SaveFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            StreamWriter SaveFile = new StreamWriter(saveFileDialog2.FileName);
            foreach (var item in listBox1.Items)
            {
                SaveFile.WriteLine(item);
            }
            SaveFile.Close();
            MessageBox.Show("Strings exported successfully!", "Toradora! OBJ Editor", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OpenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void SaveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void ImportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
        }

        private void ExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog2.ShowDialog();
        }

        private void QuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
        }

        private void OBJEGUI_Load(object sender, EventArgs e)
        {
            if (arg1 != null && arg2 != null && arg3 != null) {
                if (arg1 == "-patch")
                {
                    List<String> StringList = new List<String>();
                    Editor = new OBJHelper(File.ReadAllBytes(arg2));
                    foreach (string String in Editor.Import())
                    {
                        StringList.Add(String);
                    }

                    StringList.Clear();
                    foreach (string line in File.ReadLines(arg3))
                    {
                        StringList.Add(line);
                    }

                    string[] Strings = new string[StringList.Count];
                    for (int i = 0; i < Strings.Length; i++)
                    {
                        Strings[i] = StringList[i].ToString();
                    }

                    File.WriteAllBytes(arg2, Editor.Export(Strings));
                }
                else if (arg1 == "-export")
                {
                    List<String> StringList = new List<String>();
                    Editor = new OBJHelper(File.ReadAllBytes(arg2));
                    foreach (string String in Editor.Import())
                    {
                        StringList.Add(String);
                    }
                    string[] Strings = new string[StringList.Count];
                    StreamWriter SaveFile = new StreamWriter(arg3);
                    for (int i = 0; i < Strings.Length; i++)
                    {
                        Strings[i] = StringList[i].ToString();
                        SaveFile.WriteLine(Strings[i]);
                    }
                    SaveFile.Close();
                }
                else
                {
                    MessageBox.Show("The first argument must be \"-patch\" or \"-export\" (without quotes).", "Argument error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                System.Windows.Forms.Application.ExitThread();
            }
        }

        private void ShowHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Help for Toradora! OBJ Editor"
                + Environment.NewLine
                + Environment.NewLine +
                "Manual mode"
                + Environment.NewLine
                + Environment.NewLine +
                "Use the \"Open\" menu to open the OBJ file you want to patch or to export."
                + Environment.NewLine +
                "Use the \"Import\" menu to import the TXT file you want to apply on the OBJ file."
                + Environment.NewLine +
                "Use the \"Save\" menu to save the patched OBJ file."
                + Environment.NewLine +
                "Use the \"Export\" menu to export strings from the OBJ file into a TXT file."
                + Environment.NewLine
                + Environment.NewLine +
                "Automatic mode"
                + Environment.NewLine
                + Environment.NewLine +
                "There are 3 arguments that you must give:"
                + Environment.NewLine +
                " - verb"
                + Environment.NewLine +
                " - file 1"
                + Environment.NewLine +
                " - file 2"
                + Environment.NewLine
                + Environment.NewLine +
                "Verbs: \"-patch\" or \"-export\""
                + Environment.NewLine
                + Environment.NewLine +
                "For example:"
                + Environment.NewLine +
                "Patch OBJ: \"OBJEGUI.exe -patch  file.obj  patch.txt\""
                + Environment.NewLine +
                "Export OBJ: \"OBJEGUI.exe -export  file.obj  out.txt\""
                + Environment.NewLine
                + Environment.NewLine +
                "If you have another problem, create an issue on Github (https://github.com/SH4FS0c13ty/ToradoraOBJEditor).", 
                "Toradora! OBJ Editor Help", 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Information);
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("About Toradora! OBJ Editor"
                + Environment.NewLine
                + Environment.NewLine +
                "Copyright (c) 2019 SH4FS0c13ty (MIT License)"
                + Environment.NewLine +
                "Check out the license in the license.txt file."
                + Environment.NewLine
                + Environment.NewLine +
                "This program is based on OBJEditor by Marcus André (https://github.com/marcussacana)."
                + Environment.NewLine +
                "Many features were added but the core by Marcus André is still here."
                + Environment.NewLine +
                "Added features:"
                + Environment.NewLine +
                " - More intuitive GUI"
                + Environment.NewLine +
                " - Automatic mode"
                + Environment.NewLine +
                " - TXT patching over OBJ"
                + Environment.NewLine +
                " - Exporting strings from OBJ to TXT"
                + Environment.NewLine
                + Environment.NewLine +
                "This tool is a part of the TigerXDragon toolkit (https://github.com/SH4FS0c13ty/TigerXDragon).",
                "Toradora! OBJ Editor",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}
