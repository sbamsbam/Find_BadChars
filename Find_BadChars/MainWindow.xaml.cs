using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;

namespace Find_BadChars
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Storage Storage { get; set; }
        private bool _validFileLoaded = false;

        public MainWindow()
        {
            InitializeComponent();

            Storage = new Storage();

            DataContext = Storage;
        }

        private bool CheckForValidDebugOutput(string[] lines)
        {
            foreach (var line in lines)
            {
                if (!Regex.IsMatch(line, @"([a-zA-Z0-9]{8})  ([0-9a-zA-Z]{2} ){7,8} "))
                {
                    MessageBox.Show("This format of this file does not match what is expected.");
                    return false;
                }
            }

            return true;
        }

        private bool GetBadChars(string[] lines)
        {
            Storage.BadChars = "\\x00 ";

            if (lines.Length < 32)
            {
                var result = MessageBox.Show("It looks like you may not have the entire bad characters sequence. Double check you copied the entire thing from the immunity debugger output. Do you wish to continue?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.No)
                    return false;
            }

            List<string> chars = ParseImmunityOutput(lines);

            for (int i = 0; i < Characters.CharacterList.Count; i++)
            {
                if (Characters.CharacterList[i].ToUpper() != chars[i].ToUpper())
                {
                    Storage.BadChars += Characters.CharacterList[i] + " ";
                }
            }

            return true;
        }

        private List<string> ParseImmunityOutput(string[] lines)
        {
            List<string> chars = new List<string>();

            foreach (var line in lines)
            {
                if (Regex.IsMatch(line, @"([a-zA-Z0-9]{8})  ([0-9a-zA-Z]{2} ){7,8} "))
                {
                    var splitLine = line.Split(' ');

                    for (int i = 0; i < splitLine.Length; i++)
                    {
                        if (i > 1 && i < 10 && !string.IsNullOrEmpty(splitLine[i]))
                            chars.Add("\\x" + splitLine[i]);
                    }
                }
            }

            return chars;
        }

        private void getBadCharsButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_validFileLoaded)
            {
                errorText.Visibility = Visibility.Visible;
                return;
            }
            else
                errorText.Visibility = Visibility.Hidden;

            var lines = File.ReadAllLines(Storage.SelectedFile);

            GetBadChars(lines);

        }

        private void browseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter += "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;

            if ((bool)ofd.ShowDialog())
            {
                if (File.Exists(ofd.FileName))
                {
                    var lines = File.ReadAllLines(ofd.FileName);
                    _validFileLoaded = CheckForValidDebugOutput(lines);
                    if (!_validFileLoaded)
                        return;

                    Storage.SelectedFile = ofd.FileName;
                }

            }

        }
    }
}
